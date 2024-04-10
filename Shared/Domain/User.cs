namespace Shared.Domain;

/// <summary>
/// The full structure of a User account
/// </summary>
public class User
{
    /// <summary>
    /// The id of the user
    /// </summary>
    public int Id { get; set; }
    
    /// <summary>
    /// The handle of the user
    /// </summary>
    public string Username { get; set; } = null!;

    /// <summary>
    /// The Email address of the user
    /// </summary>
    public string Email { get; set; } = null!;

    /// <summary>
    /// The user profile description
    /// </summary>
    public string? Bio { get; set; }

    /// <summary>
    /// The current profile picture of the user
    /// </summary>
    public string? ProfilePicture { get; set; }

    /// <summary>
    /// List of other users that this one follows
    /// </summary>
    public List<User> Following { get; set; } = [];
    
    /// <summary>
    /// A list of other users who follow this one
    /// </summary>
    public List<User> Followers { get; set; } = [];
}