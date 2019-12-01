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
        
        public override Task<int> RunPart1Async()
        {
            var fuel = Inputs.Select(s => (s / 3) - 2).Sum();
            return Task.FromResult(fuel);
        }

        public override Task<int> RunPart2Async()
        {
            int total, tFuel, mFuel;
            total = Inputs.Select(s => 
            {
                tFuel = mFuel = (s / 3) - 2;
                while (mFuel > 0)
                {
                    mFuel = (mFuel / 3) - 2;
                    tFuel += (mFuel >= 0) ? mFuel : 0;
                }
                return tFuel;
            }).Sum();

            return Task.FromResult(total);
        }
    }
}
