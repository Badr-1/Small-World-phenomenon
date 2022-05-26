using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace SmallWorld
{
    class Optimization
    {
        private static Dictionary<int, Dictionary<int, int>> common = new Dictionary<int, Dictionary<int, int>>();
        private static Dictionary<string, int> index = new Dictionary<string, int>();
        private static List<string> moviesNames = new List<string>();
        private static List<List<int>> movies = new List<List<int>>();
        private static List<string> actorsName = new List<string>();
        private static List<HashSet<int>> adjs = new List<HashSet<int>>();
        private static List<List<int>> actorsMovies = new List<List<int>>();
        private static int MAX_DEGREE_FOUND = 0;
        private static int[] dosFrequency;
        public static void ChooseOpeartion()
        {
            string Choice;
            do
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("1.Find Shortest Path (Completed)");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("2.Find Strongest Path (Completed)");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("3.Find Degree of Seperation Frequency (Completed)");
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("4.Find Minimum Movies that link all actors/actresses (Pending)");
                Console.ResetColor();
                Console.Write("Enter your choice: ");
                Choice = Console.ReadLine();
                if ((Choice == "1" || Choice == "2" || Choice == "3" || Choice == "4"))
                    break;
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid Choice!");
                    Console.ResetColor();
                }
            } while (true);
            ParseMovies();
            switch (Choice)
            {
                case "1":
                    Program.ParseQueries();
                    Program.ParseSolutions();
                    Program.RunTestCase(true);
                    break;
                case "2":
                    FindStrongestPath();
                    break;
                case "3":
                    CalculateFrequency();
                    break;
                case "4":
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine("Not implemented yet");
                    Console.ResetColor();
                    break;
            }
        }
        public static void ParseMovies()
        {
            common.Clear();
            index.Clear();
            moviesNames.Clear();
            movies.Clear();
            actorsName.Clear();
            adjs.Clear();
            actorsMovies.Clear();
            StreamReader reader = new StreamReader(Program.moviesPath);
            while (true)
            {
                string line = reader.ReadLine();
                if (line == null)
                    break;
                string[] data = line.Split('/');
                string movie_name = data[0];
                List<int> movieActors = new List<int>();
                for(int i = 1;i < data.Length;i++)
                {
                    string actor_name = data[i];
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
        public static string Solve(string src, string dest)
        {
            int iFirst = index[src];
            int iSecond = index[dest];
            List<int> shortestPath = FindShortestPath(iFirst, iSecond);
            if (shortestPath != null)
                return GenerateAnswer(shortestPath);
            else
                return "";
        }
        private static string GenerateAnswer(List<int> path)
        {
            int degree = 0, relation = 0;
            Stack<string> chainOfActors = new Stack<string>();
            Stack<string> chainOfMovies = new Stack<string>();
            string answer = "";
            string commonMovies = "";
            if (!Program.sample) answer += $"{actorsName[path.Last()]}/{actorsName[path.First()]}\n";
            if (path.Count() != 1)
            {
                for (int i = 0; i < path.Count() - 1; i++)
                {
                    List<int> movies = GetIntersection(actorsMovies[path[i]], (actorsMovies[path[i + 1]]));
                    chainOfActors.Push(actorsName[path[i]]);
                    if (!Program.sample)
                        chainOfMovies.Push(moviesNames[movies[0]]);
                    else
                    {
                        commonMovies = "";
                        foreach (var m in movies)
                        {
                            if (commonMovies == "")
                                commonMovies += moviesNames[m];
                            else
                                commonMovies += moviesNames[m].Substring(moviesNames[m].Count() - 1);
                            if (m != movies.Last())
                                commonMovies += " or ";
                        }
                        chainOfMovies.Push(commonMovies);
                    }
                    relation += movies.Count;
                    degree++;
                }
            }
            else
            {
                relation += actorsMovies[path[0]].Count();
            }
            if (!Program.sample)
            {
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
            }
            else
            {
                answer += $"{actorsName[path.Last()]}/{actorsName[path.First()]}\t\t{degree}\t\t{relation}\t\t";
                while (chainOfMovies.Count() != 0)
                {
                    answer += chainOfMovies.Pop();
                    if (chainOfMovies.Count() != 0)
                        answer += " => ";
                }
            }

            return answer;
        }
        private static List<int> FindShortestPath(int start, int end)
        {
            int[] parent = new int[index.Count()];
            int[] weight = new int[index.Count()];
            bool[] visited = new bool[index.Count()];
            int maxDist = int.MaxValue;
            int[] dist = new int[index.Count()];
            bool found = false;
            if (start == end)
            {
                parent[end] = -1;
                return ConstructPath(parent, end);
            }
            else
            {
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
                        if (visited[v])
                            continue;
                        if (dist[v] == 0 || dist[v] > dist[u] + 1)
                        {
                            int commonWithU = common[v][u] + weight[u];
                            dist[v] = dist[u] + 1;
                            q.Enqueue(v);
                            parent[v] = u; weight[v] = commonWithU;
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
                            found = true;
                            maxDist = dist[v];
                        }
                    }
                    visited[u] = true;
                }
                if (found == false)
                    return null;
                return ConstructPath(parent, end);
            }
        }
        private static void GetKnown(int src)
        {
            dosFrequency[0] = 1;
            int[] parent = new int[index.Count()];
            int[] weight = new int[index.Count()];
            bool[] visited = new bool[index.Count()];
            int maxDist = int.MaxValue;
            int[] dist = new int[index.Count()];
            dist = Enumerable.Repeat(int.MaxValue, index.Count()).ToArray();
            Queue<int> q = new Queue<int>();
            q.Enqueue(src);
            parent[src] = -1;
            weight[src] = 0;
            dist[src] = 0;
            while (q.Count != 0)
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
                        int commonWithU = common[v][u] + weight[u];
                        if (dist[v] != int.MaxValue && dosFrequency[dist[v]] != 0)
                            dosFrequency[dist[v]]--;
                        dist[v] = dist[u] + 1; dosFrequency[dist[v]]++;
                        if (MAX_DEGREE_FOUND < dist[v])
                            MAX_DEGREE_FOUND = dist[v];
                        q.Enqueue(v);
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
                            parent[v] = u; weight[v] = commonWithU;
                        }
                    }


                }
                visited[u] = true;
            }
        }
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
                    result.Add(first[fIter]); fIter++; sIter++;
                }
            }
            return result;
        }
        private static List<int> ConstructPath(int[] parent, int dest)
        {
            List<int> path = new List<int>();
            int i = dest;
            while (i != -1)
            {
                path.Add(i); i = parent[i];
            }
            return path;
        }
        private static void CalculateFrequency()
        {
            int src; string actorName; do
            {
                Console.Write("Enter Actor Name: ");
                actorName = Console.ReadLine();
                if (index.ContainsKey(actorName))
                {
                    src = index[actorName]; break;
                }
                else
                {
                    Console.WriteLine("Invalid Actor/Acteress Name");
                }
            } while (true);

            MAX_DEGREE_FOUND = 0; GetKnown(src);
            Console.WriteLine("Deg.of Separ.{0,2}Frequency", "");
            for (int i = 0; i <= MAX_DEGREE_FOUND; i++)
            {
                Console.WriteLine("{0,12}{1,12}", i, dosFrequency[i]);
            }
        }
        private static void FindStrongestPath()
        {
            int src = -1, dest = -1;
            do
            {
                Console.Write("Enter Source Actor Name: ");
                string srcName = Console.ReadLine();
                if (index.ContainsKey(srcName))
                {
                    src = index[srcName];
                    Console.Write("Enter Destination Actor Name: ");
                    string destName = Console.ReadLine();
                    if (index.ContainsKey(destName))
                    {
                        dest = index[destName];
                    }
                    else
                    {
                        Console.WriteLine("Invalid Actor/Acteress Name");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid Actor/Acteress Name");
                }


            } while (src == -1 || dest == -1);

            bool isReachable = false;
            isReachable = FindShortestPath(src, dest) != null;
            if (isReachable)
            {
                bool[] discovered = new bool[index.Count()];
                Stack<int> path = new Stack<int>();
                List<List<int>> paths = new List<List<int>>();
                path.Push(src);
                Visit(src, discovered, dest, path, paths);
                Console.WriteLine(GenerateAnswer(paths[max_relation_index]));
            }
        }
        private static int max_relation = 0;
        private static int max_relation_index = 0;
        private static void Visit(int u, bool[] discovered, int dest, Stack<int> path, List<List<int>> paths, int w = 0)
        {
            discovered[u] = true;
            foreach (var adj in adjs[u])
            {
                if (adj == dest)
                {
                    path.Push(adj);
                    w += common[u][adj];
                    paths.Add(path.ToList());
                    if (w > max_relation)
                    {
                        max_relation = w;
                        max_relation_index = paths.Count() - 1;
                    }
                    path.Pop();
                    w -= common[u][adj];
                    continue;
                }

                if (!discovered[adj])
                {
                    path.Push(adj);
                    Visit(adj, discovered, dest, path, paths, w + common[u][adj]);
                    path.Pop();
                }

            }
            discovered[u] = false;
        }
    }
}