using Microsoft.EntityFrameworkCore;

namespace ReadingSyllables.Models
{
    internal class SyllablesContext : DbContext
    {
        public string DbPath { get; }
        public DbSet<Syllable> Syllables { get; set; }

        public SyllablesContext()
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = Path.Join(path, "syllables.db");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={DbPath}");
    }
}