using BankingAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BankingAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Account> Accounts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>().HasData(new Account
            {
                Id = 1,
                AccountHolderName = "John Doe",
                AccountNumber = "9876541234",
                Balance = 50000,
                UserId = "user1"
            });
        }
    }
}
