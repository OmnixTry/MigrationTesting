using System;
using System.Collections.Generic;
using EnglishTrainer.DAL.EF;
using EnglishTrainer.DAL.Entities;
using EnglishTrainer.DAL.Interfaces;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace EnglishTrainer.DAL.Repositories
{
    class MistakeRepository : IRepository<Mistake>
    {
        private readonly VocabularyContext db;

        public MistakeRepository(VocabularyContext context)
        {
            db = context;
        }
        public void Create(Mistake item)
        {
            db.Add(item);
        }

        public void Delete(int id)
        {
            Mistake mistake = db.Mistakes.Find(id);
            if (mistake != null)
                db.Mistakes.Remove(mistake);
        }

        public IEnumerable<Mistake> Find(Func<Mistake, bool> predicate)
        {
            return db.Mistakes.Include(t => t.User).Include(t => t.Word).Where(predicate).ToList();
        }

        public Mistake Get(int id)
        {
            return db.Mistakes.Find(id);
        }

        public IEnumerable<Mistake> GetAll()
        {
            return db.Mistakes.Include(t => t.User).Include(t => t.Word);
        }

        public void Update(Mistake item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}
