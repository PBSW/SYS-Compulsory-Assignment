namespace AuthService.Tests;

public class AuthUnitTests
{
    [Fact]
    public void AuthService_WithNoRepo_ShouldThrow_NullReferenceErrorWithMessage()
    {
        // Arrange
        var authService = new AuthService.Service.AuthService(null, null, null, null);
        
        // Act
        var exception = Record.Exception(() => authService.Login(null));
        
        // Assert
        Assert.NotNull(exception);
        Assert.IsType<NullReferenceException>(exception);
        Assert.Equal("AuthRepository is null", exception.Message);
    }
}