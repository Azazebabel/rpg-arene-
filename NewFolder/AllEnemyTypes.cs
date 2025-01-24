using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGFight
{ 


   public class Goblin : Character

    {

        public Goblin(string name, int health, int attackPower, int healthPotion, decimal armourValue, int maxHealth, int blockDmg, int gold,Weapon weapon,int exp)
        : base(name, health, attackPower, healthPotion, armourValue, maxHealth, blockDmg,gold,weapon,exp)
        {
        }
        public override void TakeTurn(Character player, Random random)
        {//enmey logic
            ProcessEffects();
            Console.WriteLine("\nEnemy's Turn:");
            int enemyChoice = random.Next(1, 3);

            if (enemyChoice == 1)
            {//this enemy is covardly it has only 1/3rd chance of atacking
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
   public class Zombie : Character

    {

        public Zombie(string name, int health, int attackPower, int healthPotion, decimal armourValue, int maxHealth, int blockDmg, int gold,Weapon weapon,int exp)
        : base(name, health, attackPower, healthPotion, armourValue, maxHealth, blockDmg, gold,weapon, exp)
        {
        }
        public override void TakeTurn(Character player, Random random)
        {//Enamy logic
            ProcessEffects();
            Console.WriteLine("\nEnemy's Turn:");
            int enemyChoice = random.Next(1, 3);//It has 1/3rd chance for each action

            if (enemyChoice == 1)
            {//Normal atack
                int damage = DealDamage(random);
                damage = player.TakeDamage(damage);
                Console.WriteLine($"{Name} attacks {player.Name} for {damage} damage!");
            }
            else if(enemyChoice == 2)
            {//Self heal efect
                Console.WriteLine($"{Name} Start regenerationg in compulsive way .");
                HealOverTime dot = new HealOverTime("Self Reconstruction", 5, 10);
                ApplyStatusEffect(dot);
            }
        else
            {//damage over time
                Console.WriteLine($"{Name} Start vomiting at you  in compulsive way .");
                DamageOverTime dot = new DamageOverTime("Stomach acid", 2, 10);
                player.ApplyStatusEffect(dot);
            }
        }

    }
    class Warrior : Character

    {

        public Warrior(string name, int health, int attackPower, int healthPotion, decimal armourValue, int maxHealth, int blockDmg, int gold, Weapon weapon,int exp)
        : base(name, health, attackPower, healthPotion, armourValue, maxHealth, blockDmg, gold, weapon, exp)
        {
        }
        public override void TakeTurn(Character player, Random random)
        {//Enemy logic
            ProcessEffects();
            Console.WriteLine("\nEnemy's Turn:");
            int enemyChoice = random.Next(1, 5);
            if (Health <= MaxHealth / 2)//Bigger chance of healing if damaged
                enemyChoice += 3;
            if (player.Health < MaxHealth / 2)//Bigger chance to atack if player is lov on health
                enemyChoice -= 2;
            if (enemyChoice <= 5)
            {//Atack action
                int damage = DealDamage(random);
                damage = player.TakeDamage(damage);
                Console.WriteLine($"{Name} attacks {player.Name} for {damage} damage!");
            }
            else if (enemyChoice <= 7)
            {//Heal action
                if (HealthPotion > 0)//If this enemy has still potions use them if not use weaker heal
                { int heal = HealDamage(random);
                    Console.WriteLine($"{Name} Drinks health potion for {heal}.");
                }
                else
                {
                    int heal = random.Next(0,25);
                    Health += heal;
                    
                    Console.WriteLine($"{Name} set his bones back to their place restoring  {heal} health .");
                }
            }
            else
            {//small chance of using powerfull atack power reduction
                Console.WriteLine($"{Name} Invoke a curse .");
                AttackPowerNerf dot = new AttackPowerNerf("Dark curse", 2, random.Next(10, 30));
                player.ApplyStatusEffect(dot);
            }
        }

    }
    class Champion  : Character

    {

        public Champion(string name, int health, int attackPower, int healthPotion, decimal armourValue, int maxHealth, int blockDmg, int gold, Weapon weapon,int exp)
        : base(name, health, attackPower, healthPotion, armourValue, maxHealth, blockDmg, gold, weapon, exp)
        {
        }
        public override void TakeTurn(Character player, Random random)
        {//boss logic
            ProcessEffects();
            Console.WriteLine("\nEnemy's Turn:");
            int enemyChoice = random.Next(1, 5);
            if (Health <= MaxHealth / 2)
                enemyChoice += 3;
            if (player.Health < MaxHealth / 2)
                enemyChoice -= 2;//Identical logic to warior
            if (enemyChoice <= 5)
            {//atack function
                int damage = DealDamage(random);
                damage = player.TakeDamage(damage);
                Console.WriteLine($"{Name} attacks {player.Name} for {damage} damage!");
            }
            else if (enemyChoice <= 7)
            {
                if (HealthPotion > 0)//Use potion if can use worse healing if not
                {
                    int heal = HealDamage(random);
                    Console.WriteLine($"{Name} Drinks health potion for {heal}.");
                }
                else
                {
                    int heal = random.Next(0, 25);
                    Health += heal;

                    Console.WriteLine($"{Name} set his bones back to their place restoring  {heal} health .");
                }
            }
            else
            {//Small chance for curse
                Console.WriteLine($"{Name} Invoke a curse .");
                AttackPowerNerf dot = new AttackPowerNerf("Dark curse", 2, random.Next(10, 30));
                player.ApplyStatusEffect(dot);
            }
        }

    }
}
