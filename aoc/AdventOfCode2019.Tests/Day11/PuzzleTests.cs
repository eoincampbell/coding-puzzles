namespace AdventOfCode2019.Tests.Day11
{
    using NUnit.Framework;
    using System.Threading.Tasks;

    public partial class PuzzleTests
    {
        [Test]
        public async Task Puzzle11_Impl1_Part1()
        {
            var p = new Puzzles.Day11.Impl();
            await p.ResetInputsAsync();
            var a = await p.RunPart1Async();
            Assert.AreEqual(1564, a);
        }

        [Test]
        public async Task Puzzle11_Impl1_Part2()
        {
            var p = new Puzzles.Day11.Impl();
            await p.ResetInputsAsync();
            var a = await p.RunPart2Async();
            Assert.AreEqual(258, a);
        }
    }
}