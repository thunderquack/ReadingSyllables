﻿using Microsoft.EntityFrameworkCore;

namespace ReadingSyllables.Models
{
    internal class SyllablesContext : DbContext
    {
        public string DbPath { get; }
        public DbSet<Syllable> Syllables { get; set; }
        public DbSet<Word> Words { get; set; }
        public DbSet<UploadedFile> UploadedFiles { get; set; }
        public SyllablesContext()
        {
            var folder = Directory.GetCurrentDirectory();
            DbPath = Path.Join(folder, "syllables.db");
            Database.Migrate();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={DbPath}")
            .UseLazyLoadingProxies();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}