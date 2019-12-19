namespace AdventOfCode2019.Base.IntCode
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Numerics;

    public enum VmState
    {
        Running,
        Halted,
        PausedHasOutput,
        PausedAwaitingInput
    }

    public class IntCodeVm
    {
        private readonly StateMachine _sm;
        private readonly Queue<BigInteger> _in = new Queue<BigInteger>();
        private readonly Queue<BigInteger> _out = new Queue<BigInteger>();
        public Action<string>? LogAction { get; set; }
        private BigInteger _lastOutput;
        public VmState State {get; private set; }

        public IntCodeVm(string memory) : this(memory != null ? Array.ConvertAll(memory.Split(','), BigInteger.Parse) : Array.Empty<BigInteger>()) { }

        public IntCodeVm(IEnumerable<BigInteger> memory) 
            =>_sm = new StateMachine(memory.Select((v, i) => new { v, i }).ToDictionary(kv => (BigInteger)kv.i, kv => kv.v));
        

        #region Program Control Flow

        public VmState RunProgramUntilHalt()
        {
            State = VmState.Running;
            while (State != VmState.Halted) ProcessNextCommand();
            return State;
        }

        public VmState RunProgramUntilOutputAvailable()
        {
            State = VmState.Running;
            while (State != VmState.Halted && State != VmState.PausedHasOutput) ProcessNextCommand();
            return State;
        }

        public VmState RunProgramUntilInputRequired()
        {
            State = VmState.Running;
            while (State != VmState.Halted && State != VmState.PausedAwaitingInput) ProcessNextCommand();
            return State;
        }
        
        private void ProcessNextCommand()
        {
            var oc = _sm.GetValue() % 100;
            var com = Commands.Get(oc);
            com.Execute(this, _sm);
            LogAction?.Invoke($"{oc:00} {_sm.GetPointer():0000} {com.CommandName}");            
        }

        public void Halt() => State = VmState.Halted;
        public void PauseHasOutput() => State = VmState.PausedHasOutput;
        public void PauseAwaitingInput() => State = VmState.PausedAwaitingInput;
        public void Reset() => _sm.SetPointer(0);
        #endregion

        #region IO
        
        //Values
        public BigInteger GetValue(BigInteger p) => _sm.GetValue(p);
        public void SetValue(BigInteger p, BigInteger val) => _sm.SetValue(p, val);

        //Inputs
        public BigInteger GetInput(out bool success)
        {
            success = _in.TryDequeue(out var temp);
            return success ? temp : 0;
        }

        public void SetInput(BigInteger input) => _in.Enqueue(input);
        
        //Output
        public BigInteger GetOutput() => _out.TryDequeue(out var temp) ? _lastOutput = temp : _lastOutput;
        
        public IEnumerable<BigInteger> GetOutputs()
        {
            while (_out.Any()) yield return (_lastOutput = _out.Dequeue());
        }
        public void SetOutput(BigInteger output) => _out.Enqueue(output);
        public bool HasOutputs => _out.Any();

        #endregion
    }
}