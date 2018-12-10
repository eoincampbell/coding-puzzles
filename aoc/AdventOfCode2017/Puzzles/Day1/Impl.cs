namespace AdventOfCode2017.Puzzles.Day1
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
            var numbers = Inputs.First().ToCharArray().Select(c => c - 48).ToArray();
            var sum = 0;
            for (int i = 1; i <= numbers.Length; i++)
            { 
                if(numbers[i-1] == numbers[i%numbers.Length])
                {
                    sum += numbers[i - 1];
                }
            }

            return await Task.FromResult($"{sum}");
        }

        public override async Task<string> RunPart2()
        {
            var numbers = Inputs.First().ToCharArray().Select(c => c - 48).ToArray();
            var sum = 0;
            for (int i = 1; i <= numbers.Length; i++)
            {
                if (numbers[i - 1] == numbers[(i+(numbers.Length/2)-1) % numbers.Length])
                {
                    sum += numbers[i - 1];
                }
            }

            return await Task.FromResult($"{sum}");
        }
    }
}