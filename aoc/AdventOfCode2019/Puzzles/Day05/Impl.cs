namespace AdventOfCode2019.Puzzles.Day05
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Base;
    using CommandDict = System.Collections.Generic.Dictionary<int, (int inputCount, int movePointerForward, System.Action<int[], int[]> command)>;

    public class Impl : BasePuzzle<string, int>
    {
        public Impl() : base("Day 05: ", ".\\Puzzles\\Day05\\Input.txt") { }

        public override async Task<int> RunPart1Async() 
            => RunVm(Inputs[0], 1); //16225258
        
        public override async Task<int> RunPart2Async() 
            => RunVm(Inputs[0], 5); //2808771
        
        private static int RunVm(string tape, int input) 
            => (new IntCodeVm(tape, input)).RunProgram().Last();
    }

    public class IntCodeVm
    {
        private bool _halt;
        private int _pointer;
        private readonly int _input;
        private readonly int[] _tape;
        private readonly List<int> _outputs = new List<int>();
        private readonly CommandDict _commands;

        public IntCodeVm(string tape, int input)
        {
            _tape = Array.ConvertAll(tape.Split(','), int.Parse);
            _input = input;
            _commands = new CommandDict
            {
                {01, (inputCount: 2, movePointerForward: 4, Add)},
                {02, (inputCount: 2, movePointerForward: 4, Mul)},
                {03, (inputCount: 0, movePointerForward: 2, Inp)},
                {04, (inputCount: 1, movePointerForward: 2, Out)},
                {05, (inputCount: 2, movePointerForward: 0, Jit)},
                {06, (inputCount: 2, movePointerForward: 0, Jif)},
                {07, (inputCount: 2, movePointerForward: 4, Les)},
                {08, (inputCount: 2, movePointerForward: 4, Equ)},
                {99, (inputCount: 0, movePointerForward: 1, Hlt)}
            };
        }

        public IEnumerable<int> RunProgram()
        {
            while (!_halt)
            {
                var poc = _tape[_pointer];
                var opCode = poc % 100;
                var modes = GetModes(poc);
                var (inputCount, movePtrForward, command) = _commands[opCode];
                command(modes, GetParams(inputCount, modes));
                _pointer += movePtrForward;
            }

            return _outputs;
        }

        private static int[] GetModes(int poc) 
            => new []
            {
                poc / 100 % 10,      //p1 
                poc / 100 / 10 % 10, //p2 
                poc / 100 / 100 % 10 //p3
            };

        private int [] GetParams(int count, int[] modes) 
            => Enumerable
                .Range(1, count)
                .Select(i => GetValue(i, modes[i - 1]))
                .ToArray();

        private int GetValue(int offset, int mode) 
            => (mode == 0) 
                ? _tape[_tape[_pointer + offset]] 
                : _tape[_pointer + offset];

        private void SetValue(int offset, int[] modes, int value)
        {
            var idx = (modes[offset - 1] == 0)
                ? _tape[_pointer + offset]
                : _pointer + offset;
            _tape[idx] = value;
        }

        #region command implementations 

        private void Add(int[] modes, int[] p) => SetValue(3, modes, p[0]+p[1]);

        private void Mul(int[] modes, int[] p) => SetValue(3, modes, p[0]*p[1]);

        private void Inp(int[] modes, int[] p) => SetValue(1, modes, _input);

        private void Out(int[] modes, int[] p) => _outputs.Add(p[0]);
        
        private void Jit(int[] modes, int[] p) 
            => _pointer = (p[0] != 0) 
                ? p[1] 
                : _pointer + 3;

        private void Jif(int[] modes, int[] p) 
            => _pointer = (p[0] == 0) 
                ? p[1] 
                : _pointer + 3;
        
        private void Les(int[] modes, int[] p) 
            => SetValue(3, modes, (p[0] < p[1]) ? 1 : 0);
        
        private void Equ(int[] modes, int[] p) 
            => SetValue(3, modes, (p[0] == p[1]) ? 1 : 0);

        private void Hlt(int[] modes, int[] p) => _halt = true;

        #endregion 
    }
}