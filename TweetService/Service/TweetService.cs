﻿using AutoMapper;
using Shared.Domain;
using Shared.Monitoring;
using Shared.Tweet.Dto;
using TweetService.Infrastructure;

namespace TweetService.Service;

public class TweetService : ITweetService
{
    private readonly ITweetRepository _tweetRepository;
    private readonly IMapper _mapper;
    
    public TweetService(ITweetRepository tweetRepository, IMapper mapper)
    {
        _tweetRepository = tweetRepository;
        _mapper = mapper;
    }

    public async Task<TweetDTO> CreateTweet(TweetCreate tweetCreate)
    {
        //Monitoring and logging
        using var activity = Monitoring.ActivitySource.StartActivity("TweetService.Service.Post");
        activity?.SetTag("tweet", tweetCreate.Content);

        if (tweetCreate == null)
        {   
            Monitoring.Log.Error("Tweet is null");
            throw new NullReferenceException("Tweet is null");
        }
        
        Monitoring.Log.Debug("TweetService.Post called tweetRepository.Create");
        Tweet tweet = _mapper.Map<TweetCreate, Tweet>(tweetCreate);

        tweet.CreatedAt = DateTime.Now;
        
        Tweet tweetReturn = await _tweetRepository.Create(tweet);
        
        TweetDTO tweetDto = _mapper.Map<Tweet, TweetDTO>(tweetReturn);
        
        return tweetDto;
    }

    public async Task<List<TweetDTO>> GetTweetsFromUser(int uid)
    {
        //Monitoring and logging
        using var activity = Monitoring.ActivitySource.StartActivity("TweetService.Service.GetTweetsFromUser");
        activity?.SetTag("user_id", uid.ToString());

        Monitoring.Log.Debug("TweetService.GetTweetsFromUser called");

        if (uid == null || uid <= 0)
        {
            Monitoring.Log.Error("User id is invalid");
            throw new NullReferenceException("User id is invalid");
        }
        
        List<Tweet> tweets = await _tweetRepository.AllFrom(uid);
        
        List<TweetDTO> tweetDtos = _mapper.Map<List<Tweet>, List<TweetDTO>>(tweets);

        return tweetDtos;
    }

    public async Task<List<TweetDTO>> GetAllTweets()
    {
        //Monitoring and logging
        using var activity = Monitoring.ActivitySource.StartActivity("TweetService.Service.GetRecentTweets");
        Monitoring.Log.Debug("TweetService.GetRecentTweets called");
        
        List<Tweet> tweets = await _tweetRepository.All();
        
        List<TweetDTO> tweetDtos = _mapper.Map<List<Tweet>, List<TweetDTO>>(tweets);
        
        return tweetDtos;
    }

    public async Task<bool> Delete(int id)
    {
        //Monitoring and logging
        using var activity = Monitoring.ActivitySource.StartActivity("TweetService.Service.Delete");
        activity?.SetTag("id", id.ToString());
        
        Monitoring.Log.Debug("TweetService.Delete called");

        if (id == null || id <= 0)
        {
            Monitoring.Log.Error("Tweet id is invalid");
            throw new NullReferenceException("Tweet id is invalid");
        }

        return await _tweetRepository.Delete(id);
    }
}