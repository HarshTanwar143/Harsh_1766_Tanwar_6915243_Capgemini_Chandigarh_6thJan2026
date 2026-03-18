using HotelApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace HotelApp.Controllers
{
    public class RoomsController : Controller
    {
        private readonly AppDbContext _context;

        public RoomsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: /Rooms/Index
        public IActionResult Index(DateTime? checkIn, DateTime? checkOut)
        {
            var rooms = _context.Rooms.AsQueryable();

            // ✅ Only filter if BOTH dates are provided
            if (checkIn.HasValue && checkOut.HasValue)
            {
                var bookedRoomIds = _context.Bookings
                    .Where(b => b.Status == "Confirmed" &&
                                b.CheckInDate < checkOut &&
                                b.CheckOutDate > checkIn)
                    .Select(b => b.RoomId)
                    .ToList();

                rooms = rooms.Where(r => !bookedRoomIds.Contains(r.Id));

                ViewBag.Filtered = true; // ✅ flag to show filtered message
            }
            else
            {
                ViewBag.Filtered = false; // ✅ no filter applied
            }

            ViewBag.CheckIn = checkIn?.ToString("yyyy-MM-dd");
            ViewBag.CheckOut = checkOut?.ToString("yyyy-MM-dd");

            return View(rooms.ToList());
        }

        // POST: Add Room
        [HttpPost]
        public IActionResult Create(Room room)
        {
            if (ModelState.IsValid)
            {
                _context.Rooms.Add(room);
                _context.SaveChanges();
                TempData["Success"] = "Room added successfully!";
            }
            return RedirectToAction("Index");
        }
    }
}