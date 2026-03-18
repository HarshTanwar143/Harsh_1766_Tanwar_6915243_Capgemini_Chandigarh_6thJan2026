using System.ComponentModel.DataAnnotations;

namespace HotelApp.Models
{
    public class Room
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Room number is required")]
        public string RoomNumber { get; set; }

        [Required(ErrorMessage = "Room type is required")]
        public string RoomType { get; set; }  // Single, Double, Suite

        [Required(ErrorMessage = "Price is required")]
        [Range(0.01, 999999, ErrorMessage = "Price must be greater than 0")]
        public decimal PricePerNight { get; set; }

        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }

        public string Amenities { get; set; }

        public string ImageUrl { get; set; }

        public bool IsAvailable { get; set; } = true;

        // ✅ Navigation property → one room can have many bookings
        public List<Booking> Bookings { get; set; } = new();
    }
}