using MediatR;
using PaymentAPI.Data;
using PaymentAPI.DTOs;
using PaymentAPI.Model;
using PaymentAPI.Service.Interfaces;

namespace PaymentAPI.Service.AccountServices.Command
{
    public class SendMoneyCommand : IRequest<ApiResponse<string>>
    {
        public TransferRequest TransferRequest { get; set; }     
    }
    public class SendMoneyCommandHandler : IRequestHandler<SendMoneyCommand, ApiResponse<string>>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly ITransactionsRepository _transactionsRepository;
        private readonly UserContext _userContext;
        public SendMoneyCommandHandler(IAccountRepository accountRepository, UserContext userContext, ITransactionsRepository transactionsRepository)
        {
            _accountRepository = accountRepository;
            _userContext = userContext;
            _transactionsRepository = transactionsRepository;
        }

        public async Task<ApiResponse<string>> Handle(SendMoneyCommand request, CancellationToken cancellationToken)
        {
            var transferRequest=request.TransferRequest;
            Accounts sender = new Accounts();
            var accounts = await _accountRepository.GetAllAsync();
            sender= accounts.FirstOrDefault(x => x.IbanNumber == transferRequest.SenderIbanNumber);
            Accounts reciever = new Accounts();
            reciever =  accounts.FirstOrDefault(x => x.IbanNumber == transferRequest.RecieverIbanNumber);

            if (transferRequest.Amount < 0)
            {
                return ApiResponse<string>.ErrorResponse("0 dan küçük tutar giremezsiniz.");
            }

            if (sender.UserId != _userContext.UserId)
            {
                return ApiResponse<string>.ErrorResponse("Yetkisiz işlem");
            }
            if (sender.Currency != reciever.Currency)
            {

                return ApiResponse<string>.ErrorResponse("Sadece aynı tür para birimleri arasında işlem yapılabilir"); ;
            }
            if (sender.Balance < transferRequest.Amount)
            {
                return ApiResponse<string>.ErrorResponse("Girdiğiniz tutar bakiyenizden fazladır.");
            }
            sender.Balance = sender.Balance - transferRequest.Amount;
            reciever.Balance = reciever.Balance + transferRequest.Amount;
            reciever.UpdatedDate = DateTime.Now;
            sender.UpdatedDate = DateTime.Now;
            Transactions transactions = new Transactions();
            transactions.Amount = transferRequest.Amount;
            transactions.SenderId = sender.Id;
            transactions.AmountCurrency = sender.Currency;
            transactions.ReceiverId = reciever.Id;
            transactions.Date = DateTime.Now;
            _transactionsRepository.Insert(transactions);

            return ApiResponse<string>.SuccessResponse("Transfer başarılı");
        }
    }
}
