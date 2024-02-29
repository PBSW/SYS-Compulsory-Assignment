using Microsoft.AspNetCore.Mvc;
using Shared.User.Dto;
using UserService.Service;

namespace UserService.API;

/// <summary>
/// Api contoller for tweets
/// </summary>
[ApiController]
[Route("api/user")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    
    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    [Route("/{id}")]
    public ActionResult<UserDTO> GetUser([FromRoute] int id)
    {
        throw new NotImplementedException();
    }

    [HttpPut]
    public ActionResult<UserDTO> UpdateUser(UserUpdate user)
    {
        throw new NotImplementedException();
    }

    [HttpDelete]
    [Route("{id}")]
    public ActionResult DeleteUser([FromRoute] int id)
    {
        throw new NotImplementedException();
    }

    [HttpPost]
    public ActionResult<UserAuth> Login(UserLogin user)
    {
        throw new NotImplementedException();
    }

    [HttpPost]
    public ActionResult<UserAuth> Register(UserRegister user)
    {
        throw new NotImplementedException();
    }

    [HttpGet]
    [Route("{id}/following")]
    public ActionResult<IEnumerable<UserDTO>> GetFollowers([FromRoute] int id)
    {
        throw new NotImplementedException();
    }

    [HttpPost]
    [Route("{id}/follow/{followId}")]
    public ActionResult FollowUser([FromRoute] int id, [FromRoute] int followId)
    {
        throw new NotImplementedException();
    }
    
    [HttpPost]
    [Route("{id}/unfollow/{unfollowId}")]
    public ActionResult UnfollowUser([FromRoute] int id, [FromRoute] int unfollowId)
    {
        throw new NotImplementedException();
    }
}