using System;
using System.IO;
using System.Linq;
using AOCLib;

namespace Solutions
{
    public class SolverDay01 : ISolver
    {
        public long SolvePart1(string[] lines, string text)
        {
            List<(string, int)> rotations = new();
            foreach (var line in lines)
            {
                rotations.Add((line[0].ToString(), int.Parse(line.Substring(1))));
            }

            long result = 0;
            int angle = 50;
            foreach (var rotation in rotations)
            {
                if(rotation.Item1 == "R")
                {
                    angle += rotation.Item2;
                }
                else
                    angle -= rotation.Item2;
                angle = (angle + 100) % 100;
                if (angle == 0)
                {
                    result++;
                }
            }

            return result;
        }

        public long SolvePart2(string[] lines, string text)
        {
            List<(string, int)> rotations = new();
            foreach (var line in lines)
            {
                rotations.Add((line[0].ToString(), int.Parse(line.Substring(1))));
            }

            long result = 0;
            int angle = 50;
            foreach (var rotation in rotations)
            {
                for(int i = 0; i < rotation.Item2; i++)
                {
                    if (rotation.Item1 == "R")
                    {
                        angle++;
                    }
                    else
                        angle--;
                    angle = (angle + 100) % 100;
                    if (angle == 0)
                    {
                        result++;
                    }
                }
            }

            return result;
        }
    }
}
