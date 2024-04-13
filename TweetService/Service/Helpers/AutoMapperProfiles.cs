using AutoMapper;
using Shared.Domain;
using Shared.Tweet.Dto;

namespace TweetService.Service.Helpers;

public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<Tweet, TweetDTO>();
        CreateMap<TweetDTO, Tweet>();
    }
}