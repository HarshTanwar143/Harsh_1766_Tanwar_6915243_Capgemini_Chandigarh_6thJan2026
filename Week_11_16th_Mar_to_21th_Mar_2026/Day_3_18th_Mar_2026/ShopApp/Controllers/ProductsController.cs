using Microsoft.AspNetCore.Mvc;
using ShopApp.Models;

namespace ShopApp.Controllers
{
    public class ProductsController : Controller
    {
        private readonly AppDbContext _context;

        public ProductsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: /Products/Index
        public IActionResult Index()
        {
            // ✅ Pass cart count to navbar via ViewBag
            var cart = HttpContext.Session
                .GetObjectFromJson<List<CartItem>>("Cart") ?? new();
            ViewBag.CartCount = cart.Sum(c => c.Quantity);

            var products = _context.Products.ToList();
            return View(products);
        }

        // POST: Add product to cart
        [HttpPost]
        public IActionResult AddToCart(int productId, int quantity = 1)
        {
            var product = _context.Products.Find(productId);
            if (product == null) return NotFound();

            // ✅ Get existing cart from session
            var cart = HttpContext.Session
                .GetObjectFromJson<List<CartItem>>("Cart") ?? new();

            // Check if item already in cart
            var existing = cart.FirstOrDefault(c => c.ProductId == productId);
            if (existing != null)
            {
                existing.Quantity += quantity;
            }
            else
            {
                cart.Add(new CartItem
                {
                    ProductId = product.Id,
                    ProductName = product.Name,
                    Price = product.Price,
                    Quantity = quantity,
                    ImageUrl = product.ImageUrl
                });
            }

            // ✅ Save updated cart back to session
            HttpContext.Session.SetObjectAsJson("Cart", cart);
            TempData["Success"] = $"{product.Name} added to cart!";
            return RedirectToAction("Index");
        }

        // POST: Add new product (admin)
        [HttpPost]
        public IActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Products.Add(product);
                _context.SaveChanges();
                TempData["Success"] = "Product added!";
            }
            return RedirectToAction("Index");
        }
    }
}