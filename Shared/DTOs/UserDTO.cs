namespace Shared.User;

/// <summary>
/// DTO for displaying User information
/// </summary>
public class UserDTO 
{
    /// <summary>
    /// The id of the user
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// The displayname or handle of the user
    /// </summary>
    public string? Username { get; set; }   
    
    /// <summary>
    /// The contact email address of the user
    /// </summary>
    public string? Email { get; set; }

    /// <summary>
    /// The users description 
    /// </summary>
    public string? Bio { get; set; }

    /// <summary>
    /// A link to the users profile picture
    /// </summary>
    public string? ProfilePicture { get; set; }

    public Dictionary<string, string> Headers { get; set; }
}