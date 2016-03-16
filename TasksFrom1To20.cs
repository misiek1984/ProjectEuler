using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ProjectEuler
{
    public partial class Solutions
    {
        #region Problem 12

        private static void Problem12()
        {
            for (var i = 0; i < Int32.MaxValue; ++i)
            {
                var n = SumN(i);
                if (CountDivisiros(n) > 500)
                {
                    Console.WriteLine(n);
                    return;
                }
            }
        }

        private static int CountDivisiros(int n)
        {
            var s = Math.Sqrt(n);
            var count = 0;
            for (var i = 1; i <= s; ++i)
                if (n % i == 0)
                    count += 2;

            return count;
        }

        private static int SumN(int n)
        {
            return (int)((1.0 + n) * n / 2);
        }

        #endregion
        #region Problem 13

        private static void Problem13()
        {
            long res = 0;
            foreach (var s in File.ReadAllLines(@"Input\Problem13.txt"))
                res += long.Parse(s.Substring(0, 11));

            Console.WriteLine(res);
        }

        #endregion
        #region Problem 14

        private static void Problem14()
        {
            var max = 0;
            var res = 0;

            for (var i = 1; i < 1000000; ++i)
            {
                var length = 2;
                var temp = Next(i);
                while (temp != 1)
                {
                    temp = Next(temp);
                    length++;
                }

                if (length > max)
                {
                    max = length;
                    res = i;
                }
            }

            Console.WriteLine(res);
        }

        private static long Next(long i)
        {
            //n  n/2 (n is even)
            //n  3n + 1 (n is odd)
            if (i % 2 == 0)
                return i / 2;

            return 3 * i + 1;
        }

        #endregion
        #region Problem 17

        private static void Problem17()
        {
            var unitsMap = new[] { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen" };
            var tensMap = new[] { "zero", "ten", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety" };

            var count = 0;
            for (var i = 1; i <= 1000; ++i)
            {
                Console.WriteLine(i);
                if (i <= 19)
                    count += LessThan20(unitsMap, i);
                else if (i < 100)
                    count += GreaterThan19LessThan100(unitsMap, tensMap, i);
                else if (i < 1000)
                    count += GreatarThan99LessThan1001(unitsMap, tensMap, i);
                else
                {
                    count += "onethousand".Length;
                    Console.Write("one thousand");
                }
                Console.WriteLine();
            }

            Console.WriteLine(count);
        }

        private static int LessThan20(string[] unitsMap, int i)
        {
            Console.Write(unitsMap[i] + " ");
            return unitsMap[i].Length;
        }
        private static int GreaterThan19LessThan100(string[] unitsMap, string[] tensMap, int i)
        {
            var count = 0;

            if (i / 10 != 0)
            {
                count += tensMap[i / 10].Length;
                Console.Write(tensMap[i / 10] + " ");
            }

            if (i % 10 != 0)
                count += LessThan20(unitsMap, i % 10);

            return count;
        }

        private static int GreatarThan99LessThan1001(string[] unitsMap, string[] tensMap, int i)
        {
            var count = unitsMap[i / 100].Length;
            Console.Write(unitsMap[i / 100] + " ");
            count += "hundred".Length;
            Console.Write("hundred ");

            var j = i % 100;

            if (j != 0)
            {
                count += "and".Length;
                Console.Write("and ");

                if (j <= 19)
                    count += LessThan20(unitsMap, j);
                else
                    count += GreaterThan19LessThan100(unitsMap, tensMap, j);
            }

            return count;
        }

        #endregion
        #region Problem 18

        private class Node
        {
            public int Weight { get; set; }
            public int Distance { get; set; }
            public Node Prev { get; set; }
        }

        public static int Problem18()
        {
            // Step 1: initialize graph
            List<List<Node>> graph;
            var count = ReadGraph(out graph, true);

            //Bellman–Ford

            // Step 2: relax edges repeatedly
            //  for i from 1 to size(vertices)-1:
            for (var x = 0; x < count - 1; ++x)
            {
                // for each edge (u, v) with weight w in edges:
                for (var i = 0; i < graph.Count - 1; ++i)
                {
                    for (var j = 0; j < graph[i].Count; ++j)
                    {
                        //if distance[u] + w < distance[v]:
                        //   distance[v] := distance[u] + w
                        //   predecessor[v] := u

                        var node = graph[i][j];
                        var child1 = graph[i + 1][j];
                        var child2 = graph[i + 1][j + 1];
                        if (node.Distance + node.Weight < child1.Distance)
                        {
                            child1.Distance = node.Distance + node.Weight;
                            child1.Prev = node;
                        }

                        if (node.Distance + node.Weight < child2.Distance)
                        {
                            child2.Distance = node.Distance + node.Weight;
                            child2.Prev = node;
                        }
                    }
                }
            }

            // Step 3: check for negative-weight cycles
            for (var i = 0; i < graph.Count - 1; ++i)
            {
                for (var j = 0; j < graph[i].Count; ++j)
                {
                    var node = graph[i][j];
                    var child1 = graph[i + 1][j];
                    var child2 = graph[i + 1][j + 1];

                    if (node.Distance + node.Weight < child1.Distance)
                    {
                        throw new Exception();
                    }

                    if (node.Distance + node.Weight < child2.Distance)
                    {
                        throw new Exception();
                    }
                }
            }

            var res = graph[graph.Count - 1].Min(n => n.Distance + n.Weight);
            return res;
        }

        private static int ReadGraph(out List<List<Node>> graph, bool negative)
        {
            var count = 0;
            graph = new List<List<Node>>();
            var array = File.ReadAllLines(@"Input\Problem18.txt");
            for (var i = 0; i < array.Length; ++i)
            {
                var res = array[i].Split(new[] { ' ' });

                var list = new List<Node>();
                graph.Add(list);
                foreach (var n in res)
                {
                    list.Add(new Node
                    {
                        Weight = negative ? -Int32.Parse(n) : Int32.Parse(n),
                        Distance = i == array.Length - 1 ? Int32.MaxValue : 0,
                        Prev = null
                    });
                    count++;
                }
            }
            return count;
        }

        #endregion
        #region Problem 19

        public static void Problem19()
        {
            var date = new DateTime(1901, 1, 1);
            var end = new DateTime(2000, 12, 31);
            var count = 0;
            while (date != end)
            {
                if (date.DayOfWeek == DayOfWeek.Sunday && date.Day == 1)
                    count++;

                date = date.AddDays(1);
            }

            Console.WriteLine(count);
        }

        #endregion
    }
}
