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
        _tweetRepository.Create(tweet);
    }

    public IEnumerable<Tweet> GetTweetsFromUser(int user_id)
    {
        return _tweetRepository.AllFrom(user_id);
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