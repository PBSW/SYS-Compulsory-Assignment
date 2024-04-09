using EasyNetQ;
using Shared.Domain;
using Shared.Monitoring;

namespace UserService.Service.RabbitMQ;

public class MessageHandler : BackgroundService
{
    private readonly IUserService _userService;
    private readonly IBus _bus;

    public MessageHandler(IUserService userService, IBus bus)
    {
        _userService = userService;
        _bus = bus;
    }

    private List<int> GetFollowers()
    {
        return new List<int>
        {
            1, 2, 3, 4
        };
    }

    private async void HandleUserMessage(User user)
    {
        Monitoring.Log.Debug("User - user message received...");
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        Monitoring.Log.Debug("User - Message handler is running...");

        var messageClient = new MessageClient(_bus);


        messageClient.Listen<User>(HandleUserMessage, "User");

        while (!stoppingToken.IsCancellationRequested)
        {
            await Task.Delay(1000, stoppingToken);
        }

        Monitoring.Log.Debug("User - Message handler is stopping...");
    }
}