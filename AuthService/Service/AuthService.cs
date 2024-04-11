
﻿using System.Security.Cryptography;
using AuthService.Infrastructure;
using AuthService.Service.Helpers;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Shared.Domain;
using Shared.User;
﻿using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Shared.Monitoring;
using Shared.Util;


namespace AuthService.Service;

public class AuthService : IAuthService
{
    private const int keySize = 128 / 8;
    
    private readonly IJWTProvider _jwtProvider;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IAuthRepository _authRepository;
    private readonly IMapper _mapper;
    
    
    public AuthService(
        IJWTProvider jwtProvider,
        IPasswordHasher passwordHasher,
        IAuthRepository authRepository,
        IMapper mapper)
    {
        _jwtProvider = jwtProvider;
        _passwordHasher = passwordHasher;
        _authRepository = authRepository;
        _mapper = mapper;
    }

    public async Task<IActionResult> Login(LoginDTO dto)
    { 

        //Monitoring and logging
        using var activity = Monitoring.ActivitySource.StartActivity("AuthService.Service.ValidateToken");
        activity?.SetTag("token", token);
        
        Monitoring.Log.Debug("AuthService.ValidateToken called");

        AuthUser user = await _authRepository.FindUser(dto.Email);
        
        bool isAuthenticated = await Authenticate(dto.plainPassword, user);
        
        if (!isAuthenticated)
        {
            return await Task.FromResult<IActionResult>(new UnauthorizedResult());
        }
        
        string token = _jwtProvider.GenerateToken(user.Username);
        
        return await Task.FromResult<IActionResult>(new OkObjectResult(token));
    }

    public async Task<IActionResult> Register(RegisterDTO dto)
    {
        //Monitoring and logging
        using var activity = Monitoring.ActivitySource.StartActivity("AuthService.Service.GenerateToken");
        activity?.SetTag("userId", userId.ToString());

        Monitoring.Log.Debug("AuthService.GenerateToken called");
        
        
        AuthUser authUser = _mapper.Map<RegisterDTO, AuthUser>(dto);
        
        authUser.salt = GenerateSalt();
        
        authUser.hashedPassword = await _passwordHasher.HashPassword(dto.Password, authUser.salt);
        
        await _authRepository.Register(authUser);

        return await Task.FromResult<IActionResult>(new OkResult());
    }

    private async Task<bool> Authenticate(string plainTextPassword, AuthUser user)
    {
        //Monitoring and logging
        using var activity = Monitoring.ActivitySource.StartActivity("AuthService.Service.Authenticate");
        activity?.SetTag("token", token);
        
        Monitoring.Log.Debug("AuthService.Authenticate called");
        
        if (user == null) return false;
        
        var hashedPassword = await _passwordHasher.HashPassword(plainTextPassword, user.salt);
        
        return hashedPassword.Equals(user.hashedPassword);
    }

    private byte[] GenerateSalt()
    {
        //Monitoring and logging
        using var activity = Monitoring.ActivitySource.StartActivity("AuthService.Service.HashPassword");
        activity?.SetTag("password", password);
        
        Monitoring.Log.Debug("AuthService.HashPassword called");
        
        return RandomNumberGenerator.GetBytes(keySize);
    }
}