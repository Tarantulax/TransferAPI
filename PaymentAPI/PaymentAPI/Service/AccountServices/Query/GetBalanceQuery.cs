using MediatR;
using PaymentAPI.Data;
using PaymentAPI.DTOs;
using PaymentAPI.Model;
using PaymentAPI.Service.Interfaces;

namespace PaymentAPI.Service.AccountServices.Query
{
    public class GetBalanceQuery : IRequest<ApiResponse<List<BalanceResponse>>>
    {


    }
    public class GetBalanceQueryHandler : IRequestHandler<GetBalanceQuery, ApiResponse<List<BalanceResponse>>>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly UserContext _userContext;
        public GetBalanceQueryHandler(IAccountRepository accountRepository, UserContext userContext)
        {
            _accountRepository = accountRepository;
            _userContext = userContext;
        }

        public async Task<ApiResponse<List<BalanceResponse>>> Handle(GetBalanceQuery request, CancellationToken cancellationToken)
        {
            
            List<BalanceResponse> response = new List<BalanceResponse>();
            var accounts = await _accountRepository.GetAllAsync();
            accounts = accounts.Where(x => x.UserId == _userContext.UserId).ToList();

            foreach (var account in accounts)
            {
                BalanceResponse item = new BalanceResponse();
                item.Currency = account.Currency;
                item.Balance = account.Balance;
                response.Add(item);
            }
            return ApiResponse<List<BalanceResponse>>.SuccessResponse(response);
        }
    }
}
