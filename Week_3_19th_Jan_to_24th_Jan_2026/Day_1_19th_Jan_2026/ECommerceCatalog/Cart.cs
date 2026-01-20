using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceCatalog
{
    internal class Cart
    {
        private List<Product> products = new List<Product>();

        public void AddProduct(Product product)
        {
            products.Add(product);
        }

        public double GetTotal()
        {
            double total = 0;
            foreach (var p in products)
                total += p.Price;
            return total;
        }

        public List<Product> Products { get { return products; } }
    }
}
