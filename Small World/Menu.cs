using System;
namespace SmallWorld
{
    internal class Menu
    {
        private static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Cyan; //O(1)
            Console.WriteLine("Welcome to Small World!"); //O(1)
            Console.ResetColor(); //O(1)
            while (true)
            {
                Console.WriteLine("1. Normal"); //O(1)
                Console.WriteLine("2. Optimization"); //O(1)
                Console.ForegroundColor = ConsoleColor.Red; //O(1)
                Console.WriteLine("3. Exit"); //O(1)
                Console.ForegroundColor = ConsoleColor.DarkYellow; //O(1)
                Console.Write("Choice: "); //O(1)
                string mode = Console.ReadLine(); //O(1)
                Console.ResetColor(); //O(1)
                if (mode == "1") //O(1)
                    Program.Run(false); //O(V + E)
                else if (mode == "2") //O(1)
                    Program.Run(true); //O(V + E)
                else if (mode == "3") //O(1)
                    break;
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red; //O(1)
                    Console.WriteLine("Invalid mode"); //O(1)
                    Console.ResetColor(); //O(1)
                }
            }
        }
    }
}
