using AuthService.Service.Helpers;
using Microsoft.AspNetCore.Mvc;
using Shared.User;

namespace AuthService.Service;

public class AuthService : IAuthService
{
    private readonly IJWTProvider _jwtProvider;
    
    public AuthService(IJWTProvider jwtProvider)
    {
        _jwtProvider = jwtProvider;
    }

    public Task<IActionResult> Login(LoginDTO dto)
    {
        throw new NotImplementedException();
    }

    public Task<IActionResult> Register(RegisterDTO dto)
    {
        throw new NotImplementedException();
    }
}