using AutoMapper;
using Moq;
using Shared.Domain;
using Shared.User;
using UserService.Helper;
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
            mockUsers.Add(user);
            return true;
        });

        return mock;
    }

    private IMapper GetMapper()
    {
        //Inject actual mapper here
        return new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new AutoMapperProfiles());
        }).CreateMapper();
    }
    
    private List<User> GetMockUsers()
    {
        return new List<User>()
        {
            new User()
            {
                Id = 1,
                Username = "test",
                Email = "mail1@mail.com",
            },
            new User()
            {
                Id = 2,
                Username = "test2",
                Email = "mail2@mail.com",
            },
            new User()
            {
                Id = 3,
                Username = "test3",
                Email = "mail3@mail.com",
            }
        };
    }
    
    
    [Fact]
    public async Task Test_Service_Can_Create_User()
    {
        // Arrange
        var mock = _moqRepository();
        var service = new Service.UserService(mock.Object, GetMapper());
        var user = new UserCreateDTO()
        {
            Email = "mail5@mail.com",
            Username = "test",
        };
        
        // Act
        var created = await service.CreateUser(user);
        
        // Assert
        Assert.NotNull(created);
        Assert.True(created);
    }
    
    [Fact]
    public void Test_Service_Can_Get_User()
    {
        // Arrange
        var mock = _moqRepository();
        var service = new Service.UserService(mock.Object, GetMapper());
        
        // Act
        var user = service.GetUser(1);
        
        // Assert
        Assert.NotNull(user);
        Assert.Equal(1, user.Result.Id);
    }
    
    [Fact]
    public async Task Test_Service_Can_Delete_User()
    {
        // Arrange
        var mock = _moqRepository();
        var service = new Service.UserService(mock.Object, GetMapper());
        var usersSize = mock.Object.All().Result.Count;
        
        // Act
        await service.DeleteUser(1);
        
        // Assert
        Assert.True(usersSize - 1 == mock.Object.All().Result.Count);
    }
  

    public void Dispose()
    {
        // TODO release managed resources here
    }
}