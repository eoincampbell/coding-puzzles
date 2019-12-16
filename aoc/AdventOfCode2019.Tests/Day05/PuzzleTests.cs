namespace AdventOfCode2019.Tests.Day05
{
    using NUnit.Framework;
    using System.Threading.Tasks;
    using System.Numerics;

    public partial class PuzzleTests
    {
        [Test]
        public async Task Puzzle05_Impl1_Part1()
        {
            var p = new Puzzles.Day05.Impl();
            await p.ResetInputsAsync();
            var a = await p.RunPart1Async();
            Assert.AreEqual(new BigInteger(16225258), a);
        }

        [Test]
        public async Task Puzzle05_Impl1_Part2()
        {
            var p = new Puzzles.Day05.Impl();
            await p.ResetInputsAsync();
            var a = await p.RunPart2Async();
            Assert.AreEqual(new BigInteger(2808771), a);
        }
    }
}