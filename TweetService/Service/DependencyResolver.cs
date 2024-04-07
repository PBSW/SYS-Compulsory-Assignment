using Shared.Util;
using TweetService.Infrastructure;

namespace TweetService.Service;

public class DependencyResolver
{
    public static void RegisterServices(IServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient<ITweetService, TweetService>();
        serviceCollection.AddTransient<ITweetRepository, TweetRepository>();
        serviceCollection.AddTransient<IPasswordHasher, PasswordHasher>();
    }
}