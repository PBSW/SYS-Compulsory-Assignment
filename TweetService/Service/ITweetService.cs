using Shared.Domain;

namespace TweetService.Service;

public interface ITweetService
{
    public Task<Tweet> Post(Tweet tweet);
    public Task<IEnumerable<Tweet>> GetTweetsFromUser(int user_id);
    public Task<IEnumerable<Tweet>> GetRecentTweets(int uid, DateTime fromUtc, DateTime toUtc);
    public Task<bool> Delete(int id);
    
}