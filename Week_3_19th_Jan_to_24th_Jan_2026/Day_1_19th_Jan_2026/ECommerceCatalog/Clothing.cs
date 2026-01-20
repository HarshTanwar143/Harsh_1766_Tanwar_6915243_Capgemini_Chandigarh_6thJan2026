using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceCatalog
{
    internal class Clothing : Product
    {
        public Clothing(int id, string name, double price, int qty)
            : base(id, name, price, qty) { }
    }
}
