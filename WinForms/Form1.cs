namespace WinForms
{
    public partial class Normal : Form
    {
        static string moviesPath, queriesPath, solutionPath;
        static List<KeyValuePair<string, string>> queries = new List<KeyValuePair<string, string>>();
        static Queue<string> answers = new Queue<string>();
        // optimized
        static Dictionary<string, HashSet<string>> actorsInMovie = new Dictionary<string, HashSet<string>>();
        static Dictionary<string, HashSet<string>> moviesOfActor = new Dictionary<string, HashSet<string>>();

        // bonus
        static Dictionary<int, int> degressOfSaperation = new Dictionary<int, int>();
        static List<string> moviesNames = new List<string>();
        static HashSet<string> asked = new HashSet<string>();

        // old
        static List<movie> movies = new List<movie>();

        public class actor
        {
            public int weight = 0;
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

            public actor(int weight, string name, actor parent, bool asked, bool toSkip, List<string> commonMovies, Dictionary<string, bool> askedMovies)
            {
                this.weight = weight;
                this.name = name;
                this.parent = parent;
                this.asked = asked;
                this.toSkip = toSkip;
                this.commonMovies = commonMovies;
                this.askedMovies = askedMovies;
            }

            public actor copy()
            {
                return new actor(weight, name, parent, asked, toSkip, commonMovies, askedMovies);
            }
            public void setWeight()
            {
                actor dummy = copy();
                weight = 0;
                while (dummy != null)
                {
                    weight += dummy.commonMovies.Count();
                    dummy = dummy.parent;
                }
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

            public string print(string who, string whom)
            {
                string answer = "";
                //Console.WriteLine();
                //Console.WriteLine("{0}/{1}", who, whom);
                answer += String.Format("{0}/{1}\n", who, whom);
                //Console.WriteLine("DoS = {0}, RS = {1}", degree, relation);
                answer += String.Format("DoS = {0}, RS = {1}\n", degree, relation);
                //Console.Write("CHAIN OF ACTORS: {0} -> ", who);
                answer += String.Format("CHAIN OF ACTORS: {0} -> ", who);
                while (actorList.Count() != 0)
                {
                    //Console.Write(actorList.Pop());
                    answer += actorList.Pop();
                    if (actorList.Count() != 0)
                        //Console.Write(" -> ");
                        answer += " -> ";
                }
                //Console.WriteLine();
                answer += "\n";
                //Console.Write("CHAIN OF MOVIES:  => ");
                answer += "CHAIN OF MOVIES:  =>";
                while (moviesList.Count() != 0)
                {
                    //Console.Write(moviesList.Pop());
                    answer += " " + moviesList.Pop();
                    //Console.Write(" => ");
                    answer += " =>";

                }
                //Console.WriteLine();
                answer += "\n";

                return answer;
            }
        }
        public Normal()
        {
            InitializeComponent();
        }



        public static void parseSolution()
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

        public static void selectTestCase(string choice)
        {
            string path = @"..\..\..\..\Testcases\";  
            switch (choice)
            {
                case "0":
                    path += @"Sample\";
                    moviesPath = path + "movies1.txt";
                    queriesPath = path + "queries1.txt";
                    solutionPath = path + "queries1 - Formated Solution.txt";
                    break;
                case "1":
                    path += @"Complete\small\Case1\";
                    moviesPath = path + "Movies193.txt";
                    queriesPath = path + "queries110.txt";
                    solutionPath = path + @"\Solution\queries110 - Solution.txt";
                    //solutionPath = path + @"\Solution\queries110 - Solution - lite.txt";

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

        public static bool isAnswerCorrect(string answer)
        {
            string actual = answers.Dequeue();
            return answer == actual;
        }

        public  void runTestCase()
        {
            run.Enabled = false;
            do_parse();
            long timeBefore = System.Environment.TickCount;
            double @new;
            int current = 1;
            bool passed = true;
            foreach (KeyValuePair<string, string> kvp in queries)
            {
                string actual = answers.Dequeue();
                string answer = solve(kvp.Key, kvp.Value);
                if (answer != actual)
                {
                    passed = false;
                }
                //else
                //    Console.WriteLine(answer);
                current++;
            }
            long timeAfter = System.Environment.TickCount;
            @new = (timeAfter - timeBefore) * 0.001;
            int mins = (int)@new / 60;
            @new %= 60;
            ltime.Text = String.Format("Time: {0} min(s) and {1} sec(s)", mins, @new);
            lstatus.Text = String.Format("Status: {0}",passed ? "Passed!" : "Failed!");
            if (passed)
                lstatus.ForeColor = Color.Green;
            else
                lstatus.ForeColor = Color.Red;

            run.Enabled = true;







        }
        public static string solve(string who, string whom)
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
                        return answer.print(who, whom);
                    }
                }


            }
            return "";
        }

        public static string oldSolve(string who, string whom)
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
                                        movieActor.setWeight();
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
                                        if (actor.weight + commonMovies.Count() > movieActor.parent.weight + movieActor.commonMovies.Count())
                                        {
                                            movieActor.parent = actor;
                                            movieActor.commonMovies = commonMovies;
                                            movieActor.setWeight();
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
                        return answer.print(who, whom);
                    }
                }
            }
            return "";
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

        private void do_parse()
        {
            asked.Clear();
            moviesNames.Clear();
            actorsInMovie.Clear();
            moviesOfActor.Clear();
            queries.Clear();
            movies.Clear();
            answers.Clear();
            parseMovies();
            parseQueries();
            parseSolution();
            lsummary.Text = String.Format("Summary:\n\t{0} Movies {1} Actors {2} Queries {3} Solutions", actorsInMovie.Count(), moviesOfActor.Count(), queries.Count(), answers.Count());
            run.Enabled = true;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ltime.Text = "Time:";
            lstatus.Text = "Status:";
            lsummary.Text = "Summary:";
            lstatus.ForeColor = Color.Black;
            selectTestCase(testCasesCombo.SelectedItem.ToString());
            run.Enabled = true;
        }
     

        private void run_Click(object sender, EventArgs e)
        {
            var thread = new System.Threading.Thread(runTestCase);
            thread.Start();
        }        
    }
}