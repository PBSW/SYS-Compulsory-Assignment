using UserService.Domain;

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
        throw new NotImplementedException();
    }

    public User Create(User user)
    {
        throw new NotImplementedException();
    }

    public bool Delete(int id)
    {
        throw new NotImplementedException();
    }

    public User Single(int id)
    {
        throw new NotImplementedException();
    }

    public User Update(User user)
    {
        throw new NotImplementedException();
    }
}