using System;
using System.Collections.Generic;
using System.Text;

namespace GameCharacterSystem
{
    internal class Skill
    {
        private string skillName;
        private int power;

        public Skill(string skillName, int power)
        {
            this.skillName = skillName;
            this.power = power;
        }

        public string SkillName { get { return skillName; } }
        public int Power { get { return power; } }
    }
}
