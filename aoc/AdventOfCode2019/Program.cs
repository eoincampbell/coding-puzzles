using AdventOfCode2019.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AdventOfCode2019
{
    public class Program
    {
        public static async Task Main()
        {
            var puzzles = new List<IPuzzle>
            {
                //new Puzzles.Day01.Impl(),
                //new Puzzles.Day02.Impl(),
                //new Puzzles.Day02.Impl2(),
                //new Puzzles.Day03.Impl(),
                //new Puzzles.Day04.Impl(),
                //new Puzzles.Day04.Impl2(),
                //new Puzzles.Day04.Impl3(),
                //new Puzzles.Day05.Impl(),
                //new Puzzles.Day06.Impl(),
                //new Puzzles.Day06.Impl2(),
                //new Puzzles.Day07.Impl(),
                //new Puzzles.Day08.Impl(),
                new Puzzles.Day09.Impl(),
                //new Puzzles.Day10.Impl(),
                //new Puzzles.Day11.Impl(),
                //new Puzzles.Day12.Impl(),
                //new Puzzles.Day13.Impl(),
                //new Puzzles.Day14.Impl(),
            };

            foreach (var puzzle in puzzles) 
            {
                await puzzle.RunBothPartsAsync();
                
            }
        }
    }
}
