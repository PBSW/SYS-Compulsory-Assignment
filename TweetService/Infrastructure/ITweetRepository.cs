using TweetService.Domain;

namespace TweetService.Infrastructure;

public interface ITweetRepository
{
    public IEnumerable<Tweet> AllFrom(ulong id);
    public IEnumerable<Tweet> AllRecent(IEnumerable<ulong> ids, int fromUtc, int toUtc);
    public Tweet Create(Tweet tweet);
    public bool Delete(int id);
    public Tweet Single(int id);
    public Tweet Update(Tweet tweet);

}