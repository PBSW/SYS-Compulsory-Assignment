namespace Shared.Domain;

public class Login
{
    public string email { get; set; } = null!;
    
    public string password { get; set; } = null!;
    
    public string salt { get; set; } = null!;
    
    public string Username { get; set; } = null!;
}