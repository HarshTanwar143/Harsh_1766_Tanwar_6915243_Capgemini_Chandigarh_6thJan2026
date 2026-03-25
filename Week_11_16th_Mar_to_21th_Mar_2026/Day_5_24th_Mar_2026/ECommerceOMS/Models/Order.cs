using System.ComponentModel.DataAnnotations;

namespace ECommerceOMS.Models
{
    public enum OrderStatus { Pending, Processing, Shipped, Delivered, Cancelled }

    public class Order
    {
        public int OrderId { get; set; }

        [Required]
        public DateTime OrderDate { get; set; } = DateTime.Now;

        public OrderStatus Status { get; set; } = OrderStatus.Pending;

        public decimal TotalAmount { get; set; }  

        public int CustomerId { get; set; }

        // Navigation
        public Customer Customer { get; set; } = new Customer();
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
        public ShippingDetail? ShippingDetail { get; set; }
    }
}