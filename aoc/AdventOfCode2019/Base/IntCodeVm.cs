namespace AdventOfCode2019.Base
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using CommandDict = System.Collections.Generic.Dictionary<Commands, ICommand>;
    
    public class IntCodeVm
    {
        private readonly StateMachine _sm;
        private readonly Queue<long> _in = new Queue<long>();
        private readonly Queue<long> _out = new Queue<long>();
        private readonly CommandDict _commands;
        private long _lastOutput;
        public Action<string> LogAction;
        public bool IsHalted { get; private set; }
        public bool IsPaused { get; private set; }

        public IntCodeVm(string tape) : this(Array.ConvertAll(tape.Split(','), long.Parse)) { }
        public IntCodeVm(long [] tape)
        {
            Array.Resize(ref tape, 0xffff);
            
            _sm = new StateMachine(tape);
            _commands = new CommandDict
            {
                {Commands.ADD, new AddCommand(this, _sm)},
                {Commands.MUL, new MultiplyCommand(this, _sm)},
                {Commands.INP, new InputCommand(this, _sm)},
                {Commands.OUT, new OutputCommand(this, _sm)},
                {Commands.JIT, new JumpIfTrueCommand(this, _sm)},
                {Commands.JIF, new JumpIfFalseCommand(this, _sm)},
                {Commands.LES, new LessThanCommand(this, _sm)},
                {Commands.EQU, new EqualToCommand(this, _sm)},
                {Commands.REL, new RelativeBaseCommand(this, _sm)},
                {Commands.HLT, new HaltCommand(this, _sm)},
            };
        }

        public void RunProgram()
        {
            while (!IsHalted) ProcessNextCommand();
        }

        public void RunProgramPauseAtOutput()
        {
            IsPaused = false;
            while (!IsPaused && !IsHalted) ProcessNextCommand();
        }

        private void ProcessNextCommand()
        {
            var oc = _sm.GetValue() % 100;
            var com = _commands[(Commands)oc];
            com.Execute();
            LogAction?.Invoke($"{oc:00} {_sm.Pointer:0000} {com.CommandName}");            
        }

        public void Halt() => IsHalted = true;
        public void Pause() => IsPaused = true;
        public void SetValue(int ptr, long val) => _sm.SetValue(ptr, val);
        public void AddInput(long input) => _in.Enqueue(input);
        public void AddOutput(long output) => _out.Enqueue(output);
        public long GetValue(int ptr) => _sm.GetValue(ptr);
        public long GetNextInput() => _in.Dequeue();
        public long GetNextOutput()
        {
            if (_out.TryDequeue(out long temp)) _lastOutput = temp;
            return _lastOutput;
        }
        public IEnumerable<long> GetOutputs()
        {
            while (_out.Any())
            {
                _lastOutput = _out.Dequeue();
                yield return _lastOutput;
            }
        }
    }
}