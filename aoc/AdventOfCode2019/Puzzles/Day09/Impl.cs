/*
 * Day 09: Sensor Boost
 * https://adventofcode.com/2019/day/9
 * Part 1: 2465411646
 * Part 2: 69781
 */
namespace AdventOfCode2019.Puzzles.Day09
{
    using System.Linq;
    using System.Numerics;
    using System.Threading.Tasks;
    using Base;
    using Base.IntCode;

    public class Impl : Puzzle<string, BigInteger>
    {
        public Impl() : base("Day 09: Sensor Boost", ".\\Puzzles\\Day09\\Input.txt") { }

        public override async Task<BigInteger> RunPart1Async() => await RunVm(Inputs[0], 1);

        public override async Task<BigInteger> RunPart2Async() => await RunVm(Inputs[0], 2);

        private static async Task<BigInteger> RunVm(string tape, int input) => await Task.Run(() =>
        {
            var vm = new IntCodeVm(tape);
            vm.SetInput(input);
            vm.RunProgramUntilHalt();
            var results = vm.GetOutputs().ToList();
            return results.Last();
        });
    }
}