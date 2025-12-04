using System;
using System.IO;
using System.Linq;
using AOCLib;

namespace Solutions
{
    public class SolverDay03 : ISolver
    {
        public long SolvePart1(string[] lines, string text)
        {
            long result = 0;

            for (int i = 0; i < lines.Length; i++)
            {
                result += getMax(lines[i], 2);
            }

            return result;
        }

        public long SolvePart2(string[] lines, string text)
        {
            long result = 0;

            for (int i = 0; i < lines.Length; i++)
            {
                result += getMax(lines[i], 12);
            }

            return result;
        }

        public static Dictionary<(string, int), long> maxes = new Dictionary<(string, int), long>();
        public static long getMax(string line, int digits)
        {
            if (line.Length < digits || digits == 0)
                return 0;
            if(maxes.ContainsKey((line, digits)))
                return maxes[(line, digits)];
            long res = Math.Max(
                (line[0] - '0') * (long)Math.Pow(10, digits - 1) + getMax(line.Substring(1), digits - 1),
                getMax(line.Substring(1), digits)
            );
            maxes[(line, digits)] = res;
            return res;
        }
    }
}

