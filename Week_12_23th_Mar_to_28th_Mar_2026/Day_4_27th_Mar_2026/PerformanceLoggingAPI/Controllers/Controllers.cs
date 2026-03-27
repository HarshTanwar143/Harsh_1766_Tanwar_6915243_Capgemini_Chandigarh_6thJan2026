using Microsoft.AspNetCore.Mvc;
using log4net;

// ─── Product Controller ───────────────────────────────────────────

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private static readonly ILog log = LogManager.GetLogger(typeof(ProductController));
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    // Normal speed — INFO log
    [HttpGet]
    public IActionResult GetAll()
    {
        var products = _productService.GetAllProducts();
        return Ok(products);
    }

    // id=99 → slow → WARN log | id=1,2,3 → fast → INFO log
    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var product = _productService.GetProductById(id);
        if (product == null)
            return NotFound(new { message = $"Product {id} not found" });

        return Ok(product);
    }
}

// ─── Order Controller ─────────────────────────────────────────────

[ApiController]
[Route("api/[controller]")]
public class OrderController : ControllerBase
{
    private static readonly ILog log = LogManager.GetLogger(typeof(OrderController));
    private readonly IOrderService _orderService;

    public OrderController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    // Always slow (3.5 sec) → WARN log
    [HttpGet]
    public IActionResult GetAll()
    {
        var orders = _orderService.GetAllOrders();
        return Ok(orders);
    }

    // Normal — INFO log
    [HttpPost]
    public IActionResult PlaceOrder(int productId, int quantity)
    {
        var result = _orderService.PlaceOrder(productId, quantity);
        return Ok(new { message = result });
    }
}

// ─── Report Controller ────────────────────────────────────────────

[ApiController]
[Route("api/[controller]")]
public class ReportController : ControllerBase
{
    private static readonly ILog log = LogManager.GetLogger(typeof(ReportController));
    private readonly IReportService _reportService;

    public ReportController(IReportService reportService)
    {
        _reportService = reportService;
    }

    // Always crashes → ERROR log
    [HttpGet]
    public IActionResult Generate()
    {
        try
        {
            var report = _reportService.GenerateReport();
            return Ok(report);
        }
        catch (Exception ex)
        {
            log.Error("Report generation failed at controller level", ex);
            return StatusCode(500, new { message = "Report failed: " + ex.Message });
        }
    }
}
