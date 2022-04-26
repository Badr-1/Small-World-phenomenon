using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
namespace Small_World_Phenomenon
{
    internal class Program
    {
        static string moviesPath, queriesPath;
        static Dictionary<string, HashSet<string>> actorsInMovie = new Dictionary<string, HashSet<string>>();
        static Dictionary<string, HashSet<string>> moviesOfActor = new Dictionary<string, HashSet<string>>();
        static Dictionary<int, int> degressOfSaperation = new Dictionary<int, int>();
        static List<string> moviesNames = new List<string>();
        static HashSet<string> asked = new HashSet<string>();
        static List<KeyValuePair<string, string>> queries = new List<KeyValuePair<string, string>>();
        static void Main(string[] args)
        {
            while (true)
            {


                selectTestCase();
                parseMovies();
                //TODO: for bonus
                //generateQueries();
                parseQueries();
                long timeBefore = System.Environment.TickCount;
                runTestCase(queries);
                long timeAfter = System.Environment.TickCount;
                Console.WriteLine("Time taken: {0}", timeAfter - timeBefore);
                //bonus
                //Console.WriteLine("Deg. of Separ.\tFrequency");
                //foreach (var item in degressOfSaperation)
                //{
                //    Console.WriteLine("{0}\t\t{1}", item.Key, item.Value);
                //}
                //degressOfSaperation.Clear();
                Console.ReadLine();
                Console.Clear();
            }
        }

