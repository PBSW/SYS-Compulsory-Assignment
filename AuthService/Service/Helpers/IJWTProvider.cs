namespace AuthService.Service.Helpers;

public interface IJWTProvider
{
    public string GenerateToken();
}