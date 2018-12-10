namespace AdventOfCode2017.Puzzles.Day2
{
    using Base;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class Impl : BasePuzzle
    {
        public Impl() : base("Day 02 ", ".\\Puzzles\\Day02\\Input.txt") { }

        public override async Task<string> RunPart1()
        {
            var sum = Inputs
                .Select(s => s.Split('\t').Select(int.Parse))
                .Select(arr => arr.Max() - arr.Min())
                .Sum();
            
            return await Task.FromResult($"{sum}");
        }

        public override async Task<string> RunPart2()
        {
            var sum = 0;
           
            foreach(var i in Inputs)
            {
                var arr = i.Split('\t').Select(int.Parse);
                foreach(var a in arr)
                    foreach (var b in arr)
                        if(a > b && a % b == 0)
                        {
                            sum += (a / b);
                        }
            }

            return await Task.FromResult($"{sum}");
        }
    }
}