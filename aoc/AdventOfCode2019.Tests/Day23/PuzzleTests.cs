namespace AdventOfCode2019.Tests.Day23
{
    using NUnit.Framework;
    using System.Threading.Tasks;

    public class PuzzleTests
    {
        [Test]
        public async Task Puzzle23_Impl1_Part1()
        {
            var p = new Puzzles.Day23.Impl();
            await p.ResetInputsAsync();
            var a = await p.RunPart1Async();
            Assert.AreEqual(0, a);
        }

        [Test]
        public async Task Puzzle23_Impl1_Part2()
        {
            var p = new Puzzles.Day23.Impl();
            await p.ResetInputsAsync();
            var a = await p.RunPart2Async();
            Assert.AreEqual(0, a);
        }
    }
}