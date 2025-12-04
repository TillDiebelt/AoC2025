using System;
using System.IO;
using System.Linq;
using AOCLib;
using AOCLib.Parsing;

namespace Solutions
{
    public class SolverDay04 : ISolver
    {
        public long SolvePart1(string[] lines, string text)
        {
            long result = 0;

            int[,] rolls = new int[lines.Length, lines[0].Length];
            for(int i = 0; i < lines.Length; i++)
            {
                for(int j = 0; j < lines[i].Length; j++)
                {
                    if (lines[i][j] == '@')
                    {
                        rolls[i, j] = 1;
                    }
                    else
                    {
                        rolls[i, j] = 0;
                    }
                }
            }

            for(int i = 0; i < lines.Length; i++)
            {
                for(int j = 0; j < lines[i].Length; j++)
                {
                    var neighs = rolls.NeighboursDiag(j, i);
                    if (neighs.Where(x => rolls[x.y, x.x] == 1).Count() < 4 && rolls[i,j] == 1)
                    {
                        result++;
                    }
                }
            }

            return result;
        }

        public long SolvePart2(string[] lines, string text)
        {
            long result = 0;

            int[,] rolls = new int[lines.Length, lines[0].Length];
            for (int i = 0; i < lines.Length; i++)
            {
                for (int j = 0; j < lines[i].Length; j++)
                {
                    if (lines[i][j] == '@')
                    {
                        rolls[i, j] = 1;
                    }
                    else
                    {
                        rolls[i, j] = 0;
                    }
                }
            }

            int change = 1;
            int[,] removed = new int[lines.Length, lines[0].Length];
            while (change > 0)
            {
                change = 0;
                for (int i = 0; i < lines.Length; i++)
                {
                    for (int j = 0; j < lines[i].Length; j++)
                    {
                        var neighs = rolls.NeighboursDiag(j, i);
                        if (neighs.Where(x => rolls[x.y, x.x] == 1).Count() < 4 && rolls[i, j] == 1)
                        {
                            result++;
                            removed[i, j] = 1;
                            change++;
                        }
                    }
                }
                for (int i = 0; i < lines.Length; i++)
                {
                    for (int j = 0; j < lines[i].Length; j++)
                    {
                        if (removed[i, j] == 1)
                        {
                            rolls[i, j] = 0;
                        }
                    }
                }
            }


            return result;
        }
    }
}

