using LinguisExternusDictionary.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace LinguisExternusDictionary
{
    public class Database : DbContext
    {
        private static Database? instance;
        
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Favorite> Favorites { get; set; } = null!;
        public DbSet<Word> Words { get; set; } = null!;

        public static Database getInstance()
        {
            if (instance == null)
            {
                instance = new Database();
                instance.Database.EnsureCreated();

                instance.Users.Load();
                instance.Words.Load();
                instance.Favorites.Load();

                if (instance.Words.Count() == 0)
                {
                    instance.Words.AddRange(DefaulWords);
                    instance.SaveChanges();
                }
            }
            return instance;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=.;Database=LinguisExternus;Trusted_Connection=True; TrustServerCertificate=True");
        }

        static List<Word> DefaulWords = new()
        {
            new Word() { Russian = "яблоко", Latin = "malum"},
            new Word() { Russian = "картошка", Latin = "patata" },
            new Word() { Russian = "лес", Latin = "silva" },
            new Word() { Russian = "дом", Latin = "domus" },
            new Word() { Russian = "солнце", Latin = "sol" },
            new Word() { Russian = "ночь", Latin = "noctem" },
            new Word() { Russian = "рука", Latin = "manus" },
            new Word() { Russian = "глаз", Latin = "oculus" },
            new Word() { Russian = "рукав", Latin = "brachium" },
            new Word() { Russian = "колесо", Latin = "rota" },
            new Word() { Russian = "гора", Latin = "mons" }
        };
    }
}