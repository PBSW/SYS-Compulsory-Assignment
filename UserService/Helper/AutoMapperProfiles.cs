using AutoMapper;
using Shared.Domain;
using Shared.User;

namespace UserService.Helper;

public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<UserCreateDTO, User>();
        CreateMap<User, UserDTO>();
    }
}