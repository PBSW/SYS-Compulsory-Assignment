using Shared.Messages;

namespace Shared.User.Dto;

/// <summary>
/// DTO for logging in a user
/// </summary>
public class UserLogin : IInfrastructureMessage
{
    /// <summary>
    /// The email of the user
    /// </summary>
    public string? Email { get; set; }

    /// <summary>
    /// The hashed password of the user
    /// </summary>
    public string? Password { get; set; }

    public Dictionary<string, string> Headers { get; set; }
}