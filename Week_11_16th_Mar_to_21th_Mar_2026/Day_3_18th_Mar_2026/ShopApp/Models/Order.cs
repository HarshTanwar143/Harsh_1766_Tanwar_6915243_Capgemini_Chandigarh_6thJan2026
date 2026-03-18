using System.ComponentModel.DataAnnotations;

namespace ShopApp.Models
{
    public class Order
    {
        public int Id { get; set; }

        // Address Details
        [Required(ErrorMessage = "Full name is required")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; }

        [Required(ErrorMessage = "City is required")]
        public string City { get; set; }

        [Required(ErrorMessage = "Pincode is required")]
        public string Pincode { get; set; }

        // Payment Details
        [Required(ErrorMessage = "Card number is required")]
        public string CardNumber { get; set; }

        [Required(ErrorMessage = "Expiry is required")]
        public string CardExpiry { get; set; }

        [Required(ErrorMessage = "CVV is required")]
        public string CardCVV { get; set; }

        public decimal TotalAmount { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public string Status { get; set; } = "Confirmed";
    }
}