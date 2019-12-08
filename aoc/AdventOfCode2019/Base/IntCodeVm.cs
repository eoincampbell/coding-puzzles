namespace AdventOfCode2019.Base
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using CommandDict = System.Collections.Generic.Dictionary<Commands, ICommand>;
    
    public class IntCodeVm
    {
        private readonly StateMachine _sm;
        private readonly Queue<int> _in = new Queue<int>();
        private readonly Queue<int> _out = new Queue<int>();
        private readonly CommandDict _coms;
        private int _lastOutput;
        public Action<string> LogAction;
        public bool IsHalted { get; private set; }
        public bool IsPaused { get; private set; }

        public IntCodeVm(string tape) : this(Array.ConvertAll(tape.Split(','), int.Parse)) { }
        public IntCodeVm(int [] tape)
        {
            _sm = new StateMachine(tape);
            _coms = new CommandDict
            {
                {Commands.ADD, new AddCommand(this, _sm)},
                {Commands.MUL, new MultiplyCommand(this, _sm)},
                {Commands.INP, new InputCommand(this, _sm)},
                {Commands.OUT, new OutputCommand(this, _sm)},
                {Commands.JIT, new JumpIfTrueCommand(this, _sm)},
                {Commands.JIF, new JumpIfFalseCommand(this, _sm)},
                {Commands.LES, new LessThanCommand(this, _sm)},
                {Commands.EQU, new EqualToCommand(this, _sm)},
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
            var foc= _sm.GetValue();
            var oc = foc % 100;
            var com = _coms[(Commands)oc];
            com.Execute();
            LogAction?.Invoke($"{oc:00} {_sm.Pointer:0000} {com.CommandName}");            
        }

        public void Halt() => IsHalted = true;
        public void Pause() => IsPaused = true;
        public void SetValue(int ptr, int val) => _sm.SetValue(ptr, val);
        public void AddInput(int input) => _in.Enqueue(input);
        public void AddOutput(int output) => _out.Enqueue(output);
        public int GetValue(int ptr) => _sm.GetValue(ptr);
        public int GetNextInput() => _in.Dequeue();
        public int GetNextOutput()
        {
            if (_out.TryDequeue(out int temp)) _lastOutput = temp;
            return _lastOutput;
        }
        public IEnumerable<int> GetOutputs()
        {
            while (_out.Any())
            {
                _lastOutput = _out.Dequeue();
                yield return _lastOutput;
            }
        }
    }
}