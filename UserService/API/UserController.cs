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

    [HttpGet]
    [Route("{id}/following")]
    public async Task<ActionResult<IEnumerable<UserDTO>>> GetFollowers([FromRoute] int id)
    {
        //Monitoring and logging
        using var activity = Monitoring.ActivitySource.StartActivity("UserService.GetFollowers");
        activity?.SetTag("userId", id.ToString());
        
        try
        {
            Monitoring.Log.Debug("UserService.API.GetFollowers called");
            
            var followers = await _userService.GetFollowers(id);
            return BadRequest("GetFollowers endpoint - Not implemented");
            //TODO: return Ok(followers);
        }
        catch (Exception e)
        {
            Monitoring.Log.Error("Error in UserService.API.GetFollowers", e);
            throw;
        }
       
    }

    [HttpPost]
    [Route("/follow/{id}/{followId}")]
    public async Task<ActionResult> FollowUser([FromRoute] int id, [FromRoute] int followId)
    {
        //Monitoring and logging
        using var activity = Monitoring.ActivitySource.StartActivity("UserService.FollowUser");
        activity?.SetTag("userId", id.ToString());
        activity?.SetTag("followId", followId.ToString());
        
        try
        {
            Monitoring.Log.Debug("UserService.API.FollowUser called");
            
            await _userService.FollowUser(id, followId);
            return Ok();
        }
        catch (Exception e)
        {
            Monitoring.Log.Error("Error in UserService.API.FollowUser", e);
            throw;
        }
    }
    
    [HttpPost]
    [Route("/unfollow/{id}/{unfollowId}")]
    public async Task<ActionResult> UnfollowUser([FromRoute] int id, [FromRoute] int unfollowId)
    {
        //Monitoring and logging
        using var activity = Monitoring.ActivitySource.StartActivity("UserService.UnfollowUser");
        activity?.SetTag("userId", id.ToString());
        activity?.SetTag("unfollowId", unfollowId.ToString());
        
        try
        {
            Monitoring.Log.Debug("UserService.API.UnfollowUser called");
            
            await _userService.UnfollowUser(id, unfollowId);
            return Ok();
        }
        catch (Exception e)
        {
            Monitoring.Log.Error("Error in UserService.API.UnfollowUser", e);
            throw;
        }
    }
}