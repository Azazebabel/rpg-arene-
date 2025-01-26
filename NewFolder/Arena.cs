using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;

namespace RPGFight
{
    public class Fighting
    {
        static void DisplayHealth(Character player, Character enemy)
        {
            Console.WriteLine($"\n{player.Name} Health: {player.Health}");
            Console.WriteLine($"{enemy.Name} Health: {enemy.Health}");
        }
        public void Arena(Character player, Random random)
        {
            //option tree for combat hub
            Console.WriteLine("Chose action ");
            Console.WriteLine("1. Fight ugly  Goblin ");
            Console.WriteLine("2. Fight roting Zombie");
            Console.WriteLine("3. Fight trained warrior");
            Console.WriteLine("4. Fight Arena champion");
            Console.Write("Choose an action: ");
            string choice = Console.ReadLine();

            if (choice == "1")
            {
                //go to fight 
                GoblinFight(player, random);
                player.LevelUp();//function that check if player level up 
                return;
            }
            else if (choice == "2")
            {
                //same as goblin fight
                ZombieFight(player, random);
                player.LevelUp();
                return;
            }
            else if (choice == "3")
            {
                //same as goblin fight
                WarriorFight(player, random);
                player.LevelUp();
                return;
            }
            else if (choice == "4")
            {
                //this fight is intended as boss fight ,limit fight outside big price
                Console.WriteLine($"You have {player.Gold} gold you need 1000 to chalenge arena champion ");
                if (player.Gold >= 1000)
                {
                    //second check if player is sure to fight
                    Console.WriteLine("Do you want fight Arena champion press 1 to chalenge him ");
                    choice = Console.ReadLine();
                    if (choice == "1")
                    {
                        //boss fight
                        ChampionFight(player, random);
                        player.LevelUp();

                        return;
                    }
                    else return;

                }
            }
            else
            {
                Console.WriteLine("Invalid choice! You return to Lobby");
            }


            return;
        }
        public void ChampionFight(Character player, Random random)
        {
            //boss fight higher stats than other things in game
            Champion enemy = new Champion("Lord Champion ", 990, 55, 88, 7.0m, 990, 50, 1000, new Sword(), 0);


                enemy.TakeTurn(player, random);
                if (player.Health <= 0)
                {
                    Console.WriteLine($"\n{player.Name} has been defeated!");
                    return;
                }

                DisplayHealth(player, enemy);
            }




        
      




        
        public void GoblinFight(Character player, Random random)
        {
            Goblin enemy = LoadGoblin();
            // Main game loop
            while (player.Health > 0 && enemy.Health > 0)
            {
                player.TakeTurn(enemy, random);
                if (enemy.Health <= 0)

                {
                    Console.WriteLine($"\n{enemy.Name} has been defeated!");
                    Console.WriteLine("\n You won you gain 10 coins !");
                    player.Exp = player.Exp + 10;
                    player.Gold = player.Gold + 10;
                    return;
                }

                enemy.TakeTurn(player, random);
                if (player.Health <= 0)
                {
                    Console.WriteLine($"\n{player.Name} has been defeated!");
                    return;
                }

                DisplayHealth(player, enemy);
            }




        }
        public void ZombieFight(Character player, Random random)
        {


            Zombie enemy = LoadZombie();

            // Main game loop
            while (player.Health > 0 && enemy.Health > 0)
            {
                player.TakeTurn(enemy, random);
                if (enemy.Health <= 0)

                {
                    Console.WriteLine($"\n{enemy.Name} has been defeated!");
                    Console.WriteLine("\n You won you gain 20 coins !");

                    player.Exp = player.Exp + 20;
                    player.Gold = player.Gold + 20;
                    return;
                }

                enemy.TakeTurn(player, random);
                if (player.Health <= 0)
                {
                    Console.WriteLine($"\n{player.Name} has been defeated!");
                    return;
                }

                DisplayHealth(player, enemy);
            }


        }
        public void WarriorFight(Character player, Random random)
        {
            //Here i forget to make database but it is only a prototype in the end
            Warrior enemy = new Warrior("Steave", 180, 35, 10, 0.7m, 190, 5, 50, new Fail(), 0);

            // Main game loop
            while (player.Health > 0 && enemy.Health > 0)
            {
                player.TakeTurn(enemy, random);
                if (enemy.Health <= 0)

                {
                    Console.WriteLine($"\n{enemy.Name} has been defeated!");
                    Console.WriteLine("\n You won you gain 50 coins !");
                    player.Exp = player.Exp + 50;
                    player.Gold = player.Gold + 50;
                    return;
                }

                enemy.TakeTurn(player, random);
                if (player.Health <= 0)
                {
                    Console.WriteLine($"\n{player.Name} has been defeated!");
                    return;
                }

                DisplayHealth(player, enemy);
            }


        }
        public Zombie LoadZombie()
        {

            //Load statistic from database

            using (var connection = new SqliteConnection("Data Source=Zombie.db")) //Database name
            {
                connection.Open();

                string selectQuery = "SELECT * FROM Zombie ORDER BY RANDOM() LIMIT 1"; // Get the random statistic

                using (var command = new SqliteCommand(selectQuery, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {

                            return new Zombie(
                             reader.GetString(1),  // Name
                             reader.GetInt32(2),   // Health
                             reader.GetInt32(3),   // AttackPower
                             reader.GetInt32(4),   // HealthPotion    
                             reader.GetDecimal(5), // ArmourValue
                             reader.GetInt32(6),   // MaxHealth
                             reader.GetInt32(7),   // BlockDmg
                             reader.GetInt32(8),   // Gold left here if i want to make player get gold from enemy insted of flat gold buff
                            new Fist(), // Weapon placeholder
                            0//exp enemies dont use this thats why it is 0 
                         );

                        }
                    }
                }
            }

            Console.WriteLine("No saved player data found.");
            return new Zombie("Error corpse", 100, 20, 8, 0.9m, 120, 0, 0, new Fist(), 0);//If somthing go wrong 

        }
        public Goblin LoadGoblin()
        {



            using (var connection = new SqliteConnection("Data Source=Goblin.db"))//database link
            {
                connection.Open();

                string selectQuery = "SELECT * FROM Goblin ORDER BY RANDOM() LIMIT 1"; // Get the random enemy 
                using (var command = new SqliteCommand(selectQuery, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {

                            return new Goblin(
                             reader.GetString(1),  // Name
                             reader.GetInt32(2),   // Health
                             reader.GetInt32(3),   // AttackPower
                             reader.GetInt32(4),   // HealthPotion    
                             reader.GetDecimal(5), // ArmourValue
                             reader.GetInt32(6),   // MaxHealth
                             reader.GetInt32(7),   // BlockDmg
                             reader.GetInt32(8),   // Gold
                            new Fist(), // Weapon placeholder
                            0 //exp
                         );

                        }
                    }
                }
            }

            Console.WriteLine("No saved player data found.");
            return new Goblin("Error Goblin", 100, 20, 8, 0.9m, 120, 0, 0, new Fist(), 0);

        }
      
    }
}
