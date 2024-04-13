using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Shared.Monitoring;

namespace AuthService.Service.Helpers;

public class PasswordHasher : IPasswordHasher
{
    public Task<string> HashPassword(string password, byte[] salt)
    {
        // Monitor and logging
        Monitoring.ActivitySource.StartActivity("HashPassword is called");
        Monitoring.Log.Debug("HashPassword is called");
        
        if (password == null || salt == null)
        {
            Monitoring.Log.Error("Password or salt is null");
            throw new ArgumentNullException(nameof(password), nameof(salt));
        }

        return Task.Run(() => Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password,
            salt,
            KeyDerivationPrf.HMACSHA256,
            10000,
            256 / 8)));
    }
}