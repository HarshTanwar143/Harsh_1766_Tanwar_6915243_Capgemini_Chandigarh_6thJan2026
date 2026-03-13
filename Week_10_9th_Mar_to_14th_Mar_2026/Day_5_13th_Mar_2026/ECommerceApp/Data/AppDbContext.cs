using ECommerceApp.Models;
using Microsoft.EntityFrameworkCore;

namespace ECommerceApp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Customer)
                .WithMany(c => c.Orders)
                .HasForeignKey(o => o.CustomerId);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.Product)
                .WithMany(p => p.Orders)
                .HasForeignKey(o => o.ProductId);

            // Seed Data
            modelBuilder.Entity<Customer>().HasData(
                new Customer { Id = 1, Name = "Alice", Email = "alice@example.com" },
                new Customer { Id = 2, Name = "Bob", Email = "bob@example.com" }
            );

            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Name = "Laptop", Price = 999.99m },
                new Product { Id = 2, Name = "Mouse", Price = 29.99m },
                new Product { Id = 3, Name = "Keyboard", Price = 59.99m }
            );

            modelBuilder.Entity<Order>().HasData(
                new Order { Id = 1, CustomerId = 1, ProductId = 1, OrderDate = new DateTime(2024, 1, 10) },
                new Order { Id = 2, CustomerId = 1, ProductId = 2, OrderDate = new DateTime(2024, 1, 11) },
                new Order { Id = 3, CustomerId = 2, ProductId = 3, OrderDate = new DateTime(2024, 1, 12) }
            );

            modelBuilder.Entity<Product>()
              .Property(p => p.Price)
              .HasColumnType("decimal(18, 2)");


            modelBuilder.Entity<Order>()
                .HasOne(o => o.Customer)
                .WithMany(c => c.Orders)
                .HasForeignKey(o => o.CustomerId);
        }
    }
}