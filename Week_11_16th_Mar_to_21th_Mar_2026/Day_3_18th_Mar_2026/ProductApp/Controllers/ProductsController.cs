using Microsoft.AspNetCore.Mvc;
using ProductApp.Models;

namespace ProductApp.Controllers
{
    public class ProductsController : Controller
    {
        private readonly AppDbContext _context;

        public ProductsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: /Products/Index
        // ✅ Search bar filtering by name
        public IActionResult Index(string search)
        {
            var products = _context.Products.AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                products = products.Where(p =>
                    p.Name.Contains(search));
            }

            ViewBag.Search = search;
            return View(products.ToList());
        }

        // GET: /Products/Details/5
        public IActionResult Details(int id)
        {
            var product = _context.Products.Find(id);
            if (product == null) return NotFound();
            return View(product);
        }

        // GET: /Products/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Products/Create
        [HttpPost]
        public IActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Products.Add(product);
                _context.SaveChanges();
                TempData["Success"] = "Product added successfully!";
                return RedirectToAction("Index");
            }
            return View(product);
        }
    }
}