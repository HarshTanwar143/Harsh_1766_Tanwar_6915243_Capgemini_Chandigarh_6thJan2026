using Microsoft.EntityFrameworkCore;
using SmartHospital.API.Data;
using SmartHospital.API.Models;
using SmartHospital.API.Repositories.Interfaces;

namespace SmartHospital.API.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _ctx;
    public UserRepository(ApplicationDbContext ctx) => _ctx = ctx;

    public async Task<User?> GetByIdAsync(int id) =>
        await _ctx.Users.FindAsync(id);

    public async Task<User?> GetByEmailAsync(string email) =>
        await _ctx.Users.FirstOrDefaultAsync(u => u.Email == email);

    public async Task<IEnumerable<User>> GetAllAsync() =>
        await _ctx.Users.ToListAsync();

    public async Task<IEnumerable<User>> GetByRoleAsync(string role) =>
        await _ctx.Users.Where(u => u.Role == role).ToListAsync();

    public async Task AddAsync(User user) { _ctx.Users.Add(user); await _ctx.SaveChangesAsync(); }
    public async Task UpdateAsync(User user) { _ctx.Users.Update(user); await _ctx.SaveChangesAsync(); }
    public async Task DeleteAsync(int id)
    {
        var u = await _ctx.Users.FindAsync(id);
        if (u != null) { _ctx.Users.Remove(u); await _ctx.SaveChangesAsync(); }
    }
    public async Task<bool> ExistsAsync(string email) =>
        await _ctx.Users.AnyAsync(u => u.Email == email);
}

public class DepartmentRepository : IDepartmentRepository
{
    private readonly ApplicationDbContext _ctx;
    public DepartmentRepository(ApplicationDbContext ctx) => _ctx = ctx;

    public async Task<Department?> GetByIdAsync(int id) =>
        await _ctx.Departments.Include(d => d.Doctors).FirstOrDefaultAsync(d => d.DepartmentId == id);

    public async Task<IEnumerable<Department>> GetAllAsync() =>
        await _ctx.Departments.Include(d => d.Doctors).ToListAsync();

    public async Task AddAsync(Department d) { _ctx.Departments.Add(d); await _ctx.SaveChangesAsync(); }
    public async Task UpdateAsync(Department d) { _ctx.Departments.Update(d); await _ctx.SaveChangesAsync(); }
    public async Task DeleteAsync(int id)
    {
        var d = await _ctx.Departments.FindAsync(id);
        if (d != null) { _ctx.Departments.Remove(d); await _ctx.SaveChangesAsync(); }
    }
}

public class DoctorRepository : IDoctorRepository
{
    private readonly ApplicationDbContext _ctx;
    public DoctorRepository(ApplicationDbContext ctx) => _ctx = ctx;

    private IQueryable<Doctor> Query() =>
        _ctx.Doctors.Include(d => d.User).Include(d => d.Department);

    public async Task<Doctor?> GetByIdAsync(int id) =>
        await Query().FirstOrDefaultAsync(d => d.DoctorId == id);

    public async Task<Doctor?> GetByUserIdAsync(int userId) =>
        await Query().FirstOrDefaultAsync(d => d.UserId == userId);

    public async Task<IEnumerable<Doctor>> GetAllAsync() =>
        await Query().ToListAsync();

    public async Task<IEnumerable<Doctor>> GetByDepartmentAsync(int departmentId) =>
        await Query().Where(d => d.DepartmentId == departmentId).ToListAsync();

    public async Task AddAsync(Doctor d) { _ctx.Doctors.Add(d); await _ctx.SaveChangesAsync(); }
    public async Task UpdateAsync(Doctor d) { _ctx.Doctors.Update(d); await _ctx.SaveChangesAsync(); }
    public async Task DeleteAsync(int id)
    {
        var d = await _ctx.Doctors.FindAsync(id);
        if (d != null) { _ctx.Doctors.Remove(d); await _ctx.SaveChangesAsync(); }
    }
}

public class AppointmentRepository : IAppointmentRepository
{
    private readonly ApplicationDbContext _ctx;
    public AppointmentRepository(ApplicationDbContext ctx) => _ctx = ctx;

