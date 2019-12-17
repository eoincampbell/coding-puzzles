namespace AdventOfCode2019.Tests.Day17
{
    using NUnit.Framework;
    using System.Threading.Tasks;

    public class PuzzleTests
    {
        [Test]
        public async Task Puzzle17_Impl1_Part1()
        {
            var p = new Puzzles.Day17.Impl();
            await p.ResetInputsAsync();
            var a = await p.RunPart1Async();
            Assert.AreEqual(5740, a);
        }

        [Test]
        public async Task Puzzle17_Impl1_Part2()
        {
            var p = new Puzzles.Day17.Impl();
            await p.ResetInputsAsync();
            var a = await p.RunPart2Async();
            Assert.AreEqual(1022165, a);
        }
    }
}