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
    public class EducationalContentService
    {
        private readonly LexiOwlDbContext _context;

        string dbPath = DependencyService.Get<IDbPath>().GetDatabasePath(App.DBFILENAME);

        public EducationalContentService()
        {
            _context = new LexiOwlDbContext(dbPath);
        }

        public async Task<List<EducationalContent>> GetAllAsync()
        {
            return await _context.EducationalContents.ToListAsync();
        }

        public async Task<EducationalContent> GetByIdAsync(int id)
        {
            return await _context.EducationalContents.FindAsync(id);
        }

        public async Task<EducationalContent> GetByTopicIdAsync(int id)
        {
            return await _context.EducationalContents.FirstOrDefaultAsync(topic => topic.TopicId == id);
        }

        public async Task AddAsync(EducationalContent entity)
        {
            _context.EducationalContents.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(EducationalContent entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(EducationalContent entity)
        {
            _context.EducationalContents.Remove(entity);
            await _context.SaveChangesAsync();
        }


    }
}
