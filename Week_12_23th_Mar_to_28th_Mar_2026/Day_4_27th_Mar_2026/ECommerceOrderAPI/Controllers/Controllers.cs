using ECommerceOrderAPI.Models;
using Microsoft.AspNetCore.Mvc;

// ─── User Controller ──────────────────────────────────────────────

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequest request)
    {
        if (string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Password))
            return BadRequest("Email and password are required");

        var result = _userService.Login(request);
        return Ok(new { message = result });
    }
}

// ─── Product Controller ───────────────────────────────────────────

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet("{id}")]
    public IActionResult GetProduct(int id)
    {
        var product = _productService.GetProduct(id);
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
    private readonly IOrderService _orderService;

    public OrderController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpPost]
    public IActionResult PlaceOrder([FromBody] OrderRequest request)
    {
        var result = _orderService.PlaceOrder(request);
        return Ok(new { message = result });
    }
}

// ─── Payment Controller ───────────────────────────────────────────

[ApiController]
[Route("api/[controller]")]
public class PaymentController : ControllerBase
{
    private readonly IPaymentService _paymentService;

    public PaymentController(IPaymentService paymentService)
    {
        _paymentService = paymentService;
    }

    [HttpPost]
    public IActionResult ProcessPayment([FromBody] PaymentRequest request)
    {
        var result = _paymentService.ProcessPayment(request);
        return Ok(new { message = result });
    }
}
