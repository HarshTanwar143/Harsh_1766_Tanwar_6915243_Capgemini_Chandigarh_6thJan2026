using HotelApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelApp.Controllers
{
    public class BookingsController : Controller
    {
        private readonly AppDbContext _context;

        public BookingsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: /Bookings/Index — Admin Dashboard
        public IActionResult Index()
        {
            var bookings = _context.Bookings
                .Include(b => b.Room)
                .Include(b => b.Customer)
                .ToList();

            // ✅ Dashboard stats for charts
            ViewBag.TotalBookings = bookings.Count;
            ViewBag.ConfirmedCount = bookings.Count(b => b.Status == "Confirmed");
            ViewBag.CancelledCount = bookings.Count(b => b.Status == "Cancelled");
            ViewBag.TotalRevenue = bookings
                .Where(b => b.Status == "Confirmed")
                .Sum(b => (b.CheckOutDate - b.CheckInDate).Days
                          * b.Room.PricePerNight);

            return View(bookings);
        }

        // GET: /Bookings/Create
        [HttpGet]
        public IActionResult Create(int roomId)
        {
            ViewBag.Room = _context.Rooms.Find(roomId);
            return View();
        }

        // POST: /Bookings/Create
        [HttpPost]
        public IActionResult Create(Booking booking, Customer customer)
        {
            // Save customer first
            _context.Customers.Add(customer);
            _context.SaveChanges();

            // Link customer to booking
            booking.CustomerId = customer.Id;
            booking.Status = "Confirmed";
            _context.Bookings.Add(booking);

            // Mark room as unavailable
            var room = _context.Rooms.Find(booking.RoomId);
            if (room != null) room.IsAvailable = false;

            _context.SaveChanges();

            TempData["Success"] = $"Booking confirmed for {customer.Name}!";
            return RedirectToAction("Index");
        }

        // POST: Cancel Booking
        [HttpPost]
        public IActionResult Cancel(int id)
        {
            var booking = _context.Bookings.Find(id);
            if (booking != null)
            {
                booking.Status = "Cancelled";
                // Mark room available again
                var room = _context.Rooms.Find(booking.RoomId);
                if (room != null) room.IsAvailable = true;
                _context.SaveChanges();
                TempData["Success"] = "Booking cancelled successfully!";
            }
            return RedirectToAction("Index");
        }
    }
}