using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;
namespace SmallWorld
{
    class Tools
    {
        // general
        private static string moviesPath = "", queriesPath = "", solutionPath = "";
        private static List<string> queries = new List<string>();
        private static Queue<string> answers = new Queue<string>();
        static bool sample;

        // Optimization
        private static Dictionary<int, Dictionary<int, int>> common = new Dictionary<int, Dictionary<int, int>>();
        private static Dictionary<string, int> index = new Dictionary<string, int>();
        private static List<string> moviesNames = new List<string>();
        private static List<List<int>> movies = new List<List<int>>();
        private static List<string> actorsName = new List<string>();
        private static List<HashSet<int>> adjs = new List<HashSet<int>>();
        private static List<List<int>> actorsMovies = new List<List<int>>();
        private static int MAX_DEGREE_FOUND = 0;
        private static int[] dosFrequency;
        // Normal


        private static string SelectTestCase()
        {
            string path = @"..\..\..\Testcases\";
            string choice;
            do
            {

                Console.WriteLine("0.Sample");
                Console.WriteLine("-Complete");

                Console.WriteLine(" -small");
                Console.WriteLine("\t1-Case1");
                Console.WriteLine("\t2-Case2");

                Console.WriteLine(" -medium");
                Console.WriteLine("  -Case1");
                Console.WriteLine("\t3.queries85");
                Console.WriteLine("\t4.queries4000");

                Console.WriteLine("  -Case2");
                Console.WriteLine("\t5.queries110");
                Console.WriteLine("\t6.queries2000");

                Console.WriteLine(" -large");
                Console.WriteLine("\t7.queries26");
                Console.WriteLine("\t8.queries600");


                Console.WriteLine(" -extreme");
                Console.WriteLine("\t9.queries22");
                Console.WriteLine("\t10.queries200");

                Console.Write("Select Test Case: ");
                choice = Console.ReadLine();
                if (!(choice == "0" || choice == "1" || choice == "2" || choice == "3" || choice == "4" || choice == "5" || choice == "6" || choice == "7" || choice == "8" || choice == "9" || choice == "10"))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid Choice");
                    Console.ResetColor();
                }
                else
                    break;
            }
            while (true);
            switch (choice)
            {
                case "0":
                    path += @"Sample\";
                    moviesPath = path + "movies1.txt";
                    queriesPath = path + "queries1.txt";
                    solutionPath = path + "queries1 - Solution.txt";
                    sample = true;
                    break;
                case "1":
                    path += @"Complete\small\Case1\";
                    moviesPath = path + "Movies193.txt";
                    queriesPath = path + "queries110.txt";
                    solutionPath = path + @"\Solution\queries110 - Solution.txt";

                    break;
                case "2":
                    path += @"Complete\small\Case2\";
                    moviesPath = path + "Movies187.txt";
                    queriesPath = path + "queries50.txt";
                    solutionPath = path + @"\Solution\queries50 - Solution.txt";
                    break;
                case "3":
                    path += @"Complete\medium\Case1\";
                    moviesPath = path + "Movies967.txt";
                    queriesPath = path + "queries85.txt";
                    solutionPath = path + @"\Solutions\queries85 - Solution.txt";

                    break;
                case "4":
                    path += @"Complete\medium\Case1\";
                    moviesPath = path + "Movies967.txt";
                    queriesPath = path + "queries4000.txt";
                    solutionPath = path + @"\Solutions\queries4000 - Solution.txt";
                    break;


                case "5":
                    path += @"Complete\medium\Case2\";
                    moviesPath = path + "Movies4736.txt";
                    queriesPath = path + "queries110.txt";
                    solutionPath = path + @"\Solutions\queries110 - Solution.txt";
                    break;
                case "6":
                    path += @"Complete\medium\Case2\";
                    moviesPath = path + "Movies4736.txt";
                    queriesPath = path + "queries2000.txt";
                    solutionPath = path + @"\Solutions\queries2000 - Solution.txt";
                    break;

                case "7":
                    path += @"Complete\large\";
                    moviesPath = path + "Movies14129.txt";
                    queriesPath = path + "queries26.txt";
                    solutionPath = path + @"\Solutions\queries26 - Solution.txt";
                    break;
                case "8":
                    path += @"Complete\large\";
                    moviesPath = path + "Movies14129.txt";
                    queriesPath = path + "queries600.txt";
                    solutionPath = path + @"\Solutions\queries600 - Solution.txt";
                    break;


                case "9":
                    path += @"Complete\extreme\";
                    moviesPath = path + "Movies122806.txt";
                    queriesPath = path + "queries22.txt";
                    solutionPath = path + @"\Solutions\queries22 - Solution.txt";
                    break;
                case "10":
                    path += @"Complete\extreme\";
                    moviesPath = path + "Movies122806.txt";
                    queriesPath = path + "queries200.txt";
                    solutionPath = path + @"\Solutions\queries200 - Solution.txt";
                    break;


            }
            return choice;

        }
        private static void ParseMovies()
        {
            StreamReader reader = new StreamReader(moviesPath);
            while (true)
            {
                string line = reader.ReadLine();
                if (line == null)
                    break;

                string movie_name = line.Substring(0, line.IndexOf('/'));
                List<int> movieActors = new List<int>();
                while (true)
                {
                    line = line.Remove(0, (line.IndexOf('/') != -1) ? line.IndexOf('/') + 1 : line.Length);
                    if (line.Length == 0)
                        break;
                    string actor_name = (line.IndexOf('/') != -1) ? line.Substring(0, line.IndexOf('/')) : line;


                    if (!index.ContainsKey(actor_name))
                    {
                        int newActorIndex = actorsName.Count();
                        index[actor_name] = newActorIndex;
                        movieActors.Add(newActorIndex);
                        actorsMovies.Add(new List<int>());
                        adjs.Add(new HashSet<int>());
                        actorsMovies[newActorIndex].Add(moviesNames.Count());
                        actorsName.Add(actor_name);
                    }
                    else
                    {
                        int prevActorIndex = index[actor_name];
                        movieActors.Add(prevActorIndex);
                        actorsMovies[prevActorIndex].Add(moviesNames.Count());
                    }
                }

                movies.Add(new List<int>());
                movies[moviesNames.Count()].AddRange(movieActors);
                moviesNames.Add(movie_name);
            }
            foreach (var movie in movies)
            {
                foreach (var actor in movie)
                {

                    if (!common.ContainsKey(actor))
                        common[actor] = new Dictionary<int, int>();
                    foreach (var adj in movie)
                    {


                        if (adj != actor)
                        {
                            if (!common[actor].ContainsKey(adj))
                                common[actor][adj] = 0;

                            adjs[actor].Add(adj);
                            common[actor][adj]++;
                        }
                    }
                }
            }
            dosFrequency = new int[index.Count()];
            reader.Close();
        }

        private static void ParseQueries()
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

        private static void ParseSolutions()
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

        private static void RunTestCase()
        {

            int current = 1;
            int passed = 0;
            if (sample)
                Console.WriteLine(answers.Dequeue());
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            foreach (string query in queries)
            {
                string first = query.Substring(0, query.IndexOf('/'));
                string second = query.Substring(query.IndexOf('/') + 1);
                string answer = ""/*Solve(first, second)*/;
                if (answers.Count() == 0)
                {
                    Console.WriteLine(answer);
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.Write("Query {0}/{1} ", current, queries.Count);
                    Console.ResetColor();
                    string actual = answers.Dequeue();
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
            Console.Write($"Optimized Time taken:  {stopwatch.ElapsedMilliseconds / 1000.0} Seconds | {stopwatch.ElapsedMilliseconds} Milliseconds");
            Console.ResetColor();


        }
    }
}
