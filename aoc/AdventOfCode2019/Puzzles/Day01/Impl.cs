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
        
        public override Task<int> RunPart1Async()
        {
            var fuel = Inputs.Select(Fuel).Sum();
            return Task.FromResult(fuel);
        }

        //5218616
        public override Task<int> RunPart2Async()
        {
            var fuel = Inputs.Select(FuelForFuel).Sum();
            return Task.FromResult(fuel);
        }

        public int Fuel(int x) => (x / 3) - 2;

        public int FuelForFuel(int x) => Fuel(x) > 0 ? Fuel(x) + FuelForFuel(Fuel(x)) : 0;


        //Scribbles
        //int total, tFuel, mFuel;
        //total = Inputs.Select(s => 
        //{
        //    tFuel = mFuel = (s / 3) - 2;
        //    while (mFuel > 0)
        //    {
        //        mFuel = (mFuel / 3) - 2;
        //        tFuel += (mFuel >= 0) ? mFuel : 0;
        //    }
        //    return tFuel;
        //}).Sum();
    }
}
