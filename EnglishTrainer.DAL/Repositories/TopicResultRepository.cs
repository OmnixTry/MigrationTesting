using EnglishTrainer.DAL.EF;
using EnglishTrainer.DAL.Entities;
using EnglishTrainer.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EnglishTrainer.DAL.Repositories
{
    public class TopicResultRepository : IRepository<TopicResult>
    {
        private VocabularyContext db;

    public TopicResultRepository(VocabularyContext context)
    {
        db = context;
    }
    public void Create(TopicResult item)
    {
        db.Add(item);
    }

    public void Delete(int id)
    {
        TopicResult mistake = db.TopicResults.Find(id);
        if (mistake != null)
            db.TopicResults.Remove(mistake);
    }

    public IEnumerable<TopicResult> Find(Func<TopicResult, bool> predicate)
    {
        return db.TopicResults.Include(t => t.MistakenWords).Include(t => t.Topic).Where(predicate).ToList();
    }

    public TopicResult Get(int id)
    {
        return db.TopicResults.Find(id);
    }

    public IEnumerable<TopicResult> GetAll()
    {
        return db.TopicResults.Include(t => t.MistakenWords).Include(t => t.Topic);
    }

    public void Update(TopicResult item)
    {
        db.Entry(item).State = (Microsoft.EntityFrameworkCore.EntityState)EntityState.Modified;
    }
}
}
