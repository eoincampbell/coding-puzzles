/*
 * Day 14: Space Stoichiometry
 * https://adventofcode.com/2019/day/14
 * Part 1: 1582325 ore for 1 fuel
 * Part 2: 2267486 fuel from 1 Trillion Ore
 */
using System.Collections.Generic;
namespace AdventOfCode2019.Puzzles.Day14
{
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Threading.Tasks;
    using Base;
    using TrackerDict = Dictionary<string, long>;
    using FormulaDict = Dictionary<string, (Chemical output, IEnumerable<Chemical> inputs)>;
    public class Impl : Puzzle<string, long>
    {
        private FormulaDict _formulae = new FormulaDict();
        private TrackerDict _lvls = new TrackerDict();
        private TrackerDict _amts = new TrackerDict();
        private const string FUEL = "FUEL";
        private const string ORE = "ORE";

        public Impl() : base("Day 14: Space Stoichiometry", ".\\Puzzles\\Day14\\Input.txt") { }
        
        public override async Task<long> RunPart1Async()
            => await Task.Run(() =>
            {
                Setup();
                _amts = new TrackerDict { { FUEL, 1 } };
                ProcessSubComponent();
                return _amts[ORE];
            });

        public override async Task<long> RunPart2Async()
            => await Task.Run(() =>
            {
                Setup();
                long tgt = 1_000_000_000_000, min = 0, max = 5_000_000, mid = (min + max) / 2;
                while (min <= max)
                {
                    _amts = new TrackerDict { { FUEL,  mid = (min + max) / 2 } };
                    ProcessSubComponent();
                    if (_amts[ORE] < tgt)  min = mid + 1;
                    if (_amts[ORE] > tgt)  max = mid - 1;
                }
                return mid;
            });

        private void SetLevels(string name, int lvl)
        {
            if (!_lvls.ContainsKey(name)) _lvls.Add(name, lvl);
            foreach(var n in _formulae[name].inputs.Select(s => s.Name))
            {
                if (_lvls.ContainsKey(n)) //only change the level if it'll push it lower in the hierarchy
                    _lvls[n] = (_lvls[n] < lvl + 1) ? _lvls[n] = lvl + 1 : _lvls[n];
                else
                    _lvls.Add(n, lvl + 1);
                
                if (n != ORE) SetLevels(n, lvl + 1); //don't recursively process ORE
            }
        }

        private void Setup()
        {
            _formulae = Inputs.Select(ParseFormula).ToDictionary(d => d.@out.Name, d => (d.@out, d.@in));
            _lvls = new TrackerDict();
            SetLevels("FUEL", 1);
        }

        private void ProcessSubComponent()
        {
            for(int cLvl = 1; cLvl <= _lvls.Values.Max(); cLvl++)
                foreach (var (name, lvl) in _lvls.Where(kv => kv.Value == cLvl && kv.Key != ORE))
                {
                    var (@out, @in) = _formulae[name];
                    var mul = (long)Math.Ceiling((double)_amts[name] / @out.Amount);
                    foreach (var (sn, amt) in @in.Select(s => (s.Name, s.Amount*mul)))
                        if (_amts.ContainsKey(sn))
                            _amts[sn] += amt;
                        else
                            _amts.Add(sn, amt);
                }
        }

        private static (Chemical @out, IEnumerable<Chemical> @in) ParseFormula(string s)
        {
            if (string.IsNullOrWhiteSpace(s)) throw new ArgumentNullException(nameof(s));
            var io = s.Split("=>");
            return (new Chemical(io[1]), io[0].Split(",").Select(i => new Chemical(i)));
        }
    }
    public class Chemical
    {
        public Chemical(string label)
        {
            if (string.IsNullOrWhiteSpace(label)) throw new ArgumentNullException(nameof(label));
            var p = label.Split(" ", StringSplitOptions.RemoveEmptyEntries);
            Name = p[1];
            Amount = long.Parse(p[0], CultureInfo.CurrentCulture);
        }
        public string Name { get; set; }
        public long Amount { get; set; }
        public override string ToString() => $"{Amount} {Name}";
    }
}