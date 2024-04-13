namespace Shared.User;

/// <summary>
/// DTO for editing a user profile
/// </summary>
public class UserUpdateDTO
{
    
    public int Id { get; set; }
    public string Username { get; set; }   

    public string Password { get; set; }
    
    public string Email { get; set; }
}