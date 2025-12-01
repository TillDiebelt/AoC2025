using System;
using System.IO;
using System.Linq;
using AOCLib;

namespace Solutions
{
    public class SolverDay00 : ISolver
    {
        public long SolvePart1(string[] lines, string text)
        {
            //Parse
            // 0,3 -> 1,4
            // 2,1 -> 4,3
            //var rocklines = lines.Select(x => x.Split("->").ToList().Select(y => (Int32.Parse(y.Trim().Split(',')[0]), Int32.Parse(y.Trim().Split(',')[1]))).ToList());

            //1,3,5,1,2,455,6
            //var longs = lines[0].Split(',', StringSplitOptions.RemoveEmptyEntries).Select(x => Convert.ToInt64(x));

            //1
            //3
            var sum = lines.Select(y => Convert.ToInt64(y)).Select(x => x).Aggregate((x, y) => x + y);

            //Solve
            long result = sum;
            return result;
        }

        public long SolvePart2(string[] lines, string text)
        {
            //Parse
            // 0,3 -> 1,4
            // 2,1 -> 4,3
            //var rocklines = lines.Select(x => x.Split("->").ToList().Select(y => (Int32.Parse(y.Trim().Split(',')[0]), Int32.Parse(y.Trim().Split(',')[1]))).ToList());

            //1,3,5,1,2,455,6
            //var longs = lines[0].Split(',', StringSplitOptions.RemoveEmptyEntries).Select(x => Convert.ToInt64(x));

            //1
            //3
            var sum = lines.Select(y => Convert.ToInt64(y)).Select(x => x).Aggregate((x, y) => x + y);


            //Solve
            long result = Utils.GaussSum(10, 5);
            return result;
        }
    }
}
