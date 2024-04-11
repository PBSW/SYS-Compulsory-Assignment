using Microsoft.AspNetCore.Mvc;
using Shared.Monitoring;
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
        //Monitoring and logging
        using var activity = Monitoring.ActivitySource.StartActivity("TweetService.API.GetTweetsFromUser");
        activity?.SetTag("user_id", user_id.ToString());
        
        Monitoring.Log.Debug("TweetController.GetTweetsFromUser called");
        throw new NotImplementedException("TweetService.API.GetTweetsFromUser not implemented");
    }
    
    [HttpGet]
    [Route("recent/{uid}-{fromUtc}-{toUtc}")]
    public async Task<ActionResult<IEnumerable<TweetDTO>>> GetRecentTweets([FromRoute] int uid, 
        [FromRoute] int fromUtc, [FromRoute] int toUtc)
    {
        //Monitoring and logging
        using var activity = Monitoring.ActivitySource.StartActivity("TweetService.API.GetRecentTweets");
        activity?.SetTag("uid", uid.ToString());
        activity?.SetTag("fromUtc", fromUtc.ToString());
        activity?.SetTag("toUtc", toUtc.ToString());
        
        Monitoring.Log.Debug("TweetController.GetRecentTweets called");
        
        var from = DateTimeOffset.FromUnixTimeSeconds(fromUtc).DateTime;
        var to = DateTimeOffset.FromUnixTimeSeconds(toUtc).DateTime;

        throw new NotImplementedException("TweetService.API.GetRecentTweets not implemented");
    }
    
    
    [HttpPost]
    public async Task<ActionResult<TweetDTO>> PostTweet(TweetCreate tweet)
    {
        //Monitoring and logging
        using var activity = Monitoring.ActivitySource.StartActivity("TweetService.API.PostTweet");
        activity?.SetTag("tweet", tweet.Content);
        //TODO: activity?.SetTag("author", tweet.AuthorId.ToString());
        
        throw new NotImplementedException("TweetService.API.PostTweet not implemented");
    }
    
    [HttpDelete]
    [Route("{id}")]
    public async Task<ActionResult> DeleteTweet([FromRoute] int id)
    {
        //Monitoring and logging
        using var activity = Monitoring.ActivitySource.StartActivity("TweetService.API.DeleteTweet");
        activity?.SetTag("id", id.ToString());
        
        throw new NotImplementedException("TweetService.API.DeleteTweet not implemented");
    }
    
    
    
       
}