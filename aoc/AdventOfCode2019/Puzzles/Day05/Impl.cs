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
    using System.Numerics;
    using System.Threading.Tasks;
    using Base;
    using Base.IntCode;

    public class Impl : Puzzle<string, BigInteger>
    {
        public Impl() : base("Day 05: Sunny with a Chance of Asteroids", ".\\Puzzles\\Day05\\Input.txt") { }

        public override async Task<BigInteger> RunPart1Async() 
            => await RunVm(Inputs[0], 1); 
        
        public override async Task<BigInteger> RunPart2Async() 
            => await RunVm(Inputs[0], 5);

        private async Task<BigInteger> RunVm(string tape, int input)
        {
            return await Task.Run(() =>
            {
                var vm = new IntCodeVm(tape);
                vm.SetInput(input);
                vm.RunProgram();

                return vm.GetOutputs().Last();

            });
        }

        public void WriteOutput(string arg)
            => Console.WriteLine(arg);
    }
}