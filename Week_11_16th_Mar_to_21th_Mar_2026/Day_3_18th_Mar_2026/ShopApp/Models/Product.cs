using System.ComponentModel.DataAnnotations;

namespace ShopApp.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Product name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Price is required")]
        [Range(0.01, 999999)]
        public decimal Price { get; set; }

        public string ImageUrl { get; set; }

        public int Stock { get; set; } = 10;
    }
}