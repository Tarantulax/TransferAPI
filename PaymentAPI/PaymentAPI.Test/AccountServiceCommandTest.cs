using Xunit;
using PaymentAPI.Service;
using PaymentAPI.Data;
using PaymentAPI.Model;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using PaymentAPI.DTOs;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using PaymentAPI.Service.AccountServices.Command;
using Moq;
using PaymentAPI.Service.AccountServices.Query;
using PaymentAPI.Service.Interfaces;

public class AccountServiceCommandTests
{
    private readonly Mock<IMediator> _mediatorMock;
    private readonly Mock<IAccountRepository> _accountRepositoryMock;
    private readonly Mock<ITransactionsRepository> _transactionsRepositoryMock;
    private readonly UserContext _userContext;

    public AccountServiceCommandTests()
    {
        var services = new ServiceCollection();
        services.AddMediatR(typeof(GetBalanceQueryHandler).Assembly);


        _mediatorMock = new Mock<IMediator>();
        _userContext = new UserContext { UserId = 3 }; 
        _accountRepositoryMock = new Mock<IAccountRepository>();
        _transactionsRepositoryMock = new Mock<ITransactionsRepository>();
    }

    [Fact]
    public async Task SendMoney_WithValidData_ReturnsSuccess()
    {
        // Arrange
        var senderAccount = new Accounts
        {
            Id = 1,
            UserId = _userContext.UserId,
            IbanNumber = "IBAN123",
            Balance = 1000,
            Currency = "USD"
        };
        var receiverAccount = new Accounts
        {
            Id = 2,
            UserId = 5,
            IbanNumber = "IBAN456",
            Balance = 500,
            Currency = "USD"
        };

        _accountRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(new List<Accounts> { senderAccount, receiverAccount });

        var handler = new SendMoneyCommandHandler(_accountRepositoryMock.Object, _userContext, _transactionsRepositoryMock.Object);

        var command = new SendMoneyCommand
        {
            TransferRequest = new TransferRequest
            {
                SenderIbanNumber = "IBAN123",
                RecieverIbanNumber = "IBAN456",
                Amount = 100 
            }
        };

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.Success);
    }


    [Fact]
    public async Task SendMoney_WithInsufficientBalance_ReturnsError()
    {
        // Arrange
        var senderAccount = new Accounts
        {
            Id = 1,
            UserId = _userContext.UserId,
            IbanNumber = "IBAN123",
            Balance = 50, 
            Currency = "USD"
        };
        var receiverAccount = new Accounts
        {
            Id = 2,
            UserId = 5,
            IbanNumber = "IBAN456",
            Balance = 500,
            Currency = "USD"
        };

        _accountRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(new List<Accounts> { senderAccount, receiverAccount });

        var handler = new SendMoneyCommandHandler(_accountRepositoryMock.Object, _userContext, _transactionsRepositoryMock.Object);

        var command = new SendMoneyCommand
        {
            TransferRequest = new TransferRequest
            {
                SenderIbanNumber = "IBAN123",
                RecieverIbanNumber = "IBAN456",
                Amount = 100 
            }
        };

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result.Success);
        Assert.Equal("Girdiğiniz tutar bakiyenizden fazladır.", result.Message);
    }



}
