using Microsoft.AspNetCore.Mvc;
using Shared.Domain;
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
    public async Task<IActionResult> GetTweetsFromUser([FromRoute] int uid)
    {
        //Monitoring and logging
        using var activity = Monitoring.ActivitySource.StartActivity("TweetService.API.GetTweetsFromUser");
        activity?.SetTag("user_id", uid.ToString());
        
        Monitoring.Log.Debug("TweetController.GetTweetsFromUser called");

        try
        {
            return Ok(await _tweetService.GetTweetsFromUser(uid));
        } catch (Exception e)
        {
            Monitoring.Log.Error("TweetController.GetTweetsFromUser failed", e.ToString());
            return BadRequest(e.ToString());
        }
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllTeweets()
    {
        //Monitoring and logging
        using var activity = Monitoring.ActivitySource.StartActivity("TweetService.API.GetAllTweets");
        
        Monitoring.Log.Debug("TweetController.GetAllTweets called");

        try
        {
            return Ok(await Task.FromResult(_tweetService.GetAllTweets()));
        } catch (Exception e)
        {
            Monitoring.Log.Error("TweetController.GetAllTweets failed", e.ToString());
            return BadRequest(e.ToString());
        }
    }
    
    [HttpPost]
    public async Task<IActionResult> PostTweet(TweetCreate tweet)
    {
        //Monitoring and logging
        using var activity = Monitoring.ActivitySource.StartActivity("TweetService.API.PostTweet");
        activity?.SetTag("tweet", tweet.Content);
        //TODO: activity?.SetTag("author", tweet.AuthorId.ToString());

        try
        {
            Monitoring.Log.Debug("TweetController.PostTweet called");
            return Ok(await _tweetService.CreateTweet(tweet));
        }
        catch (Exception e)
        {
            Monitoring.Log.Error("TweetController.PostTweet failed", e.ToString());
            return BadRequest(e.ToString());
        }
    }
    
    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> DeleteTweet([FromRoute] int id)
    {
        //Monitoring and logging
        using var activity = Monitoring.ActivitySource.StartActivity("TweetService.API.DeleteTweet");
        activity?.SetTag("id", id.ToString());

        try
        {
            Monitoring.Log.Debug("TweetController.DeleteTweet called");
            return Ok(await _tweetService.Delete(id));
        } catch (Exception e)
        {
            Monitoring.Log.Error("TweetController.DeleteTweet failed", e.ToString());
            return BadRequest(e.ToString());
        }
    }
    
    
    
       
}