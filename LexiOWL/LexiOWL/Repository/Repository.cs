using LexiOWL.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LexiOWL.DAL.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly LexiOwlDbContext _context;

        string dbPath = DependencyService.Get<IDbPath>().GetDatabasePath(App.DBFILENAME);

        public Repository()
        { 
            _context = new LexiOwlDbContext(dbPath);
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public Task<T> Get(Func<T, bool> predicate)
        {
            return Task.FromResult(_context.Set<T>().Where(predicate).FirstOrDefault());
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task AddAsync(T entity)
        {
            _context.Set<T>().Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }

        ~Repository() 
        {
            _context.Dispose();
        }
    }
}
