namespace AdventOfCode2019.Tests.Day03
{
    using NUnit.Framework;
    using System.Threading.Tasks;
    using System.Numerics;

    public partial class PuzzleTests
    {
        [Test]
        public async Task Puzzle03_Impl1_Part1()
        {
            var p = new Puzzles.Day03.Impl();
            await p.ResetInputsAsync();
            var a = await p.RunPart1Async();
            Assert.AreEqual(399, a);
        }

        [Test]
        public async Task Puzzle03_Impl1_Part2()
        {
            var p = new Puzzles.Day03.Impl();
            await p.ResetInputsAsync();
            var a = await p.RunPart2Async();
            Assert.AreEqual(15678, a);
        }
    }
}