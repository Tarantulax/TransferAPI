using MediatR;
using PaymentAPI.Data;
using PaymentAPI.DTOs;
using PaymentAPI.Model;
using PaymentAPI.Service.Interfaces;
using PaymentAPI.Service.Repository;
using System.Collections.Generic;

namespace PaymentAPI.Service.TransferServices.Query
{
    public class GetAllTransactionsQuery : IRequest<ApiResponse<List<TransactionDetails>>>
    {


    }
    public class GetAllTransactionsQueryHandler : IRequestHandler<GetAllTransactionsQuery, ApiResponse<List<TransactionDetails>>>
    {
        private readonly ITransactionDetailsRepository _transactionDetailsRepository;
        private readonly UserContext _userContext;
        public GetAllTransactionsQueryHandler(ITransactionDetailsRepository transactionDetailsRepository, UserContext userContext)
        {
            _transactionDetailsRepository = transactionDetailsRepository;
            _userContext = userContext;
        }

        public async Task<ApiResponse<List<TransactionDetails>>> Handle(GetAllTransactionsQuery request, CancellationToken cancellationToken)
        {
            var trs = await _transactionDetailsRepository.GetAllAsync();
            trs = trs.Where(x => x.UserId == _userContext.UserId).ToList();
            return ApiResponse<List<TransactionDetails>>.SuccessResponse(trs);
        }
    }

}
