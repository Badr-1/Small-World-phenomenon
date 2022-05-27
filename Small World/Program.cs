using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Diagnostics;
namespace SmallWorld
{
    class Program
    {
        public static string moviesPath = "", queriesPath = "", solutionPath = ""; //O(1)
        public static Queue<string> answers = new Queue<string>(); //O(1)
        public static List<string> queries = new List<string>(); //O(1)
        public static bool sample; //O(1)
        //O(V + E)
        public static void Run(bool Optimize)
        {
            answers.Clear(); queries.Clear(); sample = false; //O(1)
            SelectTestCase(); //O(1)
            if (Optimize) //O(1)
                Optimization.ChooseOpeartion(); //O(V + E)
            else
            {
                ParseSolutions(); //O(N)
                ParseQueries(); //O(N)
                Normal.ParseMovies(); //O(N * M)
                RunTestCase(Optimize); //O(V + E)
            }
        }
        //O(1)
        public static void SelectTestCase()
        {
            string path = @"..\..\..\Testcases\"; //O(1)
            string menu =
            @"0-Sample\n1-Small 1\n2-Small 2\n3-Medium 85\n4-Medium 4000\n5-Medium 110\n6-Medium 2000\n
            7-Large 26\n8-Large 600\n9-Extreme 22\n10-Extreme 200\nSelect Test Case: "; //O(1)
            int choice; //O(1)
            do
            {
                Console.Write(menu); //O(1)
                try
                {
                    choice = int.Parse(Console.ReadLine()); //O(1)
                    if (choice < 0 || choice > 10) //O(1)
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
        //O(N)
        public static void ParseSolutions()
        {
            StreamReader reader = new StreamReader(solutionPath); //O(1)
            if (!sample) //O(1)
            {
                List<string> answer = new List<string>(); //O(1)
                while (true) //O(N)
                {
                    string line = reader.ReadLine(); //O(1)
                    if (line == null) //O(1)
                        break;
                    if (line == "") //O(1)
                    {
                        string ans; //O(1)
                        ans = answer[0] + answer[1] + answer[2] + answer[3]; //O(1)
                        answers.Enqueue(ans); //O(1)
                        answer.Clear(); //O(1)
                    }
                    else answer.Add(line + "\n"); //O(1)
                }
            }
            else
            {
                while (true) //O(N)  
                {
                    string line = reader.ReadLine(); //O(1)
                    if (line == null) //O(1)
                        break; //O(1)
                    answers.Enqueue(line); //O(1)
                }
            }
        }
        //O(N)
        public static void ParseQueries()
        {
            StreamReader reader = new StreamReader(queriesPath); //O(1)
            while (true) //O(N)
            {
                string line = reader.ReadLine(); //O(1)
                if (line == null) //O(1)
                    break; //O(1)
                queries.Add(line); //O(1)
            }
        }
        //O(V + E)
        public static void RunTestCase(bool Optimize)
        {

            int current = 1; //O(1)
            int passed = 0; //O(1)
            if (sample) //O(1)
                Console.WriteLine(Program.answers.Dequeue()); //O(1)
            Stopwatch stopwatch = new Stopwatch(); //O(1)
            stopwatch.Start(); //O(1)
            foreach (string query in Program.queries) //O(V + E)
            {
                string first = query.Substring(0, query.IndexOf('/')); //O(1)
                string second = query.Substring(query.IndexOf('/') + 1); //O(1)
                string answer; //O(1)
                if (Optimize)  //O(1)
                    answer = Optimization.Solve(first, second); //O(V + E)
                else
                    answer = Normal.Solve(first, second); //O(V + E)
                if (Program.answers.Count() == 0) Console.WriteLine(answer); //O(1)
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkCyan; //O(1)
                    Console.Write("Query {0}/{1} ", current, Program.queries.Count); //O(1)
                    Console.ResetColor(); //O(1)
                    string actual = Program.answers.Dequeue(); //O(1)
                    if (answer != actual) //O(1)
                    {
                        Console.WriteLine("+=======================+"); //O(1)
                        Console.WriteLine("Query {0} failed: {1}/{2}", current, first, second); //O(1)
                        Console.WriteLine("Expected:\n {0}", actual); //O(1)
                        Console.WriteLine("Actual:\n {0}", answer); //O(1)
                        Console.ForegroundColor = ConsoleColor.Red; //O(1)
                        Console.Write($" Failed\n"); //O(1)
                        Console.ResetColor(); //O(1)
                    }
                    else
                    {
                        passed++; //O(1)
                        Console.ForegroundColor = ConsoleColor.Green; //O(1)
                        Console.Write($" Passed\n"); //O(1)
                        Console.ResetColor(); //O(1)
                        Console.WriteLine(answer); //O(1)
                    }

                    current++; //O(1)
                }

            }
            stopwatch.Stop(); //O(1)
            Console.ForegroundColor = ConsoleColor.DarkYellow; //O(1)
            if (Optimize) //O(1)
                Console.Write("Optimized "); //O(1)
            Console.Write($"Time taken:  {stopwatch.ElapsedMilliseconds / 1000.0} Seconds |"); //O(1)
            Console.Write($"{stopwatch.ElapsedMilliseconds} Milliseconds\n"); //O(1)
            Console.ResetColor(); //O(1)
        }
    }
}
