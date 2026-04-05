using System.ComponentModel.DataAnnotations;

namespace SmartHospital.API.Models;

public class Department
{
    public int DepartmentId { get; set; }

    [Required, MaxLength(100)]
    public string DepartmentName { get; set; } = string.Empty;

    public string? Description { get; set; }

    public ICollection<Doctor> Doctors { get; set; } = new List<Doctor>();
}
