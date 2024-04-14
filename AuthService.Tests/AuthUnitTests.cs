using AuthService.Infrastructure;
using AuthService.Service.Helpers;
using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Shared.Domain;
using Shared.User;

namespace AuthService.Tests;

public class AuthUnitTests
{
    private readonly Mock<IJWTProvider> _mockJwtProvider = new Mock<IJWTProvider>();
    private readonly Mock<IPasswordHasher> _mockPasswordHasher = new Mock<IPasswordHasher>();
    private readonly Mock<IAuthRepository> _mockAuthRepository = new Mock<IAuthRepository>();
    private readonly Mock<IMapper> _mockMapper = new Mock<IMapper>();
    private readonly Service.AuthService _authService;

    public AuthUnitTests()
    {
        _authService = new Service.AuthService(_mockJwtProvider.Object, _mockPasswordHasher.Object, _mockAuthRepository.Object, _mockMapper.Object);
    }

    [Fact]
    public async Task Login_UserNotFound_ReturnsArgumentException()
    {
        // Arrange
        var loginDto = new LoginDTO { Email = "test@example.com", PlainPassword = "password123" };
        _mockAuthRepository.Setup(repo => repo.FindUser(loginDto.Email)).ReturnsAsync((AuthUser)null);

        // Act
        var exception = await Assert.ThrowsAsync<ArgumentException>(() => _authService.Login(loginDto));

        // Assert
        exception.Message.Should().Be("User not found");
    }

    [Fact]
    public async Task Login_ValidCredentials_ReturnsToken()
    {
        // Arrange
        var loginDto = new LoginDTO { Email = "test@example.com", PlainPassword = "password123" };
        var authUser = new AuthUser { Username = "test", Salt = new byte[16], HashedPassword = "hashedpassword" };
        var userDto = new UserDTO { Id = 1, Username = "test" };
        var token = "validToken";

        _mockAuthRepository.Setup(repo => repo.FindUser(loginDto.Email)).ReturnsAsync(authUser);
        _mockAuthRepository.Setup(repo => repo.GetUserId(authUser.Username, "")).ReturnsAsync(userDto);
        _mockPasswordHasher.Setup(hasher => hasher.HashPassword(loginDto.PlainPassword, authUser.Salt)).ReturnsAsync(authUser.HashedPassword);
        _mockJwtProvider.Setup(jwt => jwt.GenerateToken(userDto.Id, userDto.Username, null)).Returns(token);

        // Act
        var result = await _authService.Login(loginDto) as OkObjectResult;

        // Assert
        result.Should().NotBeNull();
        result?.Value.Should().Be(token);
    }

    [Fact]
    public async Task Register_ValidData_ReturnsTrue()
    {
        // Arrange
        var registerDto = new RegisterDTO { Email = "test@example.com", PlainPassword = "password123", Username = "test" };
        var authUser = new AuthUser { Username = "test", Salt = new byte[16] };
        var userCreateDto = new UserCreateDTO { Username = "test" };

        _mockMapper.Setup(m => m.Map<RegisterDTO, AuthUser>(registerDto)).Returns(authUser);
        _mockPasswordHasher.Setup(hasher => hasher.HashPassword(registerDto.PlainPassword, authUser.Salt)).ReturnsAsync("hashedPassword");
        _mockMapper.Setup(m => m.Map<AuthUser, UserCreateDTO>(authUser)).Returns(userCreateDto);
        _mockAuthRepository.Setup(repo => repo.Register(authUser, userCreateDto, "")).ReturnsAsync(true);

        // Act
        var result = await _authService.Register(registerDto);

        // Assert
        result.Should().BeTrue();
    }
}