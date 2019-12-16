namespace AdventOfCode2019.Tests.Day12
{
    using NUnit.Framework;
    using System.Threading.Tasks;

    public  class PuzzleTests
    {
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
    }
}