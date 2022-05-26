using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Diagnostics;
namespace SmallWorld
{
    class Program
    {
        public static string moviesPath = "", queriesPath = "", solutionPath = "";
        public static Queue<string> answers = new Queue<string>();
        public static List<string> queries = new List<string>();
        public static bool sample;
        public static void Run(bool Optimize)
        {
            answers.Clear(); queries.Clear(); sample = false;
            SelectTestCase();
            if (Optimize)
                Optimization.ChooseOpeartion();
            else
            {
                ParseSolutions();
                ParseQueries();
                Normal.ParseMovies();
                RunTestCase(Optimize);
            }
        }
        public static void SelectTestCase()
        {
            string path = @"..\..\..\Testcases\";
            string menu =
            @"0-Sample\n1-Small 1\n2-Small 2\n3-Medium 85\n4-Medium 4000\n5-Medium 110\n6-Medium 2000\n
            7-Large 26\n8-Large 600\n9-Extreme 22\n10-Extreme 200\nSelect Test Case: ";
            int choice;
            do
            {

                Console.Write(menu);
                try
                {
                    choice = int.Parse(Console.ReadLine());
                    if (choice < 0 || choice > 10)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid Choice");
                        Console.ResetColor();
                    }
                    else
                        break;
                }
                catch
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid Choice");
                    Console.ResetColor();
                }
            }
            while (true);
            switch (choice)
            {
                case 0:
                    path += @"Sample\";
                    moviesPath = path + "movies1.txt"; queriesPath = path + "queries1.txt"; solutionPath = path + "queries1 - Solution.txt";
                    sample = true;
                    break;
                case 1:
                    path += @"Complete\small\Case1\"; moviesPath = path + "Movies193.txt"; queriesPath = path + "queries110.txt"; solutionPath = path + @"\Solution\queries110 - Solution.txt";
                    break;
                case 2:
                    path += @"Complete\small\Case2\"; moviesPath = path + "Movies187.txt"; queriesPath = path + "queries50.txt"; solutionPath = path + @"\Solution\queries50 - Solution.txt";
                    break;
                case 3:
                    path += @"Complete\medium\Case1\"; moviesPath = path + "Movies967.txt"; queriesPath = path + "queries85.txt"; solutionPath = path + @"\Solutions\queries85 - Solution.txt";
                    break;
                case 4:
                    path += @"Complete\medium\Case1\"; moviesPath = path + "Movies967.txt"; queriesPath = path + "queries4000.txt"; solutionPath = path + @"\Solutions\queries4000 - Solution.txt";
                    break;
                case 5:
                    path += @"Complete\medium\Case2\"; moviesPath = path + "Movies4736.txt"; queriesPath = path + "queries110.txt"; solutionPath = path + @"\Solutions\queries110 - Solution.txt";
                    break;
                case 6:
                    path += @"Complete\medium\Case2\"; moviesPath = path + "Movies4736.txt"; queriesPath = path + "queries2000.txt"; solutionPath = path + @"\Solutions\queries2000 - Solution.txt";
                    break;
                case 7:
                    path += @"Complete\large\"; moviesPath = path + "Movies14129.txt"; queriesPath = path + "queries26.txt"; solutionPath = path + @"\Solutions\queries26 - Solution.txt";
                    break;
                case 8:
                    path += @"Complete\large\"; moviesPath = path + "Movies14129.txt"; queriesPath = path + "queries600.txt"; solutionPath = path + @"\Solutions\queries600 - Solution.txt";
                    break;
                case 9:
                    path += @"Complete\extreme\"; moviesPath = path + "Movies122806.txt"; queriesPath = path + "queries22.txt"; solutionPath = path + @"\Solutions\queries22 - Solution.txt";
                    break;
                case 10:
                    path += @"Complete\extreme\"; moviesPath = path + "Movies122806.txt"; queriesPath = path + "queries200.txt"; solutionPath = path + @"\Solutions\queries200 - Solution.txt";
                    break;
            }

        }
        public static void ParseSolutions()
        {
            StreamReader reader = new StreamReader(solutionPath);
            if (!sample)
            {
                List<string> answer = new List<string>();
                while (true)
                {
                    string line = reader.ReadLine();
                    if (line == null)
                        break;
                    if (line == "")
                    {
                        string ans;
                        ans = answer[0] + answer[1] + answer[2] + answer[3];
                        answers.Enqueue(ans);
                        answer.Clear();
                    }
                    else
                    {
                        answer.Add(line + "\n");
                    }

                }
            }
            else
            {

                while (true)
                {
                    string line = reader.ReadLine();
                    if (line == null)
                        break;
                    answers.Enqueue(line);

                }
            }
        }
        public static void ParseQueries()
        {
            StreamReader reader = new StreamReader(queriesPath);
            while (true)
            {
                string line = reader.ReadLine();
                if (line == null)
                    break;
                queries.Add(line);
            }
        }
        public static void RunTestCase(bool Optimize)
        {

            int current = 1;
            int passed = 0;
            if (sample)
                Console.WriteLine(Program.answers.Dequeue());
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            foreach (string query in Program.queries)
            {
                string first = query.Substring(0, query.IndexOf('/'));
                string second = query.Substring(query.IndexOf('/') + 1);
                string answer;
                if (Optimize)
                    answer = Optimization.Solve(first, second);
                else
                    answer = Normal.Solve(first, second);
                if (Program.answers.Count() == 0)
                {
                    Console.WriteLine(answer);
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.Write("Query {0}/{1} ", current, Program.queries.Count);
                    Console.ResetColor();
                    string actual = Program.answers.Dequeue();
                    if (answer != actual)
                    {
                        Console.WriteLine("+=======================+");
                        Console.WriteLine("Query {0} failed: {1}/{2}", current, first, second);
                        Console.WriteLine("Expected:\n {0}", actual);
                        Console.WriteLine("Actual:\n {0}", answer);

                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write($" Failed\n");
                        Console.ResetColor();
                    }
                    else
                    {
                        passed++;
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write($" Passed\n");
                        Console.ResetColor();
                        Console.WriteLine(answer);
                    }

                    current++;
                }

            }
            stopwatch.Stop();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            if (Optimize)
                Console.Write("Optimized ");
            Console.Write($"Time taken:  {stopwatch.ElapsedMilliseconds / 1000.0} Seconds |");
            Console.Write($"{stopwatch.ElapsedMilliseconds} Milliseconds\n");
            Console.ResetColor();
        }
    }
}
