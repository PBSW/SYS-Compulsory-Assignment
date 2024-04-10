using AutoMapper;
using Shared.Domain;
using Shared.User;

namespace AuthService.Service.Helpers;

public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<AuthUser, LoginDTO>();
        CreateMap<RegisterDTO, AuthUser>();
    }
}