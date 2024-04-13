using Microsoft.EntityFrameworkCore;
using Shared.Domain;
using Shared.Monitoring;

namespace TweetService.Infrastructure;

public class TweetRepository : ITweetRepository
{
    private readonly DatabaseContext _context;

    public TweetRepository(DatabaseContext context)
    {
        _context = context;
        _context.Database.EnsureCreated();
    }

    public async Task<List<Tweet>> AllFrom(int id)
    {
        //Monitoring and logging
        using var activity = Monitoring.ActivitySource.StartActivity("TweetService.Infrastructure.AllFrom");
        activity?.SetTag("id", id.ToString());
        
        Monitoring.Log.Debug("TweetRepository.AllFrom called");
        
        
        var tweets = _context.Tweets.Where(t => t.AuthorId == id);
        return await tweets.ToListAsync();
    }

    public async Task<Tweet> Create(Tweet tweet)
    {
        //Monitoring and logging
        using var activity = Monitoring.ActivitySource.StartActivity("TweetService.Infrastructure.Create");
        activity?.SetTag("tweet", tweet.Content);
        activity?.SetTag("author", tweet.AuthorId.ToString());
        
        Monitoring.Log.Debug("TweetRepository.Create called");
        
        var added = await _context.Tweets.AddAsync(tweet);
        await _context.SaveChangesAsync();
        return added.Entity;
    }

    public async Task<bool> Delete(int id)
    {
        //Monitoring and logging
        using var activity = Monitoring.ActivitySource.StartActivity("TweetService.Infrastructure.Delete");
        activity?.SetTag("id", id.ToString());
        
        Monitoring.Log.Debug("TweetRepository.Delete called");
        
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
        //Monitoring and logging
        using var activity = Monitoring.ActivitySource.StartActivity("TweetService.Infrastructure.Single");
        activity?.SetTag("id", id.ToString());
        
        Monitoring.Log.Debug("TweetRepository.Single called");
        
        var tweet = await _context.Tweets.FindAsync(id);
        
        if (tweet == null)
        {
            throw new Exception("Tweet not found");
        }
        
        return tweet;
    }
}