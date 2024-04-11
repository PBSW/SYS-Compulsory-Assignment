using System.Security.Claims;

namespace AuthService.Service.Helpers;

public interface IJWTProvider
{
    public string GenerateToken(int id, string username, IEnumerable<Claim> additionalClaims = null);
}