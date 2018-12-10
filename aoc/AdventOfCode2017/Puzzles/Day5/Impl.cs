namespace AdventOfCode2017.Puzzles.Day5
{
    using Base;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class Impl : BasePuzzle
    {
        public Impl() : base("Day 5 ", ".\\Puzzles\\Day5\\Input.txt") { }

        public override async Task<string> RunPart1()
        {
            var instructions = Inputs.Select(int.Parse).ToArray();
            int currentPos = 0, jmpCount = 0;

            while (currentPos < instructions.Length && currentPos >= 0)
            {
                int jumps = instructions[currentPos];
                instructions[currentPos]++;
                currentPos += jumps;
                jmpCount++;
            }

            return await Task.FromResult($"{jmpCount}");
        }

        public override async Task<string> RunPart2()
        {
            var instructions = Inputs.Select(int.Parse).ToArray();
            int currentPos = 0, jmpCount = 0;

            while (currentPos < instructions.Length && currentPos >= 0)
            {
                int jumps = instructions[currentPos];
                instructions[currentPos] += ((jumps >= 3) ? -1 : +1);
                currentPos += jumps;
                jmpCount++;
            }

            return await Task.FromResult($"{jmpCount}");
        }
    }
}