        public static void generateQueries()
        {
            string from = actorsInMovie[moviesNames[0]].First();
            foreach (var movie in moviesNames)
            {
                HashSet<string> actors = actorsInMovie[movie];
                foreach (var actor in actors)
                {
                    if (!asked.Contains(actor))
                    {
                        queries.Add(new KeyValuePair<string, string>(from, actor));
                        asked.Add(actor);
                    }
                }
            }

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
                actorsInMovie[movie_name] = new HashSet<string>();
                moviesNames.Add(movie_name);
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

                }
            }
        }
        public static void parseQueries()
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

        public static void selectTestCase()
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
                    break;
                case "1":
                    path += @"Complete\small\Case1\";
                    moviesPath = path + "Movies193.txt";
                    queriesPath = path + "queries110.txt";
                    break;
                case "2":
                    path += @"Complete\small\Case2\";
                    moviesPath = path + "Movies187.txt";
                    queriesPath = path + "queries50.txt";
                    break;
                case "3":
                    path += @"Complete\medium\Case1\";
                    moviesPath = path + "Movies967.txt";
                    queriesPath = path + "queries85.txt";
                    break;
                case "4":
                    path += @"Complete\medium\Case1\";
                    moviesPath = path + "Movies967.txt";
                    queriesPath = path + "queries4000.txt";
                    break;


                case "5":
                    path += @"Complete\medium\Case2\";
                    moviesPath = path + "Movies4736.txt";
                    queriesPath = path + "queries110.txt";
                    break;
                case "6":
                    path += @"Complete\medium\Case2\";
                    moviesPath = path + "Movies4736.txt";
                    queriesPath = path + "queries2000.txt";
                    break;

                case "7":
                    path += @"Complete\large\";
                    moviesPath = path + "Movies14129.txt";
                    queriesPath = path + "queries26.txt";
                    break;
                case "8":
                    path += @"Complete\large\";
                    moviesPath = path + "Movies14129.txt";
                    queriesPath = path + "queries600.txt";
                    break;


                case "9":
                    path += @"Complete\extreme\";
                    moviesPath = path + "Movies122806.txt";
                    queriesPath = path + "queries22.txt";
                    break;
                case "10":
                    path += @"Complete\extreme\";
                    moviesPath = path + "Movies122806.txt";
                    queriesPath = path + "queries200.txt";
                    break;


            }
           
        }

        public static void runTestCase(List<KeyValuePair<string, string>> queries)
        {
            foreach (KeyValuePair<string, string> kvp in queries)
            {
                solve(kvp.Key, kvp.Value);
            }
            asked.Clear();
            moviesNames.Clear();
            actorsInMovie.Clear();
            moviesOfActor.Clear();
            
            queries.Clear();
            //solve("E", "N");
        }
        public static void solve(string who, string whom)
        {

            if (who == whom)
            {
                if (!degressOfSaperation.ContainsKey(0))
                    degressOfSaperation[0] = 1;
                
            }
            else
            {
                int degree = 1, answer = int.MaxValue;
                Dictionary<string, actor> dictActors = new Dictionary<string, actor>();
                Queue<actor> actorsToAsk = new Queue<actor>();
                HashSet<string> toSkip = new HashSet<string>();
                HashSet<string> askedActors = new HashSet<string>();
                HashSet<string> askedMovies = new HashSet<string>();
                HashSet<string> lvlActors = new HashSet<string>();
                HashSet<string> nextLvlActors = new HashSet<string>();
                HashSet<string> lvlAskedActors = new HashSet<string>();
                dictActors[who] = new actor(who, "", null);
                actorsToAsk.Enqueue(dictActors[who]);
                lvlActors.Add(who);
                while (actorsToAsk.Count() != 0)
                {
                    actor actor = actorsToAsk.Dequeue();
                    HashSet<string> movies = moviesOfActor[actor.name];
                    foreach (string movie in movies)
                    {
                        if (!askedMovies.Contains(movie))
                        {

                            askedMovies.Add(movie);
                            HashSet<string> actors = actorsInMovie[movie];

                            foreach (string actorInMovie in actors)
                            {
                                if (!dictActors.ContainsKey(actorInMovie))
                                    dictActors[actorInMovie] = new actor(actorInMovie, movie, actor);

                                if (!askedActors.Contains(actorInMovie) && actorInMovie != actor.name && actorInMovie != whom)
                                {
                                    if (!toSkip.Contains(actorInMovie))
                                    {
                                        actorsToAsk.Enqueue(dictActors[actorInMovie]);
                                        toSkip.Add(actorInMovie);
                                    }

                                    if (lvlActors.Contains(actor.name) && !lvlActors.Contains(actorInMovie))
                                    {
                                        nextLvlActors.Add(actorInMovie);
                                    }

                                }
                                if (actorInMovie == whom)
                                {

                                    actor whomActor = new actor(whom, movie, actor);
                                    Stack<string> moviesList = new Stack<string>();
                                    Stack<string> actorList = new Stack<string>();
                                    actor curr = new actor(whom, movie, actor);
                                    int relation = 0;
                                    do
                                    {
                                        if (curr == null || curr.parent == null)
                                            break;

                                        //get common movies between curr and parent
                                        IEnumerable<string> set = moviesOfActor[curr.name].Intersect(moviesOfActor[curr.parent.name]);
                                        string commonMovies = "";
                                        for (int i = 0; i < set.Count(); i++)
                                        {
                                            relation++;
                                            string elm = set.ElementAt(i);
                                            askedMovies.Add(elm);
                                            commonMovies += elm;
                                            if (i != set.Count() - 1)
                                                commonMovies += " or ";

                                        }

                                        moviesList.Push(commonMovies);
                                        actorList.Push(curr.name);
                                        curr = curr.parent;

                                    } while (true);
                                    Console.WriteLine("{0} -> {1}", who, whom);
                                    Console.WriteLine("degree: {0} ", degree);
                                    Console.WriteLine("Rel: {0}", relation);
                                    Console.Write("CHAIN OF ACTORS: ");
                                    while (actorList.Count() != 0)
                                    {
                                        Console.Write(actorList.Pop());
                                        if (actorList.Count() != 0)
                                            Console.Write(" -> ");
                                    }
                                    Console.WriteLine();
                                    Console.Write("CHAIN OF MOVIES: ");
                                    while (moviesList.Count() != 0)
                                    {
                                        Console.Write(moviesList.Pop());
                                        if (moviesList.Count() != 0)
                                            Console.Write(" => ");
                                    }
                                    Console.WriteLine();
                                    answer = degree;
                                    if (!degressOfSaperation.ContainsKey(degree))
                                        degressOfSaperation[degree] = 1;
                                    else
                                        degressOfSaperation[degree]++;
                                }
                            }
                        }
                    }

                    askedActors.Add(actor.name);
                    lvlAskedActors.Add(actor.name);
                    if (lvlActors.Count() == lvlAskedActors.Count())
                    {

                        degree++;
                        lvlActors.Clear();
                        lvlAskedActors.Clear();
                        lvlActors = nextLvlActors;
                        nextLvlActors = new HashSet<string>();
                    }
                    if (answer < degree)
                        break;
                }
                /*
                Q is a FIFO queue – queuing and de - queuing is O(1)
                askedMovies is Hashset
                askedActors is Hashset
                 BFS(G, s)
                 {
                    ENQUEUE(Q, who)

                     While Q != ∅
                        actor = DE - QUEUE(Q)

                           For each  movie ∈ moviesOfActor[actor]

                                          if movies! in askedMovies
                                         get actors in movie
                                         add actors to queue
                                          if whom in actors
                                              add answer = { movie,degree}
                    add movie to askedMovies

                            add actor to askedActors
                          degree++
                 }*/

                /*
                s: C
                whom: E
                askedMovies:
                askedActors:C,
                Q:B,D,Z
                actor:A
                actors:B,C
                movies:0,1,2,7
                movie:1
                answer:
                degree:2

                */
            }
        }
        public class actor
        {
            public string name;
            public string movie;
            public actor parent;
            public actor(string name, string movie, actor parent)
            {
                this.name = name;
                this.movie = movie;
                this.parent = parent;
            }

        }
    }

}
