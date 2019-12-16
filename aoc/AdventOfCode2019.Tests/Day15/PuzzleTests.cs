namespace AdventOfCode2019.Tests.Day15
{
    using NUnit.Framework;
    using System.Threading.Tasks;

    public class PuzzleTests
    {
        [Test]
        public async Task Puzzle15_Impl1_Part1()
        {
            var p = new Puzzles.Day15.Impl();
            await p.ResetInputsAsync();
            var a = await p.RunPart1Async();
            Assert.AreEqual(304, a);
        }

        [Test]
        public async Task Puzzle15_Impl1_Part2()
        {
            var p = new Puzzles.Day15.Impl();
            await p.ResetInputsAsync();
            var a = await p.RunPart2Async();
            Assert.AreEqual(310, a);
        }
    }
}