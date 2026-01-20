using System;
using System.Collections.Generic;
using System.Text;

namespace GameCharacterSystem
{
    internal class Inventory
    {
        private List<string> items = new List<string>();

        public void AddItem(string item)
        {
            items.Add(item);
        }

        public List<string> Items { get { return items; } }
    }
}
