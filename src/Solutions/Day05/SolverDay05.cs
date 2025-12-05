using System;
using System.IO;
using System.Linq;
using AOCLib;
using AOCLib.Parsing;

namespace Solutions
{
    public class SolverDay05 : ISolver
    {
        public long SolvePart1(string[] lines, string text)
        {
            long result = 0;

            List<(long,long)> ranges = new List<(long, long)>();
            List<long> longs = new();

            int j = 0;
            for(int i = 0; i < lines.Length; i++)
            {
                j++;
                string line = lines[i];
                if (line.Length == 0)
                    break;
                ranges.Add((long.Parse(line.Split("-")[0]), long.Parse(line.Split("-")[1])));
            }

            for (; j < lines.Length; j++)
            {
                longs.Add(long.Parse(lines[j]));
            }

            for(int i = 0; i < longs.Count; i++)
            {
                long num = longs[i];
                bool inRange = false;
                foreach(var range in ranges)
                {
                    if(num >= range.Item1 && num <= range.Item2)
                    {
                        inRange = true;
                        break;
                    }
                }
                if(inRange)
                    result++;
            }

            return result;
        }

        public long SolvePart2(string[] lines, string text)
        {
            long result = 0;

            List<(long, long)> ranges = new List<(long, long)>();
            List<long> longs = new();

            int j = 0;
            for (int i = 0; i < lines.Length; i++)
            {
                j++;
                string line = lines[i];
                if (line.Length == 0)
                    break;
                ranges.Add((long.Parse(line.Split("-")[0]), long.Parse(line.Split("-")[1])));
            }

            ranges = ranges.OrderBy(r => r.Item1).ToList();
            for(int i = 1; i < ranges.Count; i++)
            {
                if (ranges[i].Item1 <= ranges[i - 1].Item2)
                {
                    ranges[i - 1] = (ranges[i - 1].Item1, Math.Max(ranges[i - 1].Item2, ranges[i].Item2));
                    ranges.RemoveAt(i);
                    i--;
                }
            }
            foreach (var range in ranges)
            {
                result += range.Item2 - range.Item1 + 1;
            }

            return result;
        }
    }
}

