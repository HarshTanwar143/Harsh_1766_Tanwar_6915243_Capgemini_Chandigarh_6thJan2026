using System.ComponentModel.DataAnnotations;

namespace ECommerceOMS.Models
{
    public class ShippingDetail
    {
        public int ShippingDetailId { get; set; }

        [Required]
        [StringLength(300)]
        public string ShippingAddress { get; set; } = string.Empty;

        [StringLength(100)]
        public string? Carrier { get; set; }        

        [StringLength(100)]
        public string? TrackingNumber { get; set; }

        public DateTime? ShippedDate { get; set; }  

        public DateTime? EstimatedDelivery { get; set; }

        public bool IsDelivered { get; set; } = false;

        public int OrderId { get; set; }

        // Navigation
        public Order Order { get; set; } = new Order();
    }
}