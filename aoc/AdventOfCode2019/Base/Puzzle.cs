﻿namespace AdventOfCode2019.Base
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Threading.Tasks;
    using System.Linq;
    using System.Collections.ObjectModel;
    using System.Globalization;
    using System.Collections.Generic;

    public interface IPuzzle
    {
        string Name { get; }
        Task RunBothPartsAsync();
    }

    public abstract class Puzzle<TInput, TOutput> : IPuzzle
    {
        private readonly string _inputFile;
        private readonly Stopwatch _stopWatch;
        protected ReadOnlyCollection<TInput> Inputs { get; private set; }
        public string Name { get; }

        public abstract Task<TOutput> RunPart1Async();
        public abstract Task<TOutput> RunPart2Async();

        protected Puzzle(string name, string inputFile)
        {
            Name = name;
            Inputs = (new List<TInput>()).AsReadOnly();
            _inputFile = inputFile;
            _stopWatch = new Stopwatch();
        }

        public async Task ResetInputsAsync() =>
            Inputs = (await File.ReadAllLinesAsync(_inputFile))
                .Select(s => (TInput)Convert.ChangeType(s, typeof(TInput), CultureInfo.CurrentCulture))
                .ToList()
                .AsReadOnly();

        private async Task RunPart(int part, Func<Task<TOutput>> func)
        {
            await ResetInputsAsync();
            _stopWatch.Reset();
            _stopWatch.Start();
            var result = await func();
            _stopWatch.Stop();

            Console.WriteLine($"{Name,-50} | Part {part} | Exec: {_stopWatch.Elapsed:c} | {result}");
        }

        public async Task RunBothPartsAsync()
        {
            await RunPart(1, RunPart1Async);
            await RunPart(2, RunPart2Async);
        }
    }
}
