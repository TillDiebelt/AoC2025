using System;
using System.IO;
using System.Linq;
using System.Numerics;
using AOCLib;
using AOCLib.Parsing;

namespace Solutions
{
    public class SolverDay06 : ISolver
    {
        public long SolvePart1(string[] lines, string text)
        {
            long result = 0;

            List<List<long>> Lists = new List<List<long>>();
            for(int i = 0; i < lines.Length - 1; i++)
            {
                List<long> list = lines[i].GetNumbers();
                Lists.Add(list);
            }

            int problem = 0;
            int index = 0;
            while(index < lines[lines.Length-1].Length)
            {
                if(lines[lines.Length - 1][index] == '+')
                {
                    long res2 = 0;
                    for (int i = 0; i < Lists.Count; i++)
                    {
                        res2 += Lists[i][problem];
                    }
                    problem++;
                    result += res2;
                }

                if (lines[lines.Length - 1][index] == '*')
                {
                    long res2 = 1;
                    for (int i = 0; i < Lists.Count; i++)
                    {
                        res2 *= Lists[i][problem];
                    }
                    problem++;
                    result += res2;

                }

                index++;
            }

            return result;
        }

        public long SolvePart2(string[] lines, string text)
        {
            long result = 0;

            //after I was half way done parsing this mess I thought it would be easier to just rotate the input by 90° left but then I wanted to see if this works so here we are
            int index = 0;
            bool wasBreak = false;
            List<Problem> problems = new List<Problem>();
            Problem? currentProblem = new Problem();
            currentProblem.op = lines[lines.Length - 1][0];

            for (int i = 0; i < lines.Length - 1; i++)
            {
                currentProblem.numbers.Add(new List<int>());
            }
            while (index < lines[0].Length)
            {
                bool anyNum = false;
                for (int i = 0; i < lines.Length - 1; i++)
                {
                    anyNum |= lines[i][index] != ' ';
                }
                if (anyNum && wasBreak)
                {
                    wasBreak = false;
                    if(currentProblem != null)
                    {
                        foreach(var f in currentProblem.numbers)
                        {
                            f.RemoveAt(f.Count - 1);
                        }
                        problems.Add(currentProblem);
                    }

                    currentProblem = new Problem();
                    currentProblem.op = lines[lines.Length - 1][index];

                    for (int i = 0; i < lines.Length - 1; i++)
                    {
                        currentProblem.numbers.Add(new List<int>());
                    }
                }
                else if(lines[0][index] == ' ')
                {
                    wasBreak = true;
                    for(int i = 0;i < lines.Length - 1;i++)
                    {
                        wasBreak &= lines[i][index] == ' ';
                    }
                }
                for (int i = 0; i < lines.Length - 1; i++)
                {
                    if (lines[i][index] != ' ')
                    {
                        currentProblem.numbers[i].Add(int.Parse(lines[i][index].ToString()));
                    }
                    else
                        currentProblem.numbers[i].Add(99);

                }
                index++;
            }
            problems.Add(currentProblem);

            foreach (var problem in problems)
            {
                result += problem.calculate();
            }

            return result;
        }

        public class Problem
        {
            public List<List<int>> numbers = new List<List<int>>();
            public char op = '+';

            public long calculate()
            {
                long result = 0;
                List<long> nums = new();
                for (int i = 0; i < numbers[0].Count; i++)
                {
                    long tmp = 0;
                    long mul = 1;
                    for (int j = numbers.Count - 1; j >= 0; j--)
                    {
                        var tmp2 = numbers[j][i];
                        if(tmp2 != 99) //99 is empty space / not part of number "  9"/"9  "
                        {
                            tmp += tmp2 * mul;
                            mul *= 10;
                        }
                    }
                    if (tmp != 0)
                        nums.Add(tmp);
                }
                if (op == '+')
                {
                    long res2 = 0;
                    foreach (var num in nums)
                    {
                        res2 += num;
                    }
                    result += res2;
                }
                else
                {
                    long res2 = 1;
                    foreach (var num in nums)
                    {
                        res2 *= num;
                    }
                    result += res2;
                }
                return result;
            }
        }
    }
}
