using System;
using System.IO;
using System.Linq;
using System.Numerics;
using AOCLib;
using AOCLib.Parsing;

namespace Solutions
{
    public class SolverDay07 : ISolver
    {
        public long SolvePart1(string[] lines, string text)
        {
            long result = 0;

            int[,] map = new int[lines.Length, lines[0].Length];
            (int x, int y) start = (0, 0);
            for(int y  = 0; y < lines.Length; y++)
            {
                for(int x = 0; x < lines[0].Length; x++)
                {
                    if(lines[y][x] == 'S')
                    {
                        map[y,x] = 2;
                        start = (x, y);
                    }
                    else if (lines[y][x] == '^')
                    {
                        map[y,x] = 1;
                    }
                    else
                        map[y,x] = 0;
                }
            }

            HashSet<int> activeBeams = new HashSet<int>();
            activeBeams.Add(start.x);
            int height = 1;
            while (height + 1 < lines.Length)
            {
                HashSet<int> newBeams = new HashSet<int>();
                foreach (var b in activeBeams)
                {
                    if (map[height + 1, b] == 0)
                    {
                        newBeams.Add(b);
                    }
                    else if (map[height + 1, b] == 1)
                    {
                        if (b - 1 >= 0 && !newBeams.Contains(b - 1))
                        {
                            newBeams.Add(b - 1);
                        }
                        if (b + 1 < lines[0].Length && !newBeams.Contains(b + 1))
                        {
                            newBeams.Add(b + 1);
                        }
                        result++;
                    }
                }
                height += 1;
                activeBeams = newBeams;
            }

            return result;
        }

        public long SolvePart2(string[] lines, string text)
        {
            long result = 0;
            int[,] map = new int[lines.Length, lines[0].Length];
            (int x, int y) start = (0, 0);
            for (int y = 0; y < lines.Length; y++)
            {
                for (int x = 0; x < lines[0].Length; x++)
                {
                    if (lines[y][x] == 'S')
                    {
                        map[y, x] = 2;
                        start = (x, y);
                    }
                    else if (lines[y][x] == '^')
                    {
                        map[y, x] = 1;
                    }
                    else
                        map[y, x] = 0;
                }
            }

            Dictionary<int, long> activeBeams = new();
            activeBeams.Add(start.x, 1);
            int height = 1;
            while (height + 1 < lines.Length)
            {
                Dictionary<int, long> newBeams = new Dictionary<int, long>();
                foreach (var b in activeBeams)
                {
                    if (map[height + 1, b.Key] == 0)
                    {
                        if (newBeams.ContainsKey(b.Key))
                            newBeams[b.Key] += b.Value;
                        else
                            newBeams[b.Key] = b.Value;
                    }
                    else if (map[height + 1, b.Key] == 1)
                    {
                        if (b.Key - 1 >= 0)
                        {
                            if (newBeams.ContainsKey(b.Key - 1))
                                newBeams[b.Key - 1] += b.Value;
                            else
                                newBeams[b.Key - 1] = b.Value;
                        }
                        if (b.Key + 1 < lines[0].Length)
                        {
                            if (newBeams.ContainsKey(b.Key + 1))
                                newBeams[b.Key + 1] += b.Value;
                            else
                                newBeams[b.Key + 1] = b.Value;
                        }
                    }
                }
                height += 1;
                activeBeams = newBeams;
            }

            foreach(var v in activeBeams.Values)
            {
                result += v;
            }

            //recursive search solution
            //dyn.Clear();
            //result = search(ref map, 0, start.x);

            return result;
        }

        public static Dictionary<(int, int), long> dyn = new();
        public static long search(ref int[,] map, int height, int x)
        {
            long paths = 0;
            if (dyn.ContainsKey((height, x)))
                return dyn[(height, x)];
            if (map.GetUpperBound(0) == height)
                return 1;
            if(map[height + 1, x] == 1)
            {
                if (x - 1 >= 0)
                {
                    paths += search(ref map, height + 1, x - 1);
                }
                if (x + 1 <= map.GetUpperBound(1))
                {
                    paths += search(ref map, height + 1, x + 1);
                }
                dyn.Add((height, x), paths);
                return paths;
            }
            else
            {
                paths = search(ref map, height + 1, x);
                dyn.Add((height, x), paths);
                return paths;
            }
        }
    }
}
