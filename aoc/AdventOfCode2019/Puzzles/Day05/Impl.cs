namespace AdventOfCode2019.Puzzles.Day05
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Base;

    public class Impl : BasePuzzle<string, int>
    {
        public Impl() : base("Day 05: ", ".\\Puzzles\\Day05\\Input.txt") { }

        public override async Task<int> RunPart1Async()
        {
            var vm = new IntCodeVM(Inputs[0]);
            return vm.RunProgram(1).Last(); //16225258
        }

        public override async Task<int> RunPart2Async()
        { 
            var vm = new IntCodeVM(Inputs[0]);
            return vm.RunProgram(5).Last(); //2808771
        }
    }

    public class IntCodeVM
    {
        private readonly int[] _tape;
        private int _pointer;

        public IntCodeVM(string tape)
        {
            _tape = Array.ConvertAll(tape.Split(','), int.Parse);
            _pointer = 0;
        }

        public IEnumerable<int> RunProgram(int input)
        {
            var halt = false;
            while (!halt)
            {
                var result = ProcessCommand(input, out halt);
                foreach (var v in result) yield return v;
            }
        }

        private int GetValue(int offset, int mode) =>
            (mode == 0) ? _tape[_tape[_pointer + offset]] : _tape[_pointer + offset];

        private void SetValue(int offset, int mode, int value) => 
            _tape[ (mode == 0) ? _tape[_pointer + offset] : _pointer + offset] = value;
            
        private void AdvancePointer(int advance) => 
            _pointer += advance;

        private void SetPointer(int newPointer) => 
            _pointer = newPointer;

        private int[] ProcessCommand(int input, out bool halt)
        {
            halt = false;
            var outputs = new List<int>();
            var poc = _tape[_pointer];
            var opCode = poc % 100;
            var p1mode = poc / 100 % 10;
            var p2mode = poc / 100 / 10 % 10;
            var p3mode = poc / 100 / 100 % 10;

            var p1 = opCode switch
            {
                int i when (new[] {1, 2, 4, 5, 6, 7, 8}).Contains(i) => GetValue(1, p1mode),
                _ => 0
            };
            var p2 = opCode switch
            {
                int i when (new[] {1, 2, 5, 6, 7, 8}).Contains(i) => GetValue(2, p2mode),
                _ => 0
            };

            switch (opCode)
            {
                case 1:
                    SetValue(3, p3mode, p1 + p2);
                    AdvancePointer(4);
                    break;
                case 2:
                    SetValue(3, p3mode, p1 * p2);
                    AdvancePointer(4);
                    break;
                case 3:
                    SetValue(1, p1mode, input); ////THIS MIGHT BE A BUG CHECK LATER - not sure mode should come into it.
                    AdvancePointer(2);
                    break;
                case 4:
                    outputs.Add(p1);
                    AdvancePointer(2);
                    break;
                case 5:
                    if (p1 != 0) SetPointer(p2);
                    else AdvancePointer(3);
                    break;
                case 6:
                    if (p1 == 0) SetPointer(p2);
                    else AdvancePointer(3);
                    break;
                case 7:
                    SetValue(3, p3mode, (p1 < p2) ? 1 : 0);
                    AdvancePointer(4);
                    break;
                case 8:
                    SetValue(3, p3mode, (p1 == p2) ? 1 : 0);
                    AdvancePointer(4);
                    break;
                case 99:
                    halt = true;
                    break;
            }

            return outputs.ToArray();
        }
    }
}
