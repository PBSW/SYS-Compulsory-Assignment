using Shared.Domain;

namespace UserService.Infrastructure;

public interface IUserRepository
{
    public IEnumerable<User> All();
    public User Create(User user);
    public bool Delete(int id);
    public User Single(int id);
    public User Update(User user);
}