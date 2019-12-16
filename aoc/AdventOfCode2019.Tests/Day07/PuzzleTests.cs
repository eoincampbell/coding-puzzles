namespace AdventOfCode2019.Tests.Day07
{
    using NUnit.Framework;
    using System.Numerics;
    using System.Threading.Tasks;

    public partial class PuzzleTests
    {
        [Test]
        public async Task Puzzle07_Impl1_Part1()
        {
            var p = new Puzzles.Day07.Impl();
            await p.ResetInputsAsync();
            var a = await p.RunPart1Async();
            Assert.AreEqual(new BigInteger(22012), a);
        }

        [Test]
        public async Task Puzzle07_Impl1_Part2()
        {
            var p = new Puzzles.Day07.Impl();
            await p.ResetInputsAsync();
            var a = await p.RunPart2Async();
            Assert.AreEqual(new BigInteger(4039164), a);
        }
    }
}