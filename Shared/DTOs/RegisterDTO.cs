namespace Shared.User;

/// <summary>
/// DTO for creating a new user account 
/// </summary>
public class RegisterDTO 
{
    public string Email { get; set; }
    public string Username { get; set; }   
    public string PlainPassword { get; set; }
} 