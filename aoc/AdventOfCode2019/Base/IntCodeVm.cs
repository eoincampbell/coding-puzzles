﻿namespace AdventOfCode2019.Base
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using CommandDict = System.Collections.Generic.Dictionary<int, (int inputCount, int movePointerForward, System.Action<int[], int[]> command)>;

    public class IntCodeVm
    {
        private bool _halt;
        private bool _pause;
        private int _pointer;
        private readonly Queue<int> _inputs;
        private readonly int[] _tape;
        private int _currentOutput;
        private readonly List<int> _outputs = new List<int>();
        private readonly CommandDict _commands;

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

        public int RunProgramPauseAtOutput()
        {
            _pause = false;
            while (!_pause && !_halt)
            {
                var poc = _tape[_pointer];
                var opCode = poc % 100;
                var modes = GetModes(poc);
                var (inputCount, movePtrForward, command) = _commands[opCode];
                command(modes, GetParams(inputCount, modes));
                _pointer += movePtrForward;
            }
            return _currentOutput;
        }

        public bool IsHalted => _halt;

        private static int[] GetModes(int poc)
            => new[]
            {
                poc / 100 % 10,      //p1 
                poc / 100 / 10 % 10, //p2 
                poc / 100 / 100 % 10 //p3
            };

        private int[] GetParams(int count, int[] modes)
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
        private void Add(int[] modes, int[] p) 
            => SetValue(3, modes, p[0] + p[1]);

        private void Mul(int[] modes, int[] p) 
            => SetValue(3, modes, p[0] * p[1]);

        private void Inp(int[] modes, int[] p) 
            => SetValue(1, modes, _inputs.Dequeue());

        private void Out(int[] modes, int[] p)
        {
            _outputs.Add(p[0]);
            _currentOutput = p[0];
            _pause = true;
        }
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

        private void Hlt(int[] modes, int[] p) 
            => _halt = true;
        #endregion             
    }
}