/*
 * Day 05: Sunny with a Chance of Asteroids
 * https://adventofcode.com/2019/day/5
 * Part 1: 16225258
 * Part 2: 2808771
 */
namespace AdventOfCode2019.Puzzles.Day05
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Base;

    public class Impl : Puzzle<string, int>
    {
        public Impl() : base("Day 05:  Sunny with a Chance of Asteroids", ".\\Puzzles\\Day05\\Input.txt") { }

        public override async Task<int> RunPart1Async() 
            => await RunVm(Inputs[0], 1); 
        
        public override async Task<int> RunPart2Async() 
            => await RunVm(Inputs[0], 5);

        private async Task<int> RunVm(string tape, int input)
        {
            return await Task.Run(() =>
            {
                var vm = new IntCodeVm(tape);
                vm.AddInput(input);
                vm.RunProgram();

                return (int)vm.GetOutputs().Last();

            });
        }

        public void WriteOutput(string arg)
            => Console.WriteLine(arg);
    }
}