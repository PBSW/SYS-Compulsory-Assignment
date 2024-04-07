using Shared.Domain;

namespace UserService.Infrastructure;

public class UserRepository : IUserRepository
{
    private readonly DatabaseContext _context;
    
    public UserRepository(DatabaseContext context)
    {
        _context = context;
    }
    
    public IEnumerable<User> All()
    {
        var users = _context.Users;
        return users;
    }

    public User Create(User user)
    {
        _context.Users.Add(user);
        _context.SaveChanges();
        return user;
    }

    public bool Delete(int id)
    {
        var user = _context.Users.Find(id);
        if (user == null)
        {
            return false;
        }
        
        _context.Users.Remove(user);
        _context.SaveChanges();
        return true;
    }

    public User Single(int id)
    {
        var user = _context.Users.Find(id);
        return user;
    }

    public User Update(User user)
    {
        _context.Users.Update(user);
        _context.SaveChanges();
        return user;
    }
}