using EnglishTrainer.DAL.EF;
using EnglishTrainer.DAL.Entities;
using EnglishTrainer.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace EnglishTrainer.DAL.Repositories
{
    public class UserRepository : IRepository<User>
    {
        private VocabularyContext db;

        public UserRepository(VocabularyContext context)
        {
            db = context;
        }
        public void Create(User item)
        {
            db.Add(item);
        }

        public void Delete(int id)
        {
            User user = db.Users.Find(id);
            if (user != null)
                db.Users.Remove(user);
        }

        public IEnumerable<User> Find(Func<User, bool> predicate)
        {
            return db.Users.Include(u => u.Mistakes).Include(u => u.Topics).Where(predicate).ToList();
        }

        public User Get(int id)
        {
            return db.Users.Find(id);
        }

        public IEnumerable<User> GetAll()
        {
            return db.Users.Include(u => u.Mistakes).Include(u => u.Topics);
        }

        public void Update(User item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}
