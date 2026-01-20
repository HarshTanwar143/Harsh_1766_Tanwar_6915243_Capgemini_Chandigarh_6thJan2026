using System;
using System.Collections.Generic;
using System.Text;

namespace VehicleRentalSystem
{
    internal class Customer
    {
        private int customerId;
        private string name;

        public Customer(int customerId, string name)
        {
            this.customerId = customerId;
            this.name = name;
        }

        public int CustomerId { get { return customerId; } }
        public string Name { get { return name; } }
    }
}
