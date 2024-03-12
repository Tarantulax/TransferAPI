using Xunit;
using MediatR;
using Moq;
using PaymentAPI.Data;
using PaymentAPI.Model;
using PaymentAPI.Service.AccountServices.Query;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using PaymentAPI.DTOs;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Principal;

public class AccountServiceQueryTests
{
    private readonly Mock<IMediator> _mediatorMock;

    public AccountServiceQueryTests()
    {

        var services = new ServiceCollection();
        services.AddMediatR(typeof(GetBalanceQueryHandler).Assembly);


        // Create a Mock for IMediator
        _mediatorMock = new Mock<IMediator>();
    }

    [Fact]
    public async Task Balance_ForExistingUser_ReturnsCorrectData()
    {
        // Arrange


        var expectedResponse = ApiResponse<List<BalanceResponse>>.SuccessResponse(new List<BalanceResponse>
{
    new BalanceResponse { Currency = "USD", Balance = 1000 },
    new BalanceResponse { Currency = "EUR", Balance = 500 }
});


        _mediatorMock.Setup(m => m.Send(It.IsAny<GetBalanceQuery>(), default)).ReturnsAsync(expectedResponse);

        // Act
        var query = new GetBalanceQuery();
        var result = await _mediatorMock.Object.Send(query);

        // Assert
        Assert.NotNull(result.Data);
        Assert.Equal(2, result.Data.Count);
        Assert.Contains(result.Data, a => a.Currency == "USD" && a.Balance == 1000);
        Assert.Contains(result.Data, a => a.Currency == "EUR" && a.Balance == 500);
    }
}
