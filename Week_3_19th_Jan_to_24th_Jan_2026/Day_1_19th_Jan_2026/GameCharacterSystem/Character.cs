using System;
using System.Collections.Generic;
using System.Text;

namespace GameCharacterSystem
{
    internal class Character
    {
        private string name;
        private int level;
        private int health;
        private List<Skill> skills = new List<Skill>();
        private Inventory inventory = new Inventory();

        public Character(string name, int health)
        {
            this.name = name;
            this.health = health;
            level = 1;
        }

        public string Name { get { return name; } }
        public int Level { get { return level; } }
        public int Health { get { return health; } }
        public List<Skill> Skills { get { return skills; } }
        public Inventory Inventory { get { return inventory; } }

        public virtual int Attack()
        {
            return level * 10;
        }

        public void TakeDamage(int damage)
        {
            health -= damage;
        }

        public void LevelUp()
        {
            level++;
            health += 20;
        }

        public void AddSkill(Skill skill)
        {
            skills.Add(skill);
        }
    }
}
