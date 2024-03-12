using PaymentAPI.Data;
using PaymentAPI.Model;
using PaymentAPI.Service.Base;
using PaymentAPI.Service.Interfaces;

namespace PaymentAPI.Service.Repository
{
    public class TransactionsRepository : Repository<Transactions>, ITransactionsRepository
    {
        public TransactionsRepository(DataContext dataContext) : base(dataContext)
        {

        }

    }
}
