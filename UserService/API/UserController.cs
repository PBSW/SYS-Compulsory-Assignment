using Microsoft.AspNetCore.Mvc;
using Shared.User;
using Shared.Monitoring;
using UserService.Service;

namespace UserService.API;

/// <summary>
/// Api contoller for tweets
/// </summary>
[ApiController]
[Route("user")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    
    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    [Route("/{id}")]
    public async Task<ActionResult<UserDTO>> GetUser([FromRoute] int id)
    {
        //Monitoring and logging
        using var activity = Monitoring.ActivitySource.StartActivity("UserService.GetUser");
        activity?.SetTag("userId", id.ToString());

        try
        {
            Monitoring.Log.Debug("UserService.API.GetUser called");

            var user = await _userService.GetUser(id);
            
            return BadRequest("GetUser endpoint - Not implemented");
        }
        catch (Exception e)
        {
            Monitoring.Log.Error("Error in UserService.API.GetUser", e);
            throw;
        }
    }

    [HttpPost ("create")]
    public async Task<ActionResult<UserDTO>> CreateUser(UserCreateDTO user)
    {
        //Monitoring and logging
        using var activity = Monitoring.ActivitySource.StartActivity("UserService.CreateUser");
        activity?.SetTag("user", user.username);

        try
        {
            Monitoring.Log.Debug("UserService.API.CreateUser called");
            
            return Ok(await _userService.CreateUser(user));
        }
        catch (Exception e)
        {
            Monitoring.Log.Error("Error in UserService.API.CreateUser", e);
            return BadRequest("Error in creating user:" + e.Message);
        }
    }
    
    [HttpPut]
    public async Task<ActionResult<UserDTO>> UpdateUser(UserUpdateDTO user)
    {
        //Monitoring and logging
        using var activity = Monitoring.ActivitySource.StartActivity("UserService.UpdateUser");
        activity?.SetTag("userId", user.UserId.ToString());

        try
        {
            Monitoring.Log.Debug("UserService.API.UpdateUser called");

            var updatedUser = _userService.UpdateUser(user);
            
            
            return BadRequest("UpdateUser endpoint - Not implemented");
        }
        catch (Exception e)
        {
            Monitoring.Log.Error("Error in UserService.API.UpdateUser", e);
            throw;
        }
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<ActionResult> DeleteUser([FromRoute] int id)
    {
        //Monitoring and logging
        using var activity = Monitoring.ActivitySource.StartActivity("UserService.DeleteUser");
        activity?.SetTag("userId", id.ToString());
        
        try
        {
            Monitoring.Log.Debug("UserService.API.DeleteUser called");
            
            _userService.DeleteUser(id);
            return BadRequest("DeleteUser endpoint - Not implemented");
        }
        catch (Exception e)
        {
            Monitoring.Log.Error("Error in UserService.API.DeleteUser", e);
            throw;
        }
    }
}