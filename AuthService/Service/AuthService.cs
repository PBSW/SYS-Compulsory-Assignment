
using System.Security.Cryptography;
using AuthService.Infrastructure;
using AuthService.Service.Helpers;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Shared.Domain;
using Shared.User;
using Shared.Monitoring;



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
        _jwtProvider = jwtProvider ?? throw new NullReferenceException("jwtProvider is null");
        _passwordHasher = passwordHasher ?? throw new NullReferenceException("passwordHasher is null");
        _authRepository = authRepository ?? throw new NullReferenceException("authRepository is null");
        _mapper = mapper ?? throw new NullReferenceException("mapper is null");
    }

    public async Task<IActionResult> Login(LoginDTO dto)
    { 
        //Monitoring and logging
        using var activity = Monitoring.ActivitySource.StartActivity("AuthService.Service.ValidateToken");
        Monitoring.Log.Debug("AuthService.ValidateToken called");

        AuthUser authUser = await _authRepository.FindUser(dto.Email);
        
        if (authUser == null)
        {
            throw new ArgumentException("User not found");
        }
        
        bool isAuthenticated = await Authenticate(dto.plainPassword, authUser);
        
        if (!isAuthenticated)
        {
            return await Task.FromResult<IActionResult>(new UnauthorizedResult());
        }
        
        User user = await _authRepository.GetUserId(authUser.Username);
        
        if (user == null)
        {
            throw new NullReferenceException("User not found in user service");
        }
        
        string token = _jwtProvider.GenerateToken(user.Id, user.Username);
        
        return await Task.FromResult<IActionResult>(new OkObjectResult(token));
    }

    public async Task<IActionResult> Register(RegisterDTO dto)
    {
        //Monitoring and logging
        using var activity = Monitoring.ActivitySource.StartActivity("AuthService.Service.GenerateToken");
        Monitoring.Log.Debug("AuthService.GenerateToken called");

        if (dto == null)
        {
            throw new ArgumentNullException("Dto is null");
        }
        
        AuthUser authUser = _mapper.Map<RegisterDTO, AuthUser>(dto);
        
        authUser.salt = GenerateSalt();
        authUser.hashedPassword = await _passwordHasher.HashPassword(dto.Password, authUser.salt);
        
        var change = await _authRepository.Register(authUser);

        if (change == 0)
        {
            return await Task.FromResult<IActionResult>(new BadRequestResult());
        }
        
        return await Task.FromResult<IActionResult>(new OkResult());
    }

    private async Task<bool> Authenticate(string plainTextPassword, AuthUser user)
    {
        //Monitoring and logging
        using var activity = Monitoring.ActivitySource.StartActivity("AuthService.Service.Authenticate");
        Monitoring.Log.Debug("AuthService.Authenticate called");
        
        if (user == null) return false;
        
        var hashedPassword = await _passwordHasher.HashPassword(plainTextPassword, user.salt);
        
        return hashedPassword.Equals(user.hashedPassword);
    }

    private byte[] GenerateSalt()
    {
        //Monitoring and logging
        using var activity = Monitoring.ActivitySource.StartActivity("AuthService.Service.HashPassword");
        Monitoring.Log.Debug("AuthService.HashPassword called");
        
        return RandomNumberGenerator.GetBytes(keySize);
    }
}