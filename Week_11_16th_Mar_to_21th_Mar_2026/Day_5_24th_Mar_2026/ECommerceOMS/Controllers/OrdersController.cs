using ECommerceOMS.Data;
using ECommerceOMS.Models;
using ECommerceOMS.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ECommerceOMS.Controllers
{
    public class OrdersController : Controller
    {
        private readonly AppDbContext _context;
        public OrdersController(AppDbContext context) => _context = context;

        public async Task<IActionResult> Index()
        {
            var orders = await _context.Orders
                .Include(o => o.Customer)       
                .Include(o => o.ShippingDetail)  
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();
            return View(orders);
        }

        public async Task<IActionResult> Details(int id)
        {
            var order = await _context.Orders
                .Include(o => o.Customer)       
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Product)
                .Include(o => o.ShippingDetail)
                .FirstOrDefaultAsync(o => o.OrderId == id);

            if (order == null) return NotFound();

            var vm = new OrderDetailViewModel
            {
                Order = order,
                Customer = order.Customer,       
                OrderItems = order.OrderItems,
                ShippingDetail = order.ShippingDetail
            };
            return View(vm);
        }

        public IActionResult Create()
        {
            ViewBag.Customers = new SelectList(_context.Customers, "CustomerId", "FullName");
            ViewBag.Products = _context.Products.Include(p => p.Category).ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int customerId, int[] productIds, int[] quantities)
        {
            var order = new Order
            {
                CustomerId = customerId,
                OrderDate = DateTime.Now,
                Status = OrderStatus.Pending
            };

            decimal total = 0;
            for (int i = 0; i < productIds.Length; i++)
            {
                var product = await _context.Products.FindAsync(productIds[i]);
                if (product != null && quantities[i] > 0)
                {
                    order.OrderItems.Add(new OrderItem
                    {
                        ProductId = product.ProductId,
                        Quantity = quantities[i],
                        UnitPrice = product.Price
                    });
                    total += product.Price * quantities[i];
                }
            }
            order.TotalAmount = total;

            var customer = await _context.Customers.FindAsync(customerId);
            order.ShippingDetail = new ShippingDetail
            {
                ShippingAddress = customer?.Address ?? "N/A",
                IsDelivered = false
            };

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateStatus(int id, OrderStatus status)
        {
            var order = await _context.Orders
                .Include(o => o.ShippingDetail)
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Product)  
                .FirstOrDefaultAsync(o => o.OrderId == id);

            if (order != null)
            {
                order.Status = status;

                if (order.ShippingDetail != null)
                {
                    if (status == OrderStatus.Shipped)
                    {
                        order.ShippingDetail.ShippedDate = DateTime.Now;
                        order.ShippingDetail.EstimatedDelivery = DateTime.Now.AddDays(5);
                        order.ShippingDetail.Carrier = "BlueDart";
                        order.ShippingDetail.TrackingNumber = "BD" + new Random().Next(100000, 999999).ToString();
                        order.ShippingDetail.IsDelivered = false;
                    }
                    else if (status == OrderStatus.Delivered)
                    {
                        order.ShippingDetail.IsDelivered = true;

                        foreach (var item in order.OrderItems)
                        {
                            if (item.Product != null)
                            {
                                item.Product.Stock -= item.Quantity;

                                if (item.Product.Stock < 0)
                                    item.Product.Stock = 0;
                            }
                        }
                    }
                }

                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}