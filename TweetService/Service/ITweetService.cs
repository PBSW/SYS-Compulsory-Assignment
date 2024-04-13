using Microsoft.AspNetCore.Mvc;
using Shared.Domain;
using Shared.Tweet.Dto;

namespace TweetService.Service;

public interface ITweetService
{
    public Task<TweetDTO> CreateTweet(TweetCreate tweet);
    public Task<List<TweetDTO>> GetTweetsFromUser(int uid);
    public Task<List<TweetDTO>> GetAllTweets();
    public Task<bool> Delete(int id);
    
}