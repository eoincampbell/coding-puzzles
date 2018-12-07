namespace AdventOfCode2018.Puzzles.Day7
{
    using Base;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;

    public class Impl : BasePuzzle
    {
        public Impl() : base("Day 7 ", ".\\Puzzles\\Day7\\Input.txt") { }

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

            //keep recursively processing remaining known nodes
            while (available.Any())
            {
                var next = available
                    .OrderBy(o => o.Id)                                      //taking the first by alpha order
                    .First(f => !f.Ancestors.Any()                          //where it doesn't have any ancestors (base case) 
                                || f.Ancestors.All(a => a.PassedThrough));      //OR all it's ancestors are processed

                sb.Append(next.Id);
                next.PassedThrough = true;
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
            var sb = new StringBuilder();
            var available = new List<Node>();

            foreach (var pair in chars)
            {
                Node current = SetupNode(pair.Item1, graph),
                    next = SetupNode(pair.Item2, graph);
                current.Children.Add(next);
                next.Ancestors.Add(current);
            }

            available.AddRange(graph.Values.Where(a => !a.Ancestors.Any()));    //Start with the list of all available nodes where they've no ancestors
            var first = available.OrderBy(o => o.Id).First();                   //Take the first available
            //sb = Walk2(first, available, sb);                                    //Recursively walk the list
            return await Task.FromResult(sb.ToString());
        }

        private IEnumerable<(char, char)> ProcessInputs(IEnumerable<string> inputs)
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
            public bool PassedThrough;

            public override string ToString() =>
                $"{Id} | ({Ancestors.Select(a => a.Id).ToCsv()}) | {Children.Select(c => c.Id).ToCsv()}";
        }

        
    }
}