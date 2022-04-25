using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace Small_World_Phenomenon
{
    internal class Program
    {
        static string moviesPath, queriesPath;
        static Dictionary<string, HashSet<string>> actorsInMovie = new Dictionary<string, HashSet<string>>();
        static Dictionary<string, HashSet<string>> moviesOfActor = new Dictionary<string, HashSet<string>>();
        static List<KeyValuePair<string, string>> queries = new List<KeyValuePair<string, string>>();
        static void Main(string[] args)
        {
            while (true)
            {
               
                selectTestCase();
                parseMovies();
                parseQueries();
                runTestCase(queries);
                Console.WriteLine("movies number : {0}", actorsInMovie.Count());
                Console.WriteLine("queries number : {0}", queries.Count());
                Console.WriteLine("actros number : {0}", moviesOfActor.Count());
                Console.ReadLine();
                Console.Clear();
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
                queries.Add(new KeyValuePair<string, string>(key,value));
            }
        }

        public static void selectTestCase()
        {
            string path = @"E:\Small-World-phenomenon\Testcases\";
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
            foreach (KeyValuePair<string,string> kvp in queries)
            {
                solve(kvp.Key, kvp.Value);
            }
        }

        public static void solve(string who, string whom)
        {
            HashSet<string> whoMovies = moviesOfActor[who];
            foreach(string movie in whoMovies)
            {
                
            }
        }

    }
}
