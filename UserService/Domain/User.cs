
public class User
{
    public ulong Id { get; set; }
    public string? Username { get; set; }   
    public string? Email { get; set; }
    public string? Password { get; set; }
    public string? Bio { get; set; }
    public string? ProfilePicture { get; set; }
    public string? Token { get; set; }
    public int? TokenExpire { get; set; }
    public IEnumerable<User> Following { get; set; }
    public IEnumerable<User> Followers { get; set; }
}