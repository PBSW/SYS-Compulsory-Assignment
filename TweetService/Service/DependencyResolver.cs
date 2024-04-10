using EasyNetQ;
using TweetService.Infrastructure;
using UserService.Service.RabbitMQ;

namespace TweetService.Service;

public class DependencyResolver
{
    public IConfiguration Configuration { get; }
    
    public DependencyResolver(IConfiguration configuration)
    {
        Configuration = configuration;
    }
    public static void RegisterServices(IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton(new MessageClient(RabbitHutch.CreateBus("host=rabbitmq;port=5672;virtualHost=/;username=guest;password=guest")));
        
        serviceCollection.AddScoped<ITweetService, TweetService>();
        serviceCollection.AddScoped<ITweetRepository, TweetRepository>();
    }
}