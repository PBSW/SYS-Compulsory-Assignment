using AuthService.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace UserService.API;

/// <summary>
/// Api contoller for tweets
/// </summary>
[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    
    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost ("Login")]
    public async Task<IActionResult> Login([FromBody] UserLoginInfo dto)
    {
        return Ok();
        /**
         * Flow:
         * HttpClient to UserService to verify credentials/password
         * If valid, generate token and return it
         */
    }
    
    //Validate token
    [HttpPost ("ValidateToken")]
    public async Task<IActionResult> ValidateToken([FromBody] string token)
    {
        var result = _authService.ValidateToken(token);
        return Ok(result);
    }
}