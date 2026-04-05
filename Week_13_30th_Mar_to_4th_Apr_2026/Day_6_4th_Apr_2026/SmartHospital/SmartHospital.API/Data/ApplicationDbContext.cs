using Microsoft.EntityFrameworkCore;
using SmartHospital.API.Models;

namespace SmartHospital.API.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<User> Users => Set<User>();
    public DbSet<Department> Departments => Set<Department>();
    public DbSet<Doctor> Doctors => Set<Doctor>();
    public DbSet<Appointment> Appointments => Set<Appointment>();
    public DbSet<Prescription> Prescriptions => Set<Prescription>();
    public DbSet<Bill> Bills => Set<Bill>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // User
        modelBuilder.Entity<User>()
            .HasIndex(u => u.Email)
            .IsUnique();

        modelBuilder.Entity<User>()
            .Property(u => u.Role)
            .HasConversion<string>();

        // Doctor - User (One-to-One)
        modelBuilder.Entity<Doctor>()
            .HasOne(d => d.User)
            .WithOne(u => u.Doctor)
            .HasForeignKey<Doctor>(d => d.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        // Doctor - Department (Many-to-One)
        modelBuilder.Entity<Doctor>()
            .HasOne(d => d.Department)
            .WithMany(dep => dep.Doctors)
            .HasForeignKey(d => d.DepartmentId)
            .OnDelete(DeleteBehavior.Restrict);

        // Appointment - Patient (Many-to-One)
        modelBuilder.Entity<Appointment>()
            .HasOne(a => a.Patient)
            .WithMany(u => u.Appointments)
            .HasForeignKey(a => a.PatientId)
            .OnDelete(DeleteBehavior.Restrict);

        // Appointment - Doctor (Many-to-One)
        modelBuilder.Entity<Appointment>()
            .HasOne(a => a.Doctor)
            .WithMany(d => d.Appointments)
            .HasForeignKey(a => a.DoctorId)
            .OnDelete(DeleteBehavior.Restrict);

        // Prescription - Appointment (One-to-One)
        modelBuilder.Entity<Prescription>()
            .HasOne(p => p.Appointment)
            .WithOne(a => a.Prescription)
            .HasForeignKey<Prescription>(p => p.AppointmentId)
            .OnDelete(DeleteBehavior.Cascade);

        // Fix: explicit decimal precision for Prescription.MedicineCharges
        modelBuilder.Entity<Prescription>()
            .Property(p => p.MedicineCharges)
            .HasColumnType("decimal(10,2)");

        // Bill - Appointment (One-to-One)
        modelBuilder.Entity<Bill>()
            .HasOne(b => b.Appointment)
            .WithOne(a => a.Bill)
            .HasForeignKey<Bill>(b => b.AppointmentId)
            .OnDelete(DeleteBehavior.Cascade);

        // Seed data
        modelBuilder.Entity<Department>().HasData(
            new Department { DepartmentId = 1, DepartmentName = "Cardiology", Description = "Heart and cardiovascular diseases" },
            new Department { DepartmentId = 2, DepartmentName = "Neurology", Description = "Brain and nervous system" },
            new Department { DepartmentId = 3, DepartmentName = "Orthopedics", Description = "Bones, joints, and muscles" },
            new Department { DepartmentId = 4, DepartmentName = "General Medicine", Description = "General health care" }
        );

        // Seed admin user (password: Admin@123)
        // IMPORTANT: Pre-computed static BCrypt hash — do NOT use BCrypt.HashPassword() here
        // because it generates a new random salt each time, making the migration non-deterministic.
        modelBuilder.Entity<User>().HasData(
            new User
            {
                UserId = 1,
                FullName = "System Admin",
                Email = "admin@hospital.com",
                PasswordHash = "$2a$11$Qq5YMlMmhFaFaRsq1RLpKe8dNLm3M7eKBn/UcKhb7Eq2J3GbHyJfO",
                Role = "Admin",
                CreatedAt = new DateTime(2024, 1, 1)
            }
        );
    }
}
