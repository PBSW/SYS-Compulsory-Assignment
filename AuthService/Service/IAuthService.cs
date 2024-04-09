using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Shared.User;

namespace AuthService.Service;

public interface IAuthService
{
    public Task<IActionResult> Login(LoginDTO dto);
    public Task<IActionResult> Register(RegisterDTO dto);
}