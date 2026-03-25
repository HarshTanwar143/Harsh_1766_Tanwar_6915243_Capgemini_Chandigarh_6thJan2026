using ECommerceOMS.Data;
using ECommerceOMS.Models;
using ECommerceOMS.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace ECommerceOMS.Controllers
{

    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly AppDbContext _context;
        public AdminController(AppDbContext context) => _context = context;

        public async Task<IActionResult> Dashboard()
        {
            var topProducts = await _context.OrderItems
                .Include(oi => oi.Product)
                .GroupBy(oi => oi.Product!.Name)
                .Select(g => new TopProduct
                {
                    ProductName = g.Key,
                    TotalSold = g.Sum(x => x.Quantity)
                })
                .OrderByDescending(x => x.TotalSold)
                .Take(5)
                .ToListAsync();

            var pendingOrders = await _context.Orders
                .Include(o => o.Customer)
                .Where(o => o.Status == OrderStatus.Pending
                         || o.Status == OrderStatus.Processing)
                .ToListAsync();

            var recentCustomers = await _context.Customers
                .Take(5)
                .ToListAsync();

            int totalOrders = await _context.Orders.CountAsync();
            int totalCustomers = await _context.Customers.CountAsync();
            decimal totalRevenue = await _context.Orders.SumAsync(o => (decimal?)o.TotalAmount) ?? 0;

            var vm = new DashboardViewModel
            {
                TopProducts = topProducts,
                PendingOrders = pendingOrders,
                RecentCustomers = recentCustomers,
                TotalOrders = totalOrders,
                TotalCustomers = totalCustomers,
                TotalRevenue = totalRevenue
            };

            return View(vm);
        }
    }
}