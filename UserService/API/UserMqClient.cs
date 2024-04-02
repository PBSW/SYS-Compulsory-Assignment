using EasyNetQ;
using Shared.Messages;
using Shared.User.Dto;
using UserService.Service;

namespace UserService.API;

public class UserMqClient : BackgroundService
{
    private readonly IBus _bus;
    private readonly IUserService _userService;
    
    public UserMqClient(IBus bus, IUserService userService)
    {
        _bus = bus;
        _userService = userService;
    }
    
    
    /**
     * Init subscriptions
     */
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _bus.PubSub.SubscribeAsync<UserRegister>("user/register",  message => Register(message), stoppingToken);   
        _bus.PubSub.SubscribeAsync<UserIdMessage>("user/get",  message => GetUser(message), stoppingToken);   
        _bus.PubSub.SubscribeAsync<UserUpdate>("user/update",  message => UpdateUser(message), stoppingToken);   
        _bus.PubSub.SubscribeAsync<UserIdMessage>("user/delete",  message => DeleteUser(message), stoppingToken);   
        _bus.PubSub.SubscribeAsync<UserLogin>("user/login",  message => Login(message), stoppingToken);
        _bus.PubSub.SubscribeAsync<DoubleIdMessage>("user/follow",  FollowUser, stoppingToken);
        _bus.PubSub.SubscribeAsync<DoubleIdMessage>("user/unfollow",  UnfollowUser, stoppingToken);
        _bus.PubSub.SubscribeAsync<UserIdMessage>("user/all-followers",  message => GetFollowers(message), stoppingToken);
        
        return Task.CompletedTask;    
    }
    
    
    
    //[Route("/{id}")]
    public UserDTO GetUser(UserIdMessage message)
    {
        var userId = message.UserId;
        
        throw new NotImplementedException();
    }
    
    public UserDTO UpdateUser(UserUpdate user)
    {
        throw new NotImplementedException();
    }

    //[Route("{id}")]
    public void DeleteUser(UserIdMessage id)
    {
        throw new NotImplementedException();
    }
    
    public UserAuth Login(UserLogin user)
    {
        throw new NotImplementedException();
    }

    public UserAuth Register(UserRegister user)
    {
        throw new NotImplementedException();
    }

    //[Route("{id}/following")]
    public IEnumerable<UserDTO> GetFollowers(UserIdMessage message)
    {
        var userId = message.UserId;
        
        throw new NotImplementedException();
    }

    //[Route("{id}/follow/{followId}")]
    public void FollowUser(DoubleIdMessage message)
    {
        var userId = message.Id1;
        var unfollowId = message.Id2;

        throw new NotImplementedException();
    }
    
    //[Route("{id}/unfollow/{unfollowId}")]
    public void UnfollowUser(DoubleIdMessage message)
    {
        var userId = message.Id1;
        var unfollowId = message.Id2;
        
        throw new NotImplementedException();
    }
    
    
    public void SendMessage(IInfrastructureMessage message, string topic)
    {

        _bus.PubSub.PublishAsync(message);
    }
}