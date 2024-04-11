using System.Security.Cryptography;
using AuthService.Infrastructure;
using AuthService.Service.Helpers;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Shared.Domain;
using Shared.User;

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
        _jwtProvider = jwtProvider ?? throw new NullReferenceException("JWTProvider is null");
        _passwordHasher = passwordHasher ?? throw new NullReferenceException("PasswordHasher is null");
        _authRepository = authRepository ?? throw new NullReferenceException("AuthRepository is null");
        _mapper = mapper ?? throw new NullReferenceException("Mapper is null");
    }

    public async Task<IActionResult> Login(LoginDTO dto)
    { 
        if (dto == null)
            throw new ArgumentException("LoginDTO is null");
        
        AuthUser user = await _authRepository.FindUser(dto.Email);
        
        if (user == null)
        {
            throw new ArgumentException("User not found");
        }
        
        bool isAuthenticated = await Authenticate(dto.plainPassword, user);
        
        if (!isAuthenticated)
        {
            throw new ArgumentException("Invalid password");
        }
        
        string token = _jwtProvider.GenerateToken(user.Username);
        
        return await Task.FromResult<IActionResult>(new OkObjectResult(token));
    }

    public async Task<IActionResult> Register(RegisterDTO dto)
    {
        if (dto == null)
            throw new ArgumentException("RegisterDTO is null");
        
        AuthUser authUser = _mapper.Map<RegisterDTO, AuthUser>(dto);
        
        authUser.salt = GenerateSalt();
        
        authUser.hashedPassword = await _passwordHasher.HashPassword(dto.Password, authUser.salt);
        
        await _authRepository.Register(authUser);
        
        return await Task.FromResult<IActionResult>(new OkResult());
    }

    private async Task<bool> Authenticate(string plainTextPassword, AuthUser user)
    {
        if (user == null) return false;
        
        var hashedPassword = await _passwordHasher.HashPassword(plainTextPassword, user.salt);
        
        return hashedPassword.Equals(user.hashedPassword);
    }

    private byte[] GenerateSalt()
    {
        return RandomNumberGenerator.GetBytes(keySize);
    }
}