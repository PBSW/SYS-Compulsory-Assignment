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
        _jwtProvider = jwtProvider;
        _passwordHasher = passwordHasher;
        _authRepository = authRepository;
        _mapper = mapper;
    }

    public Task<IActionResult> Login(LoginDTO dto)
    { 
AuthUser user = _authRepository.FindUser(dto.Email);
        
        if (!Authenticate(dto.plainPassword, user))
        {
            return Task.FromResult<IActionResult>(new UnauthorizedResult());
        }
        
        string token = _jwtProvider.GenerateToken(user.Username);
        
        return Task.FromResult<IActionResult>(new OkObjectResult(token));
    }

    public Task<IActionResult> Register(RegisterDTO dto)
    {
        AuthUser authUser = _mapper.Map<RegisterDTO, AuthUser>(dto);
        
        authUser.salt = GenerateSalt();
        
        authUser.hashedPassword = _passwordHasher.HashPassword(dto.Password, authUser.salt);
        
        _authRepository.Register(authUser);

        return Task.FromResult<IActionResult>(new OkResult());
    }

    private bool Authenticate(string plainTextPassword, AuthUser user)
    {
        if (user == null) return false;
        
        var hashedPassword = _passwordHasher.HashPassword(plainTextPassword, user.salt);
        
        return hashedPassword.Equals(user.hashedPassword);
    }

    private byte[] GenerateSalt()
    {
        return RandomNumberGenerator.GetBytes(keySize);
    }
}