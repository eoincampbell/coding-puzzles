namespace AdventOfCode2019.Base
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Threading.Tasks;
    using System.Linq;
    using System.Collections.Generic;

    public abstract class BasePuzzle<TInput, TOutput> : IPuzzle<TOutput>
    {
        private readonly string _inputFile;

        private readonly Stopwatch _stopWatch;

        protected IEnumerable<TInput> Inputs;

        protected BasePuzzle(string name, string inputFile)
        {
            Name = name;
            _inputFile = inputFile;
            _stopWatch = new Stopwatch();
        }

        private async Task ResetInputsAsync()
        {
            var inputs = await File.ReadAllLinesAsync(_inputFile); 
            Inputs = inputs.Select(s => (TInput)Convert.ChangeType(s, typeof(TInput)));
        }

        public string Name { get; }

        public async Task RunBothPartsAsync()
        {
            //Part 1
            await ResetInputsAsync();
            _stopWatch.Reset();
            _stopWatch.Start();
            var p1Result = await RunPart1Async();
            _stopWatch.Stop();

            Console.WriteLine($"{Name} | Part 1 | Exec: {_stopWatch.Elapsed:c} | {p1Result}");

            //Part 2
            await ResetInputsAsync();
            _stopWatch.Reset();
            _stopWatch.Start();
            var p2Result = await RunPart2Async();
            _stopWatch.Stop();

            Console.WriteLine($"{Name} | Part 2 | Exec: {_stopWatch.Elapsed:c} | {p2Result}");
        }


        public abstract Task<TOutput> RunPart1Async();
        public abstract Task<TOutput> RunPart2Async();
    }
}
