
namespace AdventOfCode2019.Tests
{
    using NUnit.Framework;
    using System.Threading.Tasks;
    using System.Numerics;

    public partial class PuzzleTests
    {
        [Test]
        public async Task Puzzle11_Impl1_Part1()
        {
            var p = new Puzzles.Day11.Impl();
            await p.ResetInputsAsync();
            var a = await p.RunPart1Async();
            Assert.AreEqual(1564, a);
        }

        [Test]
        public async Task Puzzle11_Impl1_Part2()
        {
            var p = new Puzzles.Day11.Impl();
            await p.ResetInputsAsync();
            var a = await p.RunPart2Async();
            Assert.AreEqual(258, a);
        }

        [Test]
        public async Task Puzzle12_Impl1_Part1()
        {
            var p = new Puzzles.Day12.Impl();
            await p.ResetInputsAsync();
            var a = await p.RunPart1Async();
            Assert.AreEqual(12644, a);
        }

        [Test]
        public async Task Puzzle12_Impl1_Part2()
        {
            var p = new Puzzles.Day12.Impl();
            await p.ResetInputsAsync();
            var a = await p.RunPart2Async();
            Assert.AreEqual(290314621566528, a);
        }

        [Test]
        public async Task Puzzle12_Impl2_Part1()
        {
            var p = new Puzzles.Day12.Impl2();
            await p.ResetInputsAsync();
            var a = await p.RunPart1Async();
            Assert.AreEqual(12644, a);
        }

        [Test]
        public async Task Puzzle12_Impl2_Part2()
        {
            var p = new Puzzles.Day12.Impl2();
            await p.ResetInputsAsync();
            var a = await p.RunPart2Async();
            Assert.AreEqual(290314621566528, a);
        }

        [Test]
        [Ignore("Doesn't Work")]
        public async Task Puzzle12_Impl3_Part1()
        {
            var p = new Puzzles.Day12.Impl3();
            await p.ResetInputsAsync();
            var a = await p.RunPart1Async();
            Assert.AreEqual(12644, a);
        }

        [Test]
        [Ignore("Doesn't Work")]
        public async Task Puzzle12_Impl3_Part2()
        {
            var p = new Puzzles.Day12.Impl3();
            await p.ResetInputsAsync();
            var a = await p.RunPart2Async();
            Assert.AreEqual(290314621566528, a);
        }

        [Test]
        public async Task Puzzle13_Impl1_Part1()
        {
            var p = new Puzzles.Day13.Impl();
            await p.ResetInputsAsync();
            var a = await p.RunPart1Async();
            Assert.AreEqual(0, a);
        }

        [Test]
        public async Task Puzzle13_Impl1_Part2()
        {
            var p = new Puzzles.Day13.Impl();
            await p.ResetInputsAsync();
            var a = await p.RunPart2Async();
            Assert.AreEqual(0, a);
        }

        [Test]
        public async Task Puzzle14_Impl1_Part1()
        {
            var p = new Puzzles.Day14.Impl();
            await p.ResetInputsAsync();
            var a = await p.RunPart1Async();
            Assert.AreEqual(0, a);
        }

        [Test]
        public async Task Puzzle14_Impl1_Part2()
        {
            var p = new Puzzles.Day14.Impl();
            await p.ResetInputsAsync();
            var a = await p.RunPart2Async();
            Assert.AreEqual(0, a);
        }

        [Test]
        public async Task Puzzle15_Impl1_Part1()
        {
            var p = new Puzzles.Day15.Impl();
            await p.ResetInputsAsync();
            var a = await p.RunPart1Async();
            Assert.AreEqual(0, a);
        }

        [Test]
        public async Task Puzzle15_Impl1_Part2()
        {
            var p = new Puzzles.Day15.Impl();
            await p.ResetInputsAsync();
            var a = await p.RunPart2Async();
            Assert.AreEqual(0, a);
        }
    }
}