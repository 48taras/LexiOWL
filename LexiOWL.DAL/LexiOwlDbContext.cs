using LexiOWL.DAL.Interfaces;
using LexiOWL.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LexiOWL.DAL
{
    public class LexiOwlDbContext : DbContext
    {
        private string _databasePath;

        public DbSet<Customer> Customers { get; set; }

        public DbSet<EducationalContent> EducationalContents { get; set; }

        public DbSet<Profile> Profiles { get; set; }

        public DbSet<Statistics> Statistics { get; set; }

        public DbSet<Test> Tests { get; set; }

        public DbSet<TestAnswer> TestAnswers { get; set; }

        public DbSet<Topic> Topics { get; set; }

        public LexiOwlDbContext(string databasePath)
        {
            _databasePath = databasePath;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Filename={_databasePath}");
        }
    }
}
