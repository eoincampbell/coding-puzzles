
namespace AdventOfCode2019.Tests
{
    using NUnit.Framework;
    using System.Threading.Tasks;
    using System.Numerics;

    public partial class PuzzleTests
    {
        [Test]
        public async Task Puzzle16_Impl1_Part1()
        {
            var p = new Puzzles.Day16.Impl();
            await p.ResetInputsAsync();
            var a = await p.RunPart1Async();
            Assert.AreEqual(0, a);
        }

        [Test]
        public async Task Puzzle16_Impl1_Part2()
        {
            var p = new Puzzles.Day16.Impl();
            await p.ResetInputsAsync();
            var a = await p.RunPart2Async();
            Assert.AreEqual(0, a);
        }

        [Test]
        public async Task Puzzle117_Impl1_Part1()
        {
            var p = new Puzzles.Day17.Impl();
            await p.ResetInputsAsync();
            var a = await p.RunPart1Async();
            Assert.AreEqual(0, a);
        }

        [Test]
        public async Task Puzzle17_Impl1_Part2()
        {
            var p = new Puzzles.Day17.Impl();
            await p.ResetInputsAsync();
            var a = await p.RunPart2Async();
            Assert.AreEqual(0, a);
        }

        [Test]
        public async Task Puzzle18_Impl1_Part1()
        {
            var p = new Puzzles.Day18.Impl();
            await p.ResetInputsAsync();
            var a = await p.RunPart1Async();
            Assert.AreEqual(0, a);
        }

        [Test]
        public async Task Puzzle18_Impl1_Part2()
        {
            var p = new Puzzles.Day18.Impl();
            await p.ResetInputsAsync();
            var a = await p.RunPart2Async();
            Assert.AreEqual(0, a);
        }

        [Test]
        public async Task Puzzle19_Impl1_Part1()
        {
            var p = new Puzzles.Day19.Impl();
            await p.ResetInputsAsync();
            var a = await p.RunPart1Async();
            Assert.AreEqual(0, a);
        }

        [Test]
        public async Task Puzzle19_Impl1_Part2()
        {
            var p = new Puzzles.Day19.Impl();
            await p.ResetInputsAsync();
            var a = await p.RunPart2Async();
            Assert.AreEqual(0, a);
        }

        [Test]
        public async Task Puzzle20_Impl1_Part1()
        {
            var p = new Puzzles.Day20.Impl();
            await p.ResetInputsAsync();
            var a = await p.RunPart1Async();
            Assert.AreEqual(0, a);
        }

        [Test]
        public async Task Puzzle20_Impl1_Part2()
        {
            var p = new Puzzles.Day20.Impl();
            await p.ResetInputsAsync();
            var a = await p.RunPart2Async();
            Assert.AreEqual(0, a);
        }
    }
}