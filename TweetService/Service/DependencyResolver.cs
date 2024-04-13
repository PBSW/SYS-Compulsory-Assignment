using TweetService.Infrastructure;

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
        serviceCollection.AddAutoMapper(typeof(Program).Assembly);
        
        serviceCollection.AddScoped<ITweetService, TweetService>();
        serviceCollection.AddScoped<ITweetRepository, TweetRepository>();
    }
}