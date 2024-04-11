using AuthService.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Monitoring;
using Shared.User;

namespace AuthService.API;

[Authorize]
[ApiController]
[Route("auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    
    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [AllowAnonymous]
    [HttpPost ("login")]
    public async Task<IActionResult> Login([FromBody] LoginDTO dto)
    {
        //Monitoring and logging
        using var activity = Monitoring.ActivitySource.StartActivity("AuthService.Controller.Login");
        Monitoring.Log.Debug("AuthController.Login called");
        
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
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDTO dto)
    {
        //Monitoring and logging
        using var activity = Monitoring.ActivitySource.StartActivity("AuthService.Controller.Register");
        Monitoring.Log.Debug("AuthController.Register called");
        
        try
        {
            return Ok(await _authService.Register(dto));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}