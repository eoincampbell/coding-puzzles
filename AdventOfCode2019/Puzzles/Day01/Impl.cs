using AdventOfCode2019.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2019.Puzzles.Day01
{
    public class Impl : BasePuzzle
    {
        public Impl() : base("Day 1 ", ".\\Puzzles\\Day01\\Input.txt") { }
        
        public override Task<string> RunPart1Async()
        {
            var fuel = Inputs.Select(s => (Convert.ToInt32(s) / 3) - 2).Sum();

            return Task.FromResult(fuel.ToString());
        }

        public override Task<string> RunPart2Async()
        {
            var totalFuel = Inputs.Select(s =>
            {
                var mFuel = (Convert.ToInt32(s) / 3) - 2;
                var tFule = mFuel;

                while (mFuel > 0)
                {
                    mFuel = (mFuel / 3) - 2;
                    tFule += (mFuel >= 0) ? mFuel : 0;
                }

                return tFule;

            }).Sum();

            return Task.FromResult(totalFuel.ToString());
        }
    }
}
