using Shared.Domain;
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
        return await _tweetRepository.Create(tweet);
    }

    public async Task<IEnumerable<Tweet>> GetTweetsFromUser(int user_id)
    {
        return await _tweetRepository.AllFrom(user_id);
    }

    public async Task<IEnumerable<Tweet>> GetRecentTweets(int uid, DateTime from, DateTime to)
    {
        return await _tweetRepository.AllRecent(new List<int> { uid }, from, to);
    }

    public async Task<bool> Delete(int id)
    {
        return await _tweetRepository.Delete(id);
    }
    
}