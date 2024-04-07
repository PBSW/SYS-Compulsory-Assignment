using EasyNetQ;
using Shared.Domain;
using TweetService.Service;

namespace UserService.Service.RabbitMQ;

public class MessageHandler : BackgroundService
{
    private readonly ITweetService _tweetService;
    private readonly IBus _bus;

    public MessageHandler(ITweetService tweetService, IBus bus)
    {
        _tweetService = tweetService;
        _bus = bus;
    }

    private List<int> GetFollowers()
    {
        return new List<int>
        {
            1, 2, 3, 4
        };
    }

    private async void HandleUserMessage(Tweet tweet)
    {
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        Console.WriteLine("Message handler is running...");

        var messageClient = new MessageClient(_bus);


        messageClient.Listen<Tweet>(HandleUserMessage, "User");

        while (!stoppingToken.IsCancellationRequested)
        {
            await Task.Delay(1000, stoppingToken);
        }

        Console.WriteLine("Message handler is stopping...");
    }
}