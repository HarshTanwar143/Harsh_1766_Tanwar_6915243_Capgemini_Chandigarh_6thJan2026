using System.Diagnostics;
using log4net;
using PerformanceLoggingAPI.Models;

// ─── Product Service ──────────────────────────────────────────────

public interface IProductService
{
    List<Product> GetAllProducts();
    Product? GetProductById(int id);
}

public class ProductService : IProductService
{
    private static readonly ILog log = LogManager.GetLogger(typeof(ProductService));

    private readonly List<Product> _products = new()
    {
        new Product { Id = 1, Name = "Laptop",   Price = 60000 },
        new Product { Id = 2, Name = "Mouse",    Price = 1500  },
        new Product { Id = 3, Name = "Keyboard", Price = 2500  },
        new Product { Id = 4, Name = "Monitor",  Price = 18000 }
    };

    public List<Product> GetAllProducts()
    {
        var startTime = DateTime.Now;
        log.Info("API Started — GetAllProducts");

        try
        {
            // Simulate normal processing
            Thread.Sleep(500);

            var duration = DateTime.Now - startTime;
            log.Info($"GetAllProducts completed in {duration.TotalSeconds:F2} sec");

            return _products;
        }
        catch (Exception ex)
        {
            log.Error("API failed — GetAllProducts", ex);
            throw;
        }
    }

    public Product? GetProductById(int id)
    {
        var startTime = DateTime.Now;
        log.Info($"API Started — GetProductById: {id}");

        try
        {
            // Simulate slow DB for id=99 (heavy product lookup)
            if (id == 99)
                Thread.Sleep(4000); // 4 seconds — triggers WARN
            else
                Thread.Sleep(300);

            var duration = DateTime.Now - startTime;

            if (duration.TotalSeconds > 3)
                log.Warn($"Slow API detected — GetProductById took {duration.TotalSeconds:F1} sec");
            else
                log.Info($"GetProductById completed in {duration.TotalSeconds:F2} sec");

            var product = _products.FirstOrDefault(p => p.Id == id);

            if (product == null)
                log.Warn($"Product not found: Id={id}");

            return product;
        }
        catch (Exception ex)
        {
            log.Error($"API failed — GetProductById: {id}", ex);
            throw;
        }
    }
}

// ─── Order Service ────────────────────────────────────────────────

public interface IOrderService
{
    List<Order> GetAllOrders();
    string PlaceOrder(int productId, int quantity);
}

public class OrderService : IOrderService
{
    private static readonly ILog log = LogManager.GetLogger(typeof(OrderService));

    private readonly List<Order> _orders = new()
    {
        new Order { Id = 1, ProductId = 1, Quantity = 2, Total = 120000 },
        new Order { Id = 2, ProductId = 2, Quantity = 3, Total = 4500   }
    };

    private readonly List<Product> _products = new()
    {
        new Product { Id = 1, Name = "Laptop",   Price = 60000 },
        new Product { Id = 2, Name = "Mouse",    Price = 1500  },
        new Product { Id = 3, Name = "Keyboard", Price = 2500  }
    };

    public List<Order> GetAllOrders()
    {
        var startTime = DateTime.Now;
        log.Info("API Started — GetAllOrders");

        try
        {
            // Simulate heavy DB query (joins, aggregations)
            Thread.Sleep(3500); // triggers WARN

            var duration = DateTime.Now - startTime;

            if (duration.TotalSeconds > 3)
                log.Warn($"Slow API detected — GetAllOrders took {duration.TotalSeconds:F1} sec");
            else
                log.Info($"GetAllOrders completed in {duration.TotalSeconds:F2} sec");

            return _orders;
        }
        catch (Exception ex)
        {
            log.Error("API failed — GetAllOrders", ex);
            throw;
        }
    }

    public string PlaceOrder(int productId, int quantity)
    {
        var startTime = DateTime.Now;
        log.Info($"API Started — PlaceOrder. ProductId={productId}, Qty={quantity}");

        try
        {
            if (quantity <= 0)
                throw new ArgumentException("Quantity must be greater than 0");

            if (productId == 0)
                throw new NullReferenceException("ProductId cannot be 0");

            var product = _products.FirstOrDefault(p => p.Id == productId)
                          ?? throw new Exception($"Product {productId} not found");

            Thread.Sleep(800);

            var total = product.Price * quantity;
            var order = new Order
            {
                Id        = _orders.Count + 1,
                ProductId = productId,
                Quantity  = quantity,
                Total     = total
            };
            _orders.Add(order);

            var duration = DateTime.Now - startTime;

            if (duration.TotalSeconds > 3)
                log.Warn($"Slow API detected — PlaceOrder took {duration.TotalSeconds:F1} sec");
            else
                log.Info($"PlaceOrder completed in {duration.TotalSeconds:F2} sec. Total=₹{total}");

            return $"Order placed! Total: ₹{total}";
        }
        catch (Exception ex)
        {
            var duration = DateTime.Now - startTime;
            log.Error($"API failed — PlaceOrder after {duration.TotalSeconds:F2} sec", ex);
            return $"Order failed: {ex.Message}";
        }
    }
}

// ─── Report Service (intentionally crashes) ───────────────────────

public interface IReportService
{
    Report GenerateReport();
}

public class ReportService : IReportService
{
    private static readonly ILog log = LogManager.GetLogger(typeof(ReportService));

    public Report GenerateReport()
    {
        var startTime = DateTime.Now;
        log.Info("API Started — GenerateReport");

        try
        {
            // Simulate very slow report generation
            Thread.Sleep(5000);

            // Simulate a crash (null reference)
            string? nullValue = null;
            var length = nullValue!.Length; // intentional crash

            var duration = DateTime.Now - startTime;
            log.Info($"GenerateReport completed in {duration.TotalSeconds:F2} sec");

            return new Report();
        }
        catch (Exception ex)
        {
            var duration = DateTime.Now - startTime;
            log.Error($"API failed — GenerateReport after {duration.TotalSeconds:F2} sec", ex);
            throw;
        }
    }
}
