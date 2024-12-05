using System;
using System.Collections.Generic;
using System.IO;

namespace TextAdventureGame
{
    class Program
    {
        static Random random = new Random();
        static List<string> inventory = new List<string>();
        static int health = 100;
        static string currentLocation = "starting point";
        static Dictionary<string, string> locations = new Dictionary<string, string>
        {
            {"starting point", "You find yourself in a mysterious place."},
            {"forest", "You are in a dense forest with towering trees."},
            {"cave", "You are inside a dark, damp cave."},
            {"village", "You are in a small, peaceful village."}
        };
        static Dictionary<string, string> itemDescriptions = new Dictionary<string, string>
        {
            {"gold", "A shiny piece of gold."},
            {"rare item", "An item of great rarity and value."}
        };

        static void Main(string[] args)
        {
            // Game initialization
            Console.WriteLine("Welcome to the Text Adventure Game!");
            Console.WriteLine(locations[currentLocation]);
            Console.WriteLine("Type 'help' to see a list of commands.");

            // Main game loop
            bool isPlaying = true;
            while (isPlaying)
            {
                Console.Write("\n> ");
                string command = Console.ReadLine().ToLower();
                isPlaying = ProcessCommand(command);
            }

            Console.WriteLine("Thanks for playing! Goodbye.");
        }

        static bool ProcessCommand(string command)
        {
            switch (command)
            {
                case "help":
                    Console.WriteLine("Available commands: help, look, go [location], inventory, use [item], health, save, load, quit");
                    break;
                case "look":
                    Console.WriteLine(locations[currentLocation]);
                    TriggerRandomEvent();
                    break;
                case "go forest":
                case "go cave":
                case "go village":
                case "go starting point":
                    currentLocation = command.Substring(3);
                    Console.WriteLine(locations[currentLocation]);
                    TriggerRandomEvent();
                    break;
                case "inventory":
                    ShowInventory();
                    break;
                case "health":
                    Console.WriteLine($"Your health is {health}.");
                    break;
                case "save":
                    SaveGame();
                    break;
                case "load":
                    LoadGame();
                    break;
                case string cmd when cmd.StartsWith("use "):
                    UseItem(cmd.Substring(4));
                    break;
                case "quit":
                    return false;
                default:
                    Console.WriteLine("Unknown command. Type 'help' for a list of commands.");
                    break;
            }
            return true;
        }

        static void TriggerRandomEvent()
        {
            int eventChance = random.Next(1, 101); // Random number between 1 and 100

            if (eventChance <= 10)
            {
                VeryBadEvent();
            }
            else if (eventChance <= 30)
            {
                BadEvent();
            }
            else if (eventChance <= 70)
            {
                GoodEvent();
            }
            else
            {
                VeryGoodEvent();
            }
        }

        static void VeryBadEvent()
        {
            Console.WriteLine("A very bad event happens! You lose a lot of health.");
            health -= 30;
            CheckHealth();
        }

        static void BadEvent()
        {
            Console.WriteLine("A bad event happens. You lose some health.");
            health -= 10;
            CheckHealth();
        }

        static void GoodEvent()
        {
            Console.WriteLine("A good event happens. You find some gold.");
            AddItemToInventory("gold");
        }

        static void VeryGoodEvent()
        {
            Console.WriteLine("A very good event happens! You find a rare item.");
            AddItemToInventory("rare item");
        }

        static void CheckHealth()
        {
            if (health <= 0)
            {
                Console.WriteLine("You have died. Game over.");
                Environment.Exit(0);
            }
        }

        static void AddItemToInventory(string item)
        {
            inventory.Add(item);
            Console.WriteLine($"You picked up {item}. {itemDescriptions[item]}");
        }

        static void ShowInventory()
        {
            if (inventory.Count == 0)
            {
                Console.WriteLine("Your inventory is empty.");
            }
            else
            {
                Console.WriteLine("Your inventory contains:");
                foreach (var item in inventory)
                {
                    Console.WriteLine($"- {item}: {itemDescriptions[item]}");
                }
            }
        }

        static void UseItem(string item)
        {
            if (inventory.Contains(item))
            {
                inventory.Remove(item);
                Console.WriteLine($"You used {item}. {itemDescriptions[item]}");
                // Implement item-specific behavior here
            }
            else
            {
                Console.WriteLine($"You don't have {item} in your inventory.");
            }
        }

        static void SaveGame()
        {
            using (StreamWriter sw = new StreamWriter("savegame.txt"))
            {
                sw.WriteLine(currentLocation);
                sw.WriteLine(health);
                foreach (var item in inventory)
                {
                    sw.WriteLine(item);
                }
            }
            Console.WriteLine("Game saved.");
        }

        static void LoadGame()
        {
            if (File.Exists("savegame.txt"))
            {
                using (StreamReader sr = new StreamReader("savegame.txt"))
                {
                    currentLocation = sr.ReadLine();
                    health = int.Parse(sr.ReadLine());
                    inventory.Clear();
                    while (!sr.EndOfStream)
                    {
                        inventory.Add(sr.ReadLine());
                    }
                }
                Console.WriteLine("Game loaded.");
            }
            else
            {
                Console.WriteLine("No saved game found.");
            }
        }
    }
}
