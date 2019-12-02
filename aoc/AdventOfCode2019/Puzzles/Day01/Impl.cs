namespace AdventOfCode2019.Puzzles.Day01
{
    using Base;
    using System.Linq;
    using System.Threading.Tasks;

    public class Impl: BasePuzzle<int, int>
    {
        public Impl() : base("Day 1 ", ".\\Puzzles\\Day01\\Input.txt") { }
        
        //3481005
        public override async Task<int> RunPart1Async()
            => await Task.Run(() => Inputs.Select(CalcFuel).Sum());

        //5218616
        public override async Task<int> RunPart2Async() 
            => await Task.Run(() => Inputs.Select(CalcFuelForFuel).Sum());

        private static int CalcFuel(int mass) 
            => (mass / 3) - 2;

        private static int CalcFuelForFuel(int mass) 
            => CalcFuel(mass) > 0 
                ? CalcFuel(mass) + CalcFuelForFuel(CalcFuel(mass)) 
                : 0;
    }
}
