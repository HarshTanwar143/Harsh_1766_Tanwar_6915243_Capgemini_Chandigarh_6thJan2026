using System.ComponentModel.DataAnnotations;

namespace SmartHospital.API.Models;

public class Prescription
{
    public int PrescriptionId { get; set; }

    public int AppointmentId { get; set; }

    [MaxLength(255)]
    public string? Diagnosis { get; set; }

    public string? Medicines { get; set; }

    [MaxLength(500)]
    public string? Notes { get; set; }

    public bool IsApproved { get; set; } = false;

    public decimal MedicineCharges { get; set; } = 0;

    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public Appointment Appointment { get; set; } = null!;
}
