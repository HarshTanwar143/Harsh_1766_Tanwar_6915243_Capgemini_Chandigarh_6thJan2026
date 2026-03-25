using ECommerceOMS.Models;

namespace ECommerceOMS.ViewModels
{
    public class OrderDetailViewModel
    {
        public Order Order { get; set; } = new Order();
        public IEnumerable<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
        public ShippingDetail? ShippingDetail { get; set; }
        public Customer? Customer { get; set; }
    }
}