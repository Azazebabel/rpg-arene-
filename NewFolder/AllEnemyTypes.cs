using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGFight
{ 


    class Goblin : Character

    {

        public Goblin(string name, int health, int attackPower, int healthPotion, decimal armourValue, int maxHealth, int blockDmg)
        : base(name, health, attackPower, healthPotion, armourValue, maxHealth, blockDmg)
        {
        }
        public override void TakeTurn(Character player, Random random)
        {
            ProcessEffects();
            Console.WriteLine("\nEnemy's Turn:");
            int enemyChoice = random.Next(1, 3);

            if (enemyChoice == 1)
            {
                int damage = DealDamage(random);
                damage = player.TakeDamage(damage);
                Console.WriteLine($"{Name} attacks {player.Name} for {damage} damage!");
            }
            else
            {
                Console.WriteLine($"{Name} braces for the next attack.");
                BlockDmg = 5;
            }
        }

    }
    class Zombie : Character

    {

        public Zombie(string name, int health, int attackPower, int healthPotion, decimal armourValue, int maxHealth, int blockDmg)
        : base(name, health, attackPower, healthPotion, armourValue, maxHealth, blockDmg)
        {
        }
        public override void TakeTurn(Character player, Random random)
        {
            ProcessEffects();
            Console.WriteLine("\nEnemy's Turn:");
            int enemyChoice = random.Next(1, 3);

            if (enemyChoice == 1)
            {
                int damage = DealDamage(random);
                damage = player.TakeDamage(damage);
                Console.WriteLine($"{Name} attacks {player.Name} for {damage} damage!");
            }
            else if(enemyChoice == 2)
            {
                Console.WriteLine($"{Name} Start regenerationg in compulsive way .");
                HealOverTime dot = new HealOverTime("Self Reconstruction", 5, 10);
                player.ApplyStatusEffect(dot);
            }
        else
            {
                Console.WriteLine($"{Name} Start vomiting at you  in compulsive way .");
                DamageOverTime dot = new DamageOverTime("Stomach acid", 2, 10);
                player.ApplyStatusEffect(dot);
            }
        }

    }
}
