using System.ComponentModel.DataAnnotations;

namespace SmartHospital.API.Models;

public class Doctor
{
    public int DoctorId { get; set; }

    public int UserId { get; set; }
    public int DepartmentId { get; set; }

    [MaxLength(100)]
    public string? Specialization { get; set; }

    public int ExperienceYears { get; set; }

    [MaxLength(200)]
    public string? Availability { get; set; }

    public User User { get; set; } = null!;
    public Department Department { get; set; } = null!;
    public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
}
