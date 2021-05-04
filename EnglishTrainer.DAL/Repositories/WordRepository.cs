using EnglishTrainer.DAL.EF;
using EnglishTrainer.DAL.Entities;
using EnglishTrainer.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace EnglishTrainer.DAL.Repositories
{
    public class WordRepository : IRepository<Word>
    {
        private VocabularyContext db;

        public WordRepository(VocabularyContext context)
        {
            db = context;
        }
        public void Create(Word item)
        {
            db.Add(item);
        }

        public void Delete(int id)
        {
            Word book = db.Words.Find(id);
            if (book != null)
                db.Words.Remove(book);
        }

        public IEnumerable<Word> Find(Func<Word, bool> predicate)
        {
            return db.Words.Include(w => w.Topic).Include(w => w.Mistakes).Where(predicate).ToList();
        }

        public Word Get(int id)
        {
            return db.Words.Include(w => w.Topic).Where(w => w.Id == id).FirstOrDefault();
        }

        public IEnumerable<Word> GetAll()
        {
            return db.Words.Include(w => w.Topic).Include(w => w.Mistakes);
        }

        public void Update(Word item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}
