namespace AdventOfCode2019.Base
{
    using System.Linq;
    using System.Numerics;

    internal interface ICommand
    {
        Commands CommandName { get; }
        void Execute();
    }

    internal enum Commands
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
        protected StateMachine Sm;
        protected IntCodeVm Vm;

        protected Command(IntCodeVm vm, StateMachine sm)
        {
            Vm = vm;
            Sm = sm;
        }

        public abstract int InputCount { get; }
        public abstract Commands CommandName { get; }
        private static Mode[] GetModes(BigInteger fullOpCode)
            => new[]
            {
                (Mode)((int)(fullOpCode / 100 % 10)),      //p1 
                (Mode)((int)(fullOpCode / 100 / 10 % 10)), //p2 
                (Mode)((int)(fullOpCode / 100 / 100 % 10)) //p3
            };

        private BigInteger[] GetParams(StateMachine sm, Mode[] modes)
            => Enumerable.Range(1, InputCount)
                .Select(i => sm.GetValueOffset(i, modes[i - 1]))
                .ToArray();

        protected (Mode[] modes, BigInteger[] parameters) GetData(StateMachine sm)
        {
            var modes = GetModes(sm.GetValue());
            var parameters = GetParams(sm, modes);
            return (modes, parameters);
        }

        public void Execute()
        {
            var (modes, p) = GetData(Sm);
            ExecuteImpl(modes, p);
        }

        protected virtual void ExecuteImpl(Mode[] modes, BigInteger[] p) { }
    }

    internal sealed class AddCommand : Command
    {
        public AddCommand(IntCodeVm vm, StateMachine sm) : base(vm, sm) { }
        public override int InputCount => 2;
        public override Commands CommandName => Commands.ADD;
        protected override void ExecuteImpl(Mode[] modes, BigInteger[] p)
        {
            Sm.SetValueOffset(3, modes, p[0] + p[1]);
            Sm.SetPointer(Sm.Pointer + 4);
        }
    }

    internal sealed class MultiplyCommand : Command
    {
        public MultiplyCommand(IntCodeVm vm, StateMachine sm) : base(vm, sm) { }
        public override int InputCount => 2;
        public override Commands CommandName => Commands.MUL;
        protected override void ExecuteImpl(Mode[] modes, BigInteger[] p)
        {
            Sm.SetValueOffset(3, modes, p[0] * p[1]);
            Sm.SetPointer(Sm.Pointer + 4);
        }
    }

    internal sealed class InputCommand : Command
    {
        public InputCommand(IntCodeVm vm, StateMachine sm) : base(vm, sm) { }
        public override int InputCount => 0;
        public override Commands CommandName => Commands.INP;
        protected override void ExecuteImpl(Mode[] modes, BigInteger[] p)
        {
            Sm.SetValueOffset(1, modes, Vm.GetNextInput());
            Sm.SetPointer(Sm.Pointer + 2);
        }
    }


    internal sealed class OutputCommand : Command
    {
        public OutputCommand(IntCodeVm vm, StateMachine sm) : base(vm, sm) { }
        public override int InputCount => 1;
        public override Commands CommandName => Commands.OUT;
        protected override void ExecuteImpl(Mode[] modes, BigInteger[] p)
        {
            Vm.AddOutput(p[0]);
            Vm.Pause();
            Sm.SetPointer(Sm.Pointer + 2);
        }
    }
    internal sealed class JumpIfTrueCommand : Command
    {
        public JumpIfTrueCommand(IntCodeVm vm, StateMachine sm) : base(vm, sm) { }
        public override int InputCount => 2;
        public override Commands CommandName => Commands.JIT;
        protected override void ExecuteImpl(Mode[] modes, BigInteger[] p)
        {
            var newPointer = (p[0] != 0)
                ? p[1]
                : Sm.Pointer + 3;

            Sm.SetPointer(newPointer);
        }
    }

    internal sealed class JumpIfFalseCommand : Command
    {
        public JumpIfFalseCommand(IntCodeVm vm, StateMachine sm) : base(vm, sm) { }
        public override int InputCount => 2;
        public override Commands CommandName => Commands.JIF;
        protected override void ExecuteImpl(Mode[] modes, BigInteger[] p)
        {
            var newPointer = (p[0] == 0)
               ? p[1]
               : Sm.Pointer + 3;

            Sm.SetPointer(newPointer);
        }
    }
    internal sealed class LessThanCommand : Command
    {
        public LessThanCommand(IntCodeVm vm, StateMachine sm) : base(vm, sm) { }
        public override int InputCount => 2;
        public override Commands CommandName => Commands.LES;
        protected override void ExecuteImpl(Mode[] modes, BigInteger[] p)
        {
            Sm.SetValueOffset(3, modes, (p[0] < p[1]) ? 1 : 0);
            Sm.SetPointer(Sm.Pointer + 4);
        }
    }

    internal sealed class EqualToCommand : Command
    {
        public EqualToCommand(IntCodeVm vm, StateMachine sm) : base(vm, sm) { }
        public override int InputCount => 2;
        public override Commands CommandName => Commands.EQU;
        protected override void ExecuteImpl(Mode[] modes, BigInteger[] p)
        {
            Sm.SetValueOffset(3, modes, (p[0] == p[1]) ? 1 : 0);
            Sm.SetPointer(Sm.Pointer + 4);
        }
    }

    internal sealed class RelativeBaseCommand : Command
    {
        public RelativeBaseCommand(IntCodeVm vm, StateMachine sm) : base(vm, sm) { }
        public override int InputCount => 1;
        public override Commands CommandName => Commands.REL;
        protected override void ExecuteImpl(Mode[] modes, BigInteger[] p)
        {
            Sm.ModifyRelativeBasePoint(p[0]);
            Sm.SetPointer(Sm.Pointer + 2);
        }
    }


    internal sealed class HaltCommand : Command
    {
        public HaltCommand(IntCodeVm vm, StateMachine sm) : base(vm, sm) { }
        public override int InputCount => 0;
        public override Commands CommandName => Commands.HLT;
        protected override void ExecuteImpl(Mode[] modes, BigInteger[] p) => Vm.Halt();
    }
}