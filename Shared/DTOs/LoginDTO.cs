namespace Shared.User;

/// <summary>
/// DTO for logging in a user
/// </summary>
public class LoginDTO 
{
    /// <summary>
    /// The email of the user
    /// </summary>
    public string? Email { get; set; }

    /// <summary>
    /// The hashed password of the user
    /// </summary>
    public string? Password { get; set; }
}