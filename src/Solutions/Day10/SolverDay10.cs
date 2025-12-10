using AOCLib;
using AOCLib.Parsing;
using Google.OrTools.LinearSolver;
using Iced.Intel;
using System;
using System.Collections;
using System.IO;
using System.Linq;
using System.Numerics;

namespace Solutions
{
    public class SolverDay10 : ISolver
    {
        public long SolvePart1(string[] lines, string text)
        {
            long result = 0;

            List<LightMachine> lightMachines = new List<LightMachine>();
            foreach (var line in lines)
            {
                lightMachines.Add(new LightMachine(line));
            }

            foreach (var lightMachine in lightMachines)
            {
                result += lightMachine.solve();
            }

            return result;
        }

        public long SolvePart2(string[] lines, string text)
        {

            long result = 0;

            List<LightMachine> lightMachines = new List<LightMachine>();
            foreach (var line in lines)
            {
                lightMachines.Add(new LightMachine(line));
            }

            foreach (var lightMachine in lightMachines)
            {
                result += lightMachine.solveP2();
            }

            return result;
        }

        public class LightMachine
        {
            List<bool> goal = new List<bool>();
            List<List<int>> buttons = new List<List<int>>();
            List<int> GoalJ = new List<int>();
            public LightMachine(string line)
            {
                var parts = line.Split(' ');
                for (int i = 1; i < parts[0].Length - 1; i++)
                {
                    if(parts[0][i] == '.')
                        goal.Add(false);
                    else if (parts[0][i] == '#')
                        goal.Add(true);
                }
                for(int i = 1; i < parts.Length - 1; i++)
                {
                    var evnts = parts[i].Substring(1, parts[i].Length - 2).Split(',');
                    List<int> evts = new();
                    foreach(var v in evnts)
                        evts.Add(int.Parse(v));
                    buttons.Add(evts);
                }
                var foos = parts[parts.Length-1].Substring(1, parts[parts.Length - 1].Length - 2).Split(',');
                foreach(var fo in foos)
                {
                    GoalJ.Add(int.Parse(fo));
                }
            }

            public long solve()
            {
                long result = 0;

                dyn.Clear();
                //tried BFS but dynamic dfs is simpler to write and fast, would be faster to create and check minimal changes to abort once the first works?
                result = DFS(0, "");

                return result;
            }


            public Dictionary<(int, string), long> dyn = new();
            public long DFS(int index, string used)
            {
                if (dyn.ContainsKey((index, used)))
                    return dyn[(index, used)];
                long result = 0;
                if (index >= this.buttons.Count)
                {
                    result = isValid(used) ? used.Where(x => x == '1').Count() : 99;
                    dyn[(index, used)] = result;
                    return result;
                }
                result = Math.Min(DFS(index + 1, used + "0"), DFS(index + 1, used + "1"));
                dyn[(index, used)] = result;
                return result;
            }

            public bool isValid(string solution)
            {
                List<bool> lights = new List<bool>();
                foreach (var g in this.goal) {
                    lights.Add(false);
                }
                for(int i = 0; i < solution.Length; i++)
                {
                    if (solution[i] == '1')
                    {
                        foreach(var b in this.buttons[i])
                        {
                            lights[b] = !lights[b];
                        }
                    }
                }
                for(int i = 0; i < lights.Count; i++)
                {
                    if(lights[i] != this.goal[i])
                        return false;
                }
                return true;
            }

            public long solveP2()

            {
                long result = 0;

                List<List<int>> inst = new List<List<int>>();

                foreach(var b in this.buttons)
                {
                    List<int> ins = new List<int>();
                    for(int i = 0; i < GoalJ.Count; i++)
                    {
                        if(b.Contains(i))
                        {
                            ins.Add(1);
                        }
                        else 
                            ins.Add(0);
                    }
                    inst.Add(ins);
                }

                int n = GoalJ.Count;
                int m = buttons.Count;

                //no way I am going to write that myself: https://developers.google.com/optimization/introduction/dotnet
                Solver solver = Solver.CreateSolver("SCIP");

                Variable[] x = new Variable[m];
                for (int i = 0; i < m; i++)
                {
                    //min uses, max uses, name
                    x[i] = solver.MakeIntVar(0.0, double.PositiveInfinity, $"x{i}");
                }

                for (int j = 0; j < n; j++)
                {
                    LinearExpr sum = new LinearExpr();
                    for (int i = 0; i < m; i++)
                    {
                        sum += inst[i][j] * x[i];
                    }
                    solver.Add(sum == GoalJ[j]);
                }

                LinearExpr objective = new LinearExpr();
                for (int i = 0; i < m; i++)
                {
                    objective += x[i];
                }
                solver.Minimize(objective);

                Solver.ResultStatus resultStatus = solver.Solve();

                for (int i = 0; i < m; i++)
                {
                    if (x[i].SolutionValue() > 0)
                    {
                        //Console.WriteLine($"{i} x {x[i].SolutionValue()}");
                        result += (long)x[i].SolutionValue();
                    }
                }

                return result;
            }
        }
    }
}
