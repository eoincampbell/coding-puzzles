namespace AdventOfCode2019.Base
{
    using System.Threading.Tasks;

    public interface IPuzzle
    {
        string Name { get; }
        Task RunBothPartsAsync();
        Task<string> RunPart1Async();
        Task<string> RunPart2Async();
    }
}