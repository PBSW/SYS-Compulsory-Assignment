using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RestSharp;
using Shared.Domain;

namespace AuthService.Infrastructure;

public class AuthRepository : IAuthRepository
{
    private readonly DatabaseContext _context;
    private string BaseUrl = "http://user-service:8083";
    
    public AuthRepository(DatabaseContext context)
    {
        _context = context;
        _context.Database.EnsureCreated();
    }
    
    public async Task<int> Register(AuthUser authUser)
    {
        _context.AuthUsers.Add(authUser);
        var change = await _context.SaveChangesAsync();
        return change;
    }
    
    public async Task<AuthUser> FindUser(string username)
    {
        return await _context.AuthUsers.FirstOrDefaultAsync(user => user.email.Equals(user.email));
    }

    public async Task<User> GetUserId(string username)
    {
        var options = new RestClientOptions(BaseUrl)
        {
            ThrowOnAnyError = false,
            MaxTimeout = 6000
        };
        var client = new RestClient(options);
        var request = new RestRequest().AddHeader("Content-Type", "application/json");
        request.Method = Method.Get;
        
        var response = client.Execute(request);
        if (response is { IsSuccessStatusCode: true, Content: not null })
        {
            User user = await Task.Run(() => JsonConvert.DeserializeObject<User>(response.Content));
        }
        return null;
    }
}