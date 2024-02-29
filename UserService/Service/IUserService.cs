using UserService.Domain;

namespace UserService.Service;

public interface IUserService
{
    public User Register(User user);
    
}