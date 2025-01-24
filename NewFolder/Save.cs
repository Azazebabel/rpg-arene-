using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;

namespace RPGFight
{
    using System.Numerics;
    using Microsoft.Data.Sqlite;

    public class Save
    {
        private const string ConnectionString = "Data Source=game.db"; // SQLite file name

        public Player LoadPlayer()
        {
            //Playrer chose if he want to load save or if he want start new 
            Console.WriteLine("Chose action ");
            Console.WriteLine("1. Load save ");
            Console.WriteLine("2/any other c" +
                "haracter. New Game ");
           
            Console.Write("Choose an action: ");
            string choice = Console.ReadLine();

            if (choice == "1")
            {
                using (var connection = new SqliteConnection(ConnectionString))//load save
                {
                    connection.Open();

                    string selectQuery = "SELECT * FROM Player ORDER BY Id DESC LIMIT 1;"; // Get the latest player

                    using (var command = new SqliteCommand(selectQuery, connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                //Let player see saved values
                                Console.WriteLine($"Name: {reader.GetString(1)}");
                                Console.WriteLine($"Health: {reader.GetInt32(2)}");
                                Console.WriteLine($"AttackPower: {reader.GetInt32(3)}");
                                Console.WriteLine($"HealthPotion: {reader.GetInt32(4)}");
                                Console.WriteLine($"ArmourValue: {reader.GetDecimal(5)}");
                                Console.WriteLine($"MaxHealth: {reader.GetInt32(6)}");
                                Console.WriteLine($"BlockDmg: {reader.GetInt32(7)}");
                                Console.WriteLine($"Gold: {reader.GetInt32(8)}");
                                return new Player(
                                 reader.GetString(1),  // Name
                                 reader.GetInt32(2),   // Health
                                 reader.GetInt32(3),   // AttackPower
                                 reader.GetInt32(4),   // HealthPotion    
                                 reader.GetDecimal(5), // ArmourValue
                                 reader.GetInt32(6),   // MaxHealth
                                 reader.GetInt32(7),   // BlockDmg
                                 reader.GetInt32(8),   // Gold
                                new Fist()         , // Weapon placeholder
                                reader.GetInt32(10) //exp
                             );

                            }
                        }
                    }
                }

                Console.WriteLine("No saved player data found.");//If somthing go wrong
                return new Player("Player", 100, 20, 8, 0.9m, 120, 0, 0, new Fist(),0);
            }
            else
                return new Player("Player", 100, 20, 8, 0.9m, 120, 0, 10, new Fist(),0);//If player want to start new 
        }

        public void SaveFile(Character player)
        {
            using (var connection = new SqliteConnection(ConnectionString))
            {
                connection.Open();//magic string that makes it all work 
                string insertQuery = @"
                    INSERT INTO Player (Name, Health, AttackPower, HealthPotion, ArmourValue, MaxHealth, BlockDmg, Gold, Weapon,Exp)
                    VALUES (@Name, @Health, @AttackPower, @HealthPotion, @ArmourValue, @MaxHealth, @BlockDmg, @Gold, @Weapon,@Exp);
                    ";


                using (var command = new SqliteCommand(insertQuery, connection))//this transfer  data from player to database
                {
                    command.Parameters.AddWithValue("@Name", player.Name);
                    command.Parameters.AddWithValue("@Health", player.Health);
                    command.Parameters.AddWithValue("@AttackPower", player.AttackPower);
                    command.Parameters.AddWithValue("@HealthPotion", player.HealthPotion);
                    command.Parameters.AddWithValue("@ArmourValue", player.ArmourValue);
                    command.Parameters.AddWithValue("@MaxHealth", player.MaxHealth);
                    command.Parameters.AddWithValue("@BlockDmg", player.BlockDmg);
                    command.Parameters.AddWithValue("@Gold", player.Gold);
                    command.Parameters.AddWithValue("@Weapon", player.Wepon.Name);
                    command.Parameters.AddWithValue("@Exp", player.Exp);

                    command.ExecuteNonQuery();
                }
                Console.WriteLine($"Saving Player: {player.Name}, {player.Health}, {player.AttackPower}, {player.HealthPotion}, {player.ArmourValue}, {player.MaxHealth}, {player.BlockDmg}, {player.Gold}, {player.Wepon.Name}, {player.Exp}");//inform player abaut saved statistic
                Console.WriteLine("Player data saved to database.");
            }
        }
        public void DeleteSave(string playerName)
        {
            using (var connection = new SqliteConnection(ConnectionString))
            {
                connection.Open();

                string deleteQuery = "DELETE FROM Player WHERE Name = @Name;";//magic string that delete save

                using (var command = new SqliteCommand(deleteQuery, connection))
                {
                    command.Parameters.AddWithValue("@Name", playerName);
                    int rowsAffected = command.ExecuteNonQuery();

                  
                }
            }
        }

