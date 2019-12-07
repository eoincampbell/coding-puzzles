using NUnit.Framework;
using System.Threading.Tasks;

namespace AdventOfCode2019.Tests
{
    public class PuzzleTests
    {
        [SetUp]
        public void Setup()
        {
        }

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
            Assert.AreEqual(16225258, a);
        }


        [Test]
        public async Task Puzzle05_Impl1_Part2()
        {
            var p = new Puzzles.Day05.Impl();
            await p.ResetInputsAsync();
            var a = await p.RunPart2Async();
            Assert.AreEqual(2808771, a);
        }

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
            Assert.AreEqual(22012, a);
        }


        [Test]
        public async Task Puzzle07_Impl1_Part2()
        {
            var p = new Puzzles.Day07.Impl();
            await p.ResetInputsAsync();
            var a = await p.RunPart2Async();
            Assert.AreEqual(4039164, a);
        }

        [Test]
        public async Task Puzzle8_Impl1_Part1()
        {
            var p = new Puzzles.Day08.Impl();
            await p.ResetInputsAsync();
            var a = await p.RunPart1Async();
            Assert.AreEqual(0, a);
        }
        [Test]
        public async Task Puzzle8_Impl1_Part2()
        {
            var p = new Puzzles.Day08.Impl();
            await p.ResetInputsAsync();
            var a = await p.RunPart2Async();
            Assert.AreEqual(0, a);
        }

        [Test]
        public async Task Puzzle9_Impl1_Part1()
        {
            var p = new Puzzles.Day09.Impl();
            await p.ResetInputsAsync();
            var a = await p.RunPart1Async();
            Assert.AreEqual(0, a);
        }
        [Test]
        public async Task Puzzle9_Impl1_Part2()
        {
            var p = new Puzzles.Day09.Impl();
            await p.ResetInputsAsync();
            var a = await p.RunPart2Async();
            Assert.AreEqual(0, a);
        }

        [Test]
        public async Task Puzzle10_Impl1_Part1()
        {
            var p = new Puzzles.Day10.Impl();
            await p.ResetInputsAsync();
            var a = await p.RunPart1Async();
            Assert.AreEqual(0, a);
        }
        [Test]
        public async Task Puzzle10_Impl1_Part2()
        {
            var p = new Puzzles.Day10.Impl();
            await p.ResetInputsAsync();
            var a = await p.RunPart2Async();
            Assert.AreEqual(0, a);
        }

        [Test]
        public async Task Puzzle11_Impl1_Part1()
        {
            var p = new Puzzles.Day11.Impl();
            await p.ResetInputsAsync();
            var a = await p.RunPart1Async();
            Assert.AreEqual(0, a);
        }
        [Test]
        public async Task Puzzle11_Impl1_Part2()
        {
            var p = new Puzzles.Day11.Impl();
            await p.ResetInputsAsync();
            var a = await p.RunPart2Async();
            Assert.AreEqual(0, a);
        }

        [Test]
        public async Task Puzzle12_Impl1_Part1()
        {
            var p = new Puzzles.Day12.Impl();
            await p.ResetInputsAsync();
            var a = await p.RunPart1Async();
            Assert.AreEqual(0, a);
        }
        [Test]
        public async Task Puzzle12_Impl1_Part2()
        {
            var p = new Puzzles.Day12.Impl();
            await p.ResetInputsAsync();
            var a = await p.RunPart2Async();
            Assert.AreEqual(0, a);
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

    }
}