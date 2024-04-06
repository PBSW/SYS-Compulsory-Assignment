using Shared.User.Dto;
using UserService.Domain;
using UserService.Infrastructure;

namespace UserService.Service;

public class UserService : IUserService
{
    
    private readonly IUserRepository _userRepository;
    private readonly IAuthService _authService;
    
    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    

    public User Register(User user)
    {
        //Write to database
        
        user.Password = _authService.HashPassword(user.Password);
        
        var created = _userRepository.Create(user);
        
        created.Token = _authService.GenerateToken(user.Id);
        created.TokenExpire = DateTime.Now.AddHours(1);
        
        return _userRepository.Update(user);
    }

    public User Login(User user)
    {
        var dbUser = _userRepository.All().FirstOrDefault(u => u.Username == user.Username);
        
        if (dbUser == null)
        {
            throw new Exception("User not found");
        }
        
        if (!_authService.VerifyPassword(user.Password, dbUser.Password))
        {
            throw new Exception("Invalid password");
        }
        
        dbUser.Token = _authService.GenerateToken(dbUser.Id);
        dbUser.TokenExpire = DateTime.Now.AddHours(1);
        
        return _userRepository.Update(user);
    }
    
    public User GetUser(int userId)
    {
        return _userRepository.Single(userId);
    }

    public User UpdateUser(UserUpdate user)
    {
        var dbUser = _userRepository.Single(user.UserId);
        
        if (user.Username != null)
        {
            dbUser.Username = user.Username;
        }
        
        if (user.Password != null)
        {
            dbUser.Password = user.Password;
        }
        
        if (user.Bio != null)
        {
            dbUser.Bio = user.Bio;
        }
        
        if (user.ProfilePicture != null)
        {
            dbUser.ProfilePicture = user.ProfilePicture;
        }
        
        return _userRepository.Update(dbUser);
    }

    public void DeleteUser(int userId)
    {
        _userRepository.Delete(userId);
    }

    public void FollowUser(int userId, int followId)
    {
        //Get user from database
        //Get followId from database
        var user = _userRepository.Single(userId);
        var follow = _userRepository.Single(followId);
        
        //Add followId to user's followers
        user.Followers.Add(follow);
        
        //Add userId to followId's following
        follow.Following.Add(user);
        
        //Write to database
        _userRepository.Update(user);
        _userRepository.Update(follow);
    }

    public void UnfollowUser(int userId, int unfollowId)
    {
        //Get user from database
        //Get unfollowId from database
        var user = _userRepository.Single(userId);
        var unfollow = _userRepository.Single(unfollowId);
        
        //Remove unfollowId from user's followers
        user.Followers.Remove(unfollow);
        
        //Remove userId from unfollowId's following
        unfollow.Following.Remove(user);
        
        //Write to database
        _userRepository.Update(user);
        _userRepository.Update(unfollow);
    }

    public IEnumerable<User> GetFollowers(int userId)
    {
        //Get user from database
        var user = _userRepository.Single(userId);
        
        //Return user's followers
        return user.Followers;
    }
}