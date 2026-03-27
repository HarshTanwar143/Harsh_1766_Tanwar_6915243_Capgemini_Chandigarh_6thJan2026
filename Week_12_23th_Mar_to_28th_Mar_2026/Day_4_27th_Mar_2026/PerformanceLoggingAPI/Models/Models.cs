namespace PerformanceLoggingAPI.Models;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
}

public class Order
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal Total { get; set; }
}

public class Report
{
    public int TotalOrders { get; set; }
    public decimal TotalRevenue { get; set; }
    public string GeneratedAt { get; set; } = string.Empty;
}
