using Microsoft.EntityFrameworkCore;

namespace ReadingSyllables.Models
{
    internal class SyllablesContext : DbContext
    {
        public string DbPath { get; }
        public DbSet<Syllable> Syllables { get; set; }

        public SyllablesContext()
        {
            var folder = Directory.GetCurrentDirectory();
            DbPath = Path.Join(folder, "syllables.db");
            Database.Migrate();
            ImportSyllables();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={DbPath}");

        private void ImportSyllables()
        {

        }
    }
}