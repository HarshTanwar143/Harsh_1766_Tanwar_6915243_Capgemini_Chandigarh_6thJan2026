using System.ComponentModel.DataAnnotations;

namespace EventBookingAPI.Models;

public class Booking
{
    public int Id { get; set; }

    [Required]
    public int EventId { get; set; }
    public Event? Event { get; set; }

    [Required]
    public int UserId { get; set; }
    public User? User { get; set; }

    [Range(1, 100, ErrorMessage = "Seats booked must be between 1 and 100.")]
    public int SeatsBooked { get; set; }

    public DateTime BookedAt { get; set; } = DateTime.UtcNow;
}
