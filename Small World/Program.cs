using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace SmallWorld
{
    internal class Program
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
                    Normal.Run();
                else if (mode == "2")
                    Optimization.Run();
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
