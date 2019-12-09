/*
 * Day 09: Sensor Boost
 * https://adventofcode.com/2019/day/9
 * Part 1: 2465411646
 * Part 2: 69781
 */
namespace AdventOfCode2019.Puzzles.Day09
{
    using System.Linq;
    using System.Threading.Tasks;
    using Base;
    
    public class Impl : Puzzle<string, long>
    {
        public Impl() : base("Day 09: ", ".\\Puzzles\\Day09\\Input.txt") { }

        public override async Task<long> RunPart1Async()
        {
            return await Task.Run(() => RunVm(Inputs[0], 1));
        }

        public override async Task<long> RunPart2Async()
        {
            return await Task.Run(() => RunVm(Inputs[0], 2));
        }
        
        private static long RunVm(string tape, int input)
        {
            var vm = new IntCodeVm(tape);
            vm.AddInput(input);
            vm.RunProgram();
            var results = vm.GetOutputs().ToList();
            return results.Last();
        }

    }
}