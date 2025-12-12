using System;
using System.IO;
using System.Linq;
using AOCLib;

namespace Solutions
{
    public class SolverDay12 : ISolver
    {
        public long SolvePart1(string[] lines, string text)
        {
            long result = 0;

            List<bool[,]> shapes = new List<bool[,]>();
            List<(int, int, List<int>)> problem = new();

            for (int i = 0; i < lines.Length; i++)
            {
                if(lines[i] == "")
                    continue;
                if(shapes.Count < 6)
                {
                    bool[,] shape = new bool[3,3];
                    for(int j = 1;j < 4; j++)
                    {
                        for (int x = 0; x < 3; x++)
                        {
                            shape[j - 1, x] = lines[i + j][x] == '#';
                        }
                    }
                    shapes.Add(shape);
                    i += 4;
                    continue;
                }
                var split = lines[i].Split(":");
                var area = split[0].GetNumbers();
                var fits = split[1].GetNumbers().Select(x => (int)x).ToList();
                problem.Add(((int)area[0], (int)area[1], fits));
            }

            List<List<bool[,]>> variants = new();

            foreach(var s in shapes)
            {
                List<bool[,]> rnf = new List<bool[,]>();
                rnf.Add(copy(s));
                rnf.Add(rotateFlipHorizontal(copy(s)));
                var current = s;
                for (int i = 0; i < 3; i++)
                {
                    current = rotateRight(current);
                    rnf.Add(rotateFlipHorizontal(copy(current)));
                    rnf.Add(copy(current));
                }
                variants.Add(rnf);
            }

            foreach(var p in problem)
            {
                result += DoesFit(variants, p) ? 1 : 0;
            }

            return result;
        }

        public static bool DoesFit(List<List<bool[,]>> variants, (int x, int y, List<int>) problem)
        {
            bool fits = false;

            bool[,] map = new bool[problem.y, problem.x];

            //just bin package, easy lul
            
            return fits;
        }

        public bool[,] rotateRight(bool[,] shape)
        {
            bool[,] newShape = new bool[3, 3];
            newShape[0, 0] = shape[2,0];
            newShape[0, 1] = shape[1, 0];
            newShape[1, 1] = shape[1, 1];
            newShape[0,2] = shape[0, 0];
            newShape[1, 0] = shape[2, 1];
            newShape[2, 0] = shape[2, 2];
            newShape[2, 1] = shape[1, 2];
            newShape[2, 2] = shape[0, 2];
            newShape[1, 2] = shape[0, 1];
            return newShape;
        }
        public bool[,] copy(bool[,] shape)
        {
            bool[,] newShape = new bool[3, 3];
            for (int i = 0; i < shape.GetLength(0); i++)
            {
                for (int j = 0; j < shape.GetLength(1); j++)
                {
                    newShape[i, j] = shape[i, j];
                }
            }
            return newShape;
        }
        public bool[,] rotateFlipVertical(bool[,] shape)
        {
            bool[,] newShape = new bool[3, 3];
            for (int i = 0; i < shape.GetLength(0); i++)
            {
                for (int j = 0; j < shape.GetLength(1); j++)
                {
                    newShape[i, j] = shape[i, 2 - j];
                }
            }
            return newShape;
        }
        public bool[,] rotateFlipHorizontal(bool[,] shape)
        {
            bool[,] newShape = new bool[3, 3];
            for (int i = 0; i < shape.GetLength(0); i++)
            {
                for (int j = 0; j < shape.GetLength(1); j++)
                {
                    newShape[i, j] = shape[2 - i, j];
                }
            }
            return newShape;
        }

        public long SolvePart2(string[] lines, string text)
        {
            long result = 0;

            return result;
        }
    }
}
