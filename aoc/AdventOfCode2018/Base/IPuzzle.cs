namespace AdventOfCode2018.Base
{
    using System.Threading.Tasks;

    public interface IPuzzle
    {
        string Name { get; }
        Task RunBothParts();
        Task<string> RunPart1();
        Task<string> RunPart2();
    }
}