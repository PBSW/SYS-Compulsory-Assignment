namespace Shared.User;

/// <summary>
/// DTO for creating a new user account 
/// </summary>
public class RegisterDTO 
{
    /// <summary>
    /// The email of the new user
    /// </summary>
    public string? Email { get; set; }
    
    /// <summary>
    /// The handle of the new user 
    /// </summary>
    public string? Username { get; set; }   
    
    /// <summary>
    /// Hased password of the new user
    /// </summary>
    public string? Password { get; set; }

    public Dictionary<string, string> Headers { get; set; }
} 