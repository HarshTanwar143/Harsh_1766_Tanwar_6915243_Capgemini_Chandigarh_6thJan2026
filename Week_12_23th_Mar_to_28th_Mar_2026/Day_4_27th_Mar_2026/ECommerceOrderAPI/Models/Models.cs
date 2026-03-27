namespace ECommerceOrderAPI.Models;

public class LoginRequest
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
}

public class OrderRequest
{
    public int UserId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
}

public class PaymentRequest
{
    public int OrderId { get; set; }
    public decimal Amount { get; set; }
    public string Method { get; set; } = "Card";
}
