using Microsoft.AspNetCore.Mvc;
using Shared.Tweet.Dto;
using Shared.User.Dto;
using TweetService.Service;

namespace TweetService.API;

/// <summary>
/// Api contoller for tweets
/// </summary>
[ApiController]
[Route("api/tweet")]
public class TweetController : ControllerBase
{
    private readonly ITweetService _tweetService;
    
    TweetController(ITweetService tweetService)
    {
        _tweetService = tweetService;
    }
    
    [HttpGet]
    [Route("{user_id}")]
    public Task<ActionResult<IEnumerable<TweetDTO>>> GetTweetsFromUser([FromBody] int user_id)
    {
        throw new NotImplementedException();
    }
    
    [HttpGet]
    [Route("{uid}/Recent")]
    public Task<ActionResult<IEnumerable<TweetDTO>>> GetRecentTweets([FromRoute] int uid, 
        [FromQuery] int fromUtc, [FromQuery] int toUtc)
    {
        throw new NotImplementedException();
    }
    
    
    [HttpPost]
    public Task<ActionResult<TweetDTO>> PostTweet(TweetCreate tweet)
    {
        throw new NotImplementedException();
    }
    
    [HttpDelete]
    [Route("{id}")]
    public Task<ActionResult> DeleteTweet([FromRoute] int id)
    {
        throw new NotImplementedException();
    }
    
    
    
       
}