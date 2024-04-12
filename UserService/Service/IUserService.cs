using Shared.Domain;
using Shared.User;

namespace UserService.Service;

public interface IUserService
{
    public Task<User> CreateUser(UserCreateDTO user);
    
    public Task<User> GetUser(int userId);
    
    public Task<User> UpdateUser(UserUpdateDTO user);
    
    public Task DeleteUser(int userId);
}