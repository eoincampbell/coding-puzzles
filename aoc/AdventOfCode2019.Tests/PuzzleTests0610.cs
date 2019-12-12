
namespace AdventOfCode2019.Tests
{
    using NUnit.Framework;
    using System.Threading.Tasks;
    using System.Numerics;

    public partial class PuzzleTests
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

        [Test]
        public async Task Puzzle10_Impl1_Part1()
        {
            var p = new Puzzles.Day10.Impl();
            await p.ResetInputsAsync();
            var a = await p.RunPart1Async();
            Assert.AreEqual(214, a);
        }

        [Test]
        public async Task Puzzle10_Impl1_Part2()
        {
            var p = new Puzzles.Day10.Impl();
            await p.ResetInputsAsync();
            var a = await p.RunPart2Async();
            Assert.AreEqual(502, a);
        }
    }
}