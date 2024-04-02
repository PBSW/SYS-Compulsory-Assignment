using Shared.User.Dto;
using UserService.Domain;
using UserService.Infrastructure;

namespace UserService.Service;

public class UserService : IUserService
{
    
    private readonly IUserRepository _userRepository;
    
    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    

    public User Register(User user)
    {
        throw new NotImplementedException();
    }

    public User Login(User user)
    {
        throw new NotImplementedException();
    }

    public User GetUser(int userId)
    {
        throw new NotImplementedException();
    }

    public User UpdateUser(UserUpdate user)
    {
        throw new NotImplementedException();
    }

    public void DeleteUser(int userId)
    {
        throw new NotImplementedException();
    }

    public void FollowUser(int userId, int followId)
    {
        throw new NotImplementedException();
    }

    public void UnfollowUser(int userId, int unfollowId)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<User> GetFollowers(int userId)
    {
        throw new NotImplementedException();
    }
}