using Microsoft.EntityFrameworkCore;
using Shared.Domain;

namespace UserService.Infrastructure;

public class UserRepository : IUserRepository
{
    private readonly DatabaseContext _context;
    
    public UserRepository(DatabaseContext context)
    {
        _context = context;
    }
    
    public async Task<List<User>> All()
    {
        var users = await _context.Users.ToListAsync();
        return users;
    }

    public async Task<User> Create(User user)
    {
        var added = await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
        return added.Entity;
    }

    public async Task<bool> Delete(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null)
        {
            return false;
        }
        
        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<User> Single(int id)
    {
        var user = await _context.Users.FindAsync(id);
        
        if (user == null)
        {
            throw new Exception("User not found");
        }
        
        return user;
    }

    public async Task<User> Update(User user)
    {
        var updated = _context.Users.Update(user);
        await _context.SaveChangesAsync();
        return updated.Entity;
    }
}