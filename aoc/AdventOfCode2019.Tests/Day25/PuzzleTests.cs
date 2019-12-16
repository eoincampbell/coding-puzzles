namespace AdventOfCode2019.Tests.Day25
{
    using NUnit.Framework;
    using System.Threading.Tasks;

    public partial class PuzzleTests
    {
        [Test]
        public async Task Puzzle25_Impl1_Part1()
        {
            var p = new Puzzles.Day25.Impl();
            await p.ResetInputsAsync();
            var a = await p.RunPart1Async();
            Assert.AreEqual(0, a);
        }

        [Test]
        public async Task Puzzle25_Impl1_Part2()
        {
            var p = new Puzzles.Day25.Impl();
            await p.ResetInputsAsync();
            var a = await p.RunPart2Async();
            Assert.AreEqual(0, a);
        }
    }
}