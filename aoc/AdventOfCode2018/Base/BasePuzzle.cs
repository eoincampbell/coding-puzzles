namespace AdventOfCode2018.Base
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Threading.Tasks;

    public abstract class BasePuzzle : IPuzzle
    {
        private readonly string _inputFile;

        private readonly Stopwatch _stopWatch;

        protected string[] Inputs;

        protected BasePuzzle(string name, string inputFile)
        {
            Name = name;
            _inputFile = inputFile;
            _stopWatch = new Stopwatch();
        }

        private void ResetInputs()
        {
            _stopWatch.Reset();
            _stopWatch.Start();
            Inputs = File.ReadAllLines(_inputFile);
        }

        public string Name { get; }

        public async Task RunBothParts()
        {
            //Part 1
            ResetInputs();
            _stopWatch.Reset();
            _stopWatch.Start();
            var p1Result = await RunPart1();
            _stopWatch.Stop();

            Console.WriteLine($"{Name} Part 1 | Exec: {_stopWatch.Elapsed:c} | {p1Result}");

            //Part 2
            ResetInputs();
            _stopWatch.Reset();
            _stopWatch.Start();
            var p2Result = await RunPart2();
            _stopWatch.Stop();

            Console.WriteLine($"{Name} Part 2 | Exec: {_stopWatch.Elapsed:c} | {p2Result}");
        }


        public abstract Task<string> RunPart1();
        public abstract Task<string> RunPart2();
    }
}
