using Shared.Domain;

namespace AuthService.Infrastructure;

public class AuthRepository : IAuthRepository
{
    private readonly DatabaseContext _context;
    
    public AuthRepository(DatabaseContext context)
    {
        _context = context;
    }
    
    public void Register(AuthUser authUser)
    {
        _context.AuthUsers.Add(authUser);
        _context.SaveChanges();
    }
    
    public AuthUser FindUser(string username)
    {
        var entity = _context.AuthUsers.FirstOrDefault(user => user.email.Equals(user.email));
        return entity;
    }
}