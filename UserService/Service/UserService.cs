using AutoMapper;
using Shared.Domain;
using Shared.User;
using Shared.Monitoring;

using UserService.Infrastructure;

namespace UserService.Service;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    
    public UserService(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }
    
    public async Task<bool> CreateUser(UserCreateDTO user)
    {
        //Monitoring and logging
        using var activity = Monitoring.ActivitySource.StartActivity("UserService.Login");
        activity?.SetTag("user", user.Username);
        
        Monitoring.Log.Debug("UserService.Login called");

        if (user == null)
        {
            throw new NullReferenceException("User is null");
        }
        
        var dbUser = _mapper.Map<User>(user);
        
        return await _userRepository.Create(dbUser);
    }

    public async Task<UserDTO> GetUser(int userId)
    {
        //Monitoring and logging
        using var activity = Monitoring.ActivitySource.StartActivity("UserService.GetUser");
        activity?.SetTag("userId", userId.ToString());
        
        Monitoring.Log.Debug("UserService.GetUser called");
        
        if (userId == null || userId <= 0)
        {
            throw new NullReferenceException("User id is invalid");
        }
        
        User user = await _userRepository.Single(userId);
        
        UserDTO userDto = _mapper.Map<User, UserDTO>(user);
        
        return userDto;
    }

    public async Task<User> UpdateUser(UserUpdateDTO user)
    {
        //Monitoring and logging
        using var activity = Monitoring.ActivitySource.StartActivity("UserService.UpdateUser");
        activity?.SetTag("userId", user.Id.ToString());
        
        Monitoring.Log.Debug("UserService.UpdateUser called");
        
        
        var dbUser = await _userRepository.Single(user.Id);
        
        if (user.Username != null)
        {
            dbUser.Username = user.Username;
        }
        
        return await _userRepository.Update(dbUser);
    }

    public async Task DeleteUser(int userId)
    {
        //Monitoring and logging
        using var activity = Monitoring.ActivitySource.StartActivity("UserService.DeleteUser");
        activity?.SetTag("userId", userId.ToString());
        
        Monitoring.Log.Debug("UserService.DeleteUser called");
        
        
        await _userRepository.Delete(userId);
    }

    public async Task<UserDTO> GetUserByUsername(string username)
    {
        //Monitoring and logging
        using var activity = Monitoring.ActivitySource.StartActivity("UserService.GetUserByUsername");
        activity?.SetTag("username", username);
        
        Monitoring.Log.Debug("UserService.GetUserByUsername called");
        
        User user = await _userRepository.UserByUsername(username);

        UserDTO userDto = _mapper.Map<User, UserDTO>(user);

        return userDto;
    }
}