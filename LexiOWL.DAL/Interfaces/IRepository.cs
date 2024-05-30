using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LexiOWL.DAL.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<List<T>> GetAllAsync();

        Task<List<T>> Get(Func<T, bool> predicate);

        Task AddAsync(T entity);

        Task UpdateAsync(T entity);

        Task DeleteAsync(T entity);
    }
}
