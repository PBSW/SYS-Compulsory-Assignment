using Shared.Domain;
namespace TweetService.Infrastructure;

public class TweetRepository : ITweetRepository
{
    private readonly DatabaseContext _context;
    
    public TweetRepository(DatabaseContext context)
    {
        _context = context;
    }
    
    public IEnumerable<Tweet> AllFrom(int id)
    {
        var tweets = _context.Tweets.Where(t => t.AuthorId == id);
        return tweets;
    }

    public IEnumerable<Tweet> AllRecent(IEnumerable<int> ids, DateTime from, DateTime to)
    {
        var tweets = _context.Tweets.Where(t => ids.Contains(t.AuthorId) && t.CreatedAt >= from && t.CreatedAt <= to);
        return tweets;
    }

    public Tweet Create(Tweet tweet)
    {
        _context.Tweets.Add(tweet);
        _context.SaveChanges();
        return tweet;
    }

    public bool Delete(int id)
    {
        var tweet = _context.Tweets.Find(id);
        if (tweet == null)
        {
            return false;
        }
        
        _context.Tweets.Remove(tweet);
        _context.SaveChanges();
        return true;
    }

    public Tweet Single(int id)
    {
        var tweet = _context.Tweets.Find(id);
        return tweet;
    }

    public Tweet Update(Tweet tweet)
    {
        _context.Tweets.Update(tweet);
        _context.SaveChanges();
        return tweet;
    }
}