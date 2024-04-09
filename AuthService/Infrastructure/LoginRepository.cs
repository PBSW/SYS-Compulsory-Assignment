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
        _context.Logins.Add(login);
        _context.SaveChanges();
    }
    
    public Login GetLogin(string username)
    {
        return _context.Logins.Find(username);
    }
}