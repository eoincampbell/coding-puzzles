namespace AdventOfCode2018.Puzzles.Day1
{
    using Base;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class Impl : BasePuzzle
    {
        public Impl() : base("Day 1 ", ".\\Puzzles\\Day1\\Input.txt") { }

        public override async Task<string> RunPart1()
        {
            return await Task.FromResult(Inputs.Sum(int.Parse).ToString());
        }

        public override async Task<string> RunPart2()
        {
            int counter = 0, i = 0;
            var inputs = Inputs.Select(int.Parse).ToList();
            var occurenceTracker = new Dictionary<int, int> {{0, 1}};
            
            while (true)
            {
                counter += inputs[i];

                if (occurenceTracker.ContainsKey(counter))
                    return await Task.FromResult($"{counter} was received twice.");
                
                occurenceTracker.Add(counter, 1);
                
                if (inputs.Count == ++i) i = 0;
            }
        }
    }
}