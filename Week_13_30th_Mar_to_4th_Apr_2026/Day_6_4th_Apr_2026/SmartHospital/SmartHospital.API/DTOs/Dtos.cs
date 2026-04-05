using System.ComponentModel.DataAnnotations;

namespace SmartHospital.API.DTOs;

// ---- AUTH ----
public class RegisterDto
{
    [Required] public string FullName { get; set; } = string.Empty;
    [Required, EmailAddress] public string Email { get; set; } = string.Empty;
    [Required, MinLength(6)] public string Password { get; set; } = string.Empty;
    public string Role { get; set; } = "Patient";
}

public class LoginDto
{
    [Required, EmailAddress] public string Email { get; set; } = string.Empty;
    [Required] public string Password { get; set; } = string.Empty;
}

public class AuthResponseDto
{
    public string Token { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    public int UserId { get; set; }
}

// ---- USER ----
public class UserDto
{
    public int UserId { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}

public class UpdateUserDto
{
    [Required] public string FullName { get; set; } = string.Empty;
    [Required, EmailAddress] public string Email { get; set; } = string.Empty;
}

// ---- DEPARTMENT ----
public class DepartmentDto
{
    public int DepartmentId { get; set; }
    public string DepartmentName { get; set; } = string.Empty;
    public string? Description { get; set; }
    public int DoctorCount { get; set; }
}

public class CreateDepartmentDto
{
    [Required] public string DepartmentName { get; set; } = string.Empty;
    public string? Description { get; set; }
}

// ---- DOCTOR ----
public class DoctorDto
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

public class CreateDoctorDto
{
    [Required] public string FullName { get; set; } = string.Empty;
    [Required, EmailAddress] public string Email { get; set; } = string.Empty;
    [Required, MinLength(6)] public string Password { get; set; } = string.Empty;
    [Required] public int DepartmentId { get; set; }
    public string? Specialization { get; set; }
    public int ExperienceYears { get; set; }
    public string? Availability { get; set; }
}

public class UpdateDoctorDto
{
    [Required] public int DepartmentId { get; set; }
    public string? Specialization { get; set; }
    public int ExperienceYears { get; set; }
    public string? Availability { get; set; }
}

// ---- APPOINTMENT ----
public class AppointmentDto
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

public class CreateAppointmentDto
{
    [Required] public int DoctorId { get; set; }
    [Required] public DateTime AppointmentDate { get; set; }
}

public class UpdateAppointmentStatusDto
{
    [Required] public string Status { get; set; } = string.Empty;
}

// ---- PRESCRIPTION ----
public class PrescriptionDto
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

public class CreatePrescriptionDto
{
    [Required] public int AppointmentId { get; set; }
    public string? Diagnosis { get; set; }
    public string? Medicines { get; set; }
    public string? Notes { get; set; }
    public decimal MedicineCharges { get; set; }
}

public class ApprovePrescriptionDto
{
    [Required] public int PrescriptionId { get; set; }
    public decimal MedicineCharges { get; set; }
}

// ---- BILL ----
public class BillDto
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

public class UpdatePaymentStatusDto
{
    [Required] public string PaymentStatus { get; set; } = string.Empty;
}
