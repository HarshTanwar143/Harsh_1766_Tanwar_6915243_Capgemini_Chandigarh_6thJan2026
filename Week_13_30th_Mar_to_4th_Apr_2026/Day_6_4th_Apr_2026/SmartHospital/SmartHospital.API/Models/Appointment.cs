using System.ComponentModel.DataAnnotations;

namespace SmartHospital.API.Models;

public class Appointment
{
    public int AppointmentId { get; set; }

    public int PatientId { get; set; }
    public int DoctorId { get; set; }

    public DateTime AppointmentDate { get; set; }

    [MaxLength(20)]
    public string Status { get; set; } = "Booked";

    public User Patient { get; set; } = null!;
    public Doctor Doctor { get; set; } = null!;
    public Prescription? Prescription { get; set; }
    public Bill? Bill { get; set; }
}
