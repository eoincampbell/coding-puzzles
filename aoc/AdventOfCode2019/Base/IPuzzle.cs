namespace AdventOfCode2019.Base
{
    using System.Threading.Tasks;

    public interface IPuzzle
    {
        string Name { get; }
        Task RunBothPartsAsync();
    }

    public interface IPuzzle<TOutput> : IPuzzle
    {
        Task<TOutput> RunPart1Async();
        Task<TOutput> RunPart2Async();
    }
}