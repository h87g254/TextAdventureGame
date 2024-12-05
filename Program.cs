using System;
using System.Collections.Generic;

namespace TextAdventureGame
{
    class Program
    {
        static Random random = new Random();
        static List<string> inventory = new List<string>();

        static void Main(string[] args)
        {
            // Game initialization
            Console.WriteLine("Welcome to the Text Adventure Game!");
            Console.WriteLine("You find yourself in a mysterious place. What will you do?");
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
                    Console.WriteLine("Available commands: help, look, go [direction], inventory, use [item], quit");
                    break;
                case "look":
                    Console.WriteLine("You see nothing of interest.");
                    TriggerRandomEvent();
                    break;
                case "go north":
                case "go south":
                case "go east":
                case "go west":
                    Console.WriteLine($"You go {command.Substring(3)}.");
                    TriggerRandomEvent();
                    break;
                case "inventory":
                    ShowInventory();
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
        }

        static void BadEvent()
        {
            Console.WriteLine("A bad event happens. You lose some health.");
        }

        static void GoodEvent()
        {
            Console.WriteLine("A good event happens. You find some gold.");
            AddItemToInventory("gold");
        }

        static void VeryGoodEvent()
        {
            Console.WriteLine("A very good event happens! You find a rare item.");
            var random = new Random(10);
            switch(random):
                case 0
            AddItemToInventory("rare item");
        }

        static void AddItemToInventory(string item)
        {
            inventory.Add(item);
            Console.WriteLine($"You picked up {item}.");
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
                    Console.WriteLine($"- {item}");
                }
            }
        }

        static void UseItem(string item)
        {
            if (inventory.Contains(item))
            {
                inventory.Remove(item);
                Console.WriteLine($"You used {item}.");
                // Implement item-specific behavior here
            }
            else
            {
                Console.WriteLine($"You don't have {item} in your inventory.");
            }
        }
    }
}
