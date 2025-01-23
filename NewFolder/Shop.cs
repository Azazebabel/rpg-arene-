using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RPGFight;

namespace PGFight
{
    internal class Shop
    {
        static void DisplayGold(Character player)
        {
            Console.WriteLine($"\n{player.Name} You Have : {player.Gold} Gold");
        }

        public void Shoping(Character player)
        {

            while (true)
            {

                DisplayGold(player);
                Console.WriteLine("Chose action ");
                Console.WriteLine("1. Buy Armour ");
                Console.WriteLine("2. Buy Wepon");
                Console.WriteLine("3. Buy potion ,you have " + player.HealthPotion + " potions");
                Console.WriteLine("4. Return to hub");
                Console.Write("Choose an action: ");
                string choice = Console.ReadLine();

                if (choice == "1")
                {
                    while (true)
                    {
                        Console.WriteLine($"\n{player.Name} You Have : {player.ArmourValue} Armour Value (smaller the better)");
                        Console.WriteLine("Chose action ");
                        Console.WriteLine("1. Upegrade Armour it cost 100 coins");
                        Console.WriteLine("2. Return to shoping  hub");
                        Console.Write("Choose an action: ");
                        string choice1 = Console.ReadLine();

                        if (choice1 == "1")
                        {
                            if (player.Gold >= 100)
                            {
                                if (player.ArmourValue >= 0.6m)
                                {
                                    player.ArmourValue -= 0.1M;
                                    Console.WriteLine($"\n{player.Name} upegraded armour to  : {player.ArmourValue} Armour Value (smaller the better)");
                                    player.Gold -= 100;
                                    Console.WriteLine($"\n{player.Name} Spended  : {player.Gold} Gold");

                                }
                                else { Console.WriteLine("You already upegraded armour to full"); }

                            }
                            else { Console.WriteLine("You are too poor to upegrade your armour"); }
                        }
                        else if (choice1 == "2")
                        {
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Invalid choice! You are stupid !!");
                        }
                    }
                    }
                    else if (choice == "2")
                    {
                    while (true)
                    {
                        Console.WriteLine($"\n{player.Name} You Have : {player.Wepon} Wepon");
                        Console.WriteLine("Chose action ");
                        Console.WriteLine("1. Buy new SWORD IT COST 200 GOLD");
                        Console.WriteLine("2. Buy new FAIL it cost 250gold");
                        Console.WriteLine("3. Buy new SPEAR it cost 100 gold");
                        Console.WriteLine("4. Buy new MAGIC SWORD TM it cost 1000 gold");
                        Console.WriteLine("5. Return to shoping  hub");
                        Console.Write("Choose an action: ");
                        string choice1 = Console.ReadLine();

                        if (choice1 == "1")
                        {
                            if (player.Gold >= 200)
                            {
                               
                                    Console.WriteLine($"\n{player.Name} Replaced    : {player.Wepon}  with Sword");
                                Sword sword = new Sword();
                                player.Wepon  = sword;
                                    player.Gold -= 200;
                                    Console.WriteLine($"\n{player.Name} Spended  200 of his prcious gold you still have : {player.Gold} Gold");

                                }
                            else { Console.WriteLine($"You are too poor to replace {player.Wepon}, try buing spear"); }

                        }
                       else if (choice1 == "2")
                        {
                            if (player.Gold >= 250)
                            {

                                Console.WriteLine($"\n{player.Name} Replaced    : {player.Wepon}  with Fail");
                                Fail fail = new Fail();
                                player.Wepon = fail;
                                player.Gold -= 250;
                                Console.WriteLine($"\n{player.Name} Spended  250 gold you are still left with  : {player.Gold} Gold");

                            }
                            else { Console.WriteLine($"You are too poor to replace {player.Wepon}"); }

                        }
                        else if (choice1 == "3")
                        {
                            if (player.Gold >= 100)
                            {

                                Console.WriteLine($"\n{player.Name} Replaced    : {player.Wepon}  with Spear");
                                Spear spear = new Spear();
                                player.Wepon = spear;
                                player.Gold -= 100;
                                Console.WriteLine($"\n{player.Name} Spended  100 gold you have : {player.Gold} Gold");

                            }
                            else { Console.WriteLine($"You are too poor to replace {player.Wepon}"); }

                        }
                        else if (choice1 == "4")
                        {
                            if (player.Gold >= 1000)
                            {

                                Console.WriteLine($"\n{player.Name} Replaced    : {player.Wepon}  with Magical sword tm");
                                MagicSword tm = new MagicSword();
                                player.Wepon = tm;
                                player.Gold -= 1000;
                                Console.WriteLine($"\n{player.Name} Spended  1000 gold player has : {player.Gold} Gold");

                            }
                            else { Console.WriteLine($"You are too poor to replace {player.Wepon}"); }

                        }

                        else if (choice1 == "5")
                        {
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Invalid choice! You are stupid !!");
                        }
                    }
                }
                    else if (choice == "3")
                    {
                    while (true)
                    {
                        Console.WriteLine($"\n{player.Name} You Have : {player.HealthPotion} Health potions");
                        Console.WriteLine("Chose action ");
                        Console.WriteLine("1. Buy 1 potion  IT COST 15 GOLD");
                        Console.WriteLine("2. Buy 5 potion it cost 50gold");
                        Console.WriteLine("3. Buy 15 potions it cost 100 gold");
                        Console.WriteLine("4. Buy 1000 potions it cost 1000 gold");
                        Console.WriteLine("5. Return to shoping  hub");
                        Console.Write("Choose an action: ");
                        string choice1 = Console.ReadLine();

                        if (choice1 == "1")
                        {
                            if (player.Gold >= 15)
                            {

                              
                                player.HealthPotion += 1;
                                player.Gold -= 15;
                                Console.WriteLine($"\n{player.Name} Spended  15 of his prcious gold you still have : {player.Gold} Gold");

                            }
                            else { Console.WriteLine($"You are too poor to live"); }

                        }
                        else if (choice1 == "2")
                        {
                            if (player.Gold >= 50)
                            {

                               
                                player.HealthPotion += 5;
                                player.Gold -= 50;
                                Console.WriteLine($"\n{player.Name} Spended  50 gold you are still left with  : {player.Gold} Gold");

                            }
                            else { Console.WriteLine($"You are too poor find job"); }

                        }
                        else if (choice1 == "3")
                        {
                            if (player.Gold >= 100)
                            {

                              
                                player.HealthPotion += 15;
                                player.Gold -= 100;
                                Console.WriteLine($"\n{player.Name} Spended  100 gold you have : {player.Gold} Gold");

                            }
                            else { Console.WriteLine($"You are too poor to have helthcare "); }

                        }
                        else if (choice1 == "4")
                        {
                            if (player.Gold >= 1000)
                            {

                                
                                player.HealthPotion += 1000 ;
                                player.Gold -= 1000;
                                Console.WriteLine($"\n{player.Name} Spended  1000 gold player has : {player.Gold} Gold");

                            }
                            else { Console.WriteLine($"You are should not look that much at bulk buy cost benefit"); }

                        }

                        else if (choice1 == "5")
                        {
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Invalid choice! You are stupid !!");
                        }
                    }

                }
                    else if (choice == "4")
                    {
                    return;
                    }
                    else
                    {
                        Console.WriteLine("Invalid choice! You are stupid !!");
                    }






                }
            }
        }
    }

