namespace AdventOfCode2019.Base.IntCode
{
    internal interface ICommand
    {
        Command CommandName { get; }
        void Execute(IntCodeVm vm, StateMachine sm);
    }
}