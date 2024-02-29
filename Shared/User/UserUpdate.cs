namespace Shared.User.Dto;

/// <summary>
/// DTO for editing a user profile
/// </summary>
public class UserUpdate 
{
    /// <summary>
    /// the users new handle 
    /// </summary>
    public string? Username { get; set; }   
    
    /// <summary>
    /// The users new password (hashed)
    /// </summary>
    public string? Password { get; set; }
    
    /// <summary>
    /// The new profile description of the user
    /// </summary>
    public string? Bio { get; set; }

    /// <summary>
    /// The new profile picture of the user
    /// </summary>
    public string? ProfilePicture { get; set; }
}