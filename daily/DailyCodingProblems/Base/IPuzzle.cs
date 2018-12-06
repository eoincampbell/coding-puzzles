namespace DailyCodingProblems.Base
{
    using System.Threading.Tasks;
    public interface IPuzzle
    {
        string Name { get; }
        Task<string> Execute();
    }
}