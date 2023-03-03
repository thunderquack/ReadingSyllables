using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

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
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={DbPath}");
    }
}