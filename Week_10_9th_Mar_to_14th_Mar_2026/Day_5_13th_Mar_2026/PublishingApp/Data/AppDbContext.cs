using Microsoft.EntityFrameworkCore;
using PublishingApp.Models;

namespace PublishingApp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<AuthorBook> AuthorBooks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // COMPOSITE KEY on join table
            modelBuilder.Entity<AuthorBook>()
                .HasKey(ab => new { ab.AuthorId, ab.BookId });

            // AuthorBook → Author
            modelBuilder.Entity<AuthorBook>()
                .HasOne(ab => ab.Author)
                .WithMany(a => a.AuthorBooks)
                .HasForeignKey(ab => ab.AuthorId);

            // AuthorBook → Book
            modelBuilder.Entity<AuthorBook>()
                .HasOne(ab => ab.Book)
                .WithMany(b => b.AuthorBooks)
                .HasForeignKey(ab => ab.BookId);

            // Seed Authors
            modelBuilder.Entity<Author>().HasData(
                new Author { Id = 1, Name = "George Orwell", Bio = "English novelist and essayist." },
                new Author { Id = 2, Name = "Aldous Huxley", Bio = "English writer and philosopher." },
                new Author { Id = 3, Name = "J.R.R. Tolkien", Bio = "Author of Middle-earth." }
            );

            // Seed Books
            modelBuilder.Entity<Book>().HasData(
                new Book { Id = 1, Title = "1984", Genre = "Dystopian", PublishedYear = 1949 },
                new Book { Id = 2, Title = "Brave New World", Genre = "Dystopian", PublishedYear = 1932 },
                new Book { Id = 3, Title = "The Lord of the Rings", Genre = "Fantasy", PublishedYear = 1954 },
                new Book { Id = 4, Title = "Co-Authored Fiction", Genre = "Fiction", PublishedYear = 2000 }
            );

            // Seed AuthorBooks (join table)
            modelBuilder.Entity<AuthorBook>().HasData(
                new AuthorBook { AuthorId = 1, BookId = 1 }, // Orwell → 1984
                new AuthorBook { AuthorId = 2, BookId = 2 }, // Huxley → Brave New World
                new AuthorBook { AuthorId = 3, BookId = 3 }, // Tolkien → LOTR
                new AuthorBook { AuthorId = 1, BookId = 4 }, // Orwell → Co-Authored
                new AuthorBook { AuthorId = 2, BookId = 4 }  // Huxley → Co-Authored (same book!)
            );
        }
    }
}