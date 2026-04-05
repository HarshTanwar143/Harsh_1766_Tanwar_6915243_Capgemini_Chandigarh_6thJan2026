using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using SmartHospital.API.DTOs;
using SmartHospital.API.Models;
using SmartHospital.API.Repositories.Interfaces;

namespace SmartHospital.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class AppointmentsController : ControllerBase
{
    private readonly IAppointmentRepository _apptRepo;
    private readonly IDoctorRepository _doctorRepo;
    private readonly IMapper _mapper;

    public AppointmentsController(IAppointmentRepository apptRepo, IDoctorRepository doctorRepo, IMapper mapper)
    {
        _apptRepo = apptRepo;
        _doctorRepo = doctorRepo;
        _mapper = mapper;
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetAll()
    {
        var appts = await _apptRepo.GetAllAsync();
        return Ok(_mapper.Map<IEnumerable<AppointmentDto>>(appts));
    }

    [HttpGet("my")]
    public async Task<IActionResult> GetMine()
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var role = User.FindFirstValue(ClaimTypes.Role);

        if (role == "Doctor")
        {
            var doctor = await _doctorRepo.GetByUserIdAsync(userId);
            if (doctor == null) return NotFound();
            var appts = await _apptRepo.GetByDoctorAsync(doctor.DoctorId);
            return Ok(_mapper.Map<IEnumerable<AppointmentDto>>(appts));
        }
        else
        {
            var appts = await _apptRepo.GetByPatientAsync(userId);
            return Ok(_mapper.Map<IEnumerable<AppointmentDto>>(appts));
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var appt = await _apptRepo.GetByIdAsync(id);
        if (appt == null) return NotFound();
        return Ok(_mapper.Map<AppointmentDto>(appt));
    }

    [HttpPost]
    [Authorize(Roles = "Patient")]
    public async Task<IActionResult> Book(CreateAppointmentDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var doctor = await _doctorRepo.GetByIdAsync(dto.DoctorId);
        if (doctor == null) return BadRequest(new { message = "Doctor not found." });

        var appt = new Appointment
        {
            PatientId = userId,
            DoctorId = dto.DoctorId,
            AppointmentDate = dto.AppointmentDate,
            Status = "Booked"
        };

        await _apptRepo.AddAsync(appt);
        return Ok(new { message = "Appointment booked.", appt.AppointmentId });
    }

    [HttpPut("{id}/status")]
    [Authorize(Roles = "Admin,Doctor")]
    public async Task<IActionResult> UpdateStatus(int id, UpdateAppointmentStatusDto dto)
    {
        var appt = await _apptRepo.GetByIdAsync(id);
        if (appt == null) return NotFound();

        appt.Status = dto.Status;
        await _apptRepo.UpdateAsync(appt);
        return Ok(new { message = "Status updated." });
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin,Patient")]
    public async Task<IActionResult> Cancel(int id)
    {
        var appt = await _apptRepo.GetByIdAsync(id);
        if (appt == null) return NotFound();

        appt.Status = "Cancelled";
        await _apptRepo.UpdateAsync(appt);
        return Ok(new { message = "Appointment cancelled." });
    }
}
