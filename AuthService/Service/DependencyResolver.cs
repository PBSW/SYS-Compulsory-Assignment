using AuthService.Infrastructure;
using AuthService.Service.Helpers;
using Shared.Util;
using PasswordHasher = Shared.Util.PasswordHasher;

namespace AuthService.Service;

using Microsoft.Extensions.DependencyInjection;

public class DependencyResolver
{
    public static void RegisterServices(IServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient<IAuthService, AuthService>();
        serviceCollection.AddTransient<IPasswordHasher, PasswordHasher>();
        serviceCollection.AddTransient<IJWTProvider, JWTProvider>();
        serviceCollection.AddTransient<ILoginRepository, LoginRepository>();
    }
}