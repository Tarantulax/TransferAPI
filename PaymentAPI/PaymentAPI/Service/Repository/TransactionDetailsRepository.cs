using PaymentAPI.Data;
using PaymentAPI.Model;
using PaymentAPI.Service.Base;
using PaymentAPI.Service.Interfaces;

namespace PaymentAPI.Service.Repository
{
    public class TransactionDetailsRepository : Repository<TransactionDetails>, ITransactionDetailsRepository
    {
        public TransactionDetailsRepository(DataContext dataContext) : base(dataContext)
        {

        }

    }
}