    private IQueryable<Appointment> Query() =>
        _ctx.Appointments
            .Include(a => a.Patient)
            .Include(a => a.Doctor).ThenInclude(d => d.User)
            .Include(a => a.Doctor).ThenInclude(d => d.Department)
            .Include(a => a.Prescription)
            .Include(a => a.Bill);

    public async Task<Appointment?> GetByIdAsync(int id) =>
        await Query().FirstOrDefaultAsync(a => a.AppointmentId == id);

    public async Task<IEnumerable<Appointment>> GetAllAsync() =>
        await Query().OrderByDescending(a => a.AppointmentDate).ToListAsync();

    public async Task<IEnumerable<Appointment>> GetByPatientAsync(int patientId) =>
        await Query().Where(a => a.PatientId == patientId)
                     .OrderByDescending(a => a.AppointmentDate).ToListAsync();

    public async Task<IEnumerable<Appointment>> GetByDoctorAsync(int doctorId) =>
        await Query().Where(a => a.DoctorId == doctorId)
                     .OrderByDescending(a => a.AppointmentDate).ToListAsync();

    public async Task AddAsync(Appointment a) { _ctx.Appointments.Add(a); await _ctx.SaveChangesAsync(); }
    public async Task UpdateAsync(Appointment a) { _ctx.Appointments.Update(a); await _ctx.SaveChangesAsync(); }
    public async Task DeleteAsync(int id)
    {
        var a = await _ctx.Appointments.FindAsync(id);
        if (a != null) { _ctx.Appointments.Remove(a); await _ctx.SaveChangesAsync(); }
    }
}

public class PrescriptionRepository : IPrescriptionRepository
{
    private readonly ApplicationDbContext _ctx;
    public PrescriptionRepository(ApplicationDbContext ctx) => _ctx = ctx;

    private IQueryable<Prescription> Query() =>
        _ctx.Prescriptions
            .Include(p => p.Appointment).ThenInclude(a => a.Patient)
            .Include(p => p.Appointment).ThenInclude(a => a.Doctor).ThenInclude(d => d.User);

    public async Task<Prescription?> GetByIdAsync(int id) =>
        await Query().FirstOrDefaultAsync(p => p.PrescriptionId == id);

    public async Task<Prescription?> GetByAppointmentAsync(int appointmentId) =>
        await Query().FirstOrDefaultAsync(p => p.AppointmentId == appointmentId);

    public async Task<IEnumerable<Prescription>> GetAllAsync() =>
        await Query().OrderByDescending(p => p.CreatedAt).ToListAsync();

    public async Task AddAsync(Prescription p) { _ctx.Prescriptions.Add(p); await _ctx.SaveChangesAsync(); }
    public async Task UpdateAsync(Prescription p) { _ctx.Prescriptions.Update(p); await _ctx.SaveChangesAsync(); }
}

public class BillRepository : IBillRepository
{
    private readonly ApplicationDbContext _ctx;
    public BillRepository(ApplicationDbContext ctx) => _ctx = ctx;

    private IQueryable<Bill> Query() =>
        _ctx.Bills
            .Include(b => b.Appointment).ThenInclude(a => a.Patient)
            .Include(b => b.Appointment).ThenInclude(a => a.Doctor).ThenInclude(d => d.User);

    public async Task<Bill?> GetByIdAsync(int id) =>
        await Query().FirstOrDefaultAsync(b => b.BillId == id);

    public async Task<Bill?> GetByAppointmentAsync(int appointmentId) =>
        await Query().FirstOrDefaultAsync(b => b.AppointmentId == appointmentId);

    public async Task<IEnumerable<Bill>> GetAllAsync() =>
        await Query().OrderByDescending(b => b.BilledAt).ToListAsync();

    public async Task<IEnumerable<Bill>> GetByPatientAsync(int patientId) =>
        await Query().Where(b => b.Appointment.PatientId == patientId)
                     .OrderByDescending(b => b.BilledAt).ToListAsync();

    public async Task AddAsync(Bill b) { _ctx.Bills.Add(b); await _ctx.SaveChangesAsync(); }
    public async Task UpdateAsync(Bill b) { _ctx.Bills.Update(b); await _ctx.SaveChangesAsync(); }
}
