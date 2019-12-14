/*
 * Day 14: Space Stoichiometry
 * https://adventofcode.com/2019/day/14
 * Part 1: 1582325
 * Part 2: 
 */
using System.Collections.Generic;

namespace AdventOfCode2019.Puzzles.Day14
{
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Threading.Tasks;
    using Base;

    public class Impl : Puzzle<string, long>
    {
        public Impl() : base("Day 14: Space Stoichiometry", ".\\Puzzles\\Day14\\Input.txt")
        {
            _componentLevels = new Dictionary<string, int>();
            _formulae = new Dictionary<string, (Chemical output, List<Chemical> inputs)>();
        }

        private Dictionary<string, (Chemical output, List<Chemical> inputs)> _formulae;
        private Dictionary<string, int> _componentLevels;

        public override async Task<long> RunPart1Async()
            => await Task.Run(() =>
            {
                Setup();
                
                var amounts = new Dictionary<string, long>();
                amounts.Add("FUEL", 1);
                ProcessSubComponent(amounts);

                return amounts["ORE"];
            });



        public override async Task<long> RunPart2Async()
            => await Task.Run(() =>
            {
                return 0;
                
            });


        private void SetLevels(Chemical component, int level)
        {
            var (o, i) = _formulae[component.Name];

            foreach(var sc in i)
            {
                if (_componentLevels.ContainsKey(sc.Name))
                {
                    if(_componentLevels[sc.Name] < level + 1)
                        _componentLevels[sc.Name] = level + 1;
                }
                else
                    _componentLevels.Add(sc.Name, level + 1);
                
                if (sc.Name != "ORE")
                    SetLevels(_formulae[sc.Name].output, level + 1);
            }

        }
        private void Setup()
        {
            _componentLevels = new Dictionary<string, int>();
            _formulae = new Dictionary<string, (Chemical output, List<Chemical> inputs)>();

            foreach (var i in Inputs)
            {
                var (output, inputs) = ParseFormula(i);
                _formulae.Add(output.Name, (output, inputs));
            }

            _componentLevels.Add("FUEL", 1);
            SetLevels(_formulae["FUEL"].output, 1);
        }


        private void ProcessSubComponent(Dictionary<string, long> _amounts)
        {
            for(int currLevel = 1; currLevel <= _componentLevels.Values.Max(); currLevel++)
            {
                var components = _componentLevels.Where(kv => kv.Value == currLevel && kv.Key != "ORE");

                foreach (var c in components)
                {
                    var (o, i) = _formulae[c.Key];
                    var amountRequired = _amounts[c.Key];
                    var multiplier = (int)Math.Ceiling((double)amountRequired / o.Amount);

                    foreach (var subc in i)
                        if (_amounts.ContainsKey(subc.Name))
                            _amounts[subc.Name] += (subc.Amount * multiplier);
                        else
                            _amounts.Add(subc.Name, subc.Amount * multiplier);
                }
            }
        }

        private static (Chemical output, List<Chemical> inputs) ParseFormula(string s)
        {
            if (string.IsNullOrWhiteSpace(s))
                throw new ArgumentNullException(nameof(s));

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