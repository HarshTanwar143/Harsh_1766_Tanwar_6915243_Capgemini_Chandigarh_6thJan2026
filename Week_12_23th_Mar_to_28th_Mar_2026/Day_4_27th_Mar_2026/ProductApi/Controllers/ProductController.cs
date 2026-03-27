using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ProductController : ControllerBase
{
    private readonly IMemoryCache _cache;
    private const string CacheKey = "product_list";

    public ProductController(IMemoryCache cache)
    {
        _cache = cache;
    }

    [HttpGet]
    public IActionResult GetProducts()
    {
        bool cacheHit = _cache.TryGetValue(CacheKey, out List<string>? products);

        if (!cacheHit || products == null)
        {
            Thread.Sleep(3000);

            products = new List<string>
            {
                "Laptop", "Mobile", "Tablet", "Headphones"
            };

            var options = new MemoryCacheEntryOptions()
                .SetAbsoluteExpiration(TimeSpan.FromMinutes(5));

            _cache.Set(CacheKey, products, options);

            return Ok(new
            {
                source = "Database",
                note = "Cache was empty. Data fetched from DB and stored in cache.",
                data = products
            });
        }

        return Ok(new
        {
            source = "Cache",
            note = "Cache hit! Returned instantly from memory.",
            data = products
        });
    }

    [HttpDelete("cache")]
    public IActionResult ClearCache()
    {
        _cache.Remove(CacheKey);
        return Ok(new { message = "Cache cleared successfully. Next GET will hit the database." });
    }
}
