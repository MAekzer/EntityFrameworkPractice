namespace EntityFrameworkPractice.Entities
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime ReleaseDate { get; set; }

        public List<Author> Authors { get; set; } = new List<Author>();
        public List<Genre> Genres { get; set; } = new List<Genre>();
        public List<User> Users { get; set; } = new List<User>();
    }
}