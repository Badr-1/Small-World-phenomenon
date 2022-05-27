using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
namespace SmallWorld
{
    class Normal
    {
        private static Dictionary<string, HashSet<string>> actorsInMovie = new Dictionary<string, HashSet<string>>(); //O(1)
        private static Dictionary<string, HashSet<string>> moviesOfActor = new Dictionary<string, HashSet<string>>(); //O(1)
        //O(N * M)
        public static void ParseMovies()
        {
            actorsInMovie.Clear(); //O(1)
            moviesOfActor.Clear(); //O(1)
            StreamReader reader = new StreamReader(Program.moviesPath); //O(1)
            while (true) //O(N * M)
            {
                string line = reader.ReadLine(); //O(1) 
                if (line == null) //O(1)
                    break; //O(1)
                string[] data = line.Split('/'); //O(M)
                actorsInMovie[data[0]] = new HashSet<string>(); //O(1)
                Movie movie = new Movie(data[0]); //O(1)
                for (int i = 1; i < data.Length; i++) //O(M)
                {
                    actorsInMovie[data[0]].Add(data[i]); //O(1)
                    if (!moviesOfActor.ContainsKey(data[i])) //O(1)
                        moviesOfActor[data[i]] = new HashSet<string>(); //O(1)
                    moviesOfActor[data[i]].Add(data[0]); //O(1)
                    movie.actors.Add(data[i]); //O(1)
                }
            }
        }

