using System.ComponentModel.DataAnnotations;

namespace SmartHospital.MVC.ViewModels;

public class LoginViewModel
{
    [Required, EmailAddress]
    public string Email { get; set; } = string.Empty;
    [Required]
    public string Password { get; set; } = string.Empty;
}

public class RegisterViewModel
{
    [Required] public string FullName { get; set; } = string.Empty;
    [Required, EmailAddress] public string Email { get; set; } = string.Empty;
    [Required, MinLength(6)] public string Password { get; set; } = string.Empty;
    [Required, Compare("Password")] public string ConfirmPassword { get; set; } = string.Empty;
}

public class DepartmentViewModel
{
    public int DepartmentId { get; set; }
    public string DepartmentName { get; set; } = string.Empty;
    public string? Description { get; set; }
    public int DoctorCount { get; set; }
}

public class DoctorViewModel
{
    public int DoctorId { get; set; }
    public int UserId { get; set; }
    public string DoctorName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public int DepartmentId { get; set; }
    public string DepartmentName { get; set; } = string.Empty;
    public string? Specialization { get; set; }
    public int ExperienceYears { get; set; }
    public string? Availability { get; set; }
}

public class CreateDoctorViewModel
{
    [Required] public string FullName { get; set; } = string.Empty;
    [Required, EmailAddress] public string Email { get; set; } = string.Empty;
    [Required, MinLength(6)] public string Password { get; set; } = string.Empty;
    [Required] public int DepartmentId { get; set; }
    public string? Specialization { get; set; }
    public int ExperienceYears { get; set; }
    public string? Availability { get; set; }
    public List<DepartmentViewModel> Departments { get; set; } = new();
}

public class AppointmentViewModel
{
    public int AppointmentId { get; set; }
    public int PatientId { get; set; }
    public string PatientName { get; set; } = string.Empty;
    public int DoctorId { get; set; }
    public string DoctorName { get; set; } = string.Empty;
    public string DepartmentName { get; set; } = string.Empty;
    public DateTime AppointmentDate { get; set; }
    public string Status { get; set; } = string.Empty;
    public bool HasPrescription { get; set; }
    public bool HasBill { get; set; }
}

public class BookAppointmentViewModel
{
    [Required] public int DoctorId { get; set; }
    [Required] public DateTime AppointmentDate { get; set; } = DateTime.Now.AddDays(1);
    public List<DepartmentViewModel> Departments { get; set; } = new();
    public List<DoctorViewModel> Doctors { get; set; } = new();
    public int? SelectedDepartmentId { get; set; }
}

public class PrescriptionViewModel
{
    public int PrescriptionId { get; set; }
    public int AppointmentId { get; set; }
    public string PatientName { get; set; } = string.Empty;
    public string DoctorName { get; set; } = string.Empty;
    public string? Diagnosis { get; set; }
    public string? Medicines { get; set; }
    public string? Notes { get; set; }
    public bool IsApproved { get; set; }
    public decimal MedicineCharges { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class CreatePrescriptionViewModel
{
    [Required] public int AppointmentId { get; set; }
    public string PatientName { get; set; } = string.Empty;
    public string? Diagnosis { get; set; }
    public string? Medicines { get; set; }
    public string? Notes { get; set; }
    [Range(0, 100000)] public decimal MedicineCharges { get; set; }
}

public class ApprovePrescriptionViewModel
{
    [Required] public int PrescriptionId { get; set; }
    [Required, Range(0, 100000)] public decimal MedicineCharges { get; set; }
}

public class BillViewModel
{
    public int BillId { get; set; }
    public int AppointmentId { get; set; }
    public string PatientName { get; set; } = string.Empty;
    public string DoctorName { get; set; } = string.Empty;
    public decimal ConsultationFee { get; set; }
    public decimal MedicineCharges { get; set; }
    public decimal TotalAmount { get; set; }
    public string PaymentStatus { get; set; } = string.Empty;
    public DateTime BilledAt { get; set; }
}

public class UserViewModel
{
    public int UserId { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}
