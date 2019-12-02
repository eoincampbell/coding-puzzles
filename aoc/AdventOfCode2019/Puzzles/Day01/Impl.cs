using AdventOfCode2019.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2019.Puzzles.Day01
{
    public class Impl: BasePuzzle<int, int>
    {
        public Impl() : base("Day 1 ", ".\\Puzzles\\Day01\\Input.txt") { }
        
        //3481005
        public override async Task<int> RunPart1Async() 
            => Inputs.Select(CalcFuel).Sum();

        //5218616
        public override async Task<int> RunPart2Async() 
            => Inputs.Select(CalcFuelForFuel).Sum();

        private static int CalcFuel(int mass) 
            => (mass / 3) - 2;

        private static int CalcFuelForFuel(int mass) 
            => CalcFuel(mass) > 0 
                ? CalcFuel(mass) + CalcFuelForFuel(CalcFuel(mass)) 
                : 0;
    }
}
