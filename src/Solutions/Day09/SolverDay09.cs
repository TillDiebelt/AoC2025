using AOCLib;
using AOCLib.Parsing;
using System;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Runtime.ConstrainedExecution;
using static System.Net.Mime.MediaTypeNames;

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

            for (int i = 0; i < tups.Count; i++)
            {
                for (int j = i + 1; j < tups.Count; j++)
                {
                    var p1 = tups[i];
                    var p2 = tups[j];

                    long newArea = GetArea(ref tups, i, j);
                    if (newArea > area)
                    {
                        area = newArea;
                        Console.WriteLine($"[{p1.x}][{p1.y}] - [{p2.x}][{p2.y}]");
                    }
                }
            }

            result = area;

            return result;
        }

        public static long GetArea(ref List<(long x, long y)> tups, int start, int end)
        {
            long area = 0;

            var p1 = tups[start];
            var p2 = tups[end];

            //greedy optimization
            if (p1.x == p2.x || p1.y == p2.y)
                return 0;

            bool valid = true;
            (long x, long y) diff = ((p2.x - p1.x), (p2.y - p1.y));

            ((long x, long y) a, (long x, long y) b, (long x, long y) c) test1 = (
                (p1.x + Math.Sign(diff.x), p1.y + Math.Sign(diff.y)),
                (p2.x + Math.Sign(diff.x), p1.y + Math.Sign(diff.y)),
                (p2.x + Math.Sign(diff.x), p2.y + Math.Sign(diff.y))
            );
            ((long x, long y) a, (long x, long y) b, (long x, long y) c) test2 = (
                (p1.x + Math.Sign(diff.x), p1.y + Math.Sign(diff.y)),
                (p1.x + Math.Sign(diff.x), p2.y + Math.Sign(diff.y)),
                (p2.x + Math.Sign(diff.x), p2.y + Math.Sign(diff.y))
            );

            valid &= !IsPathCrossing(ref tups, test1);
            valid &= !IsPathCrossing(ref tups, test2);

            if (valid)
            {
                area = (Math.Abs(p1.x - p2.x) + 1) * (Math.Abs(p1.y - p2.y) + 1);
            }
            return area;
        }

        public static bool IsPathCrossing(ref List<(long x, long y)> tups, ((long x, long y) a, (long x, long y) b, (long x, long y) c) test)
        {

            bool crossing = false;

            List<((long x, long y), (long x, long y))> vectors = new();

            for (int i = 0; i < tups.Count - 1; i++)
            {
                int j = i + 1;
                var p1 = tups[i];
                var p2 = tups[j];

                vectors.Add((p1, p2));
            }

            var testVec1 = (test.a, test.b);
            var testVec2 = (test.b, test.c);

            foreach (var v in vectors)
            {
                if(IsVectorsCrossing(testVec1, v) || IsVectorsCrossing(testVec2, v))
                {
                    return true;
                }
            }
            return crossing;
        }

        public static Dictionary<(((long x, long y), (long x, long y)), ((long x, long y), (long x, long y))), bool> dyn = new();
        public static bool IsVectorsCrossing(((long x, long y), (long x, long y)) v1, ((long x, long y), (long x, long y)) v2)
        {
            bool crossing = false;

            (long x, long y) diffV1 = ((v1.Item2.x - v1.Item1.x), (v1.Item2.y - v1.Item1.y));
            (long x, long y) diffV2 = ((v2.Item2.x - v2.Item1.x), (v2.Item2.y - v2.Item1.y));

            //parallel never cross
            if (Math.Abs(diffV1.x) == Math.Abs(diffV2.x))
                return false;

            if (dyn.ContainsKey((v1, v2)))
                return dyn[(v1, v2)];

            List<(long x, long y)> pointsV1 = new List<(long x, long y)>();
            for(var i = 1; i < Math.Max(Math.Abs(diffV1.x), Math.Abs(diffV1.y)); i++)
            {
                pointsV1.Add(
                    (
                        v1.Item1.x + (i * Math.Sign(diffV1.x)),
                        v1.Item1.y + (i * Math.Sign(diffV1.y))
                    )
                );
            }

            for (var i = 1; i < Math.Max(Math.Abs(diffV2.x), Math.Abs(diffV2.y)); i++)
            {
                if(pointsV1.Contains(
                    (
                        v2.Item1.x + (i * Math.Sign(diffV2.x)),
                        v2.Item1.y + (i * Math.Sign(diffV2.y))
                    )
                ))
                {
                    dyn.Add((v1, v2), true);
                    return true;
                }
            }

            dyn.Add((v1, v2), false);
            return crossing;
        }


        /*
int[,] map = new int[tups.Max(x => x.x + 1), tups.Max(x => x.y + 1)];

long area = 0;
tups.Add(tups[0]);
(long x, long y) cur = tups[0];
for (int i = 1; i < tups.Count; i++)
{
    var next = tups[i];
    (long x, long y) dif = ((next.x - cur.x), (next.y - cur.y));
    map[cur.y, cur.x] = 1;
    for(int x = 0; x < Math.Abs(dif.x); x++)
    {
        cur.x += Math.Sign(dif.x);
        map[cur.y, cur.x] = 1;
    }
    for (int y = 0; y < Math.Abs(dif.y); y++)
    {
        cur.y += Math.Sign(dif.y);
        map[cur.y, cur.x] = 1;
    }
}
*/

        /*
         

                    bool valid = true;
                    int up = 0;
                    int left = 0;
                    var cur = tups[i];
                    for(var c = i + 1; c < j; c++)
                    {
                        var next = tups[c];
                        (long x, long y) dif = ((next.x - cur.x), (next.y - cur.y));
                        if(up == 0 || up == Math.Sign(dif.x))
                        {
                            up = Math.Sign(dif.x);
                        }
                        else
                        {
                            valid = false;
                            break;
                        }
                        if (left == 0 || left == Math.Sign(dif.y))
                        {
                            left = Math.Sign(dif.y);
                        }
                        else
                        {
                            valid = false;
                            break;
                        }
                        cur.x = next.x; cur.y = next.y;
                    }
         */
    }
}
