﻿using Shared.Domain;
using Shared.Messages.AuthMessages;
using Shared.User;
using Shared.Monitoring;
using Shared.Util;

using UserService.Infrastructure;

namespace UserService.Service;

public class UserService : IUserService
{
    
    private readonly IUserRepository _userRepository;
    
    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    
    public async Task<User> CreateUser(UserCreateDTO user)
    {
        //Monitoring and logging
        using var activity = Monitoring.ActivitySource.StartActivity("UserService.Login");
        activity?.SetTag("user", user.Username);
        
        Monitoring.Log.Debug("UserService.Login called");
        
        
        var users = await _userRepository.All();
        
        var dbUser = users.Find(u => u.Username == user.Username);
        
        if (dbUser != null)
        {
            throw new Exception("User already exists");
        }
        
        var newUser = new User()
        {
            Username = user.Username,
            Followers = new List<User>(),
            Following = new List<User>()
        };
        
        return await _userRepository.Create(newUser);
    }

    public async Task<User> Login(LoginDTO user)
    {
        //TODO: FIX/IMPLEMENT
        var users = await _userRepository.All();
        
        var dbUser = users.Find(u => u.Email == user.Email);
        
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
        
        return await _userRepository.Update(dbUser);
    }
    
    public async Task<User> GetUser(int userId)
    {
        //Monitoring and logging
        using var activity = Monitoring.ActivitySource.StartActivity("UserService.GetUser");
        activity?.SetTag("userId", userId.ToString());
        
        Monitoring.Log.Debug("UserService.GetUser called");
        
        
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
        
        if (user.ProfilePicture != null)
        {
            dbUser.ProfilePicture = user.ProfilePicture;
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

    public async Task FollowUser(int userId, int followId)
    {
        //Monitoring and logging
        using var activity = Monitoring.ActivitySource.StartActivity("UserService.FollowUser");
        activity?.SetTag("userId", userId.ToString());
        activity?.SetTag("followId", followId.ToString());
        
        Monitoring.Log.Debug("UserService.FollowUser called");
        
        
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
        //Monitoring and logging
        using var activity = Monitoring.ActivitySource.StartActivity("UserService.UnfollowUser");
        activity?.SetTag("userId", userId.ToString());
        activity?.SetTag("unfollowId", unfollowId.ToString());
        
        Monitoring.Log.Debug("UserService.UnfollowUser called");
        
        
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
        //Monitoring and logging
        using var activity = Monitoring.ActivitySource.StartActivity("UserService.GetFollowers");
        activity?.SetTag("userId", userId.ToString());
        
        Monitoring.Log.Debug("UserService.GetFollowers called");
        
        
        //Get user from database
        var user = await _userRepository.Single(userId);
        
        //Return user's followers
        return user.Followers;
    }
}