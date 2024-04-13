using Shared.Domain;
namespace TweetService.Infrastructure;

public interface ITweetRepository
{
    public Task<List<Tweet>> All();
    public Task<List<Tweet>> AllFrom(int id);
    public Task<Tweet> Create(Tweet tweet);
    public Task<bool> Delete(int id);
    public Task<Tweet> Single(int id);
}