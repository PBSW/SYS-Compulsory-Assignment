using Shared.Domain;

namespace AuthService.Infrastructure;

public interface ILoginRepository
{
    public void AddLogin(Login login);
    public Login GetLogin(string username);
}