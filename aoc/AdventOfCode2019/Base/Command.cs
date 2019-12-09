namespace AdventOfCode2019.Base
{
    using System.Linq;

    internal interface ICommand
    {
        Commands CommandName { get; }
        void Execute();
    }

    internal enum Commands : int
    {
        ADD = 1,
        MUL = 2,
        INP = 3,
        OUT = 4,
        JIT = 5,
        JIF = 6,
        LES = 7,
        EQU = 8,
        REL = 9,
        HLT = 99
    }
    internal abstract class Command : ICommand
    {
        protected StateMachine _sm;
        protected IntCodeVm _vm;

        protected Command(IntCodeVm vm, StateMachine sm)
        {
            _vm = vm;
            _sm = sm;
        }

        public abstract int InputCount { get; }
        public abstract Commands CommandName { get; }
        private static Mode[] GetModes(long fullOpCode)
            => new[]
            {
                (Mode)(fullOpCode / 100 % 10),      //p1 
                (Mode)(fullOpCode / 100 / 10 % 10), //p2 
                (Mode)(fullOpCode / 100 / 100 % 10) //p3
            };

        private long[] GetParams(StateMachine sm, Mode[] modes)
            => Enumerable.Range(1, InputCount)
                .Select(i => sm.GetValueOffset(i, modes[i - 1]))
                .ToArray();

        protected (Mode[] modes, long[] parameters) GetData(StateMachine sm)
        {
            var modes = GetModes(sm.GetValue());
            var parameters = GetParams(sm, modes);
            return (modes, parameters);
        }

        public void Execute()
        {
            var (modes, p) = GetData(_sm);
            ExecuteImpl(modes, p);
        }

        protected virtual void ExecuteImpl(Mode[] modes, long[] p) { }
    }

    internal sealed class AddCommand : Command
    {
        public AddCommand(IntCodeVm vm, StateMachine sm) : base(vm, sm) { }
        public override int InputCount => 2;
        public override Commands CommandName => Commands.ADD;
        protected override void ExecuteImpl(Mode[] modes, long[] p)
        {
            _sm.SetValueOffset(3, modes, p[0] + p[1]);
            _sm.SetPointer(_sm.Pointer + 4);
        }
    }

    internal sealed class MultiplyCommand : Command
    {
        public MultiplyCommand(IntCodeVm vm, StateMachine sm) : base(vm, sm) { }
        public override int InputCount => 2;
        public override Commands CommandName => Commands.MUL;
        protected override void ExecuteImpl(Mode[] modes, long[] p)
        {
            _sm.SetValueOffset(3, modes, p[0] * p[1]);
            _sm.SetPointer(_sm.Pointer + 4);
        }
    }

    internal sealed class InputCommand : Command
    {
        public InputCommand(IntCodeVm vm, StateMachine sm) : base(vm, sm) { }
        public override int InputCount => 0;
        public override Commands CommandName => Commands.INP;
        protected override void ExecuteImpl(Mode[] modes, long[] p)
        {
            _sm.SetValueOffset(1, modes, _vm.GetNextInput());
            _sm.SetPointer(_sm.Pointer + 2);
        }
    }


    internal sealed class OutputCommand : Command
    {
        public OutputCommand(IntCodeVm vm, StateMachine sm) : base(vm, sm) { }
        public override int InputCount => 1;
        public override Commands CommandName => Commands.OUT;
        protected override void ExecuteImpl(Mode[] modes, long[] p)
        {
            _vm.AddOutput(p[0]);
            _vm.Pause();
            _sm.SetPointer(_sm.Pointer + 2);
        }
    }
    internal sealed class JumpIfTrueCommand : Command
    {
        public JumpIfTrueCommand(IntCodeVm vm, StateMachine sm) : base(vm, sm) { }
        public override int InputCount => 2;
        public override Commands CommandName => Commands.JIT;
        protected override void ExecuteImpl(Mode[] modes, long[] p)
        {
            var newPointer = (p[0] != 0)
                ? p[1]
                : _sm.Pointer + 3;

            _sm.SetPointer(newPointer);
        }
    }

    internal sealed class JumpIfFalseCommand : Command
    {
        public JumpIfFalseCommand(IntCodeVm vm, StateMachine sm) : base(vm, sm) { }
        public override int InputCount => 2;
        public override Commands CommandName => Commands.JIF;
        protected override void ExecuteImpl(Mode[] modes, long[] p)
        {
            var newPointer = (p[0] == 0)
               ? p[1]
               : _sm.Pointer + 3;

            _sm.SetPointer(newPointer);
        }
    }
    internal sealed class LessThanCommand : Command
    {
        public LessThanCommand(IntCodeVm vm, StateMachine sm) : base(vm, sm) { }
        public override int InputCount => 2;
        public override Commands CommandName => Commands.LES;
        protected override void ExecuteImpl(Mode[] modes, long[] p)
        {
            _sm.SetValueOffset(3, modes, (p[0] < p[1]) ? 1 : 0);
            _sm.SetPointer(_sm.Pointer + 4);
        }
    }

    internal sealed class EqualToCommand : Command
    {
        public EqualToCommand(IntCodeVm vm, StateMachine sm) : base(vm, sm) { }
        public override int InputCount => 2;
        public override Commands CommandName => Commands.EQU;
        protected override void ExecuteImpl(Mode[] modes, long[] p)
        {
            _sm.SetValueOffset(3, modes, (p[0] == p[1]) ? 1 : 0);
            _sm.SetPointer(_sm.Pointer + 4);
        }
    }

    internal sealed class RelativeBaseCommand : Command
    {
        public RelativeBaseCommand(IntCodeVm vm, StateMachine sm) : base(vm, sm) { }
        public override int InputCount => 1;
        public override Commands CommandName => Commands.REL;
        protected override void ExecuteImpl(Mode[] modes, long[] p)
        {
            _sm.ModifyRelativeBasePoint(p[0]);
            _sm.SetPointer(_sm.Pointer + 2);
        }
    }


    internal sealed class HaltCommand : Command
    {
        public HaltCommand(IntCodeVm vm, StateMachine sm) : base(vm, sm) { }
        public override int InputCount => 0;
        public override Commands CommandName => Commands.HLT;
        protected override void ExecuteImpl(Mode[] modes, long[] p) => _vm.Halt();
    }
}