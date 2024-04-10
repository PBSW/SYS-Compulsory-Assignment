using AuthService.Infrastructure;
using AuthService.Service.Helpers;
using Microsoft.EntityFrameworkCore;
using Shared.Util;
using PasswordHasher = Shared.Util.PasswordHasher;

namespace AuthService.Service;

using Microsoft.Extensions.DependencyInjection;

public class DependencyResolver
{
    public static void RegisterServices(IServiceCollection serviceCollection)
    {
        serviceCollection.AddAutoMapper(typeof(Program).Assembly);
            
        serviceCollection.AddScoped<IAuthService, AuthService>();
        serviceCollection.AddScoped<IPasswordHasher, PasswordHasher>();
        serviceCollection.AddScoped<IJWTProvider, JWTProvider>();
        serviceCollection.AddScoped<ILoginRepository, LoginRepository>();
    }
}