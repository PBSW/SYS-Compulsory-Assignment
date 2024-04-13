using Microsoft.AspNetCore.Mvc;
using Shared.User;

namespace AuthService.Service;

public interface IAuthService
{
    public Task<IActionResult> Login(LoginDTO dto);
    public Task<bool> Register(RegisterDTO dto);
}