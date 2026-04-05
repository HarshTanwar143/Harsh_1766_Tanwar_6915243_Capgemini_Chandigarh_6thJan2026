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
public class PrescriptionsController : ControllerBase
{
    private readonly IPrescriptionRepository _presRepo;
    private readonly IAppointmentRepository _apptRepo;
    private readonly IDoctorRepository _doctorRepo;
    private readonly IBillRepository _billRepo;
    private readonly IMapper _mapper;

    public PrescriptionsController(
        IPrescriptionRepository presRepo,
        IAppointmentRepository apptRepo,
        IDoctorRepository doctorRepo,
        IBillRepository billRepo,
        IMapper mapper)
    {
        _presRepo = presRepo;
        _apptRepo = apptRepo;
        _doctorRepo = doctorRepo;
        _billRepo = billRepo;
        _mapper = mapper;
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetAll()
    {
        var list = await _presRepo.GetAllAsync();
        return Ok(_mapper.Map<IEnumerable<PrescriptionDto>>(list));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var p = await _presRepo.GetByIdAsync(id);
        if (p == null) return NotFound();
        return Ok(_mapper.Map<PrescriptionDto>(p));
    }

    [HttpGet("appointment/{appointmentId}")]
    public async Task<IActionResult> GetByAppointment(int appointmentId)
    {
        var p = await _presRepo.GetByAppointmentAsync(appointmentId);
        if (p == null) return NotFound();
        return Ok(_mapper.Map<PrescriptionDto>(p));
    }

    /// <summary>
    /// Doctor creates a prescription for an appointment
    /// </summary>
    [HttpPost]
    [Authorize(Roles = "Doctor")]
    public async Task<IActionResult> Create(CreatePrescriptionDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var appt = await _apptRepo.GetByIdAsync(dto.AppointmentId);
        if (appt == null) return BadRequest(new { message = "Appointment not found." });

        // Verify this doctor owns this appointment
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var doctor = await _doctorRepo.GetByUserIdAsync(userId);
        if (doctor == null || appt.DoctorId != doctor.DoctorId)
            return Forbid();

        if (appt.Prescription != null)
            return BadRequest(new { message = "Prescription already exists for this appointment." });

        var prescription = new Prescription
        {
            AppointmentId = dto.AppointmentId,
            Diagnosis = dto.Diagnosis,
            Medicines = dto.Medicines,
            Notes = dto.Notes,
            MedicineCharges = dto.MedicineCharges,
            IsApproved = false
        };

        await _presRepo.AddAsync(prescription);
        return Ok(new { message = "Prescription created.", prescription.PrescriptionId });
    }

    /// <summary>
    /// Doctor approves prescription → automatically creates/updates Bill with medicine charges
    /// </summary>
    [HttpPost("{id}/approve")]
    [Authorize(Roles = "Doctor")]
    public async Task<IActionResult> Approve(int id, ApprovePrescriptionDto dto)
    {
        var prescription = await _presRepo.GetByIdAsync(id);
        if (prescription == null) return NotFound();

        // Verify the doctor owns this
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var doctor = await _doctorRepo.GetByUserIdAsync(userId);
        var appt = await _apptRepo.GetByIdAsync(prescription.AppointmentId);
        if (doctor == null || appt == null || appt.DoctorId != doctor.DoctorId)
            return Forbid();

        // Approve prescription
        prescription.IsApproved = true;
        prescription.MedicineCharges = dto.MedicineCharges;
        await _presRepo.UpdateAsync(prescription);

        // Mark appointment as Completed
        appt.Status = "Completed";
        await _apptRepo.UpdateAsync(appt);

        // Create or update bill
        const decimal consultationFee = 500m; // Default consultation fee
        var existingBill = await _billRepo.GetByAppointmentAsync(prescription.AppointmentId);

        if (existingBill == null)
        {
            var bill = new Bill
            {
                AppointmentId = prescription.AppointmentId,
                ConsultationFee = consultationFee,
                MedicineCharges = dto.MedicineCharges,
                TotalAmount = consultationFee + dto.MedicineCharges,
                PaymentStatus = "Unpaid",
                BilledAt = DateTime.Now
            };
            await _billRepo.AddAsync(bill);
            return Ok(new { message = "Prescription approved. Bill generated.", bill.BillId, bill.TotalAmount });
        }
        else
        {
            existingBill.MedicineCharges = dto.MedicineCharges;
            existingBill.TotalAmount = existingBill.ConsultationFee + dto.MedicineCharges;
            await _billRepo.UpdateAsync(existingBill);
            return Ok(new { message = "Prescription approved. Bill updated.", existingBill.BillId, existingBill.TotalAmount });
        }
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Doctor")]
    public async Task<IActionResult> Update(int id, CreatePrescriptionDto dto)
    {
        var prescription = await _presRepo.GetByIdAsync(id);
        if (prescription == null) return NotFound();

        if (prescription.IsApproved)
            return BadRequest(new { message = "Cannot edit an approved prescription." });

        prescription.Diagnosis = dto.Diagnosis;
        prescription.Medicines = dto.Medicines;
        prescription.Notes = dto.Notes;
        prescription.MedicineCharges = dto.MedicineCharges;

        await _presRepo.UpdateAsync(prescription);
        return Ok(new { message = "Prescription updated." });
    }
}
