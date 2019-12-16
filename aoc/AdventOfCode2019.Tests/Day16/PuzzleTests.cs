namespace AdventOfCode2019.Tests.Day16
{
    using NUnit.Framework;
    using System.Threading.Tasks;

    public class PuzzleTests
    {
        [Test]
        public async Task Puzzle16_Impl1_Part1()
        {
            var p = new Puzzles.Day16.Impl();
            await p.ResetInputsAsync();
            var a = await p.RunPart1Async();
            Assert.AreEqual(27229269, a);
        }

        [Test]
        public async Task Puzzle16_Impl1_Part2()
        {
            var p = new Puzzles.Day16.Impl();
            await p.ResetInputsAsync();
            var a = await p.RunPart2Async();
            Assert.AreEqual(26857164, a);
        }
    }
}