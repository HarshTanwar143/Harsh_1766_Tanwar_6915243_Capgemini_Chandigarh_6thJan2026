using System.ComponentModel.DataAnnotations;

namespace HotelApp.Models
{
    public class Booking
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Check-in date is required")]
        [DataType(DataType.Date)]
        public DateTime CheckInDate { get; set; }

        [Required(ErrorMessage = "Check-out date is required")]
        [DataType(DataType.Date)]
        public DateTime CheckOutDate { get; set; }

        public string Status { get; set; } = "Confirmed"; // Confirmed, Cancelled

        // ✅ Foreign Keys
        public int RoomId { get; set; }
        public Room Room { get; set; }

        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        // ✅ Calculated property
        public int TotalNights =>
            (CheckOutDate - CheckInDate).Days > 0
            ? (CheckOutDate - CheckInDate).Days : 1;
    }
}