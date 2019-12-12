namespace AdventOfCode2019.Tests
{
    using NUnit.Framework;
    using System.Threading.Tasks;
    using System.Numerics;

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

        [Test]
        public async Task Puzzle03_Impl1_Part1()
        {
            var p = new Puzzles.Day03.Impl();
            await p.ResetInputsAsync();
            var a = await p.RunPart1Async();
            Assert.AreEqual(399, a);
        }

        [Test]
        public async Task Puzzle03_Impl1_Part2()
        {
            var p = new Puzzles.Day03.Impl();
            await p.ResetInputsAsync();
            var a = await p.RunPart2Async();
            Assert.AreEqual(15678, a);
        }

        [Test]
        public async Task Puzzle04_Impl1_Part1()
        {
            var p = new Puzzles.Day04.Impl();
            await p.ResetInputsAsync();
            var a = await p.RunPart1Async();
            Assert.AreEqual(1653, a);
        }

        [Test]
        public async Task Puzzle04_Impl1_Part2()
        {
            var p = new Puzzles.Day04.Impl();
            await p.ResetInputsAsync();
            var a = await p.RunPart2Async();
            Assert.AreEqual(1133, a);
        }

        [Test]
        public async Task Puzzle04_Imp2_Part1()
        {
            var p = new Puzzles.Day04.Impl2();
            await p.ResetInputsAsync();
            var a = await p.RunPart1Async();
            Assert.AreEqual(1653, a);
        }

        [Test]
        public async Task Puzzle04_Imp2_Part2()
        {
            var p = new Puzzles.Day04.Impl2();
            await p.ResetInputsAsync();
            var a = await p.RunPart2Async();
            Assert.AreEqual(1133, a);
        }

        [Test]
        public async Task Puzzle04_Imp3_Part1()
        {
            var p = new Puzzles.Day04.Impl3();
            await p.ResetInputsAsync();
            var a = await p.RunPart1Async();
            Assert.AreEqual(1653, a);
        }

        [Test]
        public async Task Puzzle04_Imp3_Part2()
        {
            var p = new Puzzles.Day04.Impl3();
            await p.ResetInputsAsync();
            var a = await p.RunPart2Async();
            Assert.AreEqual(1133, a);
        }

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