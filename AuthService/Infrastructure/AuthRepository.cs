using Microsoft.EntityFrameworkCore;
using Shared.Domain;

namespace AuthService.Infrastructure;

public class AuthRepository : IAuthRepository
{
    private readonly DatabaseContext _context;
    
    public AuthRepository(DatabaseContext context)
    {
        _context = context;
    }
    
    public async Task Register(AuthUser authUser)
    {
        _context.AuthUsers.Add(authUser);
        await _context.SaveChangesAsync();
    }
    
    public async Task<AuthUser> FindUser(string username)
    {
        return await _context.AuthUsers.FirstOrDefaultAsync(user => user.email.Equals(user.email));
    }
}