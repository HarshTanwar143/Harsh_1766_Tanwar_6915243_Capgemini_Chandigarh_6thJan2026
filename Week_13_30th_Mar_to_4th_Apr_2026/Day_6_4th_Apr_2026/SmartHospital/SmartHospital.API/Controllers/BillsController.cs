using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using SmartHospital.API.DTOs;
using SmartHospital.API.Repositories.Interfaces;

namespace SmartHospital.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class BillsController : ControllerBase
{
    private readonly IBillRepository _billRepo;
    private readonly IMapper _mapper;

    public BillsController(IBillRepository billRepo, IMapper mapper)
    {
        _billRepo = billRepo;
        _mapper = mapper;
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetAll()
    {
        var bills = await _billRepo.GetAllAsync();
        return Ok(_mapper.Map<IEnumerable<BillDto>>(bills));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var bill = await _billRepo.GetByIdAsync(id);
        if (bill == null) return NotFound();
        return Ok(_mapper.Map<BillDto>(bill));
    }

    [HttpGet("my")]
    [Authorize(Roles = "Patient")]
    public async Task<IActionResult> GetMine()
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var bills = await _billRepo.GetByPatientAsync(userId);
        return Ok(_mapper.Map<IEnumerable<BillDto>>(bills));
    }

    [HttpGet("appointment/{appointmentId}")]
    public async Task<IActionResult> GetByAppointment(int appointmentId)
    {
        var bill = await _billRepo.GetByAppointmentAsync(appointmentId);
        if (bill == null) return NotFound();
        return Ok(_mapper.Map<BillDto>(bill));
    }

    [HttpPut("{id}/payment")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdatePayment(int id, UpdatePaymentStatusDto dto)
    {
        var bill = await _billRepo.GetByIdAsync(id);
        if (bill == null) return NotFound();

        bill.PaymentStatus = dto.PaymentStatus;
        await _billRepo.UpdateAsync(bill);
        return Ok(new { message = $"Payment status updated to {dto.PaymentStatus}." });
    }
}
