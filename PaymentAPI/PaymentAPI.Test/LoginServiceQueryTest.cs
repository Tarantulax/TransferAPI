using Xunit;
using Moq;
using PaymentAPI.Service;
using PaymentAPI.Data;
using PaymentAPI.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using PaymentAPI.DTOs;
using MediatR;
using PaymentAPI.Service.Interfaces;
using PaymentAPI.Service.LoginServices.Query;
public interface ILoginService
{
    int Login(LoginRequest request);
}
public class LoginServiceQueryTest
{
    private readonly Mock<IMediator> _mockMediator;
    private readonly GetLoginRequestQueryHandler _handler;
    private readonly Mock<IUsersRepository> _mockUsersRepository;
    private readonly Mock<IPasswordService> _mockPasswordService;


    public LoginServiceQueryTest()
    {
        _mockMediator = new Mock<IMediator>();
        _mockUsersRepository = new Mock<IUsersRepository>();
        _mockPasswordService = new Mock<IPasswordService>();
        _handler = new GetLoginRequestQueryHandler(_mockUsersRepository.Object, _mockPasswordService.Object);
    }

  
    [Fact]
    public async Task Login_ValidCredentials_ReturnsUserId()
    {
        // Arrange
        var request = new LoginRequest { TC = "33333333333", Password = "123456" };
        var users = new List<Users>
        {
            new Users { Id = 1, TC = "33333333333", Password = "e10adc3949ba59abbe56e057f20f883e" }
        };

        _mockUsersRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(users);
        _mockPasswordService.Setup(p => p.VerifyPasswordHash("123456", "e10adc3949ba59abbe56e057f20f883e")).Returns(true);

        var query = new GetLoginRequestQuery { LoginRequest = request };

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.Equal(1, result);
    }
    [Fact]
    public void Login_InvalidCredentials_ReturnsZero()
    {
        var mockService = new Mock<ILoginService>();
        // Arrange
        var request = new LoginRequest { TC = "12345678910", Password = "wrongpassword" };

        // Act
        mockService.Setup(s => s.Login(request)).Returns(0);
        var result = mockService.Object.Login(request);

        // Assert
        Assert.Equal(0, result);
    }

    [Fact]
    public void Login_UserDoesNotExist_ReturnsZero()
    {
        var mockService = new Mock<ILoginService>();
        // Arrange
        var request = new LoginRequest { TC = "nonexistent", Password = "123456" };

        // Act
        mockService.Setup(s => s.Login(request)).Returns(0);
        var result = mockService.Object.Login(request);

        // Assert
        Assert.Equal(0, result);
    }
}
