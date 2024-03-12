using PaymentAPI.Data;
using PaymentAPI.Model;
using PaymentAPI.Service.Base;
using PaymentAPI.Service.Interfaces;

namespace PaymentAPI.Service.Repository
{
    public class AccountRepository : Repository<Accounts>, IAccountRepository
    {
        public AccountRepository(DataContext dataContext) : base(dataContext)
        {

        }

    }
}