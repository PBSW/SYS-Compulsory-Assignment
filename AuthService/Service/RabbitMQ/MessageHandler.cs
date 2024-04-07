using AuthService.Service;
using EasyNetQ;


namespace AuthService.Service.RabbitMQ;

public class MessageHandler : BackgroundService
{
    private readonly IAuthService _authService;
    private readonly IBus _bus;

    public MessageHandler(IAuthService authService, IBus bus)
    {
        _authService = authService;
        _bus = bus;
    }

    private List<int> GetFollowers()
    {
        return new List<int>
        {
            1, 2, 3, 4
        };
    }

    private async void HandleAuthenticationMessage(string token)
    {
        var userId = _authService.GetUserId(token);
        var followers = GetFollowers();

        if (followers.Contains(userId))
        {
            var newToken = _authService.GenerateToken(userId);
            _bus.PubSub.Publish(newToken, "Token");
        }
    }
  
    private async void HandleTokenCreationMessage()
    {
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        Console.WriteLine("Message handler is running...");

        var messageClient = new MessageClient(_bus);


        messageClient.Listen<string>(HandleAuthenticationMessage, "Auth");

        while (!stoppingToken.IsCancellationRequested)
        {
            await Task.Delay(1000, stoppingToken);
        }

        Console.WriteLine("Message handler is stopping...");
    }
}