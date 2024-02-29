using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using TweetService.Domain;
using TweetService.Service;

namespace TweetService.API;

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
    public IEnumerable<Tweet> GetTweetsFromUser([FromBody] int user_id)
    {
        throw new NotImplementedException();
    }
    
    [HttpGet]
    [Route("{uid}/Recent")]
    public IEnumerable<Tweet> GetRecentTweets([FromRoute] int uid, 
        [FromQuery] int fromUtc, [FromQuery] int toUtc)
    {
        throw new NotImplementedException();
    }
    
    
    [HttpPost]
    public Tweet PostTweet(Tweet tweet)
    {
        throw new NotImplementedException();
    }
    
    [HttpDelete]
    [Route("{id}")]
    public Tweet DeleteTweet([FromRoute] int id)
    {
        throw new NotImplementedException();
    }
    
    
    
}