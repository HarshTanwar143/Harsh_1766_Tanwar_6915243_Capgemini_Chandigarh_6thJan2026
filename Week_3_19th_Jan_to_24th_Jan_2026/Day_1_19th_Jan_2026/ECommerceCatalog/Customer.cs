using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceCatalog
{
    internal class Customer
    {
        private int customerId;
        private string name;
        private Cart cart = new Cart();

        public Customer(int customerId, string name)
        {
            this.customerId = customerId;
            this.name = name;
        }

        public int CustomerId { get { return customerId; } }
        public string Name { get { return name; } }
        public Cart Cart { get { return cart; } }
    }
}
