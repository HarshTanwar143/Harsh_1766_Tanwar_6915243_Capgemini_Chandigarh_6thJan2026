using System;
using System.Collections.Generic;
using System.Text;

namespace GameCharacterSystem
{
    internal class Archer : Character
    {
        public Archer(string name, int health) : base(name, health) { }

        public override int Attack()
        {
            return Level * 12;
        }
    }
}
