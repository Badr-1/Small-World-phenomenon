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
        static List<KeyValuePair<string, string>> queries = new List<KeyValuePair<string, string>>();

        // optimized
        static Dictionary<string, HashSet<string>> actorsInMovie = new Dictionary<string, HashSet<string>>();
        static Dictionary<string, HashSet<string>> moviesOfActor = new Dictionary<string, HashSet<string>>();

        // bonus
        static Dictionary<int, int> degressOfSaperation = new Dictionary<int, int>();
        static List<string> moviesNames = new List<string>();
        static HashSet<string> asked = new HashSet<string>();

        // old
        static List<movie> movies = new List<movie>();
        static void Main(string[] args)
        {
            while (true)
            {


                selectTestCase();
                parseMovies();
                //TODO: for bonus
                //generateQueries();
                parseQueries();
                runTestCase();
                
                //bonus
                //Console.WriteLine("Deg. of Separ.\tFrequency");
                //foreach (var item in degressOfSaperation)
                //{
                //    Console.WriteLine("{0}\t\t{1}", item.Key, item.Value);
                //}
                //degressOfSaperation.Clear();

                asked.Clear();
                moviesNames.Clear();
                actorsInMovie.Clear();
                moviesOfActor.Clear();
                queries.Clear();        
                movies.Clear();
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
                movie movie = new movie(movie_name);
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
                movies.Add(movie);
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

        public static void runTestCase()
        {



            long timeBefore = System.Environment.TickCount;
            foreach (KeyValuePair<string, string> kvp in queries)
            {
                oldSolve(kvp.Key, kvp.Value);
            }
            long timeAfter = System.Environment.TickCount;
            Console.WriteLine("Old Time taken: {0} seconds", (timeAfter - timeBefore) * 0.001);
            Console.WriteLine("============================================================");
            timeBefore = System.Environment.TickCount;
            foreach (KeyValuePair<string, string> kvp in queries)
            {
                solve(kvp.Key, kvp.Value);
            }
            timeAfter = System.Environment.TickCount;
            Console.WriteLine("New Time taken: {0} seconds", (timeAfter - timeBefore) * 0.001);





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

                                if (!dictActors[actorInMovie].asked && actorInMovie != actor.name && actorInMovie != whom)
                                {
                                    if (!dictActors[actorInMovie].toSkip)
                                    {
                                        actorsToAsk.Enqueue(dictActors[actorInMovie]);
                                        dictActors[actorInMovie].toSkip = true;
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
                                        IEnumerable<string> commonMoviesList = moviesOfActor[curr.name].Intersect(moviesOfActor[curr.parent.name]);
                                        string commonMovies = "";
                                        for (int i = 0; i < commonMoviesList.Count(); i++)
                                        {
                                            relation++;
                                            string elm = commonMoviesList.ElementAt(i);
                                            askedMovies.Add(elm);
                                            commonMovies += elm;
                                            if (i != commonMoviesList.Count() - 1)
                                                commonMovies += " or ";

                                        }

                                        moviesList.Push(commonMovies);
                                        actorList.Push(curr.name);
                                        curr = curr.parent;

                                    } while (true);
                                    Console.WriteLine("{0} -> {1}", who, whom);
                                    Console.WriteLine("degree: {0} ", degree);
                                    Console.WriteLine("Rel: {0}", relation);
                                    Console.Write("CHAIN OF ACTORS: {0} -> ", who);
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


                    dictActors[actor.name].asked = true;
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

            }
        }

        public static void oldSolve(string who, string whom)
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
                List<string> askedMovies = new List<string>();
                List<string> lvlActors = new List<string>();
                List<string> nextLvlActors = new List<string>();
                List<string> lvlAskedActors = new List<string>();
                dictActors[who] = new actor(who, "", null);
                lvlActors.Add(who);
                actorsToAsk.Enqueue(dictActors[who]);
                while (actorsToAsk.Count() != 0)
                {
                    actor actor = actorsToAsk.Dequeue();
                    foreach (movie movie in movies)
                    {
                        if (askedMovies.Contains(movie.name))
                            continue;
                      
                        if (movie.actors.Contains(actor.name) && movie.actors.Contains(whom))
                        {
                            askedMovies.Add(movie.name);
                            answer = degree;
                            actor whomActor = new actor(whom, movie.name, actor);
                            Stack<string> moviesList = new Stack<string>();
                            Stack<string> actorList = new Stack<string>();
                            actor curr = new actor(whom, movie.name, actor);
                            int relation = 0;
                            do
                            {
                                if (curr == null || curr.parent == null)
                                    break;

                                //get common movies between curr and parent
                                List<string> commonMoviesList = getCommonMovies(curr.name, curr.parent.name);
                                string commonMovies = "";
                                for (int i = 0; i < commonMoviesList.Count(); i++)
                                {
                                    relation++;
                                    string elm = commonMoviesList.ElementAt(i);
                                    askedMovies.Add(elm);
                                    commonMovies += elm;
                                    if (i != commonMoviesList.Count() - 1)
                                        commonMovies += " or ";

                                }

                                moviesList.Push(commonMovies);
                                actorList.Push(curr.name);
                                curr = curr.parent;

                            } while (true);
                            Console.WriteLine("{0} -> {1}", who, whom);
                            Console.WriteLine("degree: {0} ", degree);
                            Console.WriteLine("Rel: {0}", relation);
                            Console.Write("CHAIN OF ACTORS: {0} -> ", who);
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
                            if (!degressOfSaperation.ContainsKey(degree))
                                degressOfSaperation[degree] = 1;
                            else
                                degressOfSaperation[degree]++;
                        }
                        else if (movie.actors.Contains(actor.name))
                        {
                            askedMovies.Add(movie.name);
                            foreach (string actorInMovie in movie.actors)
                            {
                                if (!dictActors.ContainsKey(actorInMovie))
                                    dictActors[actorInMovie] = new actor(actorInMovie, movie.name, actor);

                                if (!dictActors[actorInMovie].asked && actorInMovie != actor.name && actorInMovie != whom)
                                {
                                    if (!dictActors[actorInMovie].toSkip)
                                    {
                                        actorsToAsk.Enqueue(dictActors[actorInMovie]);
                                        dictActors[actorInMovie].toSkip = true;
                                    }

                                    if (lvlActors.Contains(actor.name) && !lvlActors.Contains(actorInMovie) && !nextLvlActors.Contains(actorInMovie))
                                    {
                                        nextLvlActors.Add(actorInMovie);
                                    }

                                }
                            }
                        }
                    }

                    dictActors[actor.name].asked = true;
                    lvlAskedActors.Add(actor.name);
                    if (lvlActors.Count() == lvlAskedActors.Count())
                    {

                        degree++;

                        lvlActors.Clear();
                        lvlAskedActors.Clear();
                        lvlActors = nextLvlActors;

                        nextLvlActors = new List<string>();
                    }
                    if (answer < degree)
                        break;


                }
            }
        }


        public static List<string> getCommonMovies(string actor1, string actor2)
        {
            List<string> commonMovies = new List<string>();
            foreach(movie movie in movies)
            {
                if (movie.actors.Contains(actor1) && movie.actors.Contains(actor2))
                {
                    commonMovies.Add(movie.name);
                }
            }
            
            return commonMovies;
            
        }
        public class actor
        {
            public string name;
            public string movie;
            public actor parent;
            public bool asked = false;
            public bool toSkip = false;
            public actor(string name, string movie, actor parent)
            {
                this.name = name;
                this.movie = movie;
                this.parent = parent;
            }

        }

        public class movie
        {
            public string name;
            public List<string> actors = new List<string>();
            public movie(string name)
            {
                this.name = name;
            }
        }
    }

}
