namespace AdventOfCode2019.Base
{
    using System.Threading.Tasks;

    public interface IPuzzle
    {
        string Name { get; }
        Task RunBothPartsAsync();
    }
}