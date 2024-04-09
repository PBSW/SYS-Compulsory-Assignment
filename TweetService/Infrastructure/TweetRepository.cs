using Microsoft.EntityFrameworkCore;
using Shared.Domain;

namespace TweetService.Infrastructure;

public class TweetRepository : ITweetRepository
{
    private readonly DatabaseContext _context;

    public TweetRepository(DatabaseContext context)
    {
        _context = context;
    }

    public async Task<List<Tweet>> AllFrom(int id)
    {
        var tweets = _context.Tweets.Where(t => t.AuthorId == id);
        return await tweets.ToListAsync();
    }

    public async Task<List<Tweet>> AllRecent(IEnumerable<int> ids, DateTime from, DateTime to)
    {
        var tweets = _context.Tweets.Where(t => ids.Contains(t.AuthorId) && t.CreatedAt >= from && t.CreatedAt <= to);
        return await tweets.ToListAsync();
    }

    public async Task<Tweet> Create(Tweet tweet)
    {
        var added = await _context.Tweets.AddAsync(tweet);
        await _context.SaveChangesAsync();
        return added.Entity;
    }

    public async Task<bool> Delete(int id)
    {
        var tweet = await _context.Tweets.FindAsync(id);
        if (tweet == null)
        {
            return false;
        }

        _context.Tweets.Remove(tweet);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<Tweet> Single(int id)
    {
        var tweet = await _context.Tweets.FindAsync(id);
        
        if (tweet == null)
        {
            throw new Exception("Tweet not found");
        }
        
        return tweet;
    }

    public async Task<Tweet> Update(Tweet tweet)
    {
        var updated = _context.Tweets.Update(tweet);
        await _context.SaveChangesAsync();
        return updated.Entity;
    }
}