using TweetService.Domain;

namespace TweetService.Infrastructure;

public class TweetRepository : ITweetRepository
{
    private readonly DatabaseContext _context;
    
    public TweetRepository(DatabaseContext context)
    {
        _context = context;
    }
    
    public IEnumerable<Tweet> AllFrom(ulong id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Tweet> AllRecent(IEnumerable<ulong> ids, int fromUtc, int toUtc)
    {
        throw new NotImplementedException();
    }

    public Tweet Create(Tweet tweet)
    {
        throw new NotImplementedException();
    }

    public bool Delete(int id)
    {
        throw new NotImplementedException();
    }

    public Tweet Single(int id)
    {
        throw new NotImplementedException();
    }

    public Tweet Update(Tweet tweet)
    {
        throw new NotImplementedException();
    }
}