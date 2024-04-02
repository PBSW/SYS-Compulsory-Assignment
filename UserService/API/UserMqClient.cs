using EasyNetQ;
using Shared.Messages;
using Shared.User.Dto;
using UserService.Domain;
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
        _bus.PubSub.SubscribeAsync<UserRegister>("user/req/register",  message => Register(message), stoppingToken);   
        _bus.PubSub.SubscribeAsync<UserIdMessage>("user/req/get",  message => GetUser(message), stoppingToken);   
        _bus.PubSub.SubscribeAsync<UserUpdate>("user/req/update",  message => UpdateUser(message), stoppingToken);   
        _bus.PubSub.SubscribeAsync<UserIdMessage>("user/req/delete",  message => DeleteUser(message), stoppingToken);   
        _bus.PubSub.SubscribeAsync<UserLogin>("user/req/login",  message => Login(message), stoppingToken);
        _bus.PubSub.SubscribeAsync<DoubleIdMessage>("user/req/follow",  FollowUser, stoppingToken);
        _bus.PubSub.SubscribeAsync<DoubleIdMessage>("user/req/unfollow",  UnfollowUser, stoppingToken);
        _bus.PubSub.SubscribeAsync<UserIdMessage>("user/req/all-followers",  message => GetFollowers(message), stoppingToken);
        
        return Task.CompletedTask;    
    }
    
    
    
    //[Route("/{id}")]
    public void GetUser(UserIdMessage message)
    {
        var userId = message.UserId;
        var user = _userService.GetUser(userId);
        
        var dto = new UserDTO()
        {
            Id = user.Id,
            Email = user.Email,
            Username = user.Username,
            Bio = user.Bio,
            ProfilePicture = user.ProfilePicture
        };
        
        SendMessage(dto, "user/res/user");
    }
    
    public void UpdateUser(UserUpdate user)
    {
        var updatedUser = _userService.UpdateUser(user);
        
        var dto = new UserDTO()
        {
            Id = updatedUser.Id,
            Email = updatedUser.Email,
            Username = updatedUser.Username,
            Bio = updatedUser.Bio,
            ProfilePicture = updatedUser.ProfilePicture
        };
        
        SendMessage(dto, "user/res/update");
    }

    //[Route("{id}")]
    public void DeleteUser(UserIdMessage id)
    {
        var userId = id.UserId;
        
        _userService.DeleteUser(userId);
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
        
        var followers = _userService.GetFollowers(userId);
        
        throw new NotImplementedException();
    }

    //[Route("{id}/follow/{followId}")]
    public void FollowUser(DoubleIdMessage message)
    {
        var userId = message.Id1;
        var unfollowId = message.Id2;

        _userService.FollowUser(userId, unfollowId);
    }
    
    //[Route("{id}/unfollow/{unfollowId}")]
    public void UnfollowUser(DoubleIdMessage message)
    {
        var userId = message.Id1;
        var unfollowId = message.Id2;
        
        _userService.UnfollowUser(userId, unfollowId);
    }
    
    
    public void SendMessage(IInfrastructureMessage message, string topic)
    {
        _bus.PubSub.PublishAsync(message, topic);
    }
}