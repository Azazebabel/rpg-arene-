using System;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace RPGFight
{
    class Character
    {
        public string Name { get; set; }
        public int Health { get; set; }
        public int MaxHealth { get; set; }
        public int AttackPower { get; set; }

        public int HealthPotion  { get; set; }

        public decimal ArmourValue { get; set; }

        public Character(string name, int health, int attackPower,int healthPotion,decimal armourValue,int maxHealth)
        {
            Name = name;
            Health = health;
            AttackPower = attackPower;
            HealthPotion = healthPotion;
            ArmourValue = armourValue;
            MaxHealth = maxHealth; 
        }

        public int DealDamage(Random random)
        {
            return random.Next(AttackPower - 5, AttackPower + 5);
        }

        public int HealDamage(Random random)
        {
            HealthPotion -= 1;
            int Heal = random.Next(20, 40);
            Health += Heal;
            if (Health > MaxHealth) Health = MaxHealth; // Ensure health doesn't go over max walue
            return Heal;
        }

        public int TakeDamage(int damage)
        {
            if (ArmourValue > 0)
            {
                decimal taken = damage * ArmourValue;
                 damage = (int)Math.Round(taken, 0);
                Health -= damage;
                
            }
            else
            Health -= damage;
            if (Health < 0) Health = 0; // Ensure health doesn't go below zero
            return damage;
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the Command-Line RPG!");

            // Create player and enemy
            Character player = new Character("Player", 100, 20,8,0.9m,120);
            Character enemy = new Character("Enemy", 80, 15,8,0m,90);

            Random random = new Random();

            // Main game loop
            while (player.Health > 0 && enemy.Health > 0)
            {
                PlayerTurn(player, enemy, random);
                if (enemy.Health <= 0)
                {
                    Console.WriteLine($"\n{enemy.Name} has been defeated!");
                    break;
                }

                EnemyTurn(player, enemy, random);
                if (player.Health <= 0)
                {
                    Console.WriteLine($"\n{player.Name} has been defeated!");
                    break;
                }

                DisplayHealth(player, enemy);
            }

            Console.WriteLine("\nGame Over. Thanks for playing!");
        }

        static void PlayerTurn(Character player, Character enemy, Random random)
        {
            Console.WriteLine("\nYour Turn:");
            Console.WriteLine("1. Attack");
            Console.WriteLine("2. Defend");
            Console.WriteLine("3. Heal ,you have "+player.HealthPotion+" potions");

            Console.Write("Choose an action: ");
            string choice = Console.ReadLine();

            if (choice == "1")
            {
                int damage = player.DealDamage(random);
                damage = enemy.TakeDamage(damage);
                Console.WriteLine($"{player.Name} attacks {enemy.Name} for {damage} damage!");
            }
            else if (choice == "2")
            {
                Console.WriteLine($"{player.Name} braces for the next attack.");
            }
            else if (choice == "3")
            {
                if (player.HealthPotion > 0)
                {
                    int heal = player.HealDamage(random);
                    Console.WriteLine($"{player.Name} drinks health potion for {heal}.");
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

        static void EnemyTurn(Character player, Character enemy, Random random)
        {
            Console.WriteLine("\nEnemy's Turn:");
            int enemyChoice = random.Next(1, 3);

            if (enemyChoice == 1)
            {
                int damage = enemy.DealDamage(random);
               damage =  player.TakeDamage(damage);
                Console.WriteLine($"{enemy.Name} attacks {player.Name} for {damage} damage!");
            }
            else
            {
                Console.WriteLine($"{enemy.Name} braces for the next attack.");
            }
        }

        static void DisplayHealth(Character player, Character enemy)
        {
            Console.WriteLine($"\n{player.Name} Health: {player.Health}");
            Console.WriteLine($"{enemy.Name} Health: {enemy.Health}");
        }
    }
}
