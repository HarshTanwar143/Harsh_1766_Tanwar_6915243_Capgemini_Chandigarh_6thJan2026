using ECommerceOMS.Models;

namespace ECommerceOMS.ViewModels
{
    public class TopProduct
    {
        public string ProductName { get; set; } = string.Empty;
        public int TotalSold { get; set; }
    }

    public class DashboardViewModel
    {
        public IEnumerable<TopProduct> TopProducts { get; set; } = new List<TopProduct>();
        public IEnumerable<Order> PendingOrders { get; set; } = new List<Order>();
        public IEnumerable<Customer> RecentCustomers { get; set; } = new List<Customer>();
        public int TotalOrders { get; set; }
        public int TotalCustomers { get; set; }
        public decimal TotalRevenue { get; set; }
    }
}