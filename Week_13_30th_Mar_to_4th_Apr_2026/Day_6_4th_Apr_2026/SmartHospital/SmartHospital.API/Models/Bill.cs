using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartHospital.API.Models;

public class Bill
{
    public int BillId { get; set; }

    public int AppointmentId { get; set; }

    [Column(TypeName = "decimal(10,2)")]
    public decimal ConsultationFee { get; set; }

    [Column(TypeName = "decimal(10,2)")]
    public decimal MedicineCharges { get; set; }

    [Column(TypeName = "decimal(10,2)")]
    public decimal TotalAmount { get; set; }

    [MaxLength(20)]
    public string PaymentStatus { get; set; } = "Unpaid";

    public DateTime BilledAt { get; set; } = DateTime.Now;

    public Appointment Appointment { get; set; } = null!;
}
