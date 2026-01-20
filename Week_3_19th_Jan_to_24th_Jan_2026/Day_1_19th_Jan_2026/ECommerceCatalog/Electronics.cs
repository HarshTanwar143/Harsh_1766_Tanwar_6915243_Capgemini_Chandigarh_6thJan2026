using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceCatalog
{
    internal class Electronics : Product
    {
        public Electronics(int id, string name, double price, int qty): base(id, name, price, qty) { }
    }
}
