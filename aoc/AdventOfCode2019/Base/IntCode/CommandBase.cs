namespace AdventOfCode2019.Base.IntCode
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Numerics;

    internal abstract class CommandBase : ICommand
    {
        protected StateMachine Sm;
        protected IntCodeVm Vm;
        public abstract int InputCount { get; }
        public abstract Command CommandName { get; }

        public CommandBase()
        {
            Sm = new StateMachine(new Dictionary<BigInteger, BigInteger>());
            Vm = new IntCodeVm("0");
        }

        public void Execute(IntCodeVm vm, StateMachine sm)
        { 
            Vm = vm;
            Sm = sm;
            var (modes, p) = GetData(Sm);
            ExecuteImpl(modes, p);
        }

        protected virtual void ExecuteImpl(Mode[] modes, BigInteger[] p) { }

        protected (Mode[] modes, BigInteger[] parameters) GetData(StateMachine sm)
        {
            var modes = GetModes(sm.GetValue());
            var parameters = GetParams(sm, modes);
            return (modes, parameters);
        }

        private static Mode[] GetModes(BigInteger fullOpCode)
            => new[]
            {
                (Mode)((int)(fullOpCode / 100 % 10)),      //p1 
                (Mode)((int)(fullOpCode / 100 / 10 % 10)), //p2 
                (Mode)((int)(fullOpCode / 100 / 100 % 10)) //p3
            };

        private BigInteger[] GetParams(StateMachine sm, IReadOnlyList<Mode> modes)
            => Enumerable.Range(1, InputCount)
                .Select(i => sm.GetValueOffset(i, modes[i - 1]))
                .ToArray();

    }

    internal sealed class AddCommand : CommandBase
    {
        public override int InputCount => 2;
        public override Command CommandName => Command.Add;
        protected override void ExecuteImpl(Mode[] modes, BigInteger[] p)
        {
            Sm.SetValueOffset(3, modes, p[0] + p[1]);
            Sm.SetPointer(Sm.GetPointer() + 4);
        }
    }

    internal sealed class MultiplyCommand : CommandBase
    {
        public override int InputCount => 2;
        public override Command CommandName => Command.Mul;
        protected override void ExecuteImpl(Mode[] modes, BigInteger[] p)
        {
            Sm.SetValueOffset(3, modes, p[0] * p[1]);
            Sm.SetPointer(Sm.GetPointer() + 4);
        }
    }

    internal sealed class InputCommand : CommandBase
    {
        public override int InputCount => 0;
        public override Command CommandName => Command.Inp;
        protected override void ExecuteImpl(Mode[] modes, BigInteger[] p)
        {
            var input = Vm.GetInput(out var hasInput);
            if (hasInput)
            {
                Sm.SetValueOffset(1, modes, input);
                Sm.SetPointer(Sm.GetPointer() + 2);
            }

            Vm.PauseAwaitingInput();
        }
    }


    internal sealed class OutputCommand : CommandBase
    {
        public override int InputCount => 1;
        public override Command CommandName => Command.Out;
        protected override void ExecuteImpl(Mode[] modes, BigInteger[] p)
        {
            Vm.SetOutput(p[0]);
            Vm.PauseHasOutput();
            Sm.SetPointer(Sm.GetPointer() + 2);
        }
    }
    internal sealed class JumpIfTrueCommand : CommandBase
    {
        public override int InputCount => 2;
        public override Command CommandName => Command.Jit;
        protected override void ExecuteImpl(Mode[] modes, BigInteger[] p)
        {
            var newPointer = (p[0] != 0)
                ? p[1]
                : Sm.GetPointer() + 3;

            Sm.SetPointer(newPointer);
        }
    }

    internal sealed class JumpIfFalseCommand : CommandBase
    {
        public override int InputCount => 2;
        public override Command CommandName => Command.Jif;
        protected override void ExecuteImpl(Mode[] modes, BigInteger[] p)
        {
            var newPointer = (p[0] == 0)
               ? p[1]
               : Sm.GetPointer() + 3;

            Sm.SetPointer(newPointer);
        }
    }
    internal sealed class LessThanCommand : CommandBase
    {
        public override int InputCount => 2;
        public override Command CommandName => Command.Les;
        protected override void ExecuteImpl(Mode[] modes, BigInteger[] p)
        {
            Sm.SetValueOffset(3, modes, (p[0] < p[1]) ? 1 : 0);
            Sm.SetPointer(Sm.GetPointer() + 4);
        }
    }

    internal sealed class EqualToCommand : CommandBase
    {
        public override int InputCount => 2;
        public override Command CommandName => Command.Equ;
        protected override void ExecuteImpl(Mode[] modes, BigInteger[] p)
        {
            Sm.SetValueOffset(3, modes, (p[0] == p[1]) ? 1 : 0);
            Sm.SetPointer(Sm.GetPointer() + 4);
        }
    }

    internal sealed class RelativeBaseCommand : CommandBase
    {
        public override int InputCount => 1;
        public override Command CommandName => Command.Rel;
        protected override void ExecuteImpl(Mode[] modes, BigInteger[] p)
        {
            Sm.SetRelativeBasePointer(p[0]);
            Sm.SetPointer(Sm.GetPointer() + 2);
        }
    }


    internal sealed class HaltCommand : CommandBase
    {
        public override int InputCount => 0;
        public override Command CommandName => Command.Hlt;
        protected override void ExecuteImpl(Mode[] modes, BigInteger[] p) => Vm.Halt();
    }
}