using LexiOWL.DAL.Interfaces;
using LexiOWL.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LexiOWL.DAL.Services
{
    public class ProfileServixe
    {
        private readonly LexiOwlDbContext _context;

        string dbPath = DependencyService.Get<IDbPath>().GetDatabasePath(App.DBFILENAME);

        public ProfileServixe()
        {
            _context = new LexiOwlDbContext(dbPath);
        }

        public async Task<List<Profile>> GetAllAsync()
        {
            return await _context.Profiles.ToListAsync();
        }

        public async Task<Profile> GetByIdAsync(int id)
        {
            return await _context.Profiles.FindAsync(id);
        }
        
        public async Task<Profile> GetByCustomerIdAsync(int id)
        {
            return await _context.Profiles.FirstOrDefaultAsync(profile => profile.Id == id);
        }

        public async Task AddAsync(Profile entity)
        {
            _context.Profiles.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Profile entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Profile entity)
        {
            _context.Profiles.Remove(entity);
            await _context.SaveChangesAsync();
        }


    }
}
