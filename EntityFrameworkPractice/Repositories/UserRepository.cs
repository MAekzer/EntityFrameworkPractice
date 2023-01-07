using EntityFrameworkPractice.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameworkPractice.Repositories
{
    internal class UserRepository
    {
        private AppContext db;

        public UserRepository(AppContext db) { this.db = db; }

        public User? GetById(int id) { return db.Users.Find(id); }

        public IQueryable<User> GetAll() { return db.Users; }

        public bool HasBook(User user, Book book)
        {
            return db.Users.Any(u => u.Equals(user) && u.Books.Contains(book));
        }

        public int BookCount(User user)
        {
            return db.Books.Where(b => b.Users.Any(u => u.Equals(user))).Count();
        }

        public int Add(User user)
        {
            db.Users.Add(user);
            return db.SaveChanges();
        }

        public int Delete(User user)
        {
            db.Users.Remove(user);
            return db.SaveChanges();
        }

        public int UpdateName(int id, string name)
        {
            var user = GetById(id);
            if (user == null)
                return 0;

            user.Name = name;
            return db.SaveChanges();
        }
    }
}
