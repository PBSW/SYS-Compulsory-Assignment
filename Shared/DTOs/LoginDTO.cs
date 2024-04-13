namespace Shared.User;

/// <summary>
/// DTO for logging in a user
/// </summary>
public class LoginDTO
{
    public string Email { get; set; }
    public string PlainPassword { get; set; }
}