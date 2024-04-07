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
        return _tweetRepository.Create(tweet);
    }

    public IEnumerable<Tweet> GetTweetsFromUser(int user_id)
    {
        return _tweetRepository.AllFrom(user_id);
    }

    public IEnumerable<Tweet> GetRecentTweets(int uid, DateTime from, DateTime to)
    {
        return _tweetRepository.AllRecent(new List<int> { uid }, from, to);
    }


    public bool Delete(int id)
    {
        return _tweetRepository.Delete(id);
    }
}