using Shared.Messages;

namespace Shared.User.Dto;

/// <summary>
/// DTO for getting authentication details when logged in
/// </summary>
public class UserAuth
{
    /// <summary>
    /// The user id
    /// </summary>
    public int Id { get; set; }
    
    /// <summary>
    /// The current token that must be sent with http headers on subsequent requests 
    /// </summary>
    public string Token { get; set; } = null!;

    /// <summary>
    /// A UTC timestamp of when this token expires
    /// </summary>
    public int Expires { get; set; }

}