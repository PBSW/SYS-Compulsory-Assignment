using EasyNetQ;
using UserService.Service;

namespace UserService.API;

public class UserMqClient
{
    private readonly IBus _bus;
    private readonly IUserService _userService;
    
    public UserMqClient(IBus bus, IUserService userService)
    {
        _bus = bus;
        _userService = userService;
    }
    
    
    public void SendMessage(string message)
    {
        _bus.PubSub.PublishAsync(message);
    }
    
}