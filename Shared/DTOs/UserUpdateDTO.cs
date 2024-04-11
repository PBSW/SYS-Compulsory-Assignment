namespace Shared.User;

/// <summary>
/// DTO for editing a user profile
/// </summary>
public class UserUpdateDTO
{
    
    public int UserId { get; set; }
    
    /// <summary>
    /// the users new handle 
    /// </summary>
    public string? Username { get; set; }   
    
    /// <summary>
    /// The users new password (hashed)
    /// </summary>
    public string? Password { get; set; }
    
    public string? Email { get; set; }
    
    /// <summary>
    /// The new profile description of the user
    /// </summary>
    public string? Bio { get; set; }

    /// <summary>
    /// The new profile picture of the user
    /// </summary>
    public string? ProfilePicture { get; set; }

}