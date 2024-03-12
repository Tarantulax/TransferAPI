using PaymentAPI.Data;
using PaymentAPI.Model;
using PaymentAPI.Service.Base;
using PaymentAPI.Service.Interfaces;

namespace PaymentAPI.Service.Repository
{
    public class UsersRepository : Repository<Users>, IUsersRepository
    {
        public UsersRepository(DataContext dataContext) : base(dataContext)
        {

        }

    }
}
