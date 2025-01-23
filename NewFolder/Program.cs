using System.ComponentModel;
using System.Xml.Linq;
using PGFight;

namespace RPGFight
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the Command-Line RPG!");
            Save save = new Save();
            save.InitializeDatabase();

            save.InitializeGoblinDatabase();

            save.InitializeZombieDatabase();




            Player player = save.LoadPlayer();
            Goblin enemy = new Goblin("Enemy", 80, 15, 8, 0m, 90, 5,10,new Sword(), 0);
            Shop shop = new Shop();
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
            Fighting fighting = new Fighting();
            while (true)
            {
                DisplayHealth(player, enemy);
                Console.WriteLine("Chose action ");
                Console.WriteLine("1. Go fight ");
                Console.WriteLine("2. Go shoping");
                Console.WriteLine("3. Heal ,you have " + player.HealthPotion + " potions");
                Console.WriteLine("4. Save and exit");
                Console.Write("Choose an action: ");
                string choice = Console.ReadLine();

                if (choice == "1")
                {
                    fighting.Arena(player,random);
                }
                else if (choice == "2")
                {
                    shop.Shoping(player);
                }
                else if (choice == "4")
                {
                    
                    save.SaveFile(player);
                    return;
                }
                else if (choice == "3")
                {
                    if (player.HealthPotion > 0)
                    {
                        int heal = player.HealDamage(random);
                        Console.WriteLine($"{player.Name} drinks health potion for {heal}.");
                        DisplayHealth(player, enemy);
                    }
                    else
                    {
                        Console.WriteLine("You ran out of potions!! And you are Ugly.");
                        return;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid choice! You are stupid !!");
                }






                if (player.Health <= 0)
                {
                    Console.WriteLine("\nGame Over. Thanks for playing!");
                    save.DeleteSave(player.Name);
                    return;
                }
            }
        }





        static void DisplayHealth(Character player, Character enemy)
        {
            Console.WriteLine($"\n{player.Name} Health: {player.Health}");
            if(enemy.Health < 0)
            Console.WriteLine($"{enemy.Name} Health: {enemy.Health}");
        }
    }
}
