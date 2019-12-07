namespace AdventOfCode2019.Base
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using CommandDict = System.Collections.Generic.Dictionary<int, (int inputCount, int movePointerForward, System.Action<Mode[], int[]> command)>;

    public enum Mode
    {
        Position = 0,
        Immediate = 1
    }

    public class IntCodeVm
    {
        private readonly Queue<int> _inputs;
        private readonly int[] _tape;
        private int _currentOutput;
        private readonly List<int> _outputs = new List<int>();
        private readonly CommandDict _commands;

        public bool IsHalted { get; private set; }
        public bool Pause { get; set; }
        public int Pointer { get; private set; }

        public IntCodeVm(string tape, Queue<int> inputs)
            : this(Array.ConvertAll(tape.Split(','), int.Parse), inputs) { }

        public IntCodeVm(int [] tape, Queue<int> inputs)
        {
            _tape = tape;
            _inputs = inputs;
            _commands = new CommandDict
            {
                {01, (inputCount: 2, movePointerForward: 4, Add)},
                {02, (inputCount: 2, movePointerForward: 4, Mul)},
                {03, (inputCount: 0, movePointerForward: 2, Input)},
                {04, (inputCount: 1, movePointerForward: 2, Output)},
                {05, (inputCount: 2, movePointerForward: 0, JumpIfTrue)},
                {06, (inputCount: 2, movePointerForward: 0, JumpIfFalse)},
                {07, (inputCount: 2, movePointerForward: 4, LessThan)},
                {08, (inputCount: 2, movePointerForward: 4, EqualTo)},
                {99, (inputCount: 0, movePointerForward: 1, Halt)}
            };
        }

        public IEnumerable<int> RunProgram()
        {
            while (!IsHalted)
            {
                var poc = _tape[Pointer];
                var opCode = poc % 100;
                var modes = GetModes(poc);
                var (inputCount, movePtrForward, command) = _commands[opCode];
                command(modes, GetParams(inputCount, modes));
                Pointer += movePtrForward;
            }
            return _outputs;
        }

        public int RunProgramPauseAtOutput()
        {
            Pause = false;
            while (!Pause && !IsHalted)
            {
                var poc = _tape[Pointer];
                var opCode = poc % 100;
                var modes = GetModes(poc);
                var (inputCount, movePtrForward, command) = _commands[opCode];
                command(modes, GetParams(inputCount, modes));
                Pointer += movePtrForward;
            }
            return _currentOutput;
        }

        private static Mode[] GetModes(int poc)
            => new[]
            {
                (Mode)(poc / 100 % 10),      //p1 
                (Mode)(poc / 100 / 10 % 10), //p2 
                (Mode)(poc / 100 / 100 % 10) //p3
            };

        private int[] GetParams(int count, Mode[] modes)
            => Enumerable
                .Range(1, count)
                .Select(i => GetValueOffset(i, modes[i - 1]))
                .ToArray();

        public void SetValue(int pointer, int value)
            => _tape[pointer] = value;

        public int GetValue(int pointer) 
            =>_tape[pointer]; 

        private int GetValueOffset(int offset, Mode mode)
            => (mode == Mode.Position)
                ? _tape[_tape[Pointer + offset]]
                : _tape[Pointer + offset];

        private void SetValueOffset(int offset, Mode[] modes, int value)
        {
            var idx = (modes[offset - 1] == 0)
                ? _tape[Pointer + offset]
                : Pointer + offset;
            _tape[idx] = value;
        }

        #region command implementations 
        private void Add(Mode[] modes, int[] p) 
            => SetValueOffset(3, modes, p[0] + p[1]);

        private void Mul(Mode[] modes, int[] p) 
            => SetValueOffset(3, modes, p[0] * p[1]);

        private void Input(Mode[] modes, int[] p) 
            => SetValueOffset(1, modes, _inputs.Dequeue());

        private void Output(Mode[] modes, int[] p)
        {
            _outputs.Add(p[0]);
            _currentOutput = p[0];
            Pause = true;
        }
        private void JumpIfTrue(Mode[] modes, int[] p)
            => Pointer = (p[0] != 0)
                ? p[1]
                : Pointer + 3;

        private void JumpIfFalse(Mode[] modes, int[] p)
            => Pointer = (p[0] == 0)
                ? p[1]
                : Pointer + 3;

        private void LessThan(Mode[] modes, int[] p)
            => SetValueOffset(3, modes, (p[0] < p[1]) ? 1 : 0);

        private void EqualTo(Mode[] modes, int[] p)
            => SetValueOffset(3, modes, (p[0] == p[1]) ? 1 : 0);

        private void Halt(Mode[] modes, int[] p) 
            => IsHalted = true;
        #endregion             
    }
}