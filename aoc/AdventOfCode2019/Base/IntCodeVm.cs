namespace AdventOfCode2019.Base
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using CommandDict = System.Collections.Generic.Dictionary<int, IVmCommand>;

    public enum Mode
    {
        Position = 0,
        Immediate = 1
    }

    public interface IVmCommand
    {
        int OpCode { get; }
        int InputCount { get; }
        int MovePtrForward { get; }
        string CommandName { get; }
        Func<Mode[], int[], int> Execute { get; }
    }

    public class VmCommand : IVmCommand
    {
        public VmCommand(int opCode, string commandName, int inputCount, int movePtrFoward, Func<Mode[], int[], int> execute)
        {
            OpCode = opCode;
            CommandName = commandName;
            InputCount = inputCount;
            MovePtrForward = movePtrFoward;
            Execute = execute;
        }

        public int OpCode { get; }
        public int InputCount { get; }
        public int MovePtrForward { get; }
        public string CommandName { get; }
        public Func<Mode[], int[], int> Execute { get; }
    }

    public class StateMachine
    {
        private readonly int[] _tape;
        public int Pointer { get; private set; }

        public StateMachine(int[] tape)
        {
            _tape = tape;
        }

        public int GetValue() 
            => _tape[Pointer];

        public void SetPointer(int pointer) 
            => Pointer = pointer;

        public void SetValue(int pointer, int value) 
            => _tape[pointer] = value;

        public int GetValue(int pointer)
            => _tape[pointer];

        public int GetValueOffset(int offset, Mode mode)
            => (mode == Mode.Position)
                ? _tape[_tape[Pointer + offset]]
                : _tape[Pointer + offset];

        public void SetValueOffset(int offset, Mode[] modes, int value)
        {
            var idx = (modes[offset - 1] == 0)
                ? _tape[Pointer + offset]
                : Pointer + offset;
            _tape[idx] = value;
        }
    }

    public class IntCodeVm
    {
        private readonly StateMachine _stateMachine;
        private readonly Queue<int> _inputs;
        private readonly Queue<int> _outputs; 
        private int _lastOutput;
        private readonly CommandDict _commands;
        public Action<string> LogAction;

        public bool IsHalted { get; private set; }
        public bool IsPaused { get; set; }

        public IntCodeVm(string tape)
            : this(Array.ConvertAll(tape.Split(','), int.Parse)) { }

        public IntCodeVm(int [] tape)
        {
            _stateMachine = new StateMachine( tape);
            _inputs = new Queue<int>();
            _outputs = new Queue<int>();
            _commands = new CommandDict
            {
                {01, new VmCommand(01, "ADD", 2, 4, Add)},
                {02, new VmCommand(02, "MUL", 2, 4, Mul)},
                {03, new VmCommand(03, "INP", 0, 2, Input)},
                {04, new VmCommand(04, "OUT", 1, 2, Output)},
                {05, new VmCommand(05, "JIT", 2, 0, JumpIfTrue)},
                {06, new VmCommand(06, "JIF", 2, 0, JumpIfFalse)},
                {07, new VmCommand(07, "LES", 2, 4, LessThan)},
                {08, new VmCommand(08, "EQU", 2, 4, EqualTo)},
                {99, new VmCommand(99, "HLT", 0, 1, Halt)},
            };
        }

        public void RunProgram()
        {
            while (!IsHalted)
            {
                ProcessNextCommand();
            }
        }

        public void RunProgramPauseAtOutput()
        {
            IsPaused = false;
            while (!IsPaused && !IsHalted)
            {
                ProcessNextCommand();
            }
        }

        private void ProcessNextCommand()
        {
            var poc = _stateMachine.GetValue();
            var opCode = poc % 100;
            var modes = GetModes(poc);
            var c = _commands[opCode];
            var param = GetParams(c.InputCount, modes);
            var pointer = c.Execute(modes, param);
            LogAction?.Invoke($"{opCode:00} {_stateMachine.Pointer:0000} {c.Execute.Method.Name:10} {string.Join(" ",param)}");
            _stateMachine.SetPointer( pointer + c.MovePtrForward);
        }

        public void AddInput(int input) => _inputs.Enqueue(input);
        public int GetOutput()
        {
            if (_outputs.TryDequeue(out int temp))
                _lastOutput = temp;

            return _lastOutput;
        }

        public IEnumerable<int> GetOutputs()
        {
            var o = _outputs.ToList();
            _lastOutput = o.Last();
            return o;
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
                .Select(i => _stateMachine.GetValueOffset(i, modes[i - 1]))
                .ToArray();

        public void SetValue(int pointer, int value)
            => _stateMachine.SetValue(pointer, value);

        public int GetValue(int pointer) 
            => _stateMachine.GetValue(pointer); 


        #region command implementations 
        private int Add(Mode[] modes, int[] p)
        {
            _stateMachine.SetValueOffset(3, modes, p[0] + p[1]);
            return _stateMachine.Pointer;
        }

        private int Mul(Mode[] modes, int[] p)
        {
            _stateMachine.SetValueOffset(3, modes, p[0] * p[1]);
            return _stateMachine.Pointer;
        }

        private int Input(Mode[] modes, int[] p)
        {
            _stateMachine.SetValueOffset(1, modes, _inputs.Dequeue());
            return _stateMachine.Pointer;
        }

        private int Output(Mode[] modes, int[] p)
        {
            _outputs.Enqueue(p[0]);
            IsPaused = true;
            return _stateMachine.Pointer;
        }

        private int JumpIfTrue(Mode[] modes, int[] p)
        {
            return (p[0] != 0)
                ? p[1]
                : _stateMachine.Pointer + 3;
        }

        private int JumpIfFalse(Mode[] modes, int[] p)
        {
            return (p[0] == 0)
                ? p[1]
                : _stateMachine.Pointer + 3;
        }

        private int LessThan(Mode[] modes, int[] p)
        {
            _stateMachine.SetValueOffset(3, modes, (p[0] < p[1]) ? 1 : 0);
            return _stateMachine.Pointer;
        }

        private int EqualTo(Mode[] modes, int[] p)
        {
            _stateMachine.SetValueOffset(3, modes, (p[0] == p[1]) ? 1 : 0);
            return _stateMachine.Pointer;
        }

        private int Halt(Mode[] modes, int[] p)
        {
            IsHalted = true;
            return _stateMachine.Pointer;
        }

        #endregion             
    }
}