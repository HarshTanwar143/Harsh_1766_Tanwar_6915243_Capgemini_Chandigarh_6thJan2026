using Microsoft.EntityFrameworkCore;
using EventBookingAPI.Models;

namespace EventBookingAPI.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<Event> Events { get; set; }
    public DbSet<Booking> Bookings { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Booking>()
            .HasOne(b => b.Event)
            .WithMany(e => e.Bookings)
            .HasForeignKey(b => b.EventId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Booking>()
            .HasOne(b => b.User)
            .WithMany(u => u.Bookings)
            .HasForeignKey(b => b.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Event>().HasData(
            new Event { Id = 1, Title = "Tech Summit 2026", Description = "Annual technology conference covering latest trends", Date = new DateTime(2026, 5, 15, 10, 0, 0, DateTimeKind.Utc), Location = "New Delhi", AvailableSeats = 200 },
            new Event { Id = 2, Title = "AI & ML Workshop", Description = "Hands-on workshop on Artificial Intelligence and Machine Learning", Date = new DateTime(2026, 4, 20, 9, 0, 0, DateTimeKind.Utc), Location = "Bangalore", AvailableSeats = 50 },
            new Event { Id = 3, Title = "Cloud Computing Expo", Description = "Explore the latest in cloud technologies and DevOps", Date = new DateTime(2026, 6, 10, 11, 0, 0, DateTimeKind.Utc), Location = "Mumbai", AvailableSeats = 150 }
        );
    }
}
