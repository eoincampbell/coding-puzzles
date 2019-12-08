/*
 * Day 1: The Tyranny of the Rocket Equation
 * https://adventofcode.com/2019/day/1
 * Part 1: 3481005
 * Part 2: 5218616
 */

namespace AdventOfCode2019.Puzzles.Day01
{
    using Base;
    using System.Linq;
    using System.Threading.Tasks;

    public class Impl: Puzzle<int, int>
    {
        public Impl() : base("Day 01: The Tyranny of the Rocket Equation", ".\\Puzzles\\Day01\\Input.txt") { }

        public override async Task<int> RunPart1Async()
            => await Task.Run(() => Inputs.Select(CalcFuel).Sum());

        public override async Task<int> RunPart2Async() 
            => await Task.Run(() => Inputs.Select(CalcFuelForFuel).Sum());

        private static int CalcFuel(int mass) 
            => (mass / 3) - 2;

        private static int CalcFuelForFuel(int mass) 
            => CalcFuel(mass) > 0 
                ? CalcFuel(mass) + CalcFuelForFuel(CalcFuel(mass)) 
                : 0;
    }
}
