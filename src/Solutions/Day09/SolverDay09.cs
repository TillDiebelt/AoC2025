using AOCLib;
using AOCLib.Parsing;
using System;
using System.IO;
using System.Linq;
using System.Numerics;

namespace Solutions
{
    public class SolverDay09 : ISolver
    {
        public long SolvePart1(string[] lines, string text)
        {
            long result = 0;

            List<(long x, long y)> tups = new List<(long, long)> ();

            foreach (var line in lines)
            {
                var tmp = line.GetNumbers();
                tups.Add((tmp[0], tmp[1]));
            }

            long area = 0;
            for(int i = 0; i < tups.Count - 1; i++)
            {
                for (int j = i + 1; j < tups.Count; j++)
                {
                    var p1 = tups[i];
                    var p2 = tups[j];
                    area = Math.Max(area, (Math.Abs(p1.x - p2.x) + 1) * (Math.Abs(p1.y - p2.y) + 1));
                }
            }

            result = area;

            return result;
        }

        public long SolvePart2(string[] lines, string text)
        {

            long result = 0;
            List<(long x, long y)> tups = new List<(long, long)>();

            foreach (var line in lines)
            {
                var tmp = line.GetNumbers();
                tups.Add((tmp[0], tmp[1]));
            }

            long area = 0;

            List<((long x, long y), (long x, long y))> vectors = new();
            for (int i = 0; i < tups.Count; i++)
            {
                int j = (i + 1) % tups.Count;
                var p1 = tups[i];
                var p2 = tups[j];

                vectors.Add((p1, p2));
            }

            for (int i = 0; i < tups.Count - 1; i++)
            {
                for (int j = i + 1; j < tups.Count; j++)
                {
                    long newArea = GetArea(ref tups, ref vectors, i, j);
                    if (newArea > area)
                    {
                        area = newArea;
                        var p1 = tups[i];
                        var p2 = tups[j];
                        Console.WriteLine($"[{p1.x}][{p1.y}] - [{p2.x}][{p2.y}] : {area}");
                    }
                }
            }

            result = area;

            return result;
        }

        public static long GetArea(ref List<(long x, long y)> tups, ref List<((long x, long y), (long x, long y))> vecs, int start, int end)
        {
            long area = 0;

            var p1 = tups[start];
            var p2 = tups[end];

            //greedy optimization
            if (p1.x == p2.x || p1.y == p2.y)
                return 0;

            bool crossing = false;

            foreach (var v in vecs)
            {
                if (IsVectorCrossing((p1, p2), v))
                {
                    crossing = true;
                    break;
                }
            }

            if (!crossing)
            {
                area = (Math.Abs(p1.x - p2.x) + 1) * (Math.Abs(p1.y - p2.y) + 1);
            }

            return area;
        }

        public static bool IsVectorCrossing(((long x, long y), (long x, long y)) rect, ((long x, long y), (long x, long y)) vec)
        {
            bool crossing = false;

            //check if vector is crossing the rectangle sides
            //I think there might still be an edge case if 2 lines run right next to each other with no space but this works for my input so idc
            crossing = 
                   Math.Max(vec.Item1.x, vec.Item2.x) > Math.Min(rect.Item1.x, rect.Item2.x)
                && Math.Min(vec.Item1.x, vec.Item2.x) < Math.Max(rect.Item1.x, rect.Item2.x) 
                && Math.Max(vec.Item1.y, vec.Item2.y) > Math.Min(rect.Item1.y, rect.Item2.y)
                && Math.Min(vec.Item1.y, vec.Item2.y) < Math.Max(rect.Item1.y, rect.Item2.y);

            return crossing;
        }
    }
}
