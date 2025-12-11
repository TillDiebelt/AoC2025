using System;
using System.IO;
using System.Linq;
using AOCLib;

namespace Solutions
{
    public class SolverDay11 : ISolver
    {
        public long SolvePart1(string[] lines, string text)
        {
            long result = 0;

            List<Node> nodes = new List<Node>();
            foreach (var line in lines)
            {
                nodes.Add(new Node(line));
            }

            Node start = null;
            foreach (var node in nodes)
            {
                foreach(var o in node.outs)
                {
                    foreach(var n in nodes)
                    {
                        if(n.id == o)
                        {
                            node.outNodes.Add(n);
                            break;
                        }
                    }
                }
                if(node.id == "you")
                    start = node;
            }

            result = DFS(new List<Node>(), start);

            return result;
        }

        public static long DFS(List<Node> path, Node current)
        {
            long result = 0;
            foreach (var n in current.outs)
            {
                if (n == "out")
                    result += 1;
            }
            foreach (var node in current.outNodes)
            {
                if (path.Contains(node))
                    continue;
                List<Node> copy = new List<Node>();
                copy.Add(current);
                result += DFS(copy, node);
            }
            return result;
        }

        public Dictionary<(string, bool, bool), long> dyn = new();
        public long DFS2(List<Node> path, Node current, bool dac = false, bool fft = false)
        {
            if(dyn.ContainsKey((current.id, dac, fft)))
            {
                return dyn[(current.id, dac, fft)];
            }
            long result = 0;
            if (current.id == "fft")
                fft = true;
            if(current.id == "dac")
                dac = true;
            foreach (var n in current.outs)
            {
                if (n == "out")
                    result += dac && fft ? 1 : 0;
            }
            foreach (var node in current.outNodes)
            {
                if (path.Contains(node))
                    continue;
                List<Node> copy = new List<Node>();
                copy.Add(current);
                result += DFS2(copy, node, dac, fft);
            }
            dyn.TryAdd((current.id, dac, fft), result);
            return result;
        }

        public long SolvePart2(string[] lines, string text)
        {
            long result = 0;

            List<Node> nodes = new List<Node>();
            foreach (var line in lines)
            {
                nodes.Add(new Node(line));
            }

            Node start = null;
            foreach (var node in nodes)
            {
                foreach (var o in node.outs)
                {
                    foreach (var n in nodes)
                    {
                        if (n.id == o)
                        {
                            node.outNodes.Add(n);
                            break;
                        }
                    }
                }
                if (node.id == "svr")
                    start = node;
            }

            dyn.Clear();
            result = DFS2(new List<Node>(), start);

            return result;
        }
    }

    public class Node
    {
        public string id;
        public List<string> outs = new List<string>();
        public List<Node> outNodes = new List<Node>();

        public Node(string line)
        {
            var split = line.Split(' ');
            id = split[0].Substring(0, split[0].Length-1);
            for (int i = 1; i < split.Length; i++)
            {
                outs.Add(split[i]);
            }
        }
    }
}
