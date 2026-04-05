using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartHospital.API.DTOs;
using SmartHospital.API.Models;
using SmartHospital.API.Repositories.Interfaces;

namespace SmartHospital.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DoctorsController : ControllerBase
{
    private readonly IDoctorRepository _doctorRepo;
    private readonly IUserRepository _userRepo;
    private readonly IMapper _mapper;

    public DoctorsController(IDoctorRepository doctorRepo, IUserRepository userRepo, IMapper mapper)
    {
        _doctorRepo = doctorRepo;
        _userRepo = userRepo;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var doctors = await _doctorRepo.GetAllAsync();
        return Ok(_mapper.Map<IEnumerable<DoctorDto>>(doctors));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var doctor = await _doctorRepo.GetByIdAsync(id);
        if (doctor == null) return NotFound();
        return Ok(_mapper.Map<DoctorDto>(doctor));
    }

    [HttpGet("department/{departmentId}")]
    public async Task<IActionResult> GetByDepartment(int departmentId)
    {
        var doctors = await _doctorRepo.GetByDepartmentAsync(departmentId);
        return Ok(_mapper.Map<IEnumerable<DoctorDto>>(doctors));
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create(CreateDoctorDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        if (await _userRepo.ExistsAsync(dto.Email))
            return BadRequest(new { message = "Email already registered." });

        var user = new User
        {
            FullName = dto.FullName,
            Email = dto.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
            Role = "Doctor"
        };
        await _userRepo.AddAsync(user);

        var doctor = new Doctor
        {
            UserId = user.UserId,
            DepartmentId = dto.DepartmentId,
            Specialization = dto.Specialization,
            ExperienceYears = dto.ExperienceYears,
            Availability = dto.Availability
        };
        await _doctorRepo.AddAsync(doctor);

        return Ok(new { message = "Doctor created.", doctor.DoctorId });
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update(int id, UpdateDoctorDto dto)
    {
        var doctor = await _doctorRepo.GetByIdAsync(id);
        if (doctor == null) return NotFound();

        doctor.DepartmentId = dto.DepartmentId;
        doctor.Specialization = dto.Specialization;
        doctor.ExperienceYears = dto.ExperienceYears;
        doctor.Availability = dto.Availability;

        await _doctorRepo.UpdateAsync(doctor);
        return Ok(new { message = "Updated." });
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        var doctor = await _doctorRepo.GetByIdAsync(id);
        if (doctor == null) return NotFound();

        await _doctorRepo.DeleteAsync(id);
        await _userRepo.DeleteAsync(doctor.UserId);
        return Ok(new { message = "Doctor deleted." });
    }
}
