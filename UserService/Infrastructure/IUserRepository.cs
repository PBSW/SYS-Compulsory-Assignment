using Shared.Domain;

namespace UserService.Infrastructure;

public interface IUserRepository
{
    public Task<List<User>> All();
    public Task<int> Create(User user);
    public Task<bool> Delete(int id);
    public Task<User> Single(int id);
    public Task<User> Update(User user);
}