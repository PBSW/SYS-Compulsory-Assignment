using Microsoft.AspNetCore.Mvc;
using Shared.Tweet.Dto;
using TweetService.Service;

namespace TweetService.API;

/// <summary>
/// Api contoller for tweets
/// </summary>
[ApiController]
[Route("tweet")]
public class TweetController : ControllerBase
{
    private readonly ITweetService _tweetService;
    
    public TweetController(ITweetService tweetService)
    {
        _tweetService = tweetService;
    }
    
    [HttpGet]
    [Route("{uid}")]
    public async Task<ActionResult<IEnumerable<TweetDTO>>> GetTweetsFromUser([FromRoute] int uid)
    {
        return BadRequest("Not implemented");
    }
    
    [HttpGet]
    [Route("recent/{uid}-{fromUtc}-{toUtc}")]
    public async Task<ActionResult<IEnumerable<TweetDTO>>> GetRecentTweets([FromRoute] int uid, 
        [FromRoute] int fromUtc, [FromRoute] int toUtc)
    {
        //Convert to DateTime
        var from = DateTimeOffset.FromUnixTimeSeconds(fromUtc).DateTime;
        var to = DateTimeOffset.FromUnixTimeSeconds(toUtc).DateTime;
        
        return BadRequest("Not implemented: " + from.ToString() + " " + to.ToString());
    }
    
    
    [HttpPost]
    public async Task<ActionResult<TweetDTO>> PostTweet(TweetCreate tweet)
    {
        return BadRequest("Not implemented");
    }
    
    [HttpDelete]
    [Route("{id}")]
    public async Task<ActionResult> DeleteTweet([FromRoute] int id)
    {
        return BadRequest("Not implemented");
    }
    
    
    
       
}