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
            Console.WriteLine("Chose action ");
            Console.WriteLine("1. Load save ");
            Console.WriteLine("2/any other c" +
                "haracter. New Game ");
           
            Console.Write("Choose an action: ");
            string choice = Console.ReadLine();

            if (choice == "1")
            {
                using (var connection = new SqliteConnection(ConnectionString))
                {
                    connection.Open();

                    string selectQuery = "SELECT * FROM Player ORDER BY Id DESC LIMIT 1;"; // Get the latest player

                    using (var command = new SqliteCommand(selectQuery, connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                Console.WriteLine($"Name: {reader.GetString(1)}");
                                Console.WriteLine($"Health: {reader.GetInt32(2)}");
                                Console.WriteLine($"AttackPower: {reader.GetInt32(3)}");
                                Console.WriteLine($"HealthPotion: {reader.GetInt32(4)}");
                                Console.WriteLine($"ArmourValue: {reader.GetDecimal(5)}");
                                Console.WriteLine($"MaxHealth: {reader.GetInt32(6)}");
                                Console.WriteLine($"BlockDmg: {reader.GetInt32(7)}");
                                Console.WriteLine($"Gold: {reader.GetInt32(8)}");
                                // Ensure indices align with your database schema
                                return new Player(
                                    reader.GetString(1), // Name
                                    reader.GetInt32(2),  // Health
                                    reader.GetInt32(3),  // AttackPower
                                    reader.GetInt32(4),  // HealthPotion    

                                    reader.GetDecimal(5),  //  ArmourValue             
                                    reader.GetInt32(6),  // MaxHealth
                                    reader.GetInt32(7),  // BlockDmg
                                    reader.GetInt32(8),// Gold
                                new Sword()          //  weapon
                                );
                            }
                        }
                    }
                }

                Console.WriteLine("No saved player data found.");
                return new Player("Player", 100, 20, 8, 0.9m, 120, 0, 0, new Fist());
            }
            else
                return new Player("Player", 100, 20, 8, 0.9m, 120, 0, 0, new Fist());
        }

        public void SaveFile(Character player)
        {
            using (var connection = new SqliteConnection(ConnectionString))
            {
                connection.Open();
                string insertQuery = @"
                  INSERT INTO Player (Name, Health, MaxHealth, AttackPower, HealthPotion, BlockDmg, Gold, ArmourValue, Weapon)
                   VALUES (@Name, @Health, @MaxHealth, @AttackPower, @HealthPotion, @BlockDmg, @Gold, @ArmourValue, @Weapon);
                ";
              

                using (var command = new SqliteCommand(insertQuery, connection))
                {
                    command.Parameters.AddWithValue("@Name", player.Name);
                    command.Parameters.AddWithValue("@Health", player.Health);
                    command.Parameters.AddWithValue("@MaxHealth", player.MaxHealth);
                    command.Parameters.AddWithValue("@AttackPower", player.AttackPower);
                    command.Parameters.AddWithValue("@HealthPotion", player.HealthPotion);
                    command.Parameters.AddWithValue("@BlockDmg", player.BlockDmg);
                    command.Parameters.AddWithValue("@Gold", player.Gold);
                    command.Parameters.AddWithValue("@ArmourValue", player.ArmourValue);
                    command.Parameters.AddWithValue("@Weapon", player.Wepon.Name);

                    command.ExecuteNonQuery();
                }

                Console.WriteLine("Player data saved to database.");
            }
        }

        public void InitializeDatabase()
        {
            using (var connection = new SqliteConnection(ConnectionString))
            {
                connection.Open();

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
                    
                    Weapon TEXT NOT NULL
                );
            ";

                using (var command = new SqliteCommand(createTableQuery, connection))
                {
                    command.ExecuteNonQuery();
                }

                Console.WriteLine("Database initialized and Player table created.");
            }
        }
    }

}