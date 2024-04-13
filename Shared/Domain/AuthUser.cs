namespace Shared.Domain;

public class AuthUser
{
    public string Email { get; set; } = null!;
    public string HashedPassword { get; set; } = null!;
    public byte[] Salt { get; set; } = null!;
    public string Username { get; set; } = null!;
}