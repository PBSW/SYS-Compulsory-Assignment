using Shared.Domain;

namespace AuthService.Infrastructure;

public interface IAuthRepository
{
    public Task<int> Register(AuthUser authUser);
    public Task<AuthUser> FindUser(string username);
    public Task<User> GetUserId(string username);
}