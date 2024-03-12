using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PaymentAPI.DTOs;
using PaymentAPI.Model;
using PaymentAPI.Service;

namespace PaymentAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class TransactionController : ControllerBase
    {
        private readonly IMediator _mediator;
        public TransactionController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet(Name = "TransactionsById")]
        public async Task<ApiResponse<TransactionDetails>> TransactionsById(int id)
        {
            var query = new Service.TransferServices.Query.GetTransactionsByIdQuery{ Id=id};
            var response = await _mediator.Send(query);
            return response;
        }

        [HttpGet(Name = "Transactions")]
        public async Task<ApiResponse<List<TransactionDetails>>> Transactions()
        {
            var query = new Service.TransferServices.Query.GetAllTransactionsQuery();
            var response = await _mediator.Send(query);
            return response;
        }
    }
}
