namespace AdventOfCode2019.Tests.Day06
{
    using NUnit.Framework;
    using System.Threading.Tasks;

    public class PuzzleTests
    {
        [Test]
        public async Task Puzzle06_Impl1_Part1()
        {
            var p = new Puzzles.Day06.Impl();
            await p.ResetInputsAsync();
            var a = await p.RunPart1Async();
            Assert.AreEqual(122782, a);
        }

        [Test]
        public async Task Puzzle06_Impl1_Part2()
        {
            var p = new Puzzles.Day06.Impl();
            await p.ResetInputsAsync();
            var a = await p.RunPart2Async();
            Assert.AreEqual(271, a);
        }

        [Test]
        public async Task Puzzle06_Impl2_Part1()
        {
            var p = new Puzzles.Day06.Impl2();
            await p.ResetInputsAsync();
            var a = await p.RunPart1Async();
            Assert.AreEqual(122782, a);
        }

        [Test]
        public async Task Puzzle06_Impl2_Part2()
        {
            var p = new Puzzles.Day06.Impl2();
            await p.ResetInputsAsync();
            var a = await p.RunPart2Async();
            Assert.AreEqual(271, a);
        }
    }
}