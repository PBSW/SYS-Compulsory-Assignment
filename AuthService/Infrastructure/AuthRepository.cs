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
    
    public async Task<bool> Register(AuthUser authUser, UserCreateDTO userDTO, string jwtToken)
    {
        //Monitoring and logging
        using var activity = Monitoring.ActivitySource.StartActivity("AuthRepository.Register");
        Monitoring.Log.Debug("AuthRepository.Register called");
        
        // Set up HTTP client
        var client = new RestClient(BaseUrl);
        var request = new RestRequest("user/create", Method.Post);
        request.AddHeader("Content-Type", "application/json");
        
        // Add JWT token to Authorization header
        request.AddHeader("Authorization", $"Bearer {jwtToken}");

        // Propagate the trace context
        PropagationHelper.Inject(request, activity);
        
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
                //var authSet = _context.Set<AuthUser>();
                _context.AuthUsers.Add(authUser);
                var changes = await _context.SaveChangesAsync();

                // Return true if at least one record was changed in the database
                return changes > 0;
            }
        }

        // Return false if the HTTP request failed or the user creation was not successful
Monitoring.Log.Error($"Failed to create user {authUser.Email} in UserService: {response.ErrorMessage}");
        throw new Exception("Unable to connect to UserService:" + response.ErrorMessage);
    }
    
    public async Task<AuthUser> FindUser(string username)
    {
        //var authSet = _context.Set<AuthUser>();
        return await _context.AuthUsers.FirstOrDefaultAsync(user => user.Email.Equals(user.Email));
    }

    public async Task<UserDTO> GetUserId(string username, string jwtToken)
    {
        // Monitoring and logging
        using var activity = Monitoring.ActivitySource.StartActivity("AuthRepository.GetUserId");
        Monitoring.Log.Debug("AuthRepository.GetUserId called");

        // Set up HTTP client
        var client = new RestClient(BaseUrl);
        var request = new RestRequest($"user/username/{username}", Method.Get);
        request.AddHeader("Content-Type", "application/json");

        // Add JWT token to Authorization header
        Monitoring.Log.Debug(jwtToken);
        request.AddHeader("Authorization", $"Bearer {jwtToken}");

        // Propagate the trace context
        PropagationHelper.Inject(request, activity);

        // Execute HTTP request and await the result
        var response = await client.ExecuteAsync<UserDTO>(request);

        // Check if the HTTP response was successful
        if (response.IsSuccessful && response.Data != null)
        {
            return response.Data; // Directly return the deserialized UserDTO object
        }

        // Log and throw an exception if the HTTP request failed or the user was not successfully fetched
        Monitoring.Log.Error($"Failed to fetch user {username} from UserService: {response.ErrorMessage}");
        throw new Exception("Unable to connect to UserService: " + response.ErrorMessage);
    }
}