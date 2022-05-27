using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace SmallWorld
{
    class Optimization
    {
        private static var common = new Dictionary<int, Dictionary<int, int>>(); //O(1)
        private static var index = new Dictionary<string, int>(); //O(1)
        private static List<string> moviesNames = new List<string>(); //O(1)
        private static List<List<int>> movies = new List<List<int>>(); //O(1)
        private static List<string> actorsName = new List<string>(); //O(1)
        private static List<HashSet<int>> adjs = new List<HashSet<int>>(); //O(1)
        private static List<List<int>> actorsMovies = new List<List<int>>(); //O(1)
        private static int MAX_DEGREE_FOUND = 0; //O(1)
        private static int[] dosFrequency; //O(1)
        //O(V + E)
        public static void ChooseOpeartion()
        {
            string Choice; //O(1)
            do
            {
                Console.ForegroundColor = ConsoleColor.Green; //O(1)
                Console.WriteLine("1.Find Shortest Path (Completed)"); //O(1)
                Console.ForegroundColor = ConsoleColor.Green; //O(1)
                Console.WriteLine("2.Find Strongest Path (Completed)"); //O(1)
                Console.ForegroundColor = ConsoleColor.Green; //O(1)
                Console.WriteLine("3.Find Degree of Seperation Frequency (Completed)"); //O(1)
                Console.ForegroundColor = ConsoleColor.DarkYellow; //O(1)
                Console.WriteLine("4.Find Minimum Movies that link all actors/actresses (Pending)"); //O(1)
                Console.ResetColor(); //O(1)
                Console.Write("Enter your choice: "); //O(1)
                Choice = Console.ReadLine(); //O(1)
                if ((Choice == "1" || Choice == "2" || Choice == "3" || Choice == "4")) //O(1)
                    break; //O(1)
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red; //O(1)
                    Console.WriteLine("Invalid Choice!"); //O(1)
                    Console.ResetColor(); //O(1)
                }
            } while (true); //O(1)
            ParseMovies(); //O(V + E)
            switch (Choice)
            {
                case "1":
                    Program.ParseQueries(); //O(N)
                    Program.ParseSolutions(); //O(N) 
                    Program.RunTestCase(true); //O(V + E)
                    break; //O(1)
                case "2":
                    FindStrongestPath(); //O(V + E)
                    break; //O(1)
                case "3": //O(1)
                    CalculateFrequency(); //O(V + E)
                    break; //O(1)
                case "4": //O(1)
                    Console.ForegroundColor = ConsoleColor.DarkYellow; //O(1)
                    Console.WriteLine("Not implemented yet"); //O(1)
                    Console.ResetColor(); //O(1)
                    break; //O(1)
            }
        }
        //O(V + E)
        public static void ParseMovies()
        {
            common.Clear(); //O(1)
            index.Clear(); //O(1)
            moviesNames.Clear(); //O(1)
            movies.Clear(); //O(1)
            actorsName.Clear(); //O(1)
            adjs.Clear(); //O(1)
            actorsMovies.Clear(); //O(1)
            StreamReader reader = new StreamReader(Program.moviesPath); //O(1)
            while (true)
            {
                string line = reader.ReadLine(); //O(1)
                if (line == null) //O(1)
                    break;
                string[] data = line.Split('/'); //O(N)
                string movie_name = data[0]; //O(1)
                List<int> movieActors = new List<int>(); //O(1)
                for (int i = 1; i < data.Length; i++) //O(N)
                {
                    string actor_name = data[i]; //O(1)
                    if (!index.ContainsKey(actor_name)) //O(1)
                    {
                        int newActorIndex = actorsName.Count(); //O(1)
                        index[actor_name] = newActorIndex; //O(1)
                        movieActors.Add(newActorIndex); //O(1)
                        actorsMovies.Add(new List<int>()); //O(1)
                        adjs.Add(new HashSet<int>()); //O(1)
                        actorsMovies[newActorIndex].Add(moviesNames.Count()); //O(1)
                        actorsName.Add(actor_name); //O(1)
                    }
                    else
                    {
                        int prevActorIndex = index[actor_name]; //O(1)
                        movieActors.Add(prevActorIndex); //O(1)
                        actorsMovies[prevActorIndex].Add(moviesNames.Count()); //O(1)
                    }
                }
                movies.Add(new List<int>()); //O(1)
                movies[moviesNames.Count()].AddRange(movieActors); //O(N)
                moviesNames.Add(movie_name); //O(1)
            }
            //O(V + E)
            foreach (var movie in movies)
            {
                foreach (var actor in movie) //O(N)
                {
                    if (!common.ContainsKey(actor)) //O(1)
                        common[actor] = new Dictionary<int, int>(); //O(1)
                    foreach (var adj in movie) //O(E)
                    {
                        if (adj != actor)
                        {
                            if (!common[actor].ContainsKey(adj)) //O(1)
                                common[actor][adj] = 0; //O(1)
                            adjs[actor].Add(adj); //O(1)
                            common[actor][adj]++; //O(1)
                        }
                    }
                }
            }
            dosFrequency = new int[index.Count()]; //O(1)
            reader.Close(); //O(1)
        }
        //O(V + E)
        public static string Solve(string src, string dest)
        {
            int iFirst = index[src]; //O(1)
            int iSecond = index[dest]; //O(1)
            List<int> shortestPath = FindShortestPath(iFirst, iSecond); //O(V+E)
            if (shortestPath != null)
                return GenerateAnswer(shortestPath); //O(V+E)
            else return ""; //O(1)
        }
        //O(V + E)
        private static string GenerateAnswer(List<int> path)
        {
            int degree = 0, relation = 0; //O(1)
            Stack<string> chainOfActors = new Stack<string>(); //O(1)
            Stack<string> chainOfMovies = new Stack<string>(); //O(1)
            string answer = ""; //O(1)
            string commonMovies = ""; //O(1)
            if (!Program.sample) answer += $"{actorsName[path.Last()]}/{actorsName[path.First()]}\n"; //O(1)
            if (path.Count() != 1) //O(1) 
            {
                for (int i = 0; i < path.Count() - 1; i++) //O(V + E)
                {
                    List<int> movies = GetIntersection(actorsMovies[path[i]], (actorsMovies[path[i + 1]])); //O(N + M)
                    chainOfActors.Push(actorsName[path[i]]); //O(1)
                    if (!Program.sample) //O(1)
                        chainOfMovies.Push(moviesNames[movies[0]]); //O(1)
                    else //O(1)
                    {
                        commonMovies = ""; //O(1)
                        foreach (var m in movies) //O(N)
                        {
                            if (commonMovies == "") //O(1)
                                commonMovies += moviesNames[m]; //O(1)
                            else
                                commonMovies += moviesNames[m].Substring(moviesNames[m].Count() - 1); //O(1)
                            if (m != movies.Last()) //O(1)
                                commonMovies += " or "; //O(1)
                        }
                        chainOfMovies.Push(commonMovies); //O(1)
                    }
                    relation += movies.Count; //O(1)
                    degree++; //O(1)
                }
            }
            else
            {
                relation += actorsMovies[path[0]].Count(); //O(1)
            }
            if (!Program.sample) //O(1)
            {
                answer += $"DoS = {degree}, RS = {relation}\n"; //O(1)
                answer += $"CHAIN OF ACTORS: {actorsName[path.Last()]} -> "; //O(1)
                while (chainOfActors.Count() != 0) //O(N)
                {
                    answer += chainOfActors.Pop(); //O(1)
                    if (chainOfActors.Count() != 0) //O(1)
                        answer += " -> "; //O(1)
                }
                answer += "\n"; //O(1)
                answer += "CHAIN OF MOVIES:  =>"; //O(1)
                while (chainOfMovies.Count() != 0) //O(N)
                {
                    answer += " " + chainOfMovies.Pop(); //O(1)
                    answer += " =>"; //O(1)
                }
                answer += "\n"; //O(1)
            }
            else
            {
                answer += $"{actorsName[path.Last()]}/{actorsName[path.First()]}\t\t{degree}\t\t{relation}\t\t"; //O(1)
                while (chainOfMovies.Count() != 0) //O(N)
                {
                    answer += chainOfMovies.Pop(); //O(1)
                    if (chainOfMovies.Count() != 0) //O(1)
                        answer += " => "; //O(1)
                }
            }

            return answer; //O(1)
        }
        // O(V + E)
        private static List<int> FindShortestPath(int start, int end)
        {
            int[] parent = new int[index.Count()]; //O(1)
            int[] weight = new int[index.Count()]; //O(1)
            bool[] visited = new bool[index.Count()]; //O(1)
            int maxDist = int.MaxValue; //O(1)
            int[] dist = new int[index.Count()]; //O(1)
            bool found = false; //O(1)
            if (start == end) //O(1)
            {
                parent[end] = -1; //O(1)
                return ConstructPath(parent, end); //O(V)
            }
            else
            {
                Queue<int> q = new Queue<int>(); //O(1)
                q.Enqueue(start); //O(1)
                parent[start] = -1; //O(1)
                weight[start] = 0; //O(1)
                dist[start] = 0; //O(1)
                while (q.Count != 0) // O(V)
                {
                    int u = q.Dequeue(); //O(1)
                    if (visited[u]) //O(1)
                        continue; //O(1)
                    if (dist[u] + 1 > maxDist) //O(1)
                        break; //O(1)
                    foreach (int v in adjs[u]) //O(E)
                    {
                        if (v == start) //O(1)
                            continue; //O(1)
                        if (visited[v]) //O(1)
                            continue; //O(1)
                        if (dist[v] == 0 || dist[v] > dist[u] + 1) //O(1)
                        {
                            int commonWithU = common[v][u] + weight[u]; //O(1)
                            dist[v] = dist[u] + 1; //O(1)
                            q.Enqueue(v); //O(1)
                            parent[v] = u; weight[v] = commonWithU; //O(1)
                        }
                        else if (dist[v] == dist[u] + 1) //O(1)
                        {

                            int commonWithU = common[v][u] + weight[u]; //O(1)
                            int commonWithParent = common[v][parent[v]] + weight[parent[v]]; //O(1)
                            if (commonWithU > commonWithParent) //O(1)
                            {
                                parent[v] = u; //O(1)
                                weight[v] = commonWithU; //O(1)
                            }
                        }
                        if (v == end) //O(1)
                        {
                            found = true; //O(1)
                            maxDist = dist[v]; //O(1)
                        }
                    }
                    visited[u] = true; //O(1)
                }
                if (found == false) //O(1)
                    return null; //O(1)
                return ConstructPath(parent, end); //O(V)
            }
        }

        // O(V + E)
        private static void GetKnown(int src)
        {
            dosFrequency[0] = 1; //O(1)
            int[] parent = new int[index.Count()]; //O(1)
            int[] weight = new int[index.Count()]; //O(1)
            bool[] visited = new bool[index.Count()]; //O(1)
            int maxDist = int.MaxValue; //O(1)
            int[] dist = new int[index.Count()]; //O(1)
            dist = Enumerable.Repeat(int.MaxValue, index.Count()).ToArray(); //O(V)
            Queue<int> q = new Queue<int>(); //O(1)
            q.Enqueue(src); //O(1)
            parent[src] = -1; //O(1)
            weight[src] = 0; //O(1)
            dist[src] = 0; //O(1)
            while (q.Count != 0)
            {
                int u = q.Dequeue(); //O(1)
                if (visited[u]) //O(1)
                    continue; //O(1)
                if (dist[u] + 1 > maxDist) //O(1)
                    break; //O(1)
                foreach (int v in adjs[u]) //O(E)
                {
                    if (dist[v] > dist[u] + 1)
                    {
                        int commonWithU = common[v][u] + weight[u]; //O(1)
                        if (dist[v] != int.MaxValue && dosFrequency[dist[v]] != 0) //O(1)
                            dosFrequency[dist[v]]--; //O(1)
                        dist[v] = dist[u] + 1; dosFrequency[dist[v]]++; //O(1)
                        if (MAX_DEGREE_FOUND < dist[v]) //O(1)
                            MAX_DEGREE_FOUND = dist[v]; //O(1)
                        q.Enqueue(v); //O(1)
                        weight[v] = 0; //O(1)
                        parent[v] = u; //O(1)
                        weight[v] = commonWithU; //O(1)
                    }
                    else if (dist[v] == dist[u] + 1)
                    {
                        int commonWithU = common[v][u] + weight[u]; //O(1)
                        int commonWithParent = common[v][parent[v]] + weight[parent[v]]; //O(1)
                        if (commonWithU > commonWithParent)
                        {
                            parent[v] = u; weight[v] = commonWithU; //O(1)
                        }
                    }


                }
                visited[u] = true;
            }
        }
        //O(M + N)
        private static List<int> GetIntersection(List<int> first, List<int> second)
        {
            int fIter = 0; //O(1)
            int sIter = 0; //O(1)
            var result = new List<int>(); //O(1)
            while (fIter < first.Count && sIter < second.Count) //O(M + N)
            {
                if (first[fIter] < second[sIter]) //O(1)
                    fIter++; //O(1)
                else if (first[fIter] > second[sIter]) //O(1)
                    sIter++;
                else //O(1)
                {
                    result.Add(first[fIter]); fIter++; sIter++; //O(1)
                }
            }
            return result; //O(1)
        }

        //O(V)
        private static List<int> ConstructPath(int[] parent, int dest)
        {
            List<int> path = new List<int>(); //O(1)
            int i = dest; //O(1)
            while (i != -1) //O(V)
            {
                path.Add(i); //O(1)
                i = parent[i]; //O(1)
            }
            return path; //O(1)
        }
        //O(V + E)
        private static void CalculateFrequency()
        {
            int src; string actorName; //O(1)
            do
            {
                Console.Write("Enter Actor Name: "); //O(1)
                actorName = Console.ReadLine(); //O(1)
                if (index.ContainsKey(actorName)) //O(1) 
                {
                    src = index[actorName]; break; //O(1)
                }
                else
                {
                    Console.WriteLine("Invalid Actor/Acteress Name"); //O(1)
                }
            } while (true);

            MAX_DEGREE_FOUND = 0; GetKnown(src); //O(V + E)
            Console.WriteLine("Deg.of Separ.{0,2}Frequency", ""); //O(1)
            for (int i = 0; i <= MAX_DEGREE_FOUND; i++) //O(N)
            {
                Console.WriteLine("{0,12}{1,12}", i, dosFrequency[i]); //O(1)
            }
        }
        // O(V + E)
        private static void FindStrongestPath()
        {
            int src = -1, dest = -1; //O(1)
            do
            {
                Console.Write("Enter Source Actor Name: "); //O(1)
                string srcName = Console.ReadLine(); //O(1)
                if (index.ContainsKey(srcName)) //O(1)
                {
                    src = index[srcName];//O(1)
                    Console.Write("Enter Destination Actor Name: "); //O(1)
                    string destName = Console.ReadLine(); //O(1)
                    if (index.ContainsKey(destName)) //O(1)
                    {
                        dest = index[destName]; //O(1)
                    }
                    else
                    {
                        Console.WriteLine("Invalid Actor/Acteress Name"); //O(1)
                    }
                }
                else
                {
                    Console.WriteLine("Invalid Actor/Acteress Name"); //O(1)
                }


            } while (src == -1 || dest == -1);

            bool isReachable = false; //O(1)
            isReachable = FindShortestPath(src, dest) != null; // O(V + E)
            if (isReachable)
            {
                bool[] discovered = new bool[index.Count()]; //O(1)
                Stack<int> path = new Stack<int>(); //O(1)
                List<List<int>> paths = new List<List<int>>(); //O(1)
                path.Push(src); //O(1)
                Visit(src, discovered, dest, path, paths); //O(V) 
                Console.WriteLine(GenerateAnswer(paths[max_relation_index])); //O(V + E)
            }
        }
        private static int max_relation = 0; //O(1)
        private static int max_relation_index = 0; //O(1)
        //O(V)
        private static void Visit(int u, bool[] discovered, int dest, Stack<int> path, List<List<int>> paths, int w = 0)
        {
            discovered[u] = true; //O(1)
            foreach (var adj in adjs[u]) //O(E)
            {
                if (adj == dest) //O(1)
                {
                    path.Push(adj); //O(1)
                    w += common[u][adj]; //O(1)
                    paths.Add(path.ToList()); //O(N)
                    if (w > max_relation)
                    {
                        max_relation = w; //O(1)
                        max_relation_index = paths.Count() - 1; //O(1)
                    }
                    path.Pop(); //O(1)
                    w -= common[u][adj]; //O(1)
                    continue; //O(1)
                }

                if (!discovered[adj])
                {
                    path.Push(adj); //O(1)
                    Visit(adj, discovered, dest, path, paths, w + common[u][adj]);
                    path.Pop(); //O(1)
                }

            }
            discovered[u] = false; //O(1)
        }
    }
}