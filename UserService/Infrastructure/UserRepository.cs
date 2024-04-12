using Microsoft.EntityFrameworkCore;
using Shared.Domain;
using Shared.Monitoring;

namespace UserService.Infrastructure;

public class UserRepository : IUserRepository
{
    private readonly DatabaseContext _context;
    
    public UserRepository(DatabaseContext context)
    {
        _context = context;
        _context.Database.EnsureCreated();
    }
    
    public async Task<List<User>> All()
    {
        //Monitoring and logging
        using var activity = Monitoring.ActivitySource.StartActivity("UserService.Infrastructure.All");
        
        Monitoring.Log.Debug("UserRepository.All called");
        
        var users = await _context.Users.ToListAsync();
        return users;
    }

    public async Task<int> Create(User user)
    {
        //Monitoring and logging
        using var activity = Monitoring.ActivitySource.StartActivity("UserService.Infrastructure.Create");
        activity?.SetTag("user", user.Username);
        
        Monitoring.Log.Debug("UserRepository.Create called");
        
        var added = await _context.Users.AddAsync(user);
        return await _context.SaveChangesAsync();
    }

    public async Task<bool> Delete(int id)
    {
        //Monitoring and logging
        using var activity = Monitoring.ActivitySource.StartActivity("UserService.Infrastructure.Delete");
        activity?.SetTag("id", id.ToString());
        
        Monitoring.Log.Debug("UserRepository.Delete called");
        
        
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
        //Monitoring and logging
        using var activity = Monitoring.ActivitySource.StartActivity("UserService.Infrastructure.Single");
        activity?.SetTag("id", id.ToString());
        
        Monitoring.Log.Debug("UserRepository.Single called");
        
        
        var user = await _context.Users.FindAsync(id);
        
        if (user == null)
        {
            throw new Exception("User not found");
        }
        
        return user;
    }

    public async Task<User> Update(User user)
    {
        //Monitoring and logging
        using var activity = Monitoring.ActivitySource.StartActivity("UserService.Infrastructure.Update");
        activity?.SetTag("user", user.Username);
        
        Monitoring.Log.Debug("UserRepository.Update called");
        
        
        var updated = _context.Users.Update(user);
        await _context.SaveChangesAsync();
        return updated.Entity;
    }
}