        public void InitializeDatabase()
        {
            using (var connection = new SqliteConnection(ConnectionString))
            {
                connection.Open();
                //text that set up new database if ther is none
                string createTableQuery = @"
                        CREATE TABLE IF NOT EXISTS Player (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        Name TEXT NOT NULL,
                        Health INTEGER NOT NULL,                  
                        AttackPower INTEGER NOT NULL,
                        HealthPotion INTEGER NOT NULL,
                        ArmourValue DECIMAL NOT NULL,
                        MaxHealth INTEGER NOT NULL,
                        BlockDmg INTEGER NOT NULL,
                        Gold INTEGER NOT NULL,
                        Weapon TEXT NOT NULL,
                        Exp INTEGER NOT NULL
                    );
            ";

                using (var command = new SqliteCommand(createTableQuery, connection))
                {
                    command.ExecuteNonQuery();
                }

                Console.WriteLine("Database initialized and Player table created.");
            }
        }
        public void InitializeGoblinDatabase()
        {
            using (var connection = new SqliteConnection("Data Source=Goblin.db"))
            {
                connection.Open();

                //text that set up new database if ther is none
                string createTableQuery = @"
                        CREATE TABLE IF NOT EXISTS Goblin (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        Name TEXT NOT NULL,
                        Health INTEGER NOT NULL,                  
                        AttackPower INTEGER NOT NULL,
                        HealthPotion INTEGER NOT NULL,
                        ArmourValue DECIMAL NOT NULL,
                        MaxHealth INTEGER NOT NULL,
                        BlockDmg INTEGER NOT NULL,
                        Gold INTEGER NOT NULL,
                        Weapon TEXT NOT NULL,
                        Exp INTEGER NOT NULL
                    );
            ";

                using (var command = new SqliteCommand(createTableQuery, connection))
                {
                    command.ExecuteNonQuery();
                }

                Console.WriteLine("Database initialized and Goblin table created.");
            
           
            //ste up goblin stats posible to easly make more

                string insertQuery = @"
            INSERT INTO Goblin (Name, Health, AttackPower, HealthPotion, ArmourValue, MaxHealth, BlockDmg, Gold, Weapon,Exp)
            VALUES
                ('Sword Gobo', 80, 15, 0, 0, 80, 0, 10, 'Sword',0),
                ('Spearman Goblin', 60, 10, 0, 0.9, 60, 0, 15, 'Spear',0);
        ";

                using (var command = new SqliteCommand(insertQuery, connection))
                {
                    command.ExecuteNonQuery();
                }

                Console.WriteLine("Default Goblin data inserted.");
            }
        }
        public void InitializeZombieDatabase()
        {
            using (var connection = new SqliteConnection("Data Source=Zombie.db"))
            {
                connection.Open();

                //text that set up new database if ther is none
                string createTableQuery = @"
                        CREATE TABLE IF NOT EXISTS Zombie (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        Name TEXT NOT NULL,
                        Health INTEGER NOT NULL,                  
                        AttackPower INTEGER NOT NULL,
                        HealthPotion INTEGER NOT NULL,
                        ArmourValue DECIMAL NOT NULL,
                        MaxHealth INTEGER NOT NULL,
                        BlockDmg INTEGER NOT NULL,
                        Gold INTEGER NOT NULL,
                        Weapon TEXT NOT NULL,
                        Exp INTEGER NOT NULL
                    );
            ";

                using (var command = new SqliteCommand(createTableQuery, connection))
                {
                    command.ExecuteNonQuery();
                }

                Console.WriteLine("Database initialized and Goblin table created.");



                //set up enemy stats for zombie 
                string insertQuery = @"
            INSERT INTO Zombie (Name, Health, AttackPower, HealthPotion, ArmourValue, MaxHealth, BlockDmg, Gold, Weapon,Exp)
            VALUES
                ('Unamed Corpse', 80, 15, 0, 0.9, 80, 0, 10, 'Fist',0),
                ('Named Corpse', 60, 10, 0, 0.8, 60, 0, 15, 'Fist',0);
        ";

                using (var command = new SqliteCommand(insertQuery, connection))
                {
                    command.ExecuteNonQuery();
                }

                Console.WriteLine("Default Zombie data inserted.");
            }
        }
    }

}