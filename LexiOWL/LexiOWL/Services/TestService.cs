using LexiOWL.DAL.Interfaces;
using LexiOWL.Domain.Entities;
using LexiOWL.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace LexiOWL.DAL.Services
{
    public class TestService
    {
        private readonly LexiOwlDbContext _context;

        string dbPath = DependencyService.Get<IDbPath>().GetDatabasePath(App.DBFILENAME);

        public TestService()
        {
            _context = new LexiOwlDbContext(dbPath);
        }

        public async Task<List<Test>> GetAllAsync()
        {
            return await _context.Tests.ToListAsync();
        }

        public async Task<Test> GetByIdAsync(int id)
        {
            return await _context.Tests.FindAsync(id);
        }

        public async Task<Test> GetByTopicIdAsync(int id)
        {
            return await _context.Tests.FirstOrDefaultAsync(topic => topic.TopicId == id);
        }

        public async Task<Test> GetByTopicIdAndTypeAsync(int topicId, QuestionType questionType)
        {
            return await _context.Tests.FirstOrDefaultAsync(test => test.TopicId == topicId && test.QuestionType == questionType);
        }

        public async Task AddAsync(Test entity)
        {
            _context.Tests.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Test entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Test entity)
        {
            _context.Tests.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
