using PaymentAPI.DTOs;

namespace PaymentAPI.Service.Base
{
    public interface IRepository<T> where T : class
    {
        Task<List<T>> GetAllAsync();
        void Insert(T entity);
        Task<T> GetByIdAsync(int id);
    }
}
