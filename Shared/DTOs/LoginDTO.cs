namespace Shared.User;

/// <summary>
/// DTO for logging in a user
/// </summary>
public class LoginDTO
{
    public string email { get; set; }
    public string plainPassword { get; set; }
}