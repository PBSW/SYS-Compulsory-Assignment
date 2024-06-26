﻿using System.Text;
using AuthService.Infrastructure;
using AuthService.Tests.ServiceTests.Utils;
using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;
using Newtonsoft.Json;
using Shared.Domain;
using Shared.User;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Server;
using WireMock.Settings;

namespace AuthService.Tests;

public class AuthServiceTests : IAsyncLifetime
{
    private WireMockServer _userServiceMock;

    public async Task InitializeAsync()
    {
        string jsonBody = JsonConvert.SerializeObject(
            new UserCreateDTO()
            {
                Email = "test@mail.com",
                Username = "test"
            }
        );

        _userServiceMock = WireMockServer.Start(
            new WireMockServerSettings
            {
                Urls = new[] { "http://user-service:8080" },
            });

        _userServiceMock.Given(
            Request.Create()
                .WithPath("user/create")
                .WithBody(jsonBody)
                .UsingPost()
        ).RespondWith(
            Response.Create()
                .WithStatusCode(200)
                .WithBody("true")
        );
    }

    public async Task DisposeAsync()
    {
        _userServiceMock.Stop();
    }


    [Fact]
    public async Task Test_AuthService_Can_Register_User()
    {
        //InitializeAsync is called before each test
        await InitializeAsync();
        
        // Arrange
        var contextOptions = new DbContextOptionsBuilder<DatabaseContext>()
            .UseInMemoryDatabase("TestDatabase")
            .Options;

        var context = new DatabaseContext(contextOptions);
  
        var mockRepository = new AuthRepository(context);
        var authUser = new AuthUser()
        {
            Email = "test@mail.com",
            Username = "test",
            HashedPassword = "test",
            Salt = Encoding.ASCII.GetBytes("test")
        };
        var userDTO = new UserCreateDTO()
        {
            Email = "test@mail.com",
            Username = "test"
        };

        // Act
        var created = mockRepository.Register(authUser, userDTO, "").Result;

        // Assert
        Assert.True(created);
        
        // Clean up
        await DisposeAsync();
    }
}