using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OpenTelemetry;
using OpenTelemetry.Context.Propagation;
using Shared.User;
using Shared.Monitoring;
using UserService.Service;

namespace UserService.API;

[Authorize]
[ApiController]
[Route("user")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    
    public UserController(IUserService userService)
    {
        _userService = userService;
    }
    
    [Authorize]
    [HttpGet]
    [Route("/{id}")]
    public async Task<ActionResult<UserDTO>> GetUserById([FromRoute] int id)
    {
        //Monitoring and logging
        Monitoring.Log.Debug("UserService.API.GetUser called");
        
        var headers = Request.Headers.ToDictionary(h => h.Key, h => h.Value.ToString());
        var propagator = new TraceContextPropagator();
        var parrentContext = propagator.Extract(default, headers, (carrier, key) =>
        {
            return new List<string>(new[] { headers.ContainsKey(key) ? headers[key].ToString() : String.Empty });
        });

        Baggage.Current = parrentContext.Baggage;
        using var activity = Monitoring.ActivitySource.StartActivity("Controller.User.GetUserById received message", ActivityKind.Consumer, parrentContext.ActivityContext);
        
        try
        {
            Monitoring.Log.Debug("UserService.API.GetUser called");
            return await _userService.GetUser(id);
        }
        catch (Exception e)
        {
            Monitoring.Log.Error("Error in UserService.API.GetUser", e.Message);
            return BadRequest("Error in getting user: " + e.Message);
        }
    }

    [Authorize(Policy = "RequireAdminRole")]
    [HttpGet("username/{username}")]
    public async Task<ActionResult<UserDTO>> GetUserByUsername([FromRoute] string username)
    {
        //Monitoring and logging
        var headers = Request.Headers.ToDictionary(h => h.Key, h => h.Value.ToString());
        var propagator = new TraceContextPropagator();
        var parrentContext = propagator.Extract(default, headers, (carrier, key) =>
        {
            return new List<string>(new[] { headers.ContainsKey(key) ? headers[key].ToString() : String.Empty });
        });

        Baggage.Current = parrentContext.Baggage;
        using var activity = Monitoring.ActivitySource.StartActivity("Controller.User.GetUserByUsername received message", ActivityKind.Consumer, parrentContext.ActivityContext);
        
        try
        {
            Monitoring.Log.Debug("UserService.API.GetUser called");
            return await _userService.GetUserByUsername(username);
        }
        catch (Exception e)
        {
            Monitoring.Log.Error("Error in UserService.API.GetUser", e.Message);
            return BadRequest("Error in getting user: " + e.Message);
        }
    }
    
    [Authorize(Policy = "RequireAdminRole")]
    [HttpPost ("create")]
    public async Task<ActionResult<UserDTO>> CreateUser(UserCreateDTO user)
    {
        //Monitoring and logging
        var headers = Request.Headers.ToDictionary(h => h.Key, h => h.Value.ToString());
        var propagator = new TraceContextPropagator();
        var parrentContext = propagator.Extract(default, headers, (carrier, key) =>
        {
            return new List<string>(new[] { headers.ContainsKey(key) ? headers[key].ToString() : String.Empty });
        });

        Baggage.Current = parrentContext.Baggage;
        using var activity = Monitoring.ActivitySource.StartActivity("Controller.User.CreateUser received message", ActivityKind.Consumer, parrentContext.ActivityContext);


        try
        {
            Monitoring.Log.Debug("UserService.API.CreateUser called");
            return Ok(await _userService.CreateUser(user));
        }
        catch (Exception e)
        {
            Monitoring.Log.Error("Error in UserService.API.CreateUser", e.Message);
            return BadRequest("Error in creating user:" + e.Message);
        }
    }
    
    [Authorize]
    [HttpPut]
    public async Task<ActionResult<UserDTO>> UpdateUser(UserUpdateDTO user)
    {
        //Monitoring and logging
        using var activity = Monitoring.ActivitySource.StartActivity("UserService.UpdateUser");
        activity?.SetTag("userId", user.Id.ToString());

        try
        {
            Monitoring.Log.Debug("UserService.API.UpdateUser called");
            return Ok(await _userService.UpdateUser(user));
        }
        catch (Exception e)
        {
            Monitoring.Log.Error("Error in UserService.API.UpdateUser", e.Message);
            return BadRequest("Error in updating user: " + e.Message);
        }
    }

    [Authorize]
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
            
            return Ok(await _userService.DeleteUser(id));
        }
        catch (Exception e)
        {
            Monitoring.Log.Error("Error in UserService.API.DeleteUser", e.Message);
            return BadRequest("Error in deleting user: " + e.Message);
        }
    }
}