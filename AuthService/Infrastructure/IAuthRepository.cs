using Shared.Domain;
using Shared.User;

namespace AuthService.Infrastructure;

public interface IAuthRepository
{
    public Task<bool> Register(AuthUser authUser, UserCreateDTO userDTO);
    public Task<AuthUser> FindUser(string username);
    public Task<UserDTO> GetUserId(string username);
}