using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Shared.Monitoring;
using Shared.Util;


namespace AuthService.Service;

public class AuthService(string secretKey, IPasswordHasher passwordHasher) : IAuthService
{
    
    public bool ValidateToken(string token)
    {
        //Monitoring and logging
        using var activity = Monitoring.ActivitySource.StartActivity("AuthService.Service.ValidateToken");
        activity?.SetTag("token", token);
        
        Monitoring.Log.Debug("AuthService.ValidateToken called");
        
        
        var tokenHandler = new JwtSecurityTokenHandler();
        var validationParameters = GetValidationParameters();

        try
        {
            tokenHandler.ValidateToken(token, validationParameters, out _);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public string GenerateToken(int userId)
    {
        //Monitoring and logging
        using var activity = Monitoring.ActivitySource.StartActivity("AuthService.Service.GenerateToken");
        activity?.SetTag("userId", userId.ToString());

        Monitoring.Log.Debug("AuthService.GenerateToken called");
        
        
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(secretKey);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, userId.ToString())
            }),
            Expires = DateTime.UtcNow.AddHours(1), // Token expires in 1 hour
            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    public int GetUserId(string token)
    {
        //Monitoring and logging
        using var activity = Monitoring.ActivitySource.StartActivity("AuthService.Service.GetUserId");
        activity?.SetTag("token", token);
        
        Monitoring.Log.Debug("AuthService.GetUserId called");
        
        
        var tokenHandler = new JwtSecurityTokenHandler();
        var validationParameters = GetValidationParameters();
        var claimsPrincipal = tokenHandler.ValidateToken(token, validationParameters, out _);
        var userIdClaim = claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int userId))
        {
            return userId;
        }

        throw new Exception("Invalid token or missing user ID claim.");
    }

    public string HashPassword(string password)
    {
        //Monitoring and logging
        using var activity = Monitoring.ActivitySource.StartActivity("AuthService.Service.HashPassword");
        activity?.SetTag("password", password);
        
        Monitoring.Log.Debug("AuthService.HashPassword called");
        
        return passwordHasher.Hash(password);
    }

    public bool VerifyPassword(string password, string hash)
    {
        //Monitoring and logging
        using var activity = Monitoring.ActivitySource.StartActivity("AuthService.Service.VerifyPassword");
        activity?.SetTag("password", password);
        activity?.SetTag("hash", hash);
        
        Monitoring.Log.Debug("AuthService.VerifyPassword called");
        
        return passwordHasher.Hash(password) == hash;
    }

    private TokenValidationParameters GetValidationParameters()
    {
        //Monitoring and logging
        using var activity = Monitoring.ActivitySource.StartActivity("AuthService.Service.GetValidationParameters");
        Monitoring.Log.Debug("AuthService.GetValidationParameters called");
        
        return new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    }
}