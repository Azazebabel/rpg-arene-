using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace RPGFight
{

   public  class Player : Character
    {

        public Player(string name, int health, int attackPower, int healthPotion, decimal armourValue, int maxHealth, int blockDmg, int gold,Weapon weapon, int exp)
        : base(name, health, attackPower, healthPotion, armourValue, maxHealth, blockDmg, gold, weapon, exp)
        {
            
        }
        
        public void LevelUp() {
            if (Exp >= 100)
                { Exp -= 100;
                while (true)
                {
                    Console.WriteLine("YOU LEVEL UP !!!!!!! ");
                    Console.WriteLine("Chose what to upegrade ");
                    Console.WriteLine("1. Attack");
                    Console.WriteLine("2. Health / If you chose this you will heal to full ");
                    Console.Write("Choose an action: ");
                    string choice = Console.ReadLine();
                    if (choice == "1")
                    {
                        AttackPower += 20;

                        Console.WriteLine($"Attack incresed by 20 current attack is{AttackPower} ");
                        return;
                    }
                    else if (choice == "2")
                    {
                        MaxHealth += 20;
                        Health = MaxHealth;

                        Console.WriteLine($"Health  incresed by 20 current MaxHealth  is{MaxHealth} ");
                        return;
                    }

                }
            }


            return;
            
            
            
            }
        public override void TakeTurn(Character enemy, Random random)
        {
            Console.WriteLine("\nYour Turn:");
            ProcessEffects();
            Console.WriteLine("1. Attack");
            Console.WriteLine("2. Defend");
            Console.WriteLine("3. Heal ,you have " + HealthPotion + " potions");
            Console.WriteLine("4. Use firebal");

            Console.Write("Choose an action: ");
            string choice = Console.ReadLine();

            if (choice == "1")
            {
                int damage = DealDamage(random);
                damage = enemy.TakeDamage(damage);
                Console.WriteLine($"{Name} attacks  {enemy.Name} using {Wepon.Name} for {damage} damage!");
            }
            else if (choice == "2")
            {
                Console.WriteLine($"{Name} braces for the next attack.");
                BlockDmg = 10;
            }
            else if (choice == "4")
            {
                DamageOverTime dot = new DamageOverTime("Burning", 3, 10);
                enemy.ApplyStatusEffect(dot);
            }
            else if (choice == "3")
            {
                if (HealthPotion > 0)
                {
                    int heal = HealDamage(random);
                    Console.WriteLine($"{Name} drinks health potion for {heal}.");
                }
                else
                {
                    Console.WriteLine("You ran out of potions!! And you lose your turn searching for one.");
                    return;
                }
            }
            else
            {
                Console.WriteLine("Invalid choice! You lose your turn.");
            }

        }










    }

}
