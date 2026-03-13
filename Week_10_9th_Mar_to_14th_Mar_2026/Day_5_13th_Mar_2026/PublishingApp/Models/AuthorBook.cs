namespace PublishingApp.Models
{
    public class AuthorBook
    {
        // Composite Key (AuthorId + BookId) — no separate Id column
        public int AuthorId { get; set; }
        public Author Author { get; set; }

        public int BookId { get; set; }
        public Book Book { get; set; }
    }
}