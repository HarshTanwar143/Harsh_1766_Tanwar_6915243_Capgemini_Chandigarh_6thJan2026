using AutoMapper;
using SmartHospital.API.DTOs;
using SmartHospital.API.Models;

namespace SmartHospital.API.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // User
        CreateMap<User, UserDto>();

        // Department
        CreateMap<Department, DepartmentDto>()
            .ForMember(d => d.DoctorCount, opt => opt.MapFrom(s => s.Doctors.Count));
        CreateMap<CreateDepartmentDto, Department>();

        // Doctor
        CreateMap<Doctor, DoctorDto>()
            .ForMember(d => d.DoctorName, opt => opt.MapFrom(s => s.User.FullName))
            .ForMember(d => d.Email, opt => opt.MapFrom(s => s.User.Email))
            .ForMember(d => d.DepartmentName, opt => opt.MapFrom(s => s.Department.DepartmentName));

        // Appointment
        CreateMap<Appointment, AppointmentDto>()
            .ForMember(d => d.PatientName, opt => opt.MapFrom(s => s.Patient.FullName))
            .ForMember(d => d.DoctorName, opt => opt.MapFrom(s => s.Doctor.User.FullName))
            .ForMember(d => d.DepartmentName, opt => opt.MapFrom(s => s.Doctor.Department.DepartmentName))
            .ForMember(d => d.HasPrescription, opt => opt.MapFrom(s => s.Prescription != null))
            .ForMember(d => d.HasBill, opt => opt.MapFrom(s => s.Bill != null));

        // Prescription
        CreateMap<Prescription, PrescriptionDto>()
            .ForMember(d => d.PatientName, opt => opt.MapFrom(s => s.Appointment.Patient.FullName))
            .ForMember(d => d.DoctorName, opt => opt.MapFrom(s => s.Appointment.Doctor.User.FullName));
        CreateMap<CreatePrescriptionDto, Prescription>();

        // Bill
        CreateMap<Bill, BillDto>()
            .ForMember(d => d.PatientName, opt => opt.MapFrom(s => s.Appointment.Patient.FullName))
            .ForMember(d => d.DoctorName, opt => opt.MapFrom(s => s.Appointment.Doctor.User.FullName));
    }
}
