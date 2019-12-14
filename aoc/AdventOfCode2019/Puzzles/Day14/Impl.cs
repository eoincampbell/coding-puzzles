/*
 * Day 14: Space Stoichiometry
 * https://adventofcode.com/2019/day/14
 * Part 1: 1582325 ore for 1 fuel
 * Part 2: 2267486 fuel from 1 Trillion Ore
 */
namespace AdventOfCode2019.Puzzles.Day14
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Threading.Tasks;
    using Base;

    public class Impl : Puzzle<string, long>
    {
        public Impl() : base("Day 14: Space Stoichiometry", ".\\Puzzles\\Day14\\Input.txt")
        {
            _lvls = new Dictionary<string, int>();
            _formulae = new Dictionary<string, (Chemical output, List<Chemical> inputs)>();
        }

        private Dictionary<string, (Chemical output, List<Chemical> inputs)> _formulae;
        private Dictionary<string, int> _lvls;

        public override async Task<long> RunPart1Async()
            => await Task.Run(() =>
            {
                Setup();
                var amounts = new Dictionary<string, long> { { "FUEL", 1 } };
                ProcessSubComponent(amounts);
                return amounts["ORE"];
            });



        public override async Task<long> RunPart2Async()
            => await Task.Run(() =>
            {
                Setup();
                long target = 1_000_000_000_000, ore = 0, min = 0, max = 5_000_000, mid = (min + max) / 2;
                var amounts = new Dictionary<string, long>();
                while (min <= max)
                {
                    mid = (min + max) / 2;
                    amounts = new Dictionary<string, long> { { "FUEL", mid } };
                    ProcessSubComponent(amounts);
                    ore = amounts["ORE"];

                    if (ore < target)
                        min = mid + 1;
                    else if (ore > target)
                        max = mid - 1;
                }
                return mid;
            });

        private void SetLevels(string name, int level)
        {
            foreach(var sc in _formulae[name].inputs)
            {
                if (_lvls.ContainsKey(sc.Name))
                {
                    if(_lvls[sc.Name] < level + 1) //only change the level if it'll push it lower in the hierarchy
                        _lvls[sc.Name] = level + 1;
                }
                else
                    _lvls.Add(sc.Name, level + 1);
                
                if (sc.Name != "ORE") SetLevels(sc.Name, level + 1); //don't recursively process ORE
            }
        }

        private void Setup()
        {
            _formulae = new Dictionary<string, (Chemical output, List<Chemical> inputs)>();
            foreach (var (o,i) in Inputs.Select(ParseFormula))
                _formulae.Add(o.Name, (o, i));

            _lvls = new Dictionary<string, int>() { { "FUEL", 1 } };
            SetLevels("FUEL", 1);
        }

        private void ProcessSubComponent(Dictionary<string, long> _amounts)
        {
            for(int currLevel = 1; currLevel <= _lvls.Values.Max(); currLevel++)
            {
                foreach (var (name, lvl) in _lvls.Where(kv => kv.Value == currLevel && kv.Key != "ORE"))
                {
                    var (output, inputs) = _formulae[name];
                    var amountRequired = _amounts[name];
                    var multiplier = (long)Math.Ceiling((double)amountRequired / output.Amount);
                    foreach (var i in inputs)
                        if (_amounts.ContainsKey(i.Name))
                            _amounts[i.Name] += (i.Amount * multiplier);
                        else
                            _amounts.Add(i.Name, i.Amount * multiplier);
                }
            }
        }

        private static (Chemical output, List<Chemical> inputs) ParseFormula(string s)
        {
            if (string.IsNullOrWhiteSpace(s)) throw new ArgumentNullException(nameof(s));

            var ss = s.Split("=>");
            var oChem = new Chemical(ss[1]);
            var iChems = new List<Chemical>();
            var inputs = ss[0].Split(",");
            foreach (var i in inputs)
                iChems.Add(new Chemical(i.Trim()));

            return (oChem, iChems);
        }

        private class Chemical
        {
            public Chemical(string label)
            {
                if (string.IsNullOrWhiteSpace(label))
                    throw new ArgumentNullException(nameof(label));

                var p = label.Trim().Split(" ");
                Name = p[1].Trim();
                Amount = long.Parse(p[0].Trim(), CultureInfo.CurrentCulture);
            }
            public string Name { get; set; }
            public long Amount { get; set; }
            public override string ToString() => $"{Amount} {Name}";
        }
    }
}