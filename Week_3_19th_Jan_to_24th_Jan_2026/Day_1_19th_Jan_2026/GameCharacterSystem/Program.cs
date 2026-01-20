using System;

namespace GameCharacterSystem
{
    internal class Program
    {
        static void Main(String[] args)
        {
            Warrior warrior = new Warrior("Thor", 120);
            Mage mage = new Mage("Merlin", 90);
            Archer archer = new Archer("Robin", 100);

            warrior.AddSkill(new Skill("Slash", 30));
            mage.AddSkill(new Skill("Fireball", 50));
            archer.AddSkill(new Skill("Arrow Shot", 25));

            Console.WriteLine("Initial Stats");
            Console.WriteLine(warrior.Name + " HP: " + warrior.Health);
            Console.WriteLine(mage.Name + " HP: " + mage.Health);
            Console.WriteLine(archer.Name + " HP: " + archer.Health);
            Console.WriteLine();

            int damageToMage = warrior.Attack();
            mage.TakeDamage(damageToMage);

            Console.WriteLine("Battle Result");
            Console.WriteLine(warrior.Name + " attacks " + mage.Name + " for " + damageToMage);
            Console.WriteLine(mage.Name + " HP left: " + mage.Health);
            Console.WriteLine();

            mage.LevelUp();

            Console.WriteLine("After Level Up");
            Console.WriteLine(mage.Name + " Level: " + mage.Level);
            Console.WriteLine(mage.Name + " HP: " + mage.Health);
        }
    }
}
