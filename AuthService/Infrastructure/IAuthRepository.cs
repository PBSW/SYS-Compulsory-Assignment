using Shared.Domain;

namespace AuthService.Infrastructure;

public interface IAuthRepository
{
    public Task Register(AuthUser authUser);
    public Task<AuthUser> FindUser(string username);
}