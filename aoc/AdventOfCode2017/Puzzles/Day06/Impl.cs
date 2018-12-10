namespace AdventOfCode2017.Puzzles.Day6
{
    using Base;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class Impl : BasePuzzle
    {
        public Impl() : base("Day 06 ", ".\\Puzzles\\Day06\\Input.txt") { }

        private int _part2;
        public override async Task<string> RunPart1()
        {
            var seenCombos = new Dictionary<string, int>();
            var list = ProcessInputs(Inputs);

            seenCombos.Add(string.Join(" ", list), 0);
            int counter = 0;
            while (true)
            {
                counter++;
                RebalanceMemory(list);
                var hash = string.Join(" ", list);
                if (seenCombos.ContainsKey(hash))
                {
                    _part2 = counter - seenCombos[hash];
                    break;
                }

                seenCombos.Add(hash, counter);
                //Console.WriteLine("State: [ {hash} ]");
            }

            return await Task.FromResult($"Found [ {string.Join(" ", list)} ] after {counter} iterations");
        }

        private void RebalanceMemory(List<int> list)
        {
            int max = list.Max(), indexOfMax = list.IndexOf(max), indexOfNext = indexOfMax;
            list[indexOfMax] = 0;            
            do {
                indexOfNext = indexOfNext == list.Count - 1 
                    ? 0 
                    : indexOfNext + 1;
                list[indexOfNext]++;
                max--;
            } while (max > 0);

        }

        public override async Task<string> RunPart2() => await Task.FromResult($"{_part2}");



        public List<int> ProcessInputs(IEnumerable<string> inputs)
        {
            var i = inputs.First();
            var arr = i.Split('\t').Select(c => int.Parse(c)).ToList();
            return arr;
        }
    }
}