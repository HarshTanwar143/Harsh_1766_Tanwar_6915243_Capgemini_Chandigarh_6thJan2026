using System.ComponentModel.DataAnnotations;

namespace EventBookingAPI.DTOs;

public class RegisterDto
{
    [Required]
    public string FullName { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    [MinLength(6)]
    public string Password { get; set; } = string.Empty;
}

public class LoginDto
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string Password { get; set; } = string.Empty;
}

public class AuthResponseDto
{
    public string Token { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}

public class EventDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public string Location { get; set; } = string.Empty;
    public int AvailableSeats { get; set; }
}

public class CreateEventDto
{
    [Required(ErrorMessage = "Title is required.")]
    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public DateTime Date { get; set; }

    [Required]
    public string Location { get; set; } = string.Empty;

    [Range(1, 10000)]
    public int AvailableSeats { get; set; }
}

public class BookingDto
{
    public int Id { get; set; }
    public int EventId { get; set; }
    public string EventTitle { get; set; } = string.Empty;
    public string EventLocation { get; set; } = string.Empty;
    public DateTime EventDate { get; set; }
    public int SeatsBooked { get; set; }
    public DateTime BookedAt { get; set; }
}

public class CreateBookingDto
{
    [Required]
    public int EventId { get; set; }

    [Range(1, 100, ErrorMessage = "Seats must be between 1 and 100.")]
    public int SeatsBooked { get; set; }
}
