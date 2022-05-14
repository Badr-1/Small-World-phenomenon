| test case | movies | actors | queries | best time (s) | current time (s) | new time |
|-----------|--------|--------|---------|---------------|------------------|----------|
| 1         | 193    | 2513   | 110     | 0.236         | 1.16             |    0.312 |
| 2         | 187    | 8264   | 50      | 1.649         | 5.00             |    1.156 |
| 3         | 967    | 13848  | 85      | 10.249        | 15.00            |    2.95  |
| 4         | 967    | 13848  | 4000    | 81.556        | 4m               |    53.9  |
| 5         | 4735   | 43923  | 110     | 23.983        | 50.298           |     7.4  |
| 6         | 4735   | 43923  | 2000    | 35.609        | 2.5m             |       30.7   |
| 7         | 14129  | 170518 | 26      | 0.00          | 2m               |    87      |
| 8         | 14129  | 170518 | 600     | 8.051         | 15.407           |       10.9   |
| 9         | 122806 | 418451 | 22      | 0.00          | 12m              |       762   |
| 10        | 122806 | 418451 | 200     | 37.934        | 55.141           |      49.25    |



```c#
 public static string solve(string first, string second)
        {


            // lvl, asked, nextlvl   
            bool[] inQueue = new bool[index.Count];

            int degree = 0, answer = int.MaxValue;
            Queue<int> queue = new Queue<int>();
            int iFirst = index[first];
            int iSecond = index[second];
            queue.Enqueue(iFirst);
            inQueue[iFirst] = true;
            parent[iFirst] = null;
            while (queue.Count() != 0)
            {
                Actor current = actors[queue.Dequeue()];
                if (visited[current.index])
                {
                    continue;
                }
                // if found directly
                if (current.adjs.Contains(iSecond))
                {
                    if (parent[iSecond] == null)
                    {
                        int CommonWithCurrent = actors[iSecond].movies.Intersect(current.movies).Count();
                        parent[iSecond] = current;
                        weight[iSecond] = weight[current.index] + CommonWithCurrent;

                    }
                    else
                    {
                        changeParent(actors[iSecond], current);
                    }
                    if (answer == int.MaxValue)
                    {
                        int i = iSecond;
                        while (parent[i] != null)
                        {
                            degree++;
                            i = parent[i].index;
                        }
                        answer = degree;
                    }
                    //Console.WriteLine("Found {0}/{1} in {2}", first, second, answer);
                }
                else
                {
                    foreach (int adj in current.adjs)
                    {
                        if (visited[adj])
                            continue;
                        if (parent[adj] == null)
                        {
                            int CommonWithCurrent = actors[adj].movies.Intersect(current.movies).Count();
                            parent[adj] = current;
                            weight[adj] = weight[current.index] + CommonWithCurrent;

                        }
                        if (!inQueue[adj])
                        {
                            queue.Enqueue(adj);
                            changeParent(actors[adj], current);
                            inQueue[adj] = true;
                        }






                    }



                }
                visited[current.index] = true;


                if (degree > answer)
                    break;

            }
            string answerString = "";
            answerString += String.Format("{0}/{1}\n", first, second);
            int relation = 0;
            int chain = iSecond;
            while (parent[chain] != null)
            {
                relation += actors[chain].movies.Intersect(actors[parent[chain].index].movies).Count();
                chain = parent[chain].index;
            }
            List<string> chainActors = new List<string>();
            List<string> chainMovies = new List<string>();
            answerString += String.Format("DoS = {0}, RS = {1}\n", answer, relation);


            return answerString;
        }
```


A
B
C
D
E
F
G
H
I
J
K
L
M
N
Z

Z A

A B
A C
B C

D E
D A
D B
E A
E B

E F
E G
F G


F G
F H
G H

H I
H J
H K
I J
I K
J K

J L
J M
J N
L M
L N
M N

A B
A C
A D
B C
B D
C D

