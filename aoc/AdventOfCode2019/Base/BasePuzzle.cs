namespace AdventOfCode2019.Base
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Threading.Tasks;
    using System.Linq;

    public abstract class BasePuzzle<TInput, TOutput> : IPuzzle
    {
        private readonly string _inputFile;
        private readonly Stopwatch _stopWatch;
        protected TInput[] Inputs;
        public string Name { get; }

        public abstract Task<TOutput> RunPart1Async();
        public abstract Task<TOutput> RunPart2Async();

        protected BasePuzzle(string name, string inputFile)
        {
            Name = name;
            _inputFile = inputFile;
            _stopWatch = new Stopwatch();
        }

        private async Task ResetInputsAsync() =>
            Inputs = (await File.ReadAllLinesAsync(_inputFile))
                .Select(s => (TInput) Convert.ChangeType(s, typeof(TInput)))
                .ToArray();

        private async Task RunPart(int part, Func<Task<TOutput>> func)
        {
            await ResetInputsAsync();
            _stopWatch.Reset();
            _stopWatch.Start();
            var result = await func();
            _stopWatch.Stop();

            Console.WriteLine($"{Name} | Part {part} | Exec: {_stopWatch.Elapsed:c} | {result}");
        }

        public async Task RunBothPartsAsync()
        {
            await RunPart(1, RunPart1Async);
            await RunPart(2, RunPart2Async);
        }
    }
}
