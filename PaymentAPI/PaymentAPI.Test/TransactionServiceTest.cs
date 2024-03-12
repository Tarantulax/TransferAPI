using Xunit;
using PaymentAPI.Service;
using PaymentAPI.Data;
using PaymentAPI.Model;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using PaymentAPI.DTOs;
using System.Collections.Generic;
using System.Linq;
using Moq;



public interface ITransactionService
{
    Task<ApiResponse<List<TransactionDetails>>> GetAllTransaction();
    Task<ApiResponse<TransactionDetails>> TransactionsById(int id);

}


public class TransactionServiceTests
{
    [Fact]
    public async Task GetAllTransaction_ReturnsTransactions()
    {
        var mockService = new Mock<ITransactionService>();
        var testData = new List<TransactionDetails>
{
    new TransactionDetails { UserId = 1, Amount = 1000.00m, AmountCurrency = "USD", Date = new DateTime(2023, 3, 10), Type = "Gelen" },
    new TransactionDetails { UserId = 1, Amount = 200.00m, AmountCurrency = "EUR", Date = new DateTime(2023, 3, 11), Type = "Gelen" },
    new TransactionDetails { UserId = 2, Amount = 500.00m, AmountCurrency = "USD", Date = new DateTime(2023, 3, 12), Type = "Gonderilen" },
    new TransactionDetails { UserId = 2, Amount = 750.00m, AmountCurrency = "EUR", Date = new DateTime(2023, 3, 13), Type = "Gonderilen" },
};



        mockService.Setup(s => s.GetAllTransaction()).ReturnsAsync(new ApiResponse<List<TransactionDetails>>(true, null, testData));

        var result = await mockService.Object.GetAllTransaction();

        Assert.NotNull(result);
        Assert.True(result.Success); 
        Assert.Equal(testData.Count, result.Data.Count);
    }
    [Fact]
    public async Task GetByIdTransaction_ReturnsTransactions()
    {
        var mockService = new Mock<ITransactionService>();
        var testData = new TransactionDetails() { UserId = 2, Amount = 500.00m, AmountCurrency = "USD", Date = new DateTime(2023, 3, 12), Type = "Gonderilen" };

        mockService.Setup(s => s.TransactionsById(2)).ReturnsAsync(new ApiResponse<TransactionDetails>(true, null, testData));

        var result = await mockService.Object.TransactionsById(2);

        Assert.NotNull(result);
        Assert.True(result.Success); 
        Assert.Equal(500.00m,result.Data.Amount); 
    }
}



