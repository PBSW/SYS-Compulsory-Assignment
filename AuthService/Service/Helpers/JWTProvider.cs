using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace AuthService.Service.Helpers;

public class JWTProvider : IJWTProvider
{
    public string GenerateToken() {
        var claims = new Claim[] { };

        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes("secret")),
            SecurityAlgorithms.HmacSha256);
        
        var token = new JwtSecurityToken(
            "issuer",
            "audience",
            claims,
            null,
            DateTime.UtcNow.AddHours(8),
            signingCredentials
        );
        
        string tokenValue  = new JwtSecurityTokenHandler()
            .WriteToken(token);

        return tokenValue;
    }
}