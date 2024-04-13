namespace Shared.Domain;

/// <summary>
/// The full structure of a User account
/// </summary>
public class User
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
}