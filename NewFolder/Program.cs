using System.ComponentModel;

namespace RPGFight
{










    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the Command-Line RPG!");

            // Create player and enemy
            Player player = new Player("Player", 100, 20, 8, 0.9m, 120, 0);
            Zombie enemy = new Goblin("Enemy", 80, 15, 8, 0m, 90, 5);

            Random random = new Random();

            // Main game loop
            while (player.Health > 0 && enemy.Health > 0)
            {
                player.TakeTurn(enemy, random);
                if (enemy.Health <= 0)
                {
                    Console.WriteLine($"\n{enemy.Name} has been defeated!");
                    break;
                }

                enemy.TakeTurn(player, random);
                if (player.Health <= 0)
                {
                    Console.WriteLine($"\n{player.Name} has been defeated!");
                    break;
                }

                DisplayHealth(player, enemy);
            }
            Console.WriteLine("\nFIrt enemy slain !");
            if (player.Health <= 0)
            {
                Console.WriteLine("\nGame Over. Thanks for playing!");
                return;
            }

        }





        static void DisplayHealth(Character player, Character enemy)
        {
            Console.WriteLine($"\n{player.Name} Health: {player.Health}");
            Console.WriteLine($"{enemy.Name} Health: {enemy.Health}");
        }
    }
}
