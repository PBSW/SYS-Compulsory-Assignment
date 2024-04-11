using Shared.Domain;
using Shared.User;

namespace UserService.Service;

public interface IUserService
{
    public Task<User> Login(LoginDTO user);
    
    public Task<User> CreateUser(UserCreateDTO user);
    
    public Task<User> GetUser(int userId);
    
    public Task<User> UpdateUser(UserUpdateDTO user);
    
    public Task DeleteUser(int userId);
    
    public Task FollowUser(int userId, int followId);
    
    public Task UnfollowUser(int userId, int unfollowId);
    
    public Task<List<User>> GetFollowers(int userId);
}