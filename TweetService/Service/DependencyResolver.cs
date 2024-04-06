using TweetService.Infrastructure;

namespace TweetService.Service;

public class DependencyResolver
{
    public static void RegisterServices(IServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient<ITweetService, global::TweetService.Service.TweetService>();
        serviceCollection.AddTransient<ITweetRepository, TweetRepository>();
    }
}