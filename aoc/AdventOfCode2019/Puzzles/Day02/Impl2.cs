/*
 * Day 02: 1202 Program Alarm
 * https://adventofcode.com/2019/day/2
 * Part 1: 4462686
 * Part 2: 5936
 */
namespace AdventOfCode2019.Puzzles.Day02
{
    using System;
    using System.Threading.Tasks;
    using Base;

    public class Impl2 : BasePuzzle<string,int>
    {
        public Impl2() : base("Day 02: 1202 Program Alarm (IntCodeVm)", ".\\Puzzles\\Day02\\Input.txt") {}

        public override async Task<int> RunPart1Async()
            => await RunCode(12, 2);

        public override async Task<int> RunPart2Async()
        {
            for (var noun = 0; noun <= 99; noun++)
                for (var verb = 0; verb <= 99; verb++)
                    if (await RunCode(noun, verb) == 19690720)
                        return (noun * 100) + verb;

            return 0;
        }

        public async Task<int> RunCode(int noun, int verb)
        {
            return await Task.Run(() =>
            {
                var vm = new IntCodeVm(Inputs[0], null);
                vm.SetValue(1, noun);
                vm.SetValue(2, verb);
                //vm.LogAction = WriteOutput;
                var _ = vm.RunProgram();
                return vm.GetValue(0);
            });
        }

        public void WriteOutput(string arg)
            => System.Diagnostics.Debug.WriteLine(arg);
    }
}
