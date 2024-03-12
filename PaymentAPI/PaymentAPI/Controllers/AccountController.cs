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
    public class AccountController : ControllerBase
    {
        
        private readonly IMediator _mediator;
        public AccountController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost(Name = "SendMoney")]
        public async Task<ApiResponse<string>> SendMoney([FromBody] TransferRequest request)
        {
            var query = new Service.AccountServices.Command.SendMoneyCommand { TransferRequest=request};
            var response = await _mediator.Send(query);
            return response;
        }


        [HttpGet(Name = "Balance")]
        public async Task<ApiResponse<List<BalanceResponse>>> Balance()
        {
            var query = new Service.AccountServices.Query.GetBalanceQuery();
            var response = await _mediator.Send(query);
            return response;
        }
    }
}
