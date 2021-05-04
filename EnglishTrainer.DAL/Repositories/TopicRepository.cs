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
    public class TopicRepository : IRepository<Topic>
    {
        private VocabularyContext db;

        public TopicRepository(VocabularyContext context)
        {
            db = context;
        }
        public void Create(Topic item)
        {
            db.Add(item);
        }

        public void Delete(int id)
        {
            Topic topic = db.Topics.Find(id);
            if (topic != null)
                db.Topics.Remove(topic);
        }

        public IEnumerable<Topic> Find(Func<Topic, bool> predicate)
        {
            return db.Topics.Include(t => t.Words).Where(predicate).ToList();
        }

        public Topic Get(int id)
        {
            return db.Topics.Include(t => t.Words).Where(t => t.Id == id).FirstOrDefault();
        }

        public IEnumerable<Topic> GetAll()
        {
            return db.Topics.Include(t => t.Words);
        }

        public void Update(Topic item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}
