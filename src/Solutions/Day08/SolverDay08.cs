using System;
using System.IO;
using System.Linq;
using System.Numerics;
using AOCLib;
using AOCLib.Parsing;

namespace Solutions
{
    public class SolverDay08 : ISolver
    {
        public long SolvePart1(string[] lines, string text)
        {
            long result = 0;
            List<(long, long, long)> numbers = new List<(long, long, long)> ();

            foreach (var line in lines)
            {
                var nums = line.GetNumbers();
                numbers.Add((nums[0], nums[1], nums[2]));
            }

            List<(int, int, double)> distances = new List<(int, int, double)> ();
            for (int i = 0; i < numbers.Count; i++)
            {
                for (int j = i + 1; j < numbers.Count; j++)
                {
                    if (i == j)
                        continue;
                    distances.Add((i, j, EuclidianDistance(numbers[i], numbers[j])));
                }
            }

            List<(int, int)> connections = new();

            int foo = numbers.Count > 100 ? 1000 : 10;
            distances = distances.OrderBy(x => x.Item3).ToList();
            for(int i = 0; i < foo; i++)
            {
                var cur = distances[i];
                connections.Add((cur.Item1, cur.Item2));
            }

            List<HashSet<int>> circuits = new List<HashSet<int>>();

            foreach (var con in connections)
            {
                bool skip = false;
                for(int i = 0; i < circuits.Count; i++)
                {
                    var cir = circuits[i];
                    if(cir.Contains(con.Item1))
                    {
                        //if(!cir.Contains(con.Item2))
                            cir.Add(con.Item2);

                        for (int j = 0; j < circuits.Count; j++)
                        {
                            if (i == j)
                                continue;
                            var cir2 = circuits[j];
                            foreach(var c in cir2)
                            {
                                if(cir.Contains(c))
                                {
                                    foreach(var c2 in cir2)
                                    {
                                        cir.Add(c2);
                                    }
                                    circuits.RemoveAt(j);
                                    j--;
                                    break;
                                }
                            }
                        }
                        skip = true;
                        break;
                    }
                    if(cir.Contains(con.Item2))
                    {
                        //if (!cir.Contains(con.Item1))
                            cir.Add(con.Item1);

                        for (int j = 0; j < circuits.Count; j++)
                        {
                            if (i == j)
                                continue;
                            var cir2 = circuits[j];
                            foreach (var c in cir2)
                            {
                                if (cir.Contains(c))
                                {
                                    foreach (var c2 in cir2)
                                    {
                                        cir.Add(c2);
                                    }
                                    circuits.RemoveAt(j);
                                    j--;
                                    break;
                                }
                            }
                        }
                        skip = true;
                        break;
                    }
                }
                if(!skip)
                {
                    var bar = new HashSet<int>();
                    bar.Add(con.Item1);
                    bar.Add(con.Item2);
                    circuits.Add(bar);
                }
            }

            circuits = circuits.OrderByDescending(x => x.Count).ToList();
            result = circuits[0].Count * circuits[1].Count * circuits[2].Count;

            return result;
        }

        public long SolvePart2(string[] lines, string text)
        {
            long result = 0;


            List<(long, long, long)> numbers = new List<(long, long, long)>();

            foreach (var line in lines)
            {
                var nums = line.GetNumbers();
                numbers.Add((nums[0], nums[1], nums[2]));
            }

            List<(int, int, double)> distances = new List<(int, int, double)>();
            for (int i = 0; i < numbers.Count; i++)
            {
                for (int j = i + 1; j < numbers.Count; j++)
                {
                    if (i == j)
                        continue;
                    distances.Add((i, j, EuclidianDistance(numbers[i], numbers[j])));
                }
            }

            List<(int, int)> connections = new();

            distances = distances.OrderBy(x => x.Item3).ToList();
            foreach(var cur in distances)
            { 
                connections.Add((cur.Item1, cur.Item2));
            }

            List<HashSet<int>> circuits = new List<HashSet<int>>();

            var conInd = 0;
            (long, long) last = (0, 0);
            do
            {
                var con = connections[conInd];
                conInd++;

                last = (numbers[con.Item1].Item1, numbers[con.Item2].Item1);

                bool skip = false;
                for (int i = 0; i < circuits.Count; i++)
                {
                    var cir = circuits[i];
                    if (cir.Contains(con.Item1))
                    {
                        //if(!cir.Contains(con.Item2))
                        cir.Add(con.Item2);

                        for (int j = 0; j < circuits.Count; j++)
                        {
                            if (i == j)
                                continue;
                            var cir2 = circuits[j];
                            foreach (var c in cir2)
                            {
                                if (cir.Contains(c))
                                {
                                    foreach (var c2 in cir2)
                                    {
                                        cir.Add(c2);
                                    }
                                    circuits.RemoveAt(j);
                                    j--;
                                    break;
                                }
                            }
                        }
                        skip = true;
                        break;
                    }
                    if (cir.Contains(con.Item2))
                    {
                        //if (!cir.Contains(con.Item1))
                        cir.Add(con.Item1);

                        for (int j = 0; j < circuits.Count; j++)
                        {
                            if (i == j)
                                continue;
                            var cir2 = circuits[j];
                            foreach (var c in cir2)
                            {
                                if (cir.Contains(c))
                                {
                                    foreach (var c2 in cir2)
                                    {
                                        cir.Add(c2);
                                    }
                                    circuits.RemoveAt(j);
                                    j--;
                                    break;
                                }
                            }
                        }
                        skip = true;
                        break;
                    }
                }
                if (!skip)
                {
                    var bar = new HashSet<int>();
                    bar.Add(con.Item1);
                    bar.Add(con.Item2);
                    circuits.Add(bar);
                }
            }
            while (circuits[0].Count < numbers.Count());

            result = last.Item1 * last.Item2;

            return result;
        }

        public static double EuclidianDistance((long, long, long) a, (long, long, long) b)
        {
            double distance = 0;

            distance = Math.Sqrt(Math.Pow(a.Item1 - b.Item1, 2) +  Math.Pow(a.Item2 - b.Item2, 2) + Math.Pow(a.Item3 - b.Item3, 2));

            return distance;
        }
    }
}
