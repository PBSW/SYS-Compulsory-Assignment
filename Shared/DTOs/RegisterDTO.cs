namespace Shared.User;

/// <summary>
/// DTO for creating a new user account 
/// </summary>
public class RegisterDTO 
{
    public string email { get; set; }
    public string username { get; set; }   
    public string plainPassword { get; set; }
} 