using Shared.Domain;
using Shared.User;

namespace UserService.Service;

public interface IUserService
{
    public Task<User> Login(User user);
    
    public Task<User> GetUser(int userId);
    
    public Task<User> UpdateUser(UserUpdateDTO user);
    
    public Task DeleteUser(int userId);
    
    public Task FollowUser(int userId, int followId);
    
    public Task UnfollowUser(int userId, int unfollowId);
    
    public Task<IEnumerable<User>> GetFollowers(int userId);

    public Task<string> HashPassword(string password);
    
    public Task<bool> VerifyPassword(string password, string hash);
    

}