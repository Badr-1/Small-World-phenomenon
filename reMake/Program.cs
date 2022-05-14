using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;

namespace redone
{
    internal class Program
    {
        static string moviesPath = "", queriesPath = "", solutionPath = "";
        static List<string> queries = new List<string>();
        static Queue<string> answers = new Queue<string>();

        static Dictionary<int, Dictionary<int, int>> weight = new Dictionary<int, Dictionary<int, int>>();
        static Dictionary<string, int> index = new Dictionary<string, int>();
        static List<string> moviesNames = new List<string>();
        static List<List<int>> movies = new List<List<int>>();
        static List<string> actorsName = new List<string>();
        static List<HashSet<int>> adjs = new List<HashSet<int>>();
        static List<List<int>> actorsMovies = new List<List<int>>();

        static void Main(string[] args)
        {
            while (true)
            {

                SelectTestCase();
                Console.WriteLine("Test case selected");
                Console.WriteLine("Parsing...");
                ParseMovies();
                ParseQueries();
                ParseSolutions();
                Console.WriteLine("Parsing done");
                RunTestCase();
                ClearAll();
                Console.ReadLine();
                Console.Clear();
            }
        }

        /// <summary>
        /// Clear All Data Used in the previous test case
        /// </summary>
        public static void ClearAll()
        {
            queries.Clear();
            answers.Clear();
            index.Clear();
            movies.Clear();
            moviesNames.Clear();
            actorsName.Clear();
            adjs.Clear();
            actorsMovies.Clear();
            weight.Clear();
        }

        /// <summary>
        /// Selects the test case to run
        /// </summary>
        public static void SelectTestCase()
        {
            string path = @"..\..\..\Testcases\";
            string choice;
            do
            {
                Console.Clear();
                //Console.WriteLine("0.Sample");

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
            }
            while
            (!
                (
                    //choice == "0"
                    //||
                    choice == "1"
                    ||
                    choice == "2"
                    ||
                    choice == "3"
                    ||
                    choice == "4"
                    ||
                    choice == "5"
                    ||
                    choice == "6"
                    ||
                    choice == "7"
                    ||
                    choice == "8"
                    ||
                    choice == "9"
                    ||
                    choice == "10"
                )
            );
            switch (choice)
            {
                case "0":
                    path += @"Sample\";
                    moviesPath = path + "movies1.txt";
                    queriesPath = path + "queries1.txt";
                    solutionPath = path + "queries1 - Formated Solution.txt";
                    break;
                case "1":
                    path += @"Complete\small\Case1\";
                    moviesPath = path + "Movies193.txt";
                    queriesPath = path + "queries110.txt";
                    solutionPath = path + @"\Solution\queries110 - Solution.txt";
                    //solutionPath = path + @"\Solution\queries110 - Solution - lite.txt";

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
            
        }

        public static void ParseMovies()
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

                    if (!weight.ContainsKey(actor))
                        weight[actor] = new Dictionary<int, int>();
                    foreach (var adj in movie)
                    {

                        
                        if (adj != actor)
                        {
                            if (!weight[actor].ContainsKey(adj))
                                weight[actor][adj] = 0;

                            adjs[actor].Add(adj);
                            weight[actor][adj]++;
                        }
                    }
                }
            }


