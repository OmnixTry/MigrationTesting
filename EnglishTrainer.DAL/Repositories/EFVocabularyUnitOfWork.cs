using EnglishTrainer.DAL.EF;
using EnglishTrainer.DAL.Entities;
using EnglishTrainer.DAL.Interfaces;
using System;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace EnglishTrainer.DAL.Repositories
{
    public class EFVocabularyUnitOfWork : IVocabularyUnitOfWork
    {
        private readonly VocabularyContext db;
        private WordRepository wordRepository;
        private TopicRepository topicRepository;
        private UserRepository userRepository;
        private MistakeRepository mistakeRepository;
        private TopicResultRepository topicResultRepository;

        public EFVocabularyUnitOfWork(string connectionString)
        {
            /*
            var builder = new ConfigurationBuilder();
            
            builder.SetBasePath(Directory.GetCurrentDirectory());
            
            builder.AddJsonFile("appsettings.json");
            
            var config = builder.Build();
            
            string connectionString = config.GetConnectionString(connectionStringName);
            */
            var optionsBuilder = new DbContextOptionsBuilder<VocabularyContext>();
            var options = optionsBuilder
                .UseSqlServer(connectionString)
                .Options;

            db = new VocabularyContext(options);
        }
        public IRepository<Word> Words
        {
            get
            {
                if (wordRepository == null)
                    wordRepository = new WordRepository(db);
                return wordRepository;
            }
        }

        public IRepository<Topic> Topics
        {
            get
            {
                if (topicRepository == null)
                    topicRepository = new TopicRepository(db);
                return topicRepository;
            }
        }

        public IRepository<User> Users
        {
            get
            {
                if (userRepository == null)
                    userRepository = new UserRepository(db);
                return userRepository;
            }
        }

        public IRepository<Mistake> Mistakes
        {
            get
            {
                if (mistakeRepository == null)
                    mistakeRepository = new MistakeRepository(db);
                return mistakeRepository;
            }
        }

        public IRepository<TopicResult> TopicResults 
        {
            get
            {
                if (topicResultRepository == null)
                    topicResultRepository= new TopicResultRepository(db);
                return topicResultRepository;
            }
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Save()
        {
            db.SaveChanges();
        }
    }
}

