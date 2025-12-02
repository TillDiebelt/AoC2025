using System;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using AOCLib;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Solutions
{
    public class SolverDay02 : ISolver
    {
        public long SolvePart1(string[] lines, string text)
        {
            long result = 0;

            List<(long,long)> ranges = new List<(long, long)>();
            var pairs = lines[0].Split(",").ToList();
            foreach (var pair in pairs)
            {
                var range1 = long.Parse(pair.Split('-')[0]);
                var range2 = long.Parse(pair.Split('-')[1]);
                ranges.Add((range1, range2));
            }

            foreach(var range in ranges)
            {
                for(long i = range.Item1; i <= range.Item2; i++)
                {
                    string strI = i.ToString();
                    if (strI.Length % 2 == 1)
                    {
                        continue;
                    }
                    if(strI.Substring(0, strI.Length / 2) == strI.Substring(strI.Length / 2))
                    {
                        result += i;
                    }
                }
            }
            return result;
        }

        public long SolvePart2(string[] lines, string text)
        {
            long result = 0;

            List<(long, long)> ranges = new List<(long, long)>();
            var pairs = lines[0].Split(",").ToList();
            foreach (var pair in pairs)
            {
                var range1 = long.Parse(pair.Split('-')[0]);
                var range2 = long.Parse(pair.Split('-')[1]);
                ranges.Add((range1, range2));
            }


            foreach (var range in ranges)
            {
                for (long i = range.Item1; i <= range.Item2; i++)
                {
                    string strI = i.ToString();
                    if (strI.Length == 1) continue;
                    var factors = Factors(strI.Length);
                    bool allEqual = true;
                    for (int p = 0; p < factors.Count; p++)
                    {
                        allEqual = true;
                        string start = strI.Substring(0, strI.Length / factors[p]);
                        for (int x = 1; x < factors[p]; x++)
                        {
                            allEqual &= (start.Equals(strI.Substring(x * (strI.Length / factors[p]), strI.Length / factors[p])));
                        }
                        if (allEqual)
                        {
                            break;
                        }
                    }
                    if(allEqual)
                    {
                        result += i;
                    }
                }
            }
            return result;
        }

        public static List<int> Factors(int n)
        {
            List<int> factors = new List<int>();
            factors.Add(n);
            for (int i = 2; i <= n/2; i++)
            {
                if(n % i == 0)
                {
                    factors.Add(i);
                }
            }

            return factors;
        }
    }
}
