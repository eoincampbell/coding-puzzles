namespace AdventOfCode2018.Puzzles.Day4
{
    using Base;
    using System;
    using System.Collections.Concurrent;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;

    public class Impl : BasePuzzle
    {
        public Impl() : base("Day 4 ", ".\\Puzzles\\Day4\\Input.txt") { }

        public override async Task<string> RunPart1()
        {
            ProcessInputs();

            var bestGuard = _guardData
                .Where(gg => gg.Value.Any())
                .OrderByDescending(g => g.Value.Sum(gd => gd.Value))
                .First();

            var bestMinute = bestGuard
                .Value
                .OrderByDescending(g => g.Value)
                .First();

            return await Task.FromResult($"#{bestGuard.Key} * {bestMinute.Key} = {(bestGuard.Key * bestMinute.Key)}");
        }

        public override async Task<string> RunPart2()
        {
            ProcessInputs();

            var bestGuard = _guardData
                .Where(gg => gg.Value.Any())
                .OrderByDescending(g => g.Value.Max(gd => gd.Value))
                .First();

            var bestMinute = bestGuard
                .Value
                .OrderByDescending(g => g.Value)
                .First();

            return await Task.FromResult($"#{bestGuard.Key} * {bestMinute.Key} = {(bestGuard.Key * bestMinute.Key)}");
        }

        private ConcurrentDictionary<int, ConcurrentDictionary<int, int>> _guardData;

        private class ActionInput
        {
            public DateTime EventTime { get; set; }
            public string Action { get; set; }
        }

        private void ProcessInputs()
        {
            var inputRegex = new Regex(@"^\[(\d{0,4}-\d{0,2}-\d{0,2} \d{0,2}:\d{0,2})\] (.*)$");
            var shiftRegex = new Regex(@"^Guard #(\d{0,4}) begins shift$");

            var orderedInputs = Inputs
                .Select(i => inputRegex.Match(i))
                .Where(match => match.Success)
                .Select(m => new ActionInput
                {
                    EventTime = DateTime.Parse(m.Groups[1].Value),
                    Action = m.Groups[2].Value
                })
                .OrderBy(o => o.EventTime)
                .ToList();

            _guardData = new ConcurrentDictionary<int, ConcurrentDictionary<int, int>>(); //<GuardId, <Minute, Counter>>
            var currentGuardData = new ConcurrentDictionary<int, int>(); //<Minute, Counter>
            var startMinute = 0;

            foreach (var o in orderedInputs)
            {
                var s = shiftRegex.Match(o.Action);

                if (s.Success)
                {
                    currentGuardData = _guardData.GetOrAdd(int.Parse(s.Groups[1].Value), key => new ConcurrentDictionary<int, int>());
                }
                else if (o.Action == "falls asleep")
                {
                    startMinute = o.EventTime.Minute;
                }
                else if (o.Action == "wakes up")
                {
                    Enumerable
                        .Range(startMinute, o.EventTime.Minute - startMinute)
                        .ToList()
                        .ForEach(min => currentGuardData.AddOrUpdate(min, 1, (k, v) => v + 1));
                }
            }
        }
    }
}