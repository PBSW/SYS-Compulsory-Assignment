﻿using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RestSharp;
using Shared.Domain;
using Shared.Monitoring;
using Shared.User;

namespace AuthService.Infrastructure;

public class AuthRepository : IAuthRepository
{
    private readonly DatabaseContext _context;
    private string BaseUrl = "http://user-service:8080";
    
    public AuthRepository(DatabaseContext context)
    {
        _context = context;
        _context.Database.EnsureCreated();
    }
    
    public async Task<bool> Register(AuthUser authUser, UserCreateDTO userDTO)
    {
        //Monitoring and logging
        using var activity = Monitoring.ActivitySource.StartActivity("AuthRepository.Register");
        Monitoring.Log.Debug("AuthRepository.Register called");
        
        // Set up HTTP client
        var client = new RestClient(BaseUrl);
        var request = new RestRequest("user/create", Method.Post);
        request.AddHeader("Content-Type", "application/json");

        // Serialize the DTO to JSON using Newtonsoft.Json and add to request body
        string jsonBody = JsonConvert.SerializeObject(userDTO);
        request.AddParameter("application/json", jsonBody, ParameterType.RequestBody);

        // Execute HTTP request and await the result
        var response = await client.ExecuteAsync(request);

        // Check if the HTTP response was successful
        if (response.IsSuccessStatusCode && response.Content != null)
        {
            bool isSuccess = JsonConvert.DeserializeObject<bool>(response.Content);

            if (isSuccess)
            {
                // Add user to the auth database only if the remote service returned true
                _context.AuthUsers.Add(authUser);
                var changes = await _context.SaveChangesAsync();

                // Return true if at least one record was changed in the database
                return changes > 0;
            }
        }

        // Return false if the HTTP request failed or the user creation was not successful
        throw new Exception("Unable to connect to UserService.");
    }
    
    public async Task<AuthUser> FindUser(string username)
    {
        return await _context.AuthUsers.FirstOrDefaultAsync(user => user.email.Equals(user.email));
    }

    public async Task<User> GetUserId(string username)
    {
        //Monitoring and logging
        using var activity = Monitoring.ActivitySource.StartActivity("AuthRepository.GetUserId");
        Monitoring.Log.Debug("AuthRepository.GetUserId called");
        
        // Set up HTTP client
        var client = new RestClient(BaseUrl);
        var request = new RestRequest("user/create", Method.Post);
        request.AddHeader("Content-Type", "application/json");
        
        // Execute HTTP request and await the result
        var response = await client.ExecuteAsync(request);

        // Check if the HTTP response was successful
        if (response.IsSuccessStatusCode && response.Content != null)
        {
            return JsonConvert.DeserializeObject<User>(response.Content);
        }

        // Return false if the HTTP request failed or the user creation was not successful
        throw new Exception("Unable to connect to UserService.");
    }
}