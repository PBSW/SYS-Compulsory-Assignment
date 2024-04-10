namespace Shared.Domain;

public class AuthUser
{
    public string email { get; set; } = null!;
    public string hashedPassword { get; set; } = null!;
    public byte[] salt { get; set; } = null!;
    public string Username { get; set; } = null!;
}