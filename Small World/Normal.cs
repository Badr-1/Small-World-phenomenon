using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
namespace SmallWorld
{
    class Normal
    {
        private static Dictionary<string, HashSet<string>> actorsInMovie = new Dictionary<string, HashSet<string>>();
        private static Dictionary<string, HashSet<string>> moviesOfActor = new Dictionary<string, HashSet<string>>();
        public static void ParseMovies()
        {
            actorsInMovie.Clear();
            moviesOfActor.Clear();
            StreamReader reader = new StreamReader(Program.moviesPath);
            while (true)
            {
                string line = reader.ReadLine();
                if (line == null)
                    break;
                string[] data = line.Split('/');
                actorsInMovie[data[0]] = new HashSet<string>();
                Movie movie = new Movie(data[0]);
                for (int i = 1; i < data.Length; i++)
                {
                    actorsInMovie[data[0]].Add(data[i]);
                    if (!moviesOfActor.ContainsKey(data[i]))
                        moviesOfActor[data[i]] = new HashSet<string>();
                    moviesOfActor[data[i]].Add(data[0]);
                    movie.actors.Add(data[i]);
                }
            }
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
                            movieActor = dictActors[actorInMovie];
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
                                string commonMovies = "";
                                if (!Program.sample)
                                    commonMovies = commonMoviesList[0];
                                for (int i = 0; i < commonMoviesList.Count(); i++)
                                {
                                    relation++;
                                    if (Program.sample)
                                    {
                                        if (commonMovies == "")
                                            commonMovies += commonMoviesList[i];
                                        else
                                            commonMovies += commonMoviesList[i].Substring(commonMoviesList[i].Count() - 1);
                                        if (i != commonMoviesList.Count() - 1)
                                            commonMovies += " or ";
                                    }
                                    string elm = commonMoviesList[i];
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
                if (!Program.sample)
                {
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
                }
                else
                {
                    answer += $"{who}/{whom}\t\t{degree}\t\t{relation}\t\t";
                    while (moviesList.Count() != 0)
                    {
                        answer += moviesList.Pop();
                        if (moviesList.Count() != 0)
                            answer += " => ";
                    }
                }
                return answer;
            }
        }
    }
}