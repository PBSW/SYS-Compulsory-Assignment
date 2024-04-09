using AuthService.Service;
using AuthService.Service.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Shared.User;

namespace UserService.API;

/// <summary>
/// Api contoller for tweets
/// </summary>
[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    
    public AuthController(IAuthService authService, IJWTProvider jwtProvider)
    {
        _authService = authService;
    }

    [AllowAnonymous]
    [HttpPost ("Login")]
    public async Task<IActionResult> Login([FromBody] LoginDTO dto)
    {
        try
        {
            return Ok(await _authService.Login(dto));
        } 
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [AllowAnonymous]
    [HttpPost("Register")]
    public async Task<IActionResult> Register([FromBody] RegisterDTO dto)
    {
        try
        {
            return Ok(_authService.Register(dto));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}