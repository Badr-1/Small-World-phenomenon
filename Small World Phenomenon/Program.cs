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
                Console.WriteLine("Parsing Test Case ...");
                parseMovies();
                //TODO: for bonus
                //generateQueries();
                parseQueries();
                Console.WriteLine("Parsing Is Done");
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


            double newOne;
            long timeBefore = System.Environment.TickCount;
            foreach (KeyValuePair<string, string> kvp in queries)
            {
                solve(kvp.Key, kvp.Value);
            }
            long timeAfter = System.Environment.TickCount;
            newOne = (timeAfter - timeBefore) * 0.001;
            Console.WriteLine("============================================================");
            timeBefore = System.Environment.TickCount;
            foreach (KeyValuePair<string, string> kvp in queries)
            {
                oldSolve(kvp.Key, kvp.Value);
            }
            timeAfter = System.Environment.TickCount;
            Console.WriteLine("New Time taken: {0} seconds", (newOne));
            Console.WriteLine("Old Time taken: {0} seconds", (timeAfter - timeBefore) * 0.001);

          







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
                int degree = 1, answerDegree = int.MaxValue, maxRelation = 0;
                List<answer> answers = new List<answer>();
                Dictionary<string, actor> dictActors = new Dictionary<string, actor>();
                Queue<actor> actorsToAsk = new Queue<actor>();
                HashSet<string> lvlActors = new HashSet<string>();
                HashSet<string> nextLvlActors = new HashSet<string>();
                HashSet<string> lvlAskedActors = new HashSet<string>();
                dictActors[who] = new actor(who, null);
                actorsToAsk.Enqueue(dictActors[who]);
                lvlActors.Add(who);
                while (actorsToAsk.Count() != 0)
                {
                    actor actor = actorsToAsk.Dequeue();
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
                            actor movieActor = new actor(actorInMovie, actor);
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
                                    int current = movieActor.parent.commonMovies.Count() + movieActor.commonMovies.Count();
                                    int @new = actor.commonMovies.Count() + commonMovies.Count();
                                    if (@new > current)
                                    {
                                        movieActor.parent = actor;
                                        movieActor.commonMovies = commonMovies;
                                    }

                                }

                            }
                            if (actorInMovie == whom)
                            {
                                actor.askedMovies[movie] = true;
                                answer answer = new answer();
                                actor whomActor = new actor(whom, actor);
                                Stack<string> moviesList = new Stack<string>();
                                Stack<string> actorList = new Stack<string>();
                                actor curr = new actor(whom, actor);

                                int relation = 0;
                                do
                                {
                                    if (curr == null || curr.parent == null)
                                        break;

                                    //get common movies between curr and parent
                                    List<string> commonMoviesList = moviesOfActor[curr.name].Intersect(moviesOfActor[curr.parent.name]).ToList();
                                    string commonMovies = commonMoviesList[0];
                                    for (int i = 0; i < commonMoviesList.Count(); i++)
                                    {
                                        relation++;
                                        string elm = commonMoviesList.ElementAt(i);
                                        actor.askedMovies[elm] = true;

                                        //commonMovies += elm;
                                        //if (i != commonMoviesList.Count() - 1)
                                        //    commonMovies += " or ";


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
                                if (!degressOfSaperation.ContainsKey(degree))
                                    degressOfSaperation[degree] = 1;
                                else
                                    degressOfSaperation[degree]++;
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
                foreach (answer answer in answers)
                {
                    if (answer.relation == maxRelation)
                    {
                        answer.print(who, whom);
                        break;
                    }
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

                int degree = 1, answerDegree = int.MaxValue, maxRelation = 0;
                Queue<actor> actorsToAsk = new Queue<actor>();
                List<answer> answers = new List<answer>();
                List<actor> actors = new List<actor>();
                List<string> lvlActors = new List<string>();
                List<string> nextLvlActors = new List<string>();
                List<string> lvlAskedActors = new List<string>();
                actors.Add(new actor(who, null));
                lvlActors.Add(who);
                actorsToAsk.Enqueue(actors[0]);
                while (actorsToAsk.Count() != 0)
                {
                    actor actor = actorsToAsk.Dequeue();
                    foreach (movie movie in movies)
                    {
                        if (actor.askedMovies.ContainsKey(movie.name) && actor.askedMovies[movie.name] == true)
                            continue;
                        else
                            actor.askedMovies[movie.name] = false;

                        if (movie.actors.Contains(actor.name) && movie.actors.Contains(whom))
                        {
                            answer answer = new answer();
                            actor.askedMovies[movie.name] = true;
                            answerDegree = degree;
                            actor whomActor = new actor(whom, actor);
                            Stack<string> moviesList = new Stack<string>();
                            Stack<string> actorList = new Stack<string>();
                            actor curr = new actor(whom, actor);
                            int relation = 0;
                            do
                            {
                                if (curr == null || curr.parent == null)
                                    break;

                                //get common movies between curr and parent
                                List<string> commonMoviesList = getCommonMovies(curr.name, curr.parent.name);
                                string commonMovies = commonMoviesList[0];
                                for (int i = 0; i < commonMoviesList.Count(); i++)
                                {
                                    relation++;
                                    string elm = commonMoviesList.ElementAt(i);
                                    actor.askedMovies[elm] = true;
                                    //commonMovies += elm;
                                    //if (i != commonMoviesList.Count() - 1)
                                    //    commonMovies += " or ";

                                }

                                moviesList.Push(commonMovies);
                                actorList.Push(curr.name);
                                curr = curr.parent;

                            } while (true);
                            answer.moviesList = moviesList;
                            answer.actorList = actorList;
                            answer.degree = degree;
                            answer.relation = relation;
                            answers.Add(answer);
                            maxRelation = (maxRelation < relation) ? relation : maxRelation;
                            if (!degressOfSaperation.ContainsKey(degree))
                                degressOfSaperation[degree] = 1;
                            else
                                degressOfSaperation[degree]++;
                        }
                        else if (movie.actors.Contains(actor.name))
                        {
                            actor.askedMovies[movie.name] = true;
                            foreach (string actorInMovie in movie.actors)
                            {
                                actor movieActor = getActor(actors, actorInMovie);
                                if (movieActor == null)
                                {
                                    movieActor = new actor(actorInMovie, actor);
                                    if (actorInMovie != actor.name)
                                    {
                                        List<string> commonMovies = getCommonMovies(actorInMovie, actor.name);
                                        movieActor.commonMovies = commonMovies;
                                    }
                                    actors.Add(movieActor);
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
                                        if (!nextLvlActors.Contains(actorInMovie))
                                            nextLvlActors.Add(actorInMovie);

                                        List<string> commonMovies = getCommonMovies(actorInMovie, actor.name);
                                        int current = movieActor.parent.commonMovies.Count() + movieActor.commonMovies.Count();
                                        int @new = actor.commonMovies.Count() + commonMovies.Count();
                                        if (@new > current)
                                        {
                                            movieActor.parent = actor;
                                            movieActor.commonMovies = commonMovies;
                                        }
                                    }

                                }
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

                        nextLvlActors = new List<string>();
                    }
                    if (answerDegree < degree)
                        break;

                }
                foreach (answer answer in answers)
                {
                    if (answer.relation == maxRelation)
                    {
                        answer.print(who, whom);
                        break;
                    }
                }
            }
        }



        public static actor getActor(List<actor> actors, string name)
        {
            foreach (actor actor in actors)
            {
                if (actor.name == name)
                {
                    return actor;
                }
            }
            return null;
        }
        public static List<string> getCommonMovies(string actor1, string actor2)
        {
            List<string> commonMovies = new List<string>();
            foreach (movie movie in movies)
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
            public actor parent;
            public bool asked = false;
            public bool toSkip = false;
            public List<string> commonMovies = new List<string>();
            public Dictionary<string, bool> askedMovies = new Dictionary<string, bool>();
            public actor(string name, actor parent)
            {
                this.name = name;
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

        public class answer
        {
            public int degree;
            public int relation;
            public Stack<string> moviesList = new Stack<string>();
            public Stack<string> actorList = new Stack<string>();

            public void print(string who, string whom)
            {
                Console.WriteLine();
                Console.WriteLine("{0}/{1}", who, whom);
                Console.WriteLine("DoS = {0}, RS = {1}", degree, relation);
                Console.Write("CHAIN OF ACTORS: {0} -> ", who);
                while (actorList.Count() != 0)
                {
                    Console.Write(actorList.Pop());
                    if (actorList.Count() != 0)
                        Console.Write(" -> ");
                }
                Console.WriteLine();
                Console.Write("CHAIN OF MOVIES:  => ");
                while (moviesList.Count() != 0)
                {
                    Console.Write(moviesList.Pop());
                    Console.Write(" => ");
                }
                Console.WriteLine();
            }
        }
    }

}
