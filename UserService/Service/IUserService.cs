using Shared.Domain;
using Shared.User;

namespace UserService.Service;

public interface IUserService
{
    public Task<bool> CreateUser(UserCreateDTO user);
    
    public Task<UserDTO> GetUser(int userId);
    
    public Task<User> UpdateUser(UserUpdateDTO user);
    
    public Task<bool> DeleteUser(int userId);
    public Task<UserDTO> GetUserByUsername(string username);
}