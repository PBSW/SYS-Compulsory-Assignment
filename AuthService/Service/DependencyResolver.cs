using Shared.Util;

namespace AuthService.Service;

using Microsoft.Extensions.DependencyInjection;

public class DependencyResolver
{
    public static void RegisterServices(IServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient<IAuthService, AuthService>();
        serviceCollection.AddTransient<IPasswordHasher, PasswordHasher>();
    }
}