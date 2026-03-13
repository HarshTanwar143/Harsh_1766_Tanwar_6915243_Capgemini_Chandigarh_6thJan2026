namespace PublishingApp.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public int PublishedYear { get; set; }

        public ICollection<AuthorBook> AuthorBooks { get; set; } = new List<AuthorBook>();
    }
}