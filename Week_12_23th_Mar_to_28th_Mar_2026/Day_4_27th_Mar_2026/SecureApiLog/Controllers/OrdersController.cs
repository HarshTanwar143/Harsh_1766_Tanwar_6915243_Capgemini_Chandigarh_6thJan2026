using log4net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class OrdersController : ControllerBase
{
    private static readonly ILog log = LogManager.GetLogger(typeof(OrdersController));

    [HttpGet]
    public IActionResult GetOrders()
    {
        log.Info($"GET /api/orders called by user: {User.Identity?.Name}");

        var orders = new[]
        {
            new { Id = 1, Product = "Laptop", Amount = 75000 },
            new { Id = 2, Product = "Mobile", Amount = 25000 }
        };

        return Ok(orders);
    }

    [HttpPost]
    public IActionResult CreateOrder([FromBody] OrderRequest request)
    {
        log.Info($"POST /api/orders called by user: {User.Identity?.Name}");

        if (string.IsNullOrWhiteSpace(request.Product) || request.Amount <= 0)
        {
            log.Warn($"Invalid order data received - Product: {request.Product}, Amount: {request.Amount}");
            return BadRequest(new { message = "Invalid order data" });
        }

        log.Info($"Order created - Product: {request.Product}, Amount: {request.Amount}");
        return Ok(new { message = "Order created successfully", order = request });
    }
}

public class OrderRequest
{
    public string Product { get; set; } = string.Empty;
    public decimal Amount { get; set; }
}
