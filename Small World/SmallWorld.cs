using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;

namespace SmallWorld
{
    class Optimization
    {
        private static string moviesPath = "", queriesPath = "", solutionPath = "";
        private static List<string> queries = new List<string>();
        private static Queue<string> answers = new Queue<string>();

        private static Dictionary<int, Dictionary<int, int>> common = new Dictionary<int, Dictionary<int, int>>();
        private static Dictionary<string, int> index = new Dictionary<string, int>();
        private static List<string> moviesNames = new List<string>();
        private static List<List<int>> movies = new List<List<int>>();
        private static List<string> actorsName = new List<string>();
        private static List<HashSet<int>> adjs = new List<HashSet<int>>();
        private static List<List<int>> actorsMovies = new List<List<int>>();

        /// <summary>
        /// Runs the Optimization
        /// </summary>
        public static void Run()
        {
            SelectTestCase();
            Console.WriteLine("Test case selected");
            Console.WriteLine("Parsing...");
            ParseMovies();
            ParseQueries();
            ParseSolutions();
            Console.WriteLine("Parsing done");
            ChooseOpeartion();
            ClearAll();
            Console.ReadLine();
            Console.Clear();
        }

        private static void ChooseOpeartion()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("1.Find Shortest Path (Completed)");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("2.Find Strongest Path (Pending)");
            Console.WriteLine("3.Find Degree of Seperation Frequency (Pending)");
            Console.WriteLine("4.Find Minimum Movies that link all actors/actresses (Pending)");
            Console.ResetColor();
            Console.Write("Enter your choice: ");
            string Choice = Console.ReadLine();
            switch (Choice)
            {
                case "1":
                    RunTestCase();
                    break;
                case "2":
                    break;
                case "3":
                    break;
                case "4":
                    break;
            }
        }


        /// <summary>
        /// Clear All Data Used in the previous test case
        /// </summary>
        private static void ClearAll()
        {
            queries.Clear();
            answers.Clear();
            index.Clear();
            movies.Clear();
            moviesNames.Clear();
            actorsName.Clear();
            adjs.Clear();
            actorsMovies.Clear();
            common.Clear();
        }

        /// <summary>
        /// Selects the test case to run
        /// </summary>
        private static void SelectTestCase()
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
                    solutionPath = path + "queries1 - Solution.txt";
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


