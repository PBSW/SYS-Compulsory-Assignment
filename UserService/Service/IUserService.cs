using Shared.User.Dto;
using UserService.Domain;

namespace UserService.Service;

public interface IUserService
{
    public User Register(User user);
    
    public User Login(User user);
    
    public User GetUser(int userId);
    
    public User UpdateUser(UserUpdate user);
    
    public void DeleteUser(int userId);
    
    public void FollowUser(int userId, int followId);
    
    public void UnfollowUser(int userId, int unfollowId);
    
    public IEnumerable<User> GetFollowers(int userId);
    
}