using ECommerceOrderAPI.Models;
using log4net;

// ─── User Service ───────────────────────────────────────────────

public interface IUserService
{
    string Login(LoginRequest request);
}

public class UserService : IUserService
{
    private static readonly ILog log = LogManager.GetLogger(typeof(UserService));

    private readonly Dictionary<string, string> _users = new()
    {
        { "ravi@gmail.com", "pass123" },
        { "test@gmail.com", "test123" }
    };

    public string Login(LoginRequest request)
    {
        log.Info($"Login attempt: {request.Email}");

        if (!_users.ContainsKey(request.Email))
        {
            log.Warn($"User not found: {request.Email}");
            return "User not found";
        }

        if (_users[request.Email] != request.Password)
        {
            log.Warn($"Invalid password for: {request.Email}");
            return "Invalid password";
        }

        log.Info($"Login successful: {request.Email}");
        return "Login successful";
    }
}

// ─── Product Service ─────────────────────────────────────────────

public interface IProductService
{
    Product? GetProduct(int id);
}

public class ProductService : IProductService
{
    private static readonly ILog log = LogManager.GetLogger(typeof(ProductService));

    private readonly List<Product> _products = new()
    {
        new Product { Id = 1, Name = "Laptop", Price = 60000 },
        new Product { Id = 2, Name = "Mouse",  Price = 1500  },
        new Product { Id = 3, Name = "Keyboard", Price = 2500 }
    };

    public Product? GetProduct(int id)
    {
        log.Info($"Product fetch request for ProductId: {id}");

        var product = _products.FirstOrDefault(p => p.Id == id);

        if (product == null)
            log.Warn($"Product not found: ProductId {id}");
        else
            log.Info($"Product found: {product.Name}");

        return product;
    }
}

// ─── Order Service ────────────────────────────────────────────────

public interface IOrderService
{
    string PlaceOrder(OrderRequest request);
}

public class OrderService : IOrderService
{
    private static readonly ILog log = LogManager.GetLogger(typeof(OrderService));
    private readonly IProductService _productService;

    public OrderService(IProductService productService)
    {
        _productService = productService;
    }

    public string PlaceOrder(OrderRequest request)
    {
        log.Info($"Order creation started for UserId: {request.UserId}, ProductId: {request.ProductId}");

        try
        {
            if (request.Quantity <= 0)
                throw new ArgumentException("Quantity must be greater than 0");

            var product = _productService.GetProduct(request.ProductId);
            if (product == null)
                throw new Exception($"Product {request.ProductId} not found");

            var total = product.Price * request.Quantity;
            log.Info($"Order placed successfully. UserId: {request.UserId}, Product: {product.Name}, Total: {total}");
            return $"Order placed! Total: ₹{total}";
        }
        catch (Exception ex)
        {
            log.Error($"Order failed for UserId: {request.UserId}", ex);
            return $"Order failed: {ex.Message}";
        }
    }
}

// ─── Payment Service ──────────────────────────────────────────────

public interface IPaymentService
{
    string ProcessPayment(PaymentRequest request);
}

public class PaymentService : IPaymentService
{
    private static readonly ILog log = LogManager.GetLogger(typeof(PaymentService));

    public string ProcessPayment(PaymentRequest request)
    {
        log.Info($"Payment request received. OrderId: {request.OrderId}, Amount: ₹{request.Amount}");

        try
        {
            var startTime = DateTime.Now;

            // Simulate delay: if amount > 50000, simulate slow external gateway
            if (request.Amount > 50000)
            {
                Thread.Sleep(6000); // simulate 6 second delay
            }

            var duration = DateTime.Now - startTime;

            if (duration.TotalSeconds > 5)
            {
                log.Warn($"Payment gateway delay > 5 sec for OrderId: {request.OrderId}. Duration: {duration.TotalSeconds:F1}s");
            }

            // Simulate failure: if method is "InvalidCard"
            if (request.Method == "InvalidCard")
                throw new Exception("External payment API rejected the card");

            log.Info($"Payment successful for OrderId: {request.OrderId}");
            return "Payment successful";
        }
        catch (Exception ex)
        {
            log.Error($"Payment failed for OrderId: {request.OrderId}", ex);
            return $"Payment failed: {ex.Message}";
        }
    }
}
