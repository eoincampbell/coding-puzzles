namespace AdventOfCode2019.Tests.Day10
{
    using NUnit.Framework;
    using System.Threading.Tasks;

    public partial class PuzzleTests
    {
        [Test]
        public async Task Puzzle10_Impl1_Part1()
        {
            var p = new Puzzles.Day10.Impl();
            await p.ResetInputsAsync();
            var a = await p.RunPart1Async();
            Assert.AreEqual(214, a);
        }

        [Test]
        public async Task Puzzle10_Impl1_Part2()
        {
            var p = new Puzzles.Day10.Impl();
            await p.ResetInputsAsync();
            var a = await p.RunPart2Async();
            Assert.AreEqual(502, a);
        }
    }
}