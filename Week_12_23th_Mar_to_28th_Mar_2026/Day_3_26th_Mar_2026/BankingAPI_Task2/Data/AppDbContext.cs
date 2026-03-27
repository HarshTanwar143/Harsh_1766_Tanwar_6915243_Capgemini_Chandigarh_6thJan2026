using BankingAPI_Task2.Models;
using Microsoft.EntityFrameworkCore;

namespace BankingAPI_Task2.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Account> Accounts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Normal account — 10 digits → XXXXXX1234
            modelBuilder.Entity<Account>().HasData(new Account
            {
                Id = 1,
                AccountHolderName = "John Doe",
                AccountNumber = "9876541234",
                Balance = 50000,
                UserId = "user1"
            });

            // Short account — only 3 digits (edge case — masking handles it safely)
            modelBuilder.Entity<Account>().HasData(new Account
            {
                Id = 2,
                AccountHolderName = "Jane Smith",
                AccountNumber = "123",
                Balance = 10000,
                UserId = "user2"
            });
        }
    }
}
