using Microsoft.AspNetCore.Mvc;
using Shared.Domain;
using Shared.Tweet.Dto;

namespace TweetService.Service;

public interface ITweetService
{
    public Task<TweetDTO> CreateTweet(TweetCreate tweet);
    public Task<TweetDTO> GetTweetsFromUser(int uid);
    public Task<TweetDTO> GetAllTweets();
    public Task<bool> Delete(int id);
    
}