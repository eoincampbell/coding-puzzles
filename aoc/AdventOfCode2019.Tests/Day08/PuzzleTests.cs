namespace AdventOfCode2019.Tests.Day08
{
    using NUnit.Framework;
    using System.Threading.Tasks;

    public partial class PuzzleTests
    {
        [Test]
        public async Task Puzzle08_Impl1_Part1()
        {
            var p = new Puzzles.Day08.Impl();
            await p.ResetInputsAsync();
            var a = await p.RunPart1Async();
            Assert.AreEqual(1560, a);
        }

        [Test]
        public async Task Puzzle08_Impl1_Part2()
        {
            var p = new Puzzles.Day08.Impl();
            await p.ResetInputsAsync();
            var a = await p.RunPart2Async();
            Assert.AreEqual(15000, a);
        }
    }
}