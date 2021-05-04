using EnglishTrainer.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EnglishTrainer.DAL.Interfaces
{
    public interface IVocabularyUnitOfWork : IDisposable
    {
        IRepository<Word> Words { get; }
        IRepository<Topic> Topics { get; }
        IRepository<User> Users { get; }
        IRepository<Mistake> Mistakes { get; }
        IRepository<TopicResult> TopicResults { get; }
        void Save();
    }
}
