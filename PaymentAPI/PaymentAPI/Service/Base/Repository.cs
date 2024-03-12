using Microsoft.EntityFrameworkCore;
using PaymentAPI.Data;
using PaymentAPI.DTOs;
using System.Runtime.CompilerServices;

namespace PaymentAPI.Service.Base
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly DataContext _datacontext;
        public Repository(DataContext dataContext)
        {
            _datacontext = dataContext;
        }
        public void Insert(T model)
        {
            _datacontext.Set<T>().Add(model);
            _datacontext.SaveChanges();
        }
        public async Task<List<T>> GetAllAsync()
        {
            List<T> response = new List<T>();

            response = await _datacontext.Set<T>().ToListAsync();

            return response;
        }
        public async Task<T> GetByIdAsync(int id)
        {
            return await _datacontext.Set<T>().FindAsync(id);
        }
    }
}
