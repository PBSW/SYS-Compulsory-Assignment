using Shared.Domain;

namespace AuthService.Infrastructure;

public interface IAuthRepository
{
    public void Register(AuthUser authUser);
    public AuthUser FindUser(string username);
}