using Moq;
using Shared.Domain;
using Shared.User;
using UserService.Infrastructure;

namespace UserService.Tests;

public class UserUnitTests : IDisposable
{
    
    private IUserRepository _userRepository;

    private Mock<IUserRepository> _moqRepository()
    {
        Mock<IUserRepository> mock = new Moq.Mock<IUserRepository>();
        
        var mockUsers = GetMockUsers();
        
        mock.Setup(repo => repo.All()).ReturnsAsync(mockUsers);
        mock.Setup(repo => repo.Single(It.IsAny<int>())).ReturnsAsync((int id) => mockUsers.Find(u => u.Id == id));
        mock.Setup(repo => repo.Update(It.IsAny<User>())).ReturnsAsync((User user) => user);
        mock.Setup(repo => repo.Delete(It.IsAny<int>())).ReturnsAsync((int id) =>
        {
            var changed = mockUsers.RemoveAll(u => u.Id == id);
            if (changed == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        });
        mock.Setup(repo => repo.Create(It.IsAny<User>())).ReturnsAsync((User user) =>
        {
            user.Id = mockUsers.Count + 1;
            mockUsers.Add(user);
            return user;
        });
     

        return mock;
    }
    
    private List<User> GetMockUsers()
    {
        return new List<User>()
        {
            new User()
            {
                Id = 1,
                Username = "test",
                Bio = "",
                Email = "mail1@mail.com",
                Followers = [],
                Following = [],
            },
            new User()
            {
                Id = 2,
                Username = "test2",
                Bio = "",
                Email = "mail2@mail.com",
                Followers = [],
                Following = [],
            },
            new User()
            {
                Id = 3,
                Username = "test3",
                Bio = "",
                Email = "mail3@mail.com",
                Followers = [],
                Following = [],
            }
        };
    }
    
    
    [Fact]
    public void Test_Service_Can_Create_User()
    {
        // Arrange
        var mock = _moqRepository();
        var service = new Service.UserService(mock.Object);
        var user = new UserCreateDTO()
        {
            Username = "test",
        };
        
        // Act
        var created = service.CreateUser(user);
        
        // Assert
        Assert.NotNull(created);
        Assert.Equal("test", created.Result.Username);
    }
    
    [Fact]
    public void Test_Service_Can_Get_User()
    {
        // Arrange
        var mock = _moqRepository();
        var service = new Service.UserService(mock.Object);
        
        // Act
        var user = service.GetUser(1);
        
        // Assert
        Assert.NotNull(user);
        Assert.Equal(1, user.Result.Id);
    }
    
    [Fact]
    public void Test_Service_Can_Update_User()
    {
        // Arrange
        var mock = _moqRepository();
        var service = new Service.UserService(mock.Object);
        var user = new UserUpdateDTO()
        {
            UserId = 1,
            Username = "update-test",
            Bio = "new bio",
            Email = "newEmail@mail.com",
        };
        
        // Act
        var updated = service.UpdateUser(user);
        
        // Assert
        Assert.NotNull(updated);
        Assert.Equal("update-test", updated.Result.Username);
        Assert.Equal("new bio", updated.Result.Bio);
        Assert.Equal("newEmail@mail.com", updated.Result.Email);
    }
    
    [Fact]
    public async Task Test_Service_Can_Delete_User()
    {
        // Arrange
        var mock = _moqRepository();
        var service = new Service.UserService(mock.Object);
        var usersSize = mock.Object.All().Result.Count;
        
        // Act
        await service.DeleteUser(1);
        
        // Assert
        Assert.True(usersSize - 1 == mock.Object.All().Result.Count);
    }
    
    [Fact]
    public async Task Test_Service_Can_Follow_User()
    {
        // Arrange
        var mock = _moqRepository();
        var service = new Service.UserService(mock.Object);
        
        // Act
        await service.FollowUser(1, 2);
        
        // Assert
        Assert.Single(mock.Object.Single(2).Result.Followers, 1);
        Assert.Single(mock.Object.Single(1).Result.Following, 1);

        Assert.Contains(mock.Object.Single(2).Result.Followers, u => u.Id == 1);
        Assert.Contains(mock.Object.Single(1).Result.Following, u => u.Id == 2);
    }
    
    [Fact]
    public async Task Test_Service_Can_Unfollow_User()
    {
        // Arrange
        var mock = _moqRepository();
        var service = new Service.UserService(mock.Object);
        await service.FollowUser(1, 2);
        
        // Act
        await service.UnfollowUser(1, 2);
        
        // Assert
        Assert.Empty(mock.Object.Single(2).Result.Followers);
        Assert.Empty(mock.Object.Single(1).Result.Following);
    }
    
    [Fact]
    public void Test_Service_Can_Get_Followers()
    {
        // Arrange
        var mock = _moqRepository();
        var service = new Service.UserService(mock.Object);
        
        //Act
        var followers = service.GetFollowers(1);
        
        
        // Assert
        Assert.NotNull(followers);
        Assert.Empty(followers.Result);
    }
    
    

    public void Dispose()
    {
        // TODO release managed resources here
    }
}