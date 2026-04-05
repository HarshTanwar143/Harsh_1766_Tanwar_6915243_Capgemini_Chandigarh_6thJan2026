using System.ComponentModel.DataAnnotations;

namespace SmartHospital.API.Models;

public class User
{
    public int UserId { get; set; }

    [Required, MaxLength(100)]
    public string FullName { get; set; } = string.Empty;

    [Required, MaxLength(100)]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string PasswordHash { get; set; } = string.Empty;

    [Required]
    public string Role { get; set; } = "Patient";

    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public Doctor? Doctor { get; set; }
    public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
}
