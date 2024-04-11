namespace AuthService.Service.Helpers;

public interface IPasswordHasher
{
    public Task<string> HashPassword(string password, byte[] salt);
}