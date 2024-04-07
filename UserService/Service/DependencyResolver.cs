using UserService.Infrastructure;

namespace UserService.Service;

using Microsoft.Extensions.DependencyInjection;


public class DependencyResolver
{
    public static void RegisterServices(IServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient<IUserService, IUserService>();
        serviceCollection.AddTransient<IUserRepository, UserRepository>();
    }
}