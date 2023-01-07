using EntityFrameworkPractice.Entities;
using EntityFrameworkPractice.Repositories;
using Microsoft.EntityFrameworkCore.Internal;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;

namespace EntityFrameworkPractice
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using (Repositories.AppContext db = new())
            {
                BookRepository bookrepository = new(db);
                UserRepository userrepository = new(db);

                var user1 = new User() { Name = "user1", Email = "gmail1@gmail.com" };
                var user2 = new User() { Name = "user2", Email = "gmail2@gmail.com" };
                var user3 = new User() { Name = "user3", Email = "gmail3@gmail.com" };

                var book1 = new Book() { Title = "book1", ReleaseDate = new DateTime(2021, 01, 01) };
                var book2 = new Book() { Title = "book2", ReleaseDate = new DateTime(2022, 01, 01) };
                var book3 = new Book() { Title = "book3", ReleaseDate = new DateTime(2023, 01, 01) };
                var book4 = new Book() { Title = "book4", ReleaseDate = new DateTime(2024, 01, 01) };

                var author1 = new Author() { Name = "author1" };
                var author2 = new Author() { Name = "author2" };

                var genre1 = new Genre() { Name = "genre1" };
                var genre2 = new Genre() { Name = "genre2" };
                var genre3 = new Genre() { Name = "genre3" };

                book1.Genres.Add(genre1);
                book1.Genres.Add(genre3);
                book2.Genres.Add(genre2);
                book2.Genres.Add(genre3);
                book3.Genres.Add(genre1);
                book3.Genres.Add(genre2);
                book4.Genres.Add(genre3);

                book1.Authors.Add(author1);
                book2.Authors.Add(author2);
                book3.Authors.AddRange(new Author[2] { author1, author2 });
                book4.Authors.AddRange(new Author[2] { author1, author2 });

                book1.Users.Add(user3);
                book2.Users.AddRange(new User[2] { user1, user2 });
                book3.Users.AddRange(new User[2] { user1, user3 });
                book4.Users.AddRange(new User[2] { user2, user3 });

                db.Authors.AddRange(author1, author2);
                db.Books.AddRange(book1, book2, book3, book4);
                db.Users.AddRange(user1, user2, user3);
                db.Genres.AddRange(genre1, genre2, genre3);
                db.SaveChanges();

                string input;
                Book book;
                User user;
                int min, max, count;

                while (true)
                {
                    input = Console.ReadLine();
                    if (input == "stop")
                        break;

                    switch (input)
                    {
                        case "GetById":
                            input = Console.ReadLine();
                            book = bookrepository.GetById(Int32.Parse(input));
                            Console.WriteLine(book.Title);
                            break;
                        case "GetAll":
                            var books = bookrepository.GetAll();
                            foreach (var _book in books)
                                Console.WriteLine($"{_book.Id}, {_book.Title}, {_book.ReleaseDate.Date}");
                            break;
                        case "GetAllByGenre":
                            input = Console.ReadLine();
                            min = Int32.Parse(Console.ReadLine());
                            max = Int32.Parse(Console.ReadLine());
                            var booksbygenre = bookrepository.GetAllByGenreAndYear(input, min, max);
                            foreach (var _book in booksbygenre)
                                Console.WriteLine($"{_book.Id}, {_book.Title}, {_book.ReleaseDate.Date}");
                            break;
                        case "CountByAuthor":
                            input = Console.ReadLine();
                            Console.WriteLine(bookrepository.CountByAuthor(input));
                            break;
                        case "GetLatestBook":
                            book = bookrepository.GetLatestBook();
                            Console.WriteLine(book.Title + " " + book.ReleaseDate);
                            break;
                        case "GetAllUsers":
                            var users = userrepository.GetAll();
                            foreach (var _user in users)
                                Console.WriteLine($"{_user.Id}, {_user.Name}, {_user.Email}");
                            break;
                        case "UserBookCount":
                            count = Int32.Parse(Console.ReadLine());
                            user = userrepository.GetById(count);
                            count = userrepository.BookCount(user);
                            Console.WriteLine(count);
                            break;
                        case "UserHasBook":
                            count = Int32.Parse(Console.ReadLine());
                            min = Int32.Parse(Console.ReadLine());
                            user = userrepository.GetById(count);
                            book = bookrepository.GetById(min);
                            var hasbook = userrepository.HasBook(user, book);
                            Console.WriteLine(hasbook);
                            break;
                    }
                }
            }
        }
    }
}