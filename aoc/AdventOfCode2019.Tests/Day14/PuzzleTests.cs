namespace AdventOfCode2019.Tests.Day14
{
    using NUnit.Framework;
    using System.Threading.Tasks;

    public class PuzzleTests
    {
        [Test]
        public async Task Puzzle14_Impl1_Part1()
        {
            var p = new Puzzles.Day14.Impl();
            await p.ResetInputsAsync();
            var a = await p.RunPart1Async();
            Assert.AreEqual(1582325, a);
        }

        [Test]
        public async Task Puzzle14_Impl1_Part2()
        {
            var p = new Puzzles.Day14.Impl();
            await p.ResetInputsAsync();
            var a = await p.RunPart2Async();
            Assert.AreEqual(2267486, a);
        }
    }
}