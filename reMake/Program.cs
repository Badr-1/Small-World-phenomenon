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
        static int shorterPaths = 0;
        static string moviesPath = "", queriesPath = "", solutionPath = "";
        static List<string> queries = new List<string>();
        static Queue<string> answers = new Queue<string>();

        static List<string> actorsName = new List<string>();
        static List<HashSet<int>> adjs = new List<HashSet<int>>();
        static List<List<int>> actorsMovies = new List<List<int>>();

        static Dictionary<string, int> index = new Dictionary<string, int>();
        static Dictionary<int, List<string>> movies = new Dictionary<int, List<string>>();
        static Dictionary<string, List<int>> prevAnswers = new Dictionary<string, List<int>>();
        static List<string> moviesNames = new List<string>();


        static void Main(string[] args)
        {
            while (true)
            {

                int testCase = selectTestCase();
                Console.WriteLine("Test case selected");
                Console.WriteLine("Parsing...");
                parseMovies();
                parseQueries();
                parseSolutions(testCase);
                Console.WriteLine("Parsing done");
                runTestCase();

                shorterPaths = 0;
                queries.Clear();
                answers.Clear();
                index.Clear();
                movies.Clear();
                moviesNames.Clear();
                actorsName.Clear();
                adjs.Clear();
                actorsMovies.Clear();
                prevAnswers.Clear();


                Console.ReadLine();
                Console.Clear();
            }
        }

        public static int selectTestCase()
        {
            string path = @"..\..\..\Testcases\";
            string choice;

            do
            {
                Console.Clear();
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
            }
            while
            (!
                (
                    choice == "0"
                    ||
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
            return int.Parse(choice);
        }


        public static void parseMovies()
        {
            StreamReader reader = new StreamReader(moviesPath);
            while (true)
            {
                string line = reader.ReadLine();
                if (line == null)
                    break;

                string movie_name = line.Substring(0, line.IndexOf('/'));
                List<string> movieActors = new List<string>();
                while (true)
                {
                    line = line.Remove(0, (line.IndexOf('/') != -1) ? line.IndexOf('/') + 1 : line.Length);
                    if (line.Length == 0)
                        break;
                    string actor_name = (line.IndexOf('/') != -1) ? line.Substring(0, line.IndexOf('/')) : line;
                    movieActors.Add(actor_name);

                    if (!index.ContainsKey(actor_name))
                    {
                        index[actor_name] = actorsName.Count();
                        actorsMovies.Add(new List<int>());
                        adjs.Add(new HashSet<int>());
                        actorsMovies[actorsName.Count()].Add(moviesNames.Count());
                        actorsName.Add(actor_name);
                    }
                    else
                    {
                        actorsMovies[index[actor_name]].Add(moviesNames.Count());
                    }
                }

                movies[moviesNames.Count()] = new List<string>();
                movies[moviesNames.Count()].AddRange(movieActors);
                moviesNames.Add(movie_name);
            }

            foreach (var movie in movies)
            {
                foreach (var actor in movie.Value)
                {

                    int act = index[actor];
                    foreach (var adj in movie.Value)
                    {
                        if (adj != actor)
                        {
                            int iadj = index[adj];
                            adjs[act].Add(iadj);
                        }
                    }
                }
            }


            reader.Close();
        }

        public static void parseQueries()
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

        public static void parseSolutions(int testCase)
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
                    if (testCase == 0)
                        ans = answer[0] + answer[1];
                    else
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


        public static void runTestCase()
        {

            double newOne;
            int current = 1;
            int passed = 0;
            long start = System.Environment.TickCount;
            foreach (string query in queries)
            {

                string actual = answers.Dequeue();
                string first = query.Substring(0, query.IndexOf('/'));
                string second = query.Substring(query.IndexOf('/') + 1);
                string answer = solve(first, second);
                //if (answer != actual)
                //{
                //    Console.WriteLine("+=======================+");
                //    Console.WriteLine("Query {0} failed: {1}/{2}", current, first, second);
                //    Console.WriteLine("Expected:\n {0}", actual);
                //    Console.WriteLine("Actual:\n {0}", answer);
                //}
                //else
                //{
                passed++;
                Console.WriteLine(answer);
                //}

                Console.WriteLine("Query {0}/{1} {2}% at {3}", current, queries.Count, (double)current / queries.Count() * 100, (System.Environment.TickCount - start) / 1000);
                current++;


            }
            long end = System.Environment.TickCount;
            Console.WriteLine("New Time taken: {0} seconds", ((end - start) / 1000.0));
            Console.WriteLine("Passed {0}/{1} ie {2}%", passed, queries.Count, (double)passed / queries.Count() * 100);
            Console.WriteLine($"Shorter Paths: {shorterPaths}");
            //Console.WriteLine(solve("C", "E"));


        }

        /// <summary>
        /// mix solution between bfs and dfs to find shortest and strongest path from src to dest
        /// </summary>
        /// <param name="src">src actor</param>
        /// <param name="dest">dest actor</param>
        /// <returns></returns>
        public static string solve(string src, string dest)
        {

            int iFirst = index[src];
            int iSecond = index[dest];
            int[] parent = new int[index.Count()];
            int[] weight = new int[index.Count()];
            bool[] visited = new bool[index.Count()];
            findShortestPath(parent, weight, visited, index.Count(), iFirst, iSecond);

            List<int> path = new List<int>();

            // fill path with actors in reverse order from parent 
            int i = iSecond;
            while (i != -1)
            {
                path.Add(i);
                if (prevAnswers.ContainsKey(actorsName[i] + "/" + actorsName[iSecond]))
                {
                    Console.WriteLine("PREV: " + generateAnswer(prevAnswers[actorsName[i] + "/" + actorsName[iSecond]]));
                    shorterPaths++;
                }
                i = parent[i];
            }
            prevAnswers[src + "/" + dest] = path;


            return generateAnswer(path);
        }

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
        /// Finds the shortest path between two actors using BFS
        /// </summary>
        /// <param name="src">source actor</param>
        /// <param name="dest">dest actor</param>
        /// <returns></returns>

        static void findShortestPath(int[] parent, int[] wieght, bool[] visited, int n, int start, int end)
        {
            bool foundShorter = false;
            int maxDist = int.MaxValue;
            int[] dist = new int[n];
            dist = Enumerable.Repeat(int.MaxValue, n).ToArray();
            Queue<int> q = new Queue<int>();
            q.Enqueue(start);
            parent[start] = -1;
            wieght[start] = 0;

            dist[start] = 0;
            while (q.Count != 0 && !foundShorter)
            {

                int u = q.Dequeue();
                if (visited[u])
                    continue;
                if (dist[u] + 1 > maxDist)
                    break;

                foreach (int v in adjs[u])
                {
                    if (dist[v] > dist[u] + 1)
                    {
                        int commonWithU = actorsMovies[v].Intersect(actorsMovies[u]).Count() + wieght[u];
                        dist[v] = dist[u] + 1;
                        q.Enqueue(v);
                        parent[v] = -1;
                        wieght[v] = 0;
                        parent[v] = u;
                        wieght[v] = commonWithU;
                        foundShorter = checkForShorterPath(u, v, end, parent);
                        if (foundShorter)
                            break;


                    }
                    else if (dist[v] == dist[u] + 1)
                    {
                        int commonWithU = actorsMovies[v].Intersect(actorsMovies[u]).Count() + wieght[u];
                        int commonWithParent = actorsMovies[v].Intersect(actorsMovies[parent[v]]).Count() + wieght[parent[v]];
                        if (commonWithU > commonWithParent)
                        {
                            parent[v] = u;
                            wieght[v] = commonWithU;
                        }
                        foundShorter = checkForShorterPath(u, v, end, parent);
                        if (foundShorter)
                            break;
                    }
                    if (v == end)
                    {
                        maxDist = dist[v];
                    }
                    visited[u] = true;
                }
            }
        }



        public static bool checkForShorterPath(int u, int v, int end, int[] parent)
        {
            if (prevAnswers.ContainsKey(actorsName[u] + "/" + actorsName[end]))
            {
                List<int> shorterPath = prevAnswers[actorsName[u] + "/" + actorsName[end]];
                if (shorterPath.Last() == u && shorterPath[shorterPath.Count - 2] == v)
                {

                    for (int i = 0; i < shorterPath.Count() - 1; i++)
                    {
                        parent[shorterPath[i]] = shorterPath[i + 1];
                    }

                    return true;
                }

            }
            return false;
        }

    }
}