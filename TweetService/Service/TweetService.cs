using System.Diagnostics;
using OpenTelemetry;
using Shared.Domain;
using Shared.Monitoring;
using TweetService.Infrastructure;

namespace TweetService.Service;

public class TweetService : ITweetService
{
    private readonly ITweetRepository _tweetRepository;

    public TweetService(ITweetRepository tweetRepository)
    {
        _tweetRepository = tweetRepository;
    }

    public async Task<Tweet> Post(Tweet tweet)
    {
        //Monitoring and logging
        using var activity = Monitoring.ActivitySource.StartActivity("TweetService.Service.Post");
        activity?.SetTag("tweet", tweet.Content);
        activity?.SetTag("author", tweet.AuthorId.ToString());

        Monitoring.Log.Debug("TweetService.Post called");

        
        return await _tweetRepository.Create(tweet);
    }

    public async Task<IEnumerable<Tweet>> GetTweetsFromUser(int user_id)
    {
        //Monitoring and logging
        using var activity = Monitoring.ActivitySource.StartActivity("TweetService.Service.GetTweetsFromUser");
        activity?.SetTag("user_id", user_id.ToString());

        Monitoring.Log.Debug("TweetService.GetTweetsFromUser called");


        return await _tweetRepository.AllFrom(user_id);
    }

    public async Task<IEnumerable<Tweet>> GetRecentTweets(int uid, DateTime from, DateTime to)
    {
        //Monitoring and logging
        using var activity = Monitoring.ActivitySource.StartActivity("TweetService.Service.GetRecentTweets");
        activity?.SetTag("uid", uid.ToString());
        activity?.SetTag("from", from.ToString());
        activity?.SetTag("to", to.ToString());
        
        Monitoring.Log.Debug("TweetService.GetRecentTweets called");
        
        
        return await _tweetRepository.AllRecent(new List<int> { uid }, from, to);
    }

    public async Task<bool> Delete(int id)
    {
        //Monitoring and logging
        using var activity = Monitoring.ActivitySource.StartActivity("TweetService.Service.Delete");
        activity?.SetTag("id", id.ToString());
        
        Monitoring.Log.Debug("TweetService.Delete called");
        
        
        return await _tweetRepository.Delete(id);
    }
}