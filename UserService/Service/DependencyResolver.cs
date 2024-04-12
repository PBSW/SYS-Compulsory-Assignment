using UserService.Infrastructure;

namespace UserService.Service;

using Microsoft.Extensions.DependencyInjection;


public class DependencyResolver
{
    public static void RegisterServices(IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IUserService, UserService>();
        serviceCollection.AddScoped<IUserRepository, UserRepository>();
        
        serviceCollection.AddAutoMapper(typeof(Program).Assembly);
    }
}