using System.Security.Claims;

namespace AuthService.Service.Helpers;

public interface IJWTProvider
{
    public string GenerateToken(string username, IEnumerable<Claim> additionalClaims = null);
}