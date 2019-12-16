namespace AdventOfCode2019.Tests.Day01
{
    using NUnit.Framework;
    using System.Threading.Tasks;

    public partial class PuzzleTests
    {
        [Test]
        public async Task Puzzle01_Impl1_Part1()
        {
            var p = new Puzzles.Day01.Impl();
            await p.ResetInputsAsync();
            var a = await p.RunPart1Async();
            Assert.AreEqual(3481005, a);
        }

        [Test]
        public async Task Puzzle01_Impl1_Part2()
        {
            var p = new Puzzles.Day01.Impl();
            await p.ResetInputsAsync();
            var a = await p.RunPart2Async();
            Assert.AreEqual(5218616, a);
        }
    }
}