namespace AdventOfCode2019.Tests.Day04
{
    using NUnit.Framework;
    using System.Threading.Tasks;
    using System.Numerics;

    public partial class PuzzleTests
    {
       
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
    }
}