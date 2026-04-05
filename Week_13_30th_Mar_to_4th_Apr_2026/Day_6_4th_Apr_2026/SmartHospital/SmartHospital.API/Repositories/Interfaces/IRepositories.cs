using SmartHospital.API.Models;

namespace SmartHospital.API.Repositories.Interfaces;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(int id);
    Task<User?> GetByEmailAsync(string email);
    Task<IEnumerable<User>> GetAllAsync();
    Task<IEnumerable<User>> GetByRoleAsync(string role);
    Task AddAsync(User user);
    Task UpdateAsync(User user);
    Task DeleteAsync(int id);
    Task<bool> ExistsAsync(string email);
}

public interface IDepartmentRepository
{
    Task<Department?> GetByIdAsync(int id);
    Task<IEnumerable<Department>> GetAllAsync();
    Task AddAsync(Department department);
    Task UpdateAsync(Department department);
    Task DeleteAsync(int id);
}

public interface IDoctorRepository
{
    Task<Doctor?> GetByIdAsync(int id);
    Task<Doctor?> GetByUserIdAsync(int userId);
    Task<IEnumerable<Doctor>> GetAllAsync();
    Task<IEnumerable<Doctor>> GetByDepartmentAsync(int departmentId);
    Task AddAsync(Doctor doctor);
    Task UpdateAsync(Doctor doctor);
    Task DeleteAsync(int id);
}

public interface IAppointmentRepository
{
    Task<Appointment?> GetByIdAsync(int id);
    Task<IEnumerable<Appointment>> GetAllAsync();
    Task<IEnumerable<Appointment>> GetByPatientAsync(int patientId);
    Task<IEnumerable<Appointment>> GetByDoctorAsync(int doctorId);
    Task AddAsync(Appointment appointment);
    Task UpdateAsync(Appointment appointment);
    Task DeleteAsync(int id);
}

public interface IPrescriptionRepository
{
    Task<Prescription?> GetByIdAsync(int id);
    Task<Prescription?> GetByAppointmentAsync(int appointmentId);
    Task<IEnumerable<Prescription>> GetAllAsync();
    Task AddAsync(Prescription prescription);
    Task UpdateAsync(Prescription prescription);
}

public interface IBillRepository
{
    Task<Bill?> GetByIdAsync(int id);
    Task<Bill?> GetByAppointmentAsync(int appointmentId);
    Task<IEnumerable<Bill>> GetAllAsync();
    Task<IEnumerable<Bill>> GetByPatientAsync(int patientId);
    Task AddAsync(Bill bill);
    Task UpdateAsync(Bill bill);
}
