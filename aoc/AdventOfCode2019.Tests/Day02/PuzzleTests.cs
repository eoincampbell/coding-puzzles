namespace AdventOfCode2019.Tests.Day02
{
    using NUnit.Framework;
    using System.Threading.Tasks;

    public partial class PuzzleTests
    {
        [Test]
        public async Task Puzzle02_Impl1_Part1()
        {
            var p = new Puzzles.Day02.Impl();
            await p.ResetInputsAsync();
            var a = await p.RunPart1Async();
            Assert.AreEqual(4462686, a);
        }

        [Test]
        public async Task Puzzle02_Impl1_Part2()
        {
            var p = new Puzzles.Day02.Impl();
            await p.ResetInputsAsync();
            var a = await p.RunPart2Async();
            Assert.AreEqual(5936, a);
        }

        [Test]
        public async Task Puzzle02_Impl2_Part1()
        {
            var p = new Puzzles.Day02.Impl2();
            await p.ResetInputsAsync();
            var a = await p.RunPart1Async();
            Assert.AreEqual(4462686, a);
        }

        [Test]
        public async Task Puzzle02_Impl2_Part2()
        {
            var p = new Puzzles.Day02.Impl2();
            await p.ResetInputsAsync();
            var a = await p.RunPart2Async();
            Assert.AreEqual(5936, a);
        }
    }
}