        //O(V + E)
        public static string Solve(string who, string whom)
        {
            int degree = 1, answerDegree = int.MaxValue, maxRelation = 0; //O(1)
            List<Answer> answers = new List<Answer>(); //O(1)
            var dictActors = new Dictionary<string, Actor>(); //O(1)
            Queue<Actor> actorsToAsk = new Queue<Actor>(); //O(1)
            HashSet<string> lvlActors = new HashSet<string>(); //O(1)
            HashSet<string> nextLvlActors = new HashSet<string>(); //O(1)
            HashSet<string> lvlAskedActors = new HashSet<string>(); //O(1)
            dictActors[who] = new Actor(who, null); //O(1)
            actorsToAsk.Enqueue(dictActors[who]); //O(1)
            lvlActors.Add(who); //O(1)
            while (actorsToAsk.Count() != 0) //O(V + E)
            {
                Actor actor = actorsToAsk.Dequeue(); //O(1)
                List<string> movies = moviesOfActor[actor.name].ToList(); //O(1)
                foreach (string movie in movies) //O(V + E)
                {
                    if (actor.askedMovies.ContainsKey(movie) && actor.askedMovies[movie] == true) //O(1)
                        continue; //O(1)
                    else
                        actor.askedMovies[movie] = false; //O(1)
                    List<string> actors = actorsInMovie[movie].ToList(); //O(N)
                    foreach (string actorInMovie in actors) //O(E)
                    {
                        Actor movieActor = new Actor(actorInMovie, actor); //O(1)
                        if (dictActors.ContainsKey(actorInMovie)) //O(1)
                            movieActor = dictActors[actorInMovie]; //O(1)
                        else
                        {
                            if (actorInMovie != actor.name) //O(1)
                            {
                                List<string> commonMovies = moviesOfActor[actorInMovie].Intersect(moviesOfActor[actor.name]).ToList(); //O(N^2)
                                movieActor.commonMovies = commonMovies; //O(1)
                                movieActor.weight = movieActor.parent.weight + movieActor.commonMovies.Count(); //O(1)
                            }
                            dictActors[actorInMovie] = movieActor; //O(1)
                        }
                        if (!movieActor.asked && actorInMovie != actor.name && actorInMovie != whom) //O(1)
                        {
                            if (!movieActor.toSkip) //O(1)
                            {
                                actorsToAsk.Enqueue(movieActor); //O(1)
                                movieActor.toSkip = true; //O(1)
                            }
                            if (lvlActors.Contains(actor.name) && !lvlActors.Contains(actorInMovie))
                            {
                                List<string> commonMovies = moviesOfActor[actorInMovie].Intersect(moviesOfActor[actor.name]).ToList(); //O(N^2)
                                nextLvlActors.Add(actorInMovie); //O(1)
                                if (actor.weight + commonMovies.Count() > movieActor.parent.weight + movieActor.commonMovies.Count()) //O(1)
                                {
                                    movieActor.parent = actor; //O(1)
                                    movieActor.commonMovies = commonMovies; //O(1)
                                    movieActor.weight = movieActor.parent.weight + movieActor.commonMovies.Count(); //O(1)
                                }
                            }
                        }
                        if (actorInMovie == whom)
                        {
                            actor.askedMovies[movie] = true; //O(1)
                            Answer answer = new Answer(); //O(1)
                            Actor whomActor = new Actor(whom, actor); //O(1)
                            Stack<string> moviesList = new Stack<string>(); //O(1)
                            Stack<string> actorList = new Stack<string>(); //O(1)
                            Actor curr = new Actor(whom, actor); //O(1)
                            int relation = 0;
                            do
                            {
                                if (curr == null || curr.parent == null) //O(1)
                                    break; //O(1)
                                List<string> commonMoviesList = moviesOfActor[curr.name].Intersect(moviesOfActor[curr.parent.name]).ToList(); //O(N^2)
                                string commonMovies = ""; //O(1)
                                if (!Program.sample) //O(1)
                                    commonMovies = commonMoviesList[0]; //O(1)
                                for (int i = 0; i < commonMoviesList.Count(); i++) //O(N)
                                {
                                    relation++; //O(1)
                                    if (Program.sample)
                                    {
                                        if (commonMovies == "")
                                            commonMovies += commonMoviesList[i]; //O(1)
                                        else
                                            commonMovies += commonMoviesList[i].Substring(commonMoviesList[i].Count() - 1); //O(1)
                                        if (i != commonMoviesList.Count() - 1) //O(1)
                                            commonMovies += " or "; //O(1)
                                    }
                                    string elm = commonMoviesList[i]; //O(1)
                                    actor.askedMovies[elm] = true; //O(1)
                                }
                                moviesList.Push(commonMovies); //O(1)
                                actorList.Push(curr.name); //O(1)
                                curr = curr.parent; //O(1)
                            } while (true); //O(N^3)
                            maxRelation = (maxRelation < relation) ? relation : maxRelation; //O(1)
                            answer.moviesList = moviesList; //O(1)
                            answer.actorList = actorList; //O(1)
                            answer.degree = degree; //O(1)
                            answer.relation = relation; //O(1)
                            answerDegree = degree; //O(1)
                            answers.Add(answer); //O(1)
                        }
                    }

                }
                actor.asked = true; //O(1)
                lvlAskedActors.Add(actor.name); //O(1)
                if (lvlActors.Count() == lvlAskedActors.Count())
                {

                    degree++; //O(1)
                    lvlActors.Clear(); //O(1)
                    lvlAskedActors.Clear(); //O(1)
                    lvlActors = nextLvlActors; //O(1)
                    nextLvlActors = new HashSet<string>(); //O(1)
                }
                if (answerDegree < degree) //O(1)
                    break; //O(1)
            }
            foreach (Answer answer in answers) //O(N)
            {
                if (answer.relation == maxRelation)
                {
                    return answer.print(who, whom); //O(1)
                }
            }
            return ""; //O(1)
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
            //O(M + N)
            public string print(string who, string whom)
            {
                string answer = "";
                if (!Program.sample)
                {
                    answer += String.Format("{0}/{1}\n", who, whom); //O(1)
                    answer += String.Format("DoS = {0}, RS = {1}\n", degree, relation); //O(1)
                    answer += String.Format("CHAIN OF ACTORS: {0} -> ", who); //O(1)
                    while (actorList.Count() != 0) //O(N)
                    {
                        answer += actorList.Pop();//O(1)
                        if (actorList.Count() != 0)
                            answer += " -> ";//O(1)
                    }
                    answer += "\n";//O(1)
                    answer += "CHAIN OF MOVIES:  =>";//O(1)
                    while (moviesList.Count() != 0)//O(M)
                    {
                        answer += " " + moviesList.Pop(); //O(1)
                        answer += " =>"; //O(1)
                    }
                    answer += "\n"; //O(1)
                }
                else
                {
                    answer += $"{who}/{whom}\t\t{degree}\t\t{relation}\t\t"; //O(1)
                    while (moviesList.Count() != 0) //O(N)
                    {
                        answer += moviesList.Pop(); //O(1)
                        if (moviesList.Count() != 0) //O(1)
                            answer += " => "; //O(1)
                    }
                }
                return answer; //O(1)
            }
        }
    }
}