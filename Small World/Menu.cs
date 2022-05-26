using System;
namespace SmallWorld
{
    internal class Menu
    {
        private static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Welcome to Small World!");
            Console.ResetColor();
            while (true)
            {
                Console.WriteLine("1. Normal");
                Console.WriteLine("2. Optimization");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("3. Exit");
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write("Choice: ");
                string mode = Console.ReadLine();
                Console.ResetColor();
                if (mode == "1")
                    Program.Run(false);
                else if (mode == "2")
                    Program.Run(true);
                else if (mode == "3")
                    break;
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid mode");
                    Console.ResetColor();
                   
                }
            }
        }
    }
}
