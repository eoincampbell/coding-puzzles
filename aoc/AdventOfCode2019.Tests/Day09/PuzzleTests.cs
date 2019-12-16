namespace AdventOfCode2019.Tests.Day09
{
    using NUnit.Framework;
    using System.Numerics;
    using System.Threading.Tasks;

    public partial class PuzzleTests
    {
        [Test]
        public async Task Puzzle09_Impl1_Part1()
        {
            var p = new Puzzles.Day09.Impl();
            await p.ResetInputsAsync();
            var a = await p.RunPart1Async();
            Assert.AreEqual(new BigInteger(2465411646), a);
        }

        [Test]
        public async Task Puzzle09_Impl1_Part2()
        {
            var p = new Puzzles.Day09.Impl();
            await p.ResetInputsAsync();
            var a = await p.RunPart2Async();
            Assert.AreEqual(new BigInteger(69781), a);
        }
    }
}