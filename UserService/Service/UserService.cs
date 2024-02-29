using UserService.Domain;
using UserService.Infrastructure;

namespace UserService.Service;

public class UserService : IUserService
{
    
    private readonly IUserRepository _userRepository;
    
    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    

    public User Register(User user)
    {
        throw new NotImplementedException();
    }
}