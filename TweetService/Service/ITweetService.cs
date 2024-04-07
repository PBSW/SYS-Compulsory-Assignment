using TweetService.Domain;

namespace TweetService.Service;

public interface ITweetService
{
    public Tweet Post(Tweet tweet);
    public IEnumerable<Tweet> GetTweetsFromUser(int user_id);
    public IEnumerable<Tweet> GetRecentTweets(int uid, DateTime fromUtc, DateTime toUtc);
    public bool Delete(int id);
}