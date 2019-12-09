namespace AdventOfCode2019.Base.IntCode
{
    using System.Collections.Generic;
    using System.Numerics;

    internal static class Commands
    {
        private static readonly Dictionary<Command, ICommand> Values = new Dictionary<Command, ICommand>
        {
            {Command.Add, new AddCommand()},
            {Command.Mul, new MultiplyCommand()},
            {Command.Inp, new InputCommand()},
            {Command.Out, new OutputCommand()},
            {Command.Jit, new JumpIfTrueCommand()},
            {Command.Jif, new JumpIfFalseCommand()},
            {Command.Les, new LessThanCommand()},
            {Command.Equ, new EqualToCommand()},
            {Command.Rel, new RelativeBaseCommand()},
            {Command.Hlt, new HaltCommand()},
        };

        public static ICommand Get(BigInteger key)
            => Values.GetValueOrDefault((Command) ((int) key));
    }
}