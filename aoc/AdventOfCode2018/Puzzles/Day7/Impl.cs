namespace AdventOfCode2018.Puzzles.Day7
{
    using System;
    using Base;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;

    public class Impl : BasePuzzle
    {
        //public const int ELF_COUNT = 2;
        //public const int TIME_OFFSET = 0;
        //public const bool SHOW_TABLE = true;
        //public const string FILE = ".\\Puzzles\\Day7\\InputSimple.txt";
        public const int ELF_COUNT = 5;
        public const int TIME_OFFSET = 60;
        public const bool SHOW_TABLE = false;
        public const string FILE = ".\\Puzzles\\Day7\\Input.txt";


        public Impl() : base("Day 7 ", FILE) { }

        public override async Task<string> RunPart1()
        {
            var chars = ProcessInputs(Inputs);
            var graph = new Dictionary<char, Node>();
            var sb = new StringBuilder();
            
            foreach (var pair in chars)
            {
                Node current = SetupNode(pair.Item1, graph), next = SetupNode(pair.Item2, graph);
                current.Children.Add(next);
                next.Ancestors.Add(current);
            }

            var available = graph.Values.Where(a => !a.Ancestors.Any()).ToList();    //Start with the list of all available nodes where they've no ancestors

            
            while (available.Any())
            {
                var next = available
                    .OrderBy(o => o.Id)                                     //taking the first by alpha order
                    .First(f => !f.Ancestors.Any()                          //where it doesn't have any ancestors (base case) 
                                || f.Ancestors.All(a => a.Done));           //OR all it's ancestors are processed

                sb.Append(next.Id);
                next.Done = true;
                if (available.Contains(next)) available.Remove(next);

                //add all the children so we know about them
                available.AddRange(next
                    .Children
                    .Where(w => !available.Contains(w)));
            }

            return await Task.FromResult(sb.ToString());
        }

        private static Node SetupNode(char n, IDictionary<char, Node> graph)
        {
            if (!graph.ContainsKey(n))
                graph.Add(n, new Node { Id = n });
            return graph[n];
        }

        public override async Task<string> RunPart2()
        {
            var chars = ProcessInputs(Inputs);
            var graph = new Dictionary<char, Node>();

            foreach (var pair in chars)
            {
                Node current = SetupNode(pair.Item1, graph), next = SetupNode(pair.Item2, graph);
                current.Children.Add(next);
                next.Ancestors.Add(current);
            }

            var nodes = graph.Values.ToList();                                          //This time we'll check ancestors on the fly in our elf loop
            var elves = Enumerable.Range(0, ELF_COUNT).Select(s => new Elf()).ToArray();
            var timecode = 0;

            while (nodes.Any() || elves.Any(w => w.TimeCode >= timecode))               //Keep Looping while we've remained nodes or elves are still working
            {
                foreach (var e in elves)                                                //Check their current work
                    if (timecode > e.TimeCode && e.CurrentNode != null)
                    {
                        e.CurrentNode.Done = true;                                      //Update the nodes, and make the elf available
                        e.CurrentNode = null;
                        e.TimeCode = 0;
                    }
                
                var ready = nodes                                                       //Get the next set of nodes
                    .Where(n => !n.Ancestors.Any() || n.Ancestors.All(a => a.Done))
                    .ToList();

                foreach (var e in elves.Where(w => w.TimeCode == 0))                    //find available elves
                {
                    var nextNode = ready.FirstOrDefault();                              //since we're in a loop, if work is 
                    if (nextNode == null) break;                                        //no longer available, skip the remainder
                    e.CurrentNode = nextNode;
                    e.TimeCode = timecode + nextNode.DoneTime;
                    ready.Remove(nextNode);
                    nodes.Remove(nextNode);
                }

                if(SHOW_TABLE) PrintState(elves, timecode);                             //Output elf assignments to console
                timecode++;
            }

            return await Task.FromResult($"{timecode}");
        }

        private static void PrintState(IEnumerable<Elf> e, int timecode) =>
            Console.WriteLine($"{timecode:000}\t{e.ToTsv()}");


        private static IEnumerable<(char, char)> ProcessInputs(IEnumerable<string> inputs)
        {
            var r = new Regex("Step ([A-Z]) must be finished before step ([A-Z]) can begin.");

            return inputs
                .Select(s =>
                {
                    var m = r.Match(s);
                    return (m.Groups[1].Value[0], m.Groups[2].Value[0]);
                })
                .ToList();
        }

        private class Node
        {
            public char Id;
            public readonly List<Node> Children = new List<Node>();
            public readonly List<Node> Ancestors = new List<Node>();
            public bool Done;
            public int DoneTime => Id - 65 + TIME_OFFSET; //A = 65 ==> -64 offset | -1 offbyone for zero timecode | + 60 or whatever timeoffset
            public override string ToString() =>
                $"{Id} | ({Ancestors.Select(a => a.Id).ToCsv()}) | {Children.Select(c => c.Id).ToCsv()}";
        }

        private class Elf
        {
            public Node CurrentNode;
            public int TimeCode;
            public override string ToString() => $"{CurrentNode?.Id}: {TimeCode:000}";
        }
    }
}