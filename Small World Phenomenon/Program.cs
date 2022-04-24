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
        static void Main(string[] args)
        {
            while (true)
            {
                Dictionary<string, List<string>> movies = new Dictionary<string, List<string>>();
                List<KeyValuePair<string,string>> queries = new List<KeyValuePair<string,string>>();
                selectTestCase();
                parseMovies(movies, moviesPath);
                parseQueries(queries, queriesPath);
                Console.WriteLine("movies numbers : {0}", movies.Count());
                Console.WriteLine("queries numbers : {0}", queries.Count());
                Console.ReadLine();
                Console.Clear();
            }
        }

        public static void parseMovies(Dictionary<string, List<string>> dict, string path)
        {
            StreamReader reader = new StreamReader(path);
            while (true)
            {
                string line = reader.ReadLine();
                if (line == null)
                    break;

                string movie_name = line.Substring(0, line.IndexOf('/'));
                dict[movie_name] = new List<string>();
                while (true)
                {
                    line = line.Remove(0, (line.IndexOf('/') != -1) ? line.IndexOf('/') + 1 : line.Length);
                    if (line.Length == 0)
                        break;
                    string actor_name = (line.IndexOf('/') != -1) ? line.Substring(0, line.IndexOf('/')) : line;
                    dict[movie_name].Add(actor_name);

                }
            }
        }
        public static void parseQueries(List<KeyValuePair<string, string>> query, string path)
        {
            StreamReader reader = new StreamReader(path);
            while (true)
            {
                string line = reader.ReadLine();
                if (line == null)
                    break;
                string key = line.Substring(0, line.IndexOf('/'));
                line = line.Remove(0, key.Length);
                string value = line.Substring(1);
                query.Add(new KeyValuePair<string, string>(key,value));
                //query[key] = value;
            }
        }

        /*        public static void selectTestCase()
                {
                    string path = @"E:\Small_World_henomenon\Testcases\";
                    string choice;
                Ask: do
                    {
                        Console.WriteLine("1.Sample\t2.Complete");
                        Console.Write("Select Test Case: ");
                        choice = Console.ReadLine();
                    } while (!(choice == "1" || choice == "2"));
                    switch (choice)
                    {
                        case "1":
                            path += @"Sample\";
                            moviesPath = path + "movies1.txt";
                            queriesPath = path + "queries1.txt";
                            break;
                        case "2":

                            do
                            {
                                Console.WriteLine("1.small\t2.medium");
                                Console.WriteLine("3.large\t4.extreme");
                                Console.WriteLine("0.back");
                                Console.Write("Select Test Case Level: ");
                                choice = Console.ReadLine();


                            } while (!(choice == "1" || choice == "2" || choice == "3" || choice == "4" || choice == "0"));

                            switch (choice)
                            {
                                case "1":
                                    path += @"Complete\small\";
                                    do
                                    {

                                        Console.WriteLine("1.Case1\t2.Case2");
                                        Console.Write("Select Test Case: ");
                                        choice = Console.ReadLine();
                                    } while (!(choice == "1" || choice == "2"));


                                    switch (choice)
                                    {
                                        case "1":
                                            path += @"Case1\";
                                            moviesPath = path + "Movies193.txt";
                                            queriesPath = path + "queries110.txt";
                                            break;
                                        case "2":
                                            path += @"Case2\";
                                            moviesPath = path + "Movies187.txt";
                                            queriesPath = path + "queries50.txt";

                                            break;
                                    }


                                    break;
                                case "2":
                                    path += @"Complete\medium\";
                                    do
                                    {
                                        Console.WriteLine("1.Case1\t2.Case2");
                                        Console.Write("Select Test Case: ");
                                        choice = Console.ReadLine();
                                    } while (!(choice == "1" || choice == "2"));


                                    switch (choice)
                                    {
                                        case "1":
                                            path += @"Case1\";
                                            moviesPath = path + "Movies967.txt";
                                            queriesPath = path + "queries4000.txt";
                                            //queriesPath = path + "queries85.txt";
                                            break;
                                        case "2":
                                            path += @"Case2\";
                                            moviesPath = path + "Movies4736.txt";
                                            queriesPath = path + "queries2000.txt";
                                            //queriesPath = path + "queries110.txt";
                                            break;
                                    }
                                    break;
                                case "3":
                                    path += @"Complete\large\";
                                    moviesPath = path + "Movies14129.txt";
                                    queriesPath = path + "queries600.txt";
                                    //queriesPath = path + "queries26.txt";
                                    break;
                                case "4":
                                    path += @"Complete\extreme\";
                                    moviesPath = path + "Movies122806.txt";
                                    queriesPath = path + "queries200.txt";
                                    //queriesPath = path + "queries22.txt";
                                    break;
                                case "0":
                                    goto Ask;
                            }
                            break;
                    }



                }
        */
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

    }
}
