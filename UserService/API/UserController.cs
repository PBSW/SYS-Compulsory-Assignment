using Microsoft.AspNetCore.Mvc;
using Shared.User;
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
    public Task<ActionResult<UserDTO>> GetUser([FromRoute] int id)
    {
        throw new NotImplementedException();
    }

    [HttpPut]
    public Task<ActionResult<UserDTO>> UpdateUser(UserUpdateDTO user)
    {
        throw new NotImplementedException();
    }

    [HttpDelete]
    [Route("{id}")]
    public Task<ActionResult> DeleteUser([FromRoute] int id)
    {
        throw new NotImplementedException();
    }

    [HttpGet]
    [Route("{id}/following")]
    public Task<ActionResult<IEnumerable<UserDTO>>> GetFollowers([FromRoute] int id)
    {
        throw new NotImplementedException();
    }

    [HttpPost]
    [Route("/follow/{id}/{followId}")]
    public Task<ActionResult> FollowUser([FromRoute] int id, [FromRoute] int followId)
    {
        throw new NotImplementedException();
    }
    
    [HttpPost]
    [Route("/unfollow/{id}/{unfollowId}")]
    public async Task<ActionResult> UnfollowUser([FromRoute] int id, [FromRoute] int unfollowId)
    {
        try
        {
            await _userService.UnfollowUser(id, unfollowId);
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}