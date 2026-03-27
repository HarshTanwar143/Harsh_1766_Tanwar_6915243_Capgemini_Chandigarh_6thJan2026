using BankingAPI_Task3.Models;
using Microsoft.EntityFrameworkCore;

namespace BankingAPI_Task3.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Account> Accounts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Admin's account
            modelBuilder.Entity<Account>().HasData(new Account
            {
                Id = 1,
                AccountHolderName = "Alice Admin",
                AccountNumber = "1111222233334444",
                Balance = 999999,
                Email = "alice@bank.com",
                UserId = "admin1"
            });

            // Regular user's account
            modelBuilder.Entity<Account>().HasData(new Account
            {
                Id = 2,
                AccountHolderName = "Bob User",
                AccountNumber = "9876541234",
                Balance = 25000,
                Email = "bob@bank.com",
                UserId = "user1"
            });
        }
    }
}