            reader.Close();
        }

        /// <summary>
        /// Parse the queries file for the Choosen test Case from <c>SelectTestCase</c> method and store the queries in <c>queries</c> list.
        /// </summary>
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

        /// <summary>
        /// Parse the solution file for the Choosen test Case from  <c>SelectTestCase</c> method and stores in <c>answers</c> Queue
        /// </summary>
        private static void ParseSolutions()
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
        private static void RunTestCase()
        {

            int current = 1;
            int passed = 0;
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            foreach (string query in queries)
            {
                string first = query.Substring(0, query.IndexOf('/'));
                string second = query.Substring(query.IndexOf('/') + 1);
                string answer = Solve(first, second);
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

        /// <summary>
        /// mix solution between bfs and dfs to find shortest and strongest path from src to dest
        /// </summary>
        /// <param name="src">source actor</param>
        /// <param name="dest">destination actor</param>
        /// <returns>string with answer</returns>
        private static string Solve(string src, string dest)
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
        private static string generateAnswer(List<int> path)
        {
            int degree = 0, relation = 0;
            Stack<string> chainOfActors = new Stack<string>();
            Stack<string> chainOfMovies = new Stack<string>();
            string answer = "";
            answer += $"{actorsName[path.Last()]}/{actorsName[path.First()]}\n";
            for (int i = 0; i < path.Count() - 1; i++)
            {
                List<int> movies = GetIntersection(actorsMovies[path[i]], (actorsMovies[path[i + 1]]));
                chainOfActors.Push(actorsName[path[i]]);
                chainOfMovies.Push(moviesNames[movies[0]]);
                relation += movies.Count;
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
        private static List<int> FindShortestPath(int start, int end)
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
                        int commonWithU = common[v][u] + weight[u];
                        dist[v] = dist[u] + 1;
                        q.Enqueue(v);
                        parent[v] = -1;
                        weight[v] = 0;
                        parent[v] = u;
                        weight[v] = commonWithU;
                    }
                    else if (dist[v] == dist[u] + 1)
                    {

                        int commonWithU = common[v][u] + weight[u];
                        int commonWithParent = common[v][parent[v]] + weight[parent[v]];
                        if (commonWithU > commonWithParent)
                        {
                            parent[v] = u;
                            weight[v] = commonWithU;
                        }
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
        /// Takes two Lists and return the intersection of the two with complexity O(n)
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns>Intersection between two Lists</returns>
        private static List<int> GetIntersection(List<int> first, List<int> second)
        {
            int fIter = 0;
            int sIter = 0;
            var result = new List<int>();
            while (fIter < first.Count && sIter < second.Count)
            {
                if (first[fIter] < second[sIter])
                {
                    fIter++;
                }
                else if (first[fIter] > second[sIter])
                {
                    sIter++;
                }
                else
                {
                    result.Add(first[fIter]);
                    fIter++;
                    sIter++;

                }
            }
            return result;
        }


        /// <summary>
        /// Construct path from parent array
        /// </summary>
        /// <param name="parent">integer array Contains parent index of every actor</param>
        /// <param name="dest">destination actor index</param>
        /// <returns>return path indices</returns>
        private static List<int> ConstructPath(int[] parent, int dest)
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
    class Normal
    {

        private static string moviesPath, queriesPath, solutionPath;
        private static List<KeyValuePair<string, string>> queries = new List<KeyValuePair<string, string>>();
        private static Queue<string> answers = new Queue<string>();
        private static Dictionary<string, HashSet<string>> actorsInMovie = new Dictionary<string, HashSet<string>>();
        private static Dictionary<string, HashSet<string>> moviesOfActor = new Dictionary<string, HashSet<string>>();
        public static void Run()
        {
            SelectTestCase();
            Console.WriteLine("Parsing Test Case ...");
            ParseMovies();
            ParseQueries();
            ParseSolution();
            Console.WriteLine("Parsing Is Done");
            RunTestCase();
            ClearAll();
            Console.ReadLine();
            Console.Clear();
        }
        private static void ClearAll()
        {
            actorsInMovie.Clear();
            moviesOfActor.Clear();
            queries.Clear();
            answers.Clear();
        }
        private static void ParseSolution()
        {
            StreamReader reader = new StreamReader(solutionPath);
            string answer = "";
            while (true)
            {
                string line = reader.ReadLine();
                if (line == null)
                    break;
                if (line == "")
                {
                    answers.Enqueue(answer);
                    answer = "";
                }
                else
                {
                    answer += line + "\n";
                }

            }
            Console.WriteLine("Solution Parsing Done, {0} answers", answers.Count());
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
                actorsInMovie[movie_name] = new HashSet<string>();
                Movie movie = new Movie(movie_name);
                while (true)
                {
                    line = line.Remove(0, (line.IndexOf('/') != -1) ? line.IndexOf('/') + 1 : line.Length);
                    if (line.Length == 0)
                        break;
                    string actor_name = (line.IndexOf('/') != -1) ? line.Substring(0, line.IndexOf('/')) : line;
                    actorsInMovie[movie_name].Add(actor_name);
                    if (!moviesOfActor.ContainsKey(actor_name))
                        moviesOfActor[actor_name] = new HashSet<string>();
                    moviesOfActor[actor_name].Add(movie_name);
                    movie.actors.Add(actor_name);

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
                string key = line.Substring(0, line.IndexOf('/'));
                line = line.Remove(0, key.Length);
                string value = line.Substring(1);
                queries.Add(new KeyValuePair<string, string>(key, value));
            }
        }
        private static void SelectTestCase()
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
                    solutionPath = path + "queries1 - Solution.txt";
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

        }
        public static void RunTestCase()
        {
            int current = 1;
            int passed = 0;
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            foreach (KeyValuePair<string, string> kvp in queries)
            {
                string answer = Solve(kvp.Key, kvp.Value);
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
                        Console.WriteLine("Query {0} failed: {1}/{2}", current, kvp.Key, kvp.Value);
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
            Console.Write($"Time taken:  {stopwatch.ElapsedMilliseconds / 1000.0} Seconds | {stopwatch.ElapsedMilliseconds} Milliseconds");
            Console.ResetColor();

        }
        public static string Solve(string who, string whom)
        {

            int degree = 1, answerDegree = int.MaxValue, maxRelation = 0;
            List<Answer> answers = new List<Answer>();
            Dictionary<string, Actor> dictActors = new Dictionary<string, Actor>();
            Queue<Actor> actorsToAsk = new Queue<Actor>();
            HashSet<string> lvlActors = new HashSet<string>();
            HashSet<string> nextLvlActors = new HashSet<string>();
            HashSet<string> lvlAskedActors = new HashSet<string>();
            dictActors[who] = new Actor(who, null);
            actorsToAsk.Enqueue(dictActors[who]);
            lvlActors.Add(who);
            while (actorsToAsk.Count() != 0)
            {
                Actor actor = actorsToAsk.Dequeue();
                List<string> movies = moviesOfActor[actor.name].ToList();
                foreach (string movie in movies)
                {

                    if (actor.askedMovies.ContainsKey(movie) && actor.askedMovies[movie] == true)
                        continue;
                    else
                        actor.askedMovies[movie] = false;


                    List<string> actors = actorsInMovie[movie].ToList();

                    foreach (string actorInMovie in actors)
                    {
                        Actor movieActor = new Actor(actorInMovie, actor);
                        if (dictActors.ContainsKey(actorInMovie))
                        {
                            movieActor = dictActors[actorInMovie];
                        }
                        else
                        {
                            if (actorInMovie != actor.name)
                            {

                                List<string> commonMovies = moviesOfActor[actorInMovie].Intersect(moviesOfActor[actor.name]).ToList();
                                movieActor.commonMovies = commonMovies;
                                movieActor.weight = movieActor.parent.weight + movieActor.commonMovies.Count();
                            }
                            dictActors[actorInMovie] = movieActor;
                        }

                        if (!movieActor.asked && actorInMovie != actor.name && actorInMovie != whom)
                        {
                            if (!movieActor.toSkip)
                            {
                                actorsToAsk.Enqueue(movieActor);
                                movieActor.toSkip = true;
                            }

                            if (lvlActors.Contains(actor.name) && !lvlActors.Contains(actorInMovie))
                            {
                                List<string> commonMovies = moviesOfActor[actorInMovie].Intersect(moviesOfActor[actor.name]).ToList();

                                nextLvlActors.Add(actorInMovie);

                                if (actor.weight + commonMovies.Count() > movieActor.parent.weight + movieActor.commonMovies.Count())
                                {
                                    movieActor.parent = actor;
                                    movieActor.commonMovies = commonMovies;
                                    movieActor.weight = movieActor.parent.weight + movieActor.commonMovies.Count();
                                }

                            }

                        }
                        if (actorInMovie == whom)
                        {
                            actor.askedMovies[movie] = true;
                            Answer answer = new Answer();
                            Actor whomActor = new Actor(whom, actor);
                            Stack<string> moviesList = new Stack<string>();
                            Stack<string> actorList = new Stack<string>();
                            Actor curr = new Actor(whom, actor);

                            int relation = 0;
                            do
                            {
                                if (curr == null || curr.parent == null)
                                    break;

                                List<string> commonMoviesList = moviesOfActor[curr.name].Intersect(moviesOfActor[curr.parent.name]).ToList();
                                string commonMovies = commonMoviesList[0];
                                for (int i = 0; i < commonMoviesList.Count(); i++)
                                {
                                    relation++;
                                    string elm = commonMoviesList.ElementAt(i);
                                    actor.askedMovies[elm] = true;
                                }

                                moviesList.Push(commonMovies);
                                actorList.Push(curr.name);
                                curr = curr.parent;

                            } while (true);

                            maxRelation = (maxRelation < relation) ? relation : maxRelation;

                            answer.moviesList = moviesList;
                            answer.actorList = actorList;
                            answer.degree = degree;
                            answer.relation = relation;
                            answerDegree = degree;
                            answers.Add(answer);
                        }
                    }

                }


                actor.asked = true;
                lvlAskedActors.Add(actor.name);
                if (lvlActors.Count() == lvlAskedActors.Count())
                {

                    degree++;
                    lvlActors.Clear();
                    lvlAskedActors.Clear();
                    lvlActors = nextLvlActors;
                    nextLvlActors = new HashSet<string>();
                }
                if (answerDegree < degree)
                    break;
            }
            foreach (Answer answer in answers)
            {
                if (answer.relation == maxRelation)
                {
                    return answer.print(who, whom);
                }
            }



            return "";
        }
        public class Actor
        {
            public int weight = 0;
            public string name;
            public Actor parent;
            public bool asked = false;
            public bool toSkip = false;
            public List<string> commonMovies = new List<string>();
            public Dictionary<string, bool> askedMovies = new Dictionary<string, bool>();
            public Actor(string name, Actor parent)
            {
                this.name = name;
                this.parent = parent;
            }

            public Actor(int weight, string name, Actor parent, bool asked, bool toSkip, List<string> commonMovies, Dictionary<string, bool> askedMovies)
            {
                this.weight = weight;
                this.name = name;
                this.parent = parent;
                this.asked = asked;
                this.toSkip = toSkip;
                this.commonMovies = commonMovies;
                this.askedMovies = askedMovies;
            }

            public Actor copy()
            {
                return new Actor(weight, name, parent, asked, toSkip, commonMovies, askedMovies);
            }
            public void setWeight()
            {
                Actor dummy = copy();
                weight = 0;
                while (dummy != null)
                {
                    weight += dummy.commonMovies.Count();
                    dummy = dummy.parent;
                }
            }

        }
        public class Movie
        {
            public string name;
            public List<string> actors = new List<string>();
            public Movie(string name)
            {
                this.name = name;
            }
        }
        public class Answer
        {
            public int degree;
            public int relation;
            public Stack<string> moviesList = new Stack<string>();
            public Stack<string> actorList = new Stack<string>();

            public string print(string who, string whom)
            {
                string answer = "";
                answer += String.Format("{0}/{1}\n", who, whom);
                answer += String.Format("DoS = {0}, RS = {1}\n", degree, relation);
                answer += String.Format("CHAIN OF ACTORS: {0} -> ", who);
                while (actorList.Count() != 0)
                {
                    answer += actorList.Pop();
                    if (actorList.Count() != 0)
                        answer += " -> ";
                }
                answer += "\n";
                answer += "CHAIN OF MOVIES:  =>";
                while (moviesList.Count() != 0)
                {
                    answer += " " + moviesList.Pop();
                    answer += " =>";

                }
                answer += "\n";

                return answer;
            }
        }
    }

}
