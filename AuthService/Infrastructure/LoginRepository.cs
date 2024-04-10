using Shared.Domain;

namespace AuthService.Infrastructure;

public class LoginRepository : ILoginRepository
{
    private readonly DatabaseContext _context;
    
    public LoginRepository(DatabaseContext context)
    {
        _context = context;
    }
    
    public void AddLogin(Login login)
    {
        throw new NotImplementedException();
    }
    
    public Login GetLogin(string username)
    {
        throw new NotImplementedException();
    }
}