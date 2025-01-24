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
            //Here i create databases that this program uses
            save.InitializeDatabase();

            save.InitializeGoblinDatabase();

            save.InitializeZombieDatabase();



            //Loading player and set up other important stuff
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
            //load the arena
            Fighting fighting = new Fighting();
            while (true)//in this loop is operatet gamae main loop
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
                    fighting.Arena(player,random);//Go to fight hub
                }
                else if (choice == "2")
                {
                    shop.Shoping(player);//Go to shop hub
                }
                else if (choice == "4")
                {
                    //load saving fuction and exit game
                    save.SaveFile(player);
                    return;
                }
                else if (choice == "3")
                {
                    //option to heal out of combat
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
                    //return to start of hub loop 
                    Console.WriteLine("Invalid choice! You are stupid !!");
                }






                if (player.Health <= 0)
                {
                    Console.WriteLine("\nGame Over. Thanks for playing!");
                    save.DeleteSave(player.Name);//If player is defetet delate save file
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
