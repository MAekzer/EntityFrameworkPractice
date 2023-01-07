using EntityFrameworkPractice.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameworkPractice.Repositories
{
    internal class BookRepository
    {
        private AppContext db;

        public BookRepository(AppContext DB) { db = DB; }

        public Book? GetById(int id) { return db.Books.Find(id); }

        public IQueryable<Book> GetAllByGenreAndYear(string targetgenre, int minyear, int maxyear) 
        {
            return from book in db.Books
                   where book.Genres.Any(g => g.Name == targetgenre)
                   && book.ReleaseDate.Year >= minyear && book.ReleaseDate.Year <= maxyear
                   select book;
        }

        public IQueryable<Book> GetAll() { return db.Books; }

        public int CountByAuthor(string author) 
        {
            return (from book in db.Books
                   where book.Authors.Any(a => a.Name == author)
                   select book).Count();
        }

        public int CountByGenre(string genre)
        {
            return (from book in db.Books
                    where book.Genres.Any(g => g.Name == genre)
                    select book).Count();
        }

        public bool FindBookByNameAndAuthor(string name, string author)
        {
            return db.Books
                .Any(b => b.Title == name && b.Authors
                .Any(a => a.Name == author));
        }

        public Book? GetLatestBook() 
        {
            var date = db.Books.Max(b => b.ReleaseDate);
            return db.Books.First(b => b.ReleaseDate == date);
        }

        public IQueryable<Book> GetAllSortedByTitle() { return db.Books.OrderBy(b => b.Title); }

        public IQueryable<Book> GetAllSortedByDate() { return db.Books.OrderByDescending(b => b.ReleaseDate); }

        public int Add(Book book)
        {
            db.Books.Add(book);
            return db.SaveChanges();
        }

        public int Delete(Book book)
        {
            db.Books.Remove(book);
            return db.SaveChanges();
        }

        public int UpdateReleaseDate(int id, DateTime date) 
        {
            var book = GetById(id);
            if (book == null)
                return 0;

            book.ReleaseDate = date;
            return db.SaveChanges();
        }
    }
}
