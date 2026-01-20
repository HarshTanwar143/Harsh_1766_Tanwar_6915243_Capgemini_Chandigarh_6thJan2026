using System;
using System.Collections.Generic;
using System.Text;

namespace GameCharacterSystem
{
    internal class Warrior : Character
    {
        public Warrior(string name, int health) : base(name, health) { }

        public override int Attack()
        {
            return Level * 15;
        }
    }
}
