using EnglishTrainer.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EnglishTrainer.DAL.EF
{
    public class VocabularyContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Word> Words { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<Mistake> Mistakes { get; set; }
        public DbSet<TopicResult> TopicResults { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasAlternateKey(u => u.NickName);
            var cascadeFKs = modelBuilder.Model.GetEntityTypes()
                .Select(t => t.GetForeignKeys());


            foreach (var fk in cascadeFKs)
                foreach (var item in fk)
                    item.DeleteBehavior = DeleteBehavior.Restrict;

            base.OnModelCreating(modelBuilder);

        }

        public VocabularyContext(DbContextOptions<VocabularyContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public VocabularyContext()
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(local);Initial Catalog=Vocabulary4; Integrated Security= SSPI");
        }
    }
}
