using MediatR;
using PaymentAPI.Data;
using PaymentAPI.DTOs;
using PaymentAPI.Model;
using PaymentAPI.Service.Interfaces;

namespace PaymentAPI.Service.TransferServices.Query
{
    public class GetTransactionsByIdQuery : IRequest<ApiResponse<TransactionDetails>>
    {
        public int Id { get; set; }


    }
    public class GetTransactionsByIdQueryHandler : IRequestHandler<GetTransactionsByIdQuery, ApiResponse<TransactionDetails>>
    {
        private readonly ITransactionDetailsRepository _transactionDetailsRepository;
        private readonly UserContext _userContext;
        public GetTransactionsByIdQueryHandler(ITransactionDetailsRepository transactionDetailsRepository, UserContext userContext)
        {
            _transactionDetailsRepository = transactionDetailsRepository;
            _userContext = userContext;
        }

        public async Task<ApiResponse<TransactionDetails>> Handle(GetTransactionsByIdQuery request, CancellationToken cancellationToken)
        {
            var trs = await _transactionDetailsRepository.GetAllAsync();
            var transaction = trs.FirstOrDefault(x => x.UserId == _userContext.UserId && x.Id==request.Id);
            return ApiResponse<TransactionDetails>.SuccessResponse(transaction);
        }
    }

}
