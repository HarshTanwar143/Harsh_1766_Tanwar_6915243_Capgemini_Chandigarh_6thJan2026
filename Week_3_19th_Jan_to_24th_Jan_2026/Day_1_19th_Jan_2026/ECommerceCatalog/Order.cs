using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceCatalog
{
    internal class Order
    {
        private Cart cart;
        private DateTime orderDate;

        public Order(Cart cart)
        {
            this.cart = cart;
            orderDate = DateTime.Now;
        }

        public double GetOrderAmount()
        {
            return cart.GetTotal();
        }

        public DateTime OrderDate { get { return orderDate; } }
    }
}
