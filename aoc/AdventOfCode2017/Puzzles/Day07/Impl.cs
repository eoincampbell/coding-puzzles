namespace AdventOfCode2017.Puzzles.Day7
{
    using Base;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;

    public class Impl : BasePuzzle
    {
        public Impl() : base("Day 07 ", ".\\Puzzles\\Day07\\Input.txt") { }

        public override async Task<string> RunPart1()
        {
            var dict = ProcessInputs(Inputs);

            var bottom = dict.Values.Where(v => v.ParentProgram == null).First();

            return await Task.FromResult($"{bottom.ProgramName}");
        }

        public override async Task<string> RunPart2()
        {
            var dict = ProcessInputs(Inputs);

            var bottom = dict.Values.Where(v => v.ParentProgram == null).First();

            return await Task.FromResult($"");
        }
        public Dictionary<string, TowerInput> ProcessInputs(IEnumerable<string> inputs)
        {
            //tbxxc (372)
            //ippmv (21) -> btxts, daonbjx, zebuqzh, gyqrut
            Regex r = new Regex(@"(\w+) \((\d+)\)( -\> (.*))?");

            var dict = inputs
                .Select(s => r.Match(s).Groups)
                .Select(g => new TowerInput
                {
                    ProgramName = g[1].Value,
                    Weight = int.Parse(g[2].Value),
                    ChildPrograms = new List<TowerInput>()
                })
                .ToDictionary(k => k.ProgramName, v => v);

            foreach (var i in inputs)
            {
                var g = r.Match(i).Groups;
                var program = dict[g[1].Value];

                if (!string.IsNullOrWhiteSpace(g[4].Value))
                {
                    foreach(var c in g[4].Value.Split(',').Select(s => s.Trim()))
                    {
                        var childProgram = dict[c];
                        childProgram.ParentProgram = program;
                        program.ChildPrograms.Add(childProgram);
                    }
                }
            }
            
            return dict;
        }

        public class TowerInput
        {
            public TowerInput ParentProgram;
            public string ProgramName;
            public int Weight;
            public List<TowerInput> ChildPrograms;
            public int TotalWeight => Weight + (ChildPrograms.Any() ? ChildPrograms.Sum(c => c.TotalWeight) : 0);

            public override string ToString() => $"Name :{ProgramName} | TotalWeight: {TotalWeight}";
        }
    }
}