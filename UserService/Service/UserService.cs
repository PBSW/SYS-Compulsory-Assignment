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
        activity?.SetTag("user", user.username);
        
        Monitoring.Log.Debug("UserService.Login called");

        if (user == null)
        {
            throw new NullReferenceException("User is null");
        }
        
        var dbUser = _mapper.Map<User>(user);
        
        var reponse = await _userRepository.Create(dbUser);
        
        if (reponse == 0)
        {
            return false;
        }
        
        return true;
    }

    public async Task<User> GetUser(int userId)
    {
        //Monitoring and logging
        using var activity = Monitoring.ActivitySource.StartActivity("UserService.GetUser");
        activity?.SetTag("userId", userId.ToString());
        
        Monitoring.Log.Debug("UserService.GetUser called");
        
        if (userId == null || userId <= 0)
        {
            throw new NullReferenceException("User id is invalid");
        }
        
        return await _userRepository.Single(userId);
    }

    public async Task<User> UpdateUser(UserUpdateDTO user)
    {
        //Monitoring and logging
        using var activity = Monitoring.ActivitySource.StartActivity("UserService.UpdateUser");
        activity?.SetTag("userId", user.UserId.ToString());
        
        Monitoring.Log.Debug("UserService.UpdateUser called");
        
        
        var dbUser = await _userRepository.Single(user.UserId);
        
        if (user.Username != null)
        {
            dbUser.Username = user.Username;
        }
        
        if (user.Password != null)
        {
            //dbUser.Password = user.Password;
        }
        
        if (user.Bio != null)
        {
            dbUser.Bio = user.Bio;
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
}