            reader.Close();
        }

        /// <summary>
        /// Parse the queries file for the Choosen test Case from <c>SelectTestCase</c> method and store the queries in <c>queries</c> list.
        /// </summary>
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

        /// <summary>
        /// Parse the solution file for the Choosen test Case from  <c>SelectTestCase</c> method and stores in <c>answers</c> Queue
        /// </summary>
        public static void ParseSolutions()
        {
            StreamReader reader = new StreamReader(solutionPath);
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
            Console.WriteLine("Solution Parsing Done, {0} answers", answers.Count());
        }

        /// <summary>
        /// Run test Choosen test Case from <c>SelectTestCase()</c> method
        /// </summary>
        public static void RunTestCase()
        {

            int current = 1;
            int passed = 0;
            long start = System.Environment.TickCount;
            foreach (string query in queries)
            {
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.Write("Query {0}/{1} ", current, queries.Count);
                Console.ResetColor();
                string actual = answers.Dequeue();
                string first = query.Substring(0, query.IndexOf('/'));
                string second = query.Substring(query.IndexOf('/') + 1);
                string answer = Solve(first, second);
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
            long end = System.Environment.TickCount;
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write("Time taken: {0} seconds", ((end - start) / 1000.0));
            Console.ResetColor();


        }

        /// <summary>
        /// mix solution between bfs and dfs to find shortest and strongest path from src to dest
        /// </summary>
        /// <param name="src">source actor</param>
        /// <param name="dest">destination actor</param>
        /// <returns>string with answer</returns>
        public static string Solve(string src, string dest)
        {

            int iFirst = index[src];
            int iSecond = index[dest];
            List<int> shortestPath = FindShortestPath(iFirst, iSecond);

            return generateAnswer(shortestPath);
        }


        /// <summary>
        /// Generate an Answer from path to a desired format
        /// </summary>
        /// <param name="path">contains the indices of actors from src to dest</param>
        /// <returns>string following the desired format</returns>
        public static string generateAnswer(List<int> path)
        {
            int degree = 0, relation = 0;
            Stack<string> chainOfActors = new Stack<string>();
            Stack<string> chainOfMovies = new Stack<string>();
            string answer = "";
            answer += $"{actorsName[path.Last()]}/{actorsName[path.First()]}\n";
            for (int i = 0; i < path.Count() - 1; i++)
            {
                //split adjsWeight into movies
                int[] movies = actorsMovies[path[i]].Intersect(actorsMovies[path[i + 1]]).ToArray();
                chainOfActors.Push(actorsName[path[i]]);
                chainOfMovies.Push(moviesNames[movies[0]]);
                relation += movies.Length;
                degree++;
            }
            answer += $"DoS = {degree}, RS = {relation}\n";
            answer += $"CHAIN OF ACTORS: {actorsName[path.Last()]} -> ";
            while (chainOfActors.Count() != 0)
            {
                answer += chainOfActors.Pop();
                if (chainOfActors.Count() != 0)
                    answer += " -> ";
            }
            answer += "\n";
            answer += "CHAIN OF MOVIES:  =>";
            while (chainOfMovies.Count() != 0)
            {
                answer += " " + chainOfMovies.Pop();
                answer += " =>";

            }
            answer += "\n";


            return answer;

        }


        /// <summary>
        /// Find shortest path between src and dest using bfs using weight array
        /// </summary>
        /// <param name="start">Source Actor</param>
        /// <param name="end">Destination Actor</param>
        /// <returns>return shrotest path from <c>start</c> to <c>end</c></returns>
        static List<int> FindShortestPath(int start, int end)
        {
            int[] parent = new int[index.Count()];
            int[] weight = new int[index.Count()];
            bool[] visited = new bool[index.Count()];
            int maxDist = int.MaxValue;
            int[] dist = new int[index.Count()];
            Queue<int> q = new Queue<int>();
            q.Enqueue(start);
            parent[start] = -1;
            weight[start] = 0;

            dist[start] = 0;
            while (q.Count != 0)
            {

                int u = q.Dequeue();
                if (visited[u])
                    continue;
                if (dist[u] + 1 > maxDist)
                    break;

                foreach (int v in adjs[u])
                {
                    if (v == start)
                        continue;
                    if (dist[v] == 0 || dist[v] > dist[u] + 1)
                    {
                        int commonWithU = Program.weight[v][u] /*actorsMovies[v].Intersect(actorsMovies[u]).Count()*/ + weight[u];
                        dist[v] = dist[u] + 1;
                        q.Enqueue(v);
                        parent[v] = -1;
                        weight[v] = 0;
                        parent[v] = u;
                        weight[v] = commonWithU;
                        //checkForShorterPath(u, v, end, parent);
                    }
                    else if (dist[v] == dist[u] + 1)
                    {
                        int commonWithU = Program.weight[v][u] /* actorsMovies[v].Intersect(actorsMovies[u]).Count()*/ + weight[u];
                        int commonWithParent = Program.weight[v][parent[v]] /*actorsMovies[v].Intersect(actorsMovies[parent[v]]).Count()*/ + weight[parent[v]];
                        if (commonWithU > commonWithParent)
                        {
                            parent[v] = u;
                            weight[v] = commonWithU;
                        }
                        //checkForShorterPath(u, v, end, parent);

                    }
                    if (v == end)
                    {
                        maxDist = dist[v];
                    }
                    visited[u] = true;
                }
            }
            return ConstructPath(parent, end);
        }


        /// <summary>
        /// Construct path from parent array
        /// </summary>
        /// <param name="parent">integer array Contains parent index of every actor</param>
        /// <param name="dest">destination actor index</param>
        /// <returns>return path indices</returns>
        public static List<int> ConstructPath(int[] parent, int dest)
        {
            List<int> path = new List<int>();
            int i = dest;
            while (i != -1)
            {
                path.Add(i);
                i = parent[i];
            }
            return path;
        }

    }
}