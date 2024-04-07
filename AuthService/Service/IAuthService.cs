namespace AuthService.Service;

public interface IAuthService
{
    public bool ValidateToken(string token);
    
    public string GenerateToken(int userId);
    
    public int GetUserId(string token);
    
    public string HashPassword(string password);
    
    public bool VerifyPassword(string password, string hash);
}