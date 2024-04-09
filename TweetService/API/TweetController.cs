using Microsoft.AspNetCore.Mvc;
using Shared.Monitoring;
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
        //Monitoring and logging
        using var activity = Monitoring.ActivitySource.StartActivity("TweetService.API.GetTweetsFromUser");
        activity?.SetTag("user_id", user_id.ToString());
        
        Monitoring.Log.Debug("TweetController.GetTweetsFromUser called");
        throw new NotImplementedException("TweetService.API.GetTweetsFromUser not implemented");
    }
    
    [HttpGet]
    [Route("{uid}/Recent")]
    public Task<ActionResult<IEnumerable<TweetDTO>>> GetRecentTweets([FromRoute] int uid, 
        [FromQuery] int fromUtc, [FromQuery] int toUtc)
    {
        //Monitoring and logging
        using var activity = Monitoring.ActivitySource.StartActivity("TweetService.API.GetRecentTweets");
        activity?.SetTag("uid", uid.ToString());
        activity?.SetTag("fromUtc", fromUtc.ToString());
        activity?.SetTag("toUtc", toUtc.ToString());
        
        Monitoring.Log.Debug("TweetController.GetRecentTweets called");
        
        throw new NotImplementedException("TweetService.API.GetRecentTweets not implemented");
    }
    
    
    [HttpPost]
    public Task<ActionResult<TweetDTO>> PostTweet(TweetCreate tweet)
    {
        //Monitoring and logging
        using var activity = Monitoring.ActivitySource.StartActivity("TweetService.API.PostTweet");
        activity?.SetTag("tweet", tweet.Content);
        //TODO: activity?.SetTag("author", tweet.AuthorId.ToString());
        
        throw new NotImplementedException("TweetService.API.PostTweet not implemented");
    }
    
    [HttpDelete]
    [Route("{id}")]
    public Task<ActionResult> DeleteTweet([FromRoute] int id)
    {
        //Monitoring and logging
        using var activity = Monitoring.ActivitySource.StartActivity("TweetService.API.DeleteTweet");
        activity?.SetTag("id", id.ToString());
        
        throw new NotImplementedException("TweetService.API.DeleteTweet not implemented");
    }
    
    
    
       
}