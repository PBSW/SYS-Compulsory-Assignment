using Microsoft.AspNetCore.Mvc;
using UserService.Service;

namespace UserService.API;

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
    public User GetUser([FromRoute] int id)
    {
        throw new NotImplementedException();
    }

    [HttpPut]
    public User UpdateUser(User user)
    {
        throw new NotImplementedException();
    }

    [HttpDelete]
    [Route("{id}")]
    public User DeleteUser([FromRoute] int id)
    {
        throw new NotImplementedException();
    }

    [HttpPost]
    public User Login(User user)
    {
        throw new NotImplementedException();
    }

    [HttpPost]
    public User Register(User user)
    {
        throw new NotImplementedException();
    }

    [HttpGet]
    [Route("{id}/following")]
    public IEnumerable<User> GetFollowers([FromRoute] int id)
    {
        throw new NotImplementedException();
    }

    [HttpPost]
    [Route("{id}/follow/{followId}")]
    public User FollowUser([FromRoute] int id, [FromRoute] int followId)
    {
        throw new NotImplementedException();
    }
    
    [HttpPost]
    [Route("{id}/unfollow/{unfollowId}")]
    public User UnfollowUser([FromRoute] int id, [FromRoute] int unfollowId)
    {
        throw new NotImplementedException();
    }
}