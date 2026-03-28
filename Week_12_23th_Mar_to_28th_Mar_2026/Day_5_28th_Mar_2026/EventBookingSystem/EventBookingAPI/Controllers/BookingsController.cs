using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using EventBookingAPI.Data;
using EventBookingAPI.DTOs;
using EventBookingAPI.Models;

namespace EventBookingAPI.Controllers;

[ApiController]
[Route("api/bookings")]
[Authorize]
public class BookingsController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public BookingsController(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetMyBookings()
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var bookings = await _context.Bookings
            .Include(b => b.Event)
            .Where(b => b.UserId == userId)
            .ToListAsync();
        return Ok(_mapper.Map<List<BookingDto>>(bookings));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var booking = await _context.Bookings
            .Include(b => b.Event)
            .FirstOrDefaultAsync(b => b.Id == id && b.UserId == userId);
        if (booking == null) return NotFound(new { message = "Booking not found." });
        return Ok(_mapper.Map<BookingDto>(booking));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateBookingDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        var ev = await _context.Events.FindAsync(dto.EventId);
        if (ev == null)
            return NotFound(new { message = "Event not found." });

        if (ev.AvailableSeats < dto.SeatsBooked)
            return BadRequest(new { message = $"Only {ev.AvailableSeats} seats available." });

        ev.AvailableSeats -= dto.SeatsBooked;

        var booking = new Booking
        {
            EventId = dto.EventId,
            UserId = userId,
            SeatsBooked = dto.SeatsBooked,
            BookedAt = DateTime.UtcNow
        };

        _context.Bookings.Add(booking);
        await _context.SaveChangesAsync();

        await _context.Entry(booking).Reference(b => b.Event).LoadAsync();
        return Ok(_mapper.Map<BookingDto>(booking));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Cancel(int id)
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var booking = await _context.Bookings
            .Include(b => b.Event)
            .FirstOrDefaultAsync(b => b.Id == id && b.UserId == userId);

        if (booking == null)
            return NotFound(new { message = "Booking not found." });

        if (booking.Event != null)
            booking.Event.AvailableSeats += booking.SeatsBooked;

        _context.Bookings.Remove(booking);
        await _context.SaveChangesAsync();
        return Ok(new { message = "Booking cancelled successfully." });
    }
}
