using System.Text;
using EasyNetQ;
using Shared.Domain;
using Shared.Messages.AuthMessages;
using Shared.User;
using Shared.Util;
using UserService.Infrastructure;
using UserService.Service.RabbitMQ;

namespace UserService.Service;

public class UserService : IUserService
{
    
    private readonly IUserRepository _userRepository;
    private readonly MessageClient _messageClient;
    private readonly IBus _bus;
    private readonly IPasswordHasher _passwordHasher;
    
    public UserService(IUserRepository userRepository, IBus bus, IPasswordHasher passwordHasher)
    {
        _userRepository = userRepository;
        _bus = bus;
        _passwordHasher = passwordHasher;
        _messageClient = new MessageClient(_bus);
    }

    public async Task<User> Login(User user)
    {
        var users = await _userRepository.All();
        
        var dbUser = users.Find(u => u.Username == user.Username);
        
        if (dbUser == null)
        {
            throw new Exception("User not found");
        }

        //var verified = await VerifyPassword(user.Password, dbUser.Password); 
        
        //if (!verified)
        //{
        //    throw new Exception("Invalid password");
        //}


        var message = new CreateTokenMessage()
        {
            UserId = dbUser.Id,
            Expiration = DateTime.Now.AddHours(1)
        };
        
        _messageClient.Publish(message, "Auth");
        
        return await _userRepository.Update(user);
    }
    
    public async Task<User> GetUser(int userId)
    {
        return await _userRepository.Single(userId);
    }

    public async Task<User> UpdateUser(UserUpdateDTO user)
    {
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
        
        if (user.ProfilePicture != null)
        {
            dbUser.ProfilePicture = user.ProfilePicture;
        }
        
        return await _userRepository.Update(dbUser);
    }

    public async Task DeleteUser(int userId)
    {
        await _userRepository.Delete(userId);
    }

    public async Task FollowUser(int userId, int followId)
    {
        //Get user from database
        //Get followId from database
        var user = await _userRepository.Single(userId);
        var follow = await _userRepository.Single(followId);
        
        //Add followId to user's followers
        user.Followers.Add(follow);
        
        //Add userId to followId's following
        follow.Following.Add(user);
        
        //Write to database
        await _userRepository.Update(user);
        await _userRepository.Update(follow);
    }

    public async Task UnfollowUser(int userId, int unfollowId)
    {
        //Get user from database
        //Get unfollowId from database
        var user = await _userRepository.Single(userId);
        var unfollow = await _userRepository.Single(unfollowId);
        
        //Remove unfollowId from user's followers
        user.Followers.Remove(unfollow);
        
        //Remove userId from unfollowId's following
        unfollow.Following.Remove(user);
        
        //Write to database
        await _userRepository.Update(user);
        await _userRepository.Update(unfollow);
    }

    public async Task<List<User>> GetFollowers(int userId)
    {
        //Get user from database
        var user = await _userRepository.Single(userId);
        
        //Return user's followers
        return user.Followers;
    }
    
    
    public async Task<string> HashPassword(string password)
    {
        return _passwordHasher.Hash(password);
    }

    public async Task<bool> VerifyPassword(string password, string hash)
    {
        return await HashPassword(password) == hash;
    }
}