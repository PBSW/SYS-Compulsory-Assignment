using TweetService.Domain;
using TweetService.Infrastructure;

namespace TweetService.Service;

public class TweetService : ITweetService
{
    private readonly ITweetRepository _tweetRepository;
    
    public TweetService(ITweetRepository tweetRepository)
    {
        _tweetRepository = tweetRepository;
    }

    public Tweet Post(Tweet tweet)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Tweet> GetTweetsFromUser(int user_id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Tweet> GetRecentTweets(int uid, int fromUtc, int toUtc)
    {
        throw new NotImplementedException();
    }

    public Tweet Delete(int id)
    {
        throw new NotImplementedException();
    }
}