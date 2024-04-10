using AuthService.Infrastructure;
using AuthService.Service.Helpers;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Shared.Domain;
using Shared.User;
using Shared.Util;

namespace AuthService.Service;

public class AuthService : IAuthService
{
    private readonly IJWTProvider _jwtProvider;
    private readonly IPasswordHasher _passwordHasher;
    private readonly ILoginRepository _loginRepository;
    private readonly IMapper _mapper;
    
    public AuthService(
        IJWTProvider jwtProvider,
        IPasswordHasher passwordHasher,
        ILoginRepository loginRepository,
        IMapper mapper)
    {
        _jwtProvider = jwtProvider;
        _passwordHasher = passwordHasher;
        _loginRepository = loginRepository;
        _mapper = mapper;
    }

    public Task<IActionResult> Login(LoginDTO dto)
    { 
        throw new NotImplementedException();
    }

    public Task<IActionResult> Register(RegisterDTO dto)
    {
        throw new NotImplementedException();
    }
}