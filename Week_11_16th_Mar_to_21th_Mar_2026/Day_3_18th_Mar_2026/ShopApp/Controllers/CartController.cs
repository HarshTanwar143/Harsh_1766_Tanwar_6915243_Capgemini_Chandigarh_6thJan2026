using Microsoft.AspNetCore.Mvc;
using ShopApp.Models;

namespace ShopApp.Controllers
{
    public class CartController : Controller
    {
        private readonly AppDbContext _context;

        public CartController(AppDbContext context)
        {
            _context = context;
        }

        // GET: /Cart/Index
        public IActionResult Index()
        {
            var cart = HttpContext.Session
                .GetObjectFromJson<List<CartItem>>("Cart") ?? new();
            ViewBag.CartCount = cart.Sum(c => c.Quantity);
            return View(cart);
        }

        // POST: Update quantity
        [HttpPost]
        public IActionResult UpdateQuantity(int productId, int quantity)
        {
            var cart = HttpContext.Session
                .GetObjectFromJson<List<CartItem>>("Cart") ?? new();

            var item = cart.FirstOrDefault(c => c.ProductId == productId);
            if (item != null)
            {
                if (quantity <= 0)
                    cart.Remove(item);
                else
                    item.Quantity = quantity;
            }

            HttpContext.Session.SetObjectAsJson("Cart", cart);
            return RedirectToAction("Index");
        }

        // POST: Remove item from cart
        [HttpPost]
        public IActionResult Remove(int productId)
        {
            var cart = HttpContext.Session
                .GetObjectFromJson<List<CartItem>>("Cart") ?? new();

            cart.RemoveAll(c => c.ProductId == productId);
            HttpContext.Session.SetObjectAsJson("Cart", cart);
            TempData["Success"] = "Item removed from cart!";
            return RedirectToAction("Index");
        }

        // GET: /Cart/Checkout
        [HttpGet]
        public IActionResult Checkout()
        {
            var cart = HttpContext.Session
                .GetObjectFromJson<List<CartItem>>("Cart") ?? new();

            if (cart.Count == 0)
                return RedirectToAction("Index");

            ViewBag.Cart = cart;
            ViewBag.Total = cart.Sum(c => c.Total);
            ViewBag.CartCount = cart.Sum(c => c.Quantity);
            return View(new Order());
        }

        // POST: /Cart/PlaceOrder
        [HttpPost]
        public IActionResult PlaceOrder(Order order)
        {
            var cart = HttpContext.Session
                .GetObjectFromJson<List<CartItem>>("Cart") ?? new();

            if (ModelState.IsValid)
            {
                order.TotalAmount = cart.Sum(c => c.Total);
                order.OrderDate = DateTime.Now;
                order.Status = "Confirmed";

                _context.Orders.Add(order);
                _context.SaveChanges();

                // ✅ Clear cart after order
                HttpContext.Session.Remove("Cart");
                TempData["OrderId"] = order.Id;
                TempData["CustomerName"] = order.FullName;
                TempData["Total"] = order.TotalAmount.ToString("0.00");
                return RedirectToAction("Confirmation");
            }

            ViewBag.Cart = cart;
            ViewBag.Total = cart.Sum(c => c.Total);
            return View("Checkout", order);
        }

        // GET: /Cart/Confirmation
        public IActionResult Confirmation()
        {
            return View();
        }
    }
}