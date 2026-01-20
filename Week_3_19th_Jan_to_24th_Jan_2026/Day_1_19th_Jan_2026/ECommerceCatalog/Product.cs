using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceCatalog
{
    internal class Product
    {
        private int productId;
        private string name;
        private double price;
        private int quantity;

        public Product(int productId, string name, double price, int quantity)
        {
            this.productId = productId;
            this.name = name;
            this.price = price;
            this.quantity = quantity;
        }

        public int ProductId { get { return productId; } }
        public string Name { get { return name; } }
        public double Price { get { return price; } }
        public int Quantity { get { return quantity; } }

        public void ReduceStock(int count)
        {
            quantity -= count;
        }
    }

}
