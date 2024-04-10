namespace AuthService.Service.Helpers;

public interface IPasswordHasher
{
    public string HashPassword(string password, byte[] salt);
}