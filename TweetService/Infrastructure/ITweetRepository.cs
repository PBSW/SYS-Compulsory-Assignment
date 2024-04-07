using TweetService.Domain;

namespace TweetService.Infrastructure;

public interface ITweetRepository
{
    public IEnumerable<Tweet> AllFrom(int id);
    public IEnumerable<Tweet> AllRecent(IEnumerable<int> ids, int fromUtc, int toUtc);
    public Tweet Create(Tweet tweet);
    public bool Delete(int id);
    public Tweet Single(int id);
    public Tweet Update(Tweet tweet);

}