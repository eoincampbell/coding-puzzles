/*
 * Day 06: Universal Orbit Map
 * https://adventofcode.com/2019/day/6
 * Part 1: 122782
 * Part 2: 271
 */
namespace AdventOfCode2019.Puzzles.Day06
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Base;

    public class Impl : Puzzle<string, int>
    {
        public Impl() : base("Day 06: Universal Orbit Map", ".\\Puzzles\\Day06\\Input.txt") { }

        private Dictionary<string, int> _levels;
        private Dictionary<string, string> _orbits;
        private int _numTransfers = 0;

        public override async Task<int> RunPart1Async()
        {
            await DoWork();
            return _levels.Values.Sum();
        }

        public override async Task<int> RunPart2Async()
        {
            await DoWork();
            return _numTransfers;
        }
     
        
        public async Task DoWork()
        {
            _levels = new Dictionary<string, int>();
            _orbits = new Dictionary<string, string>();

            await Task.Run(() =>
            {
                var inputs = Inputs
                    .Select(s => s.Split(')'))
                    .Select(o => (Center: o[0], Planet: o[1]))
                    .ToList();

                var planets = inputs.Select(p => p.Planet);
                var cou = inputs.Select(c => c.Center).First(p => !planets.Contains(p));
                _levels.Add(cou, 0);

                var q = new Queue<(string Center, string Planet)>(inputs);

                while (q.Any())
                {
                    var pair = q.Dequeue();
                    if (!_levels.ContainsKey(pair.Center)) q.Enqueue(pair);
                    else
                    {
                        var orbitalLevel = _levels[pair.Center];
                        _levels.Add(pair.Planet, ++orbitalLevel);
                        _orbits.Add(pair.Planet, pair.Center);
                    }
                }

                string y = "YOU", s = "SAN";
                _numTransfers = 0;

                while (true)
                {
                    if (_levels[y] > _levels[s])
                    {
                        _numTransfers++;
                        y = _orbits[y];
                    }
                    else if (_levels[y] < _levels[s])
                    {
                        _numTransfers++;
                        s = _orbits[s];
                    }
                    else if (_levels[y] == _levels[s] &&
                             _orbits[y] != _orbits[s])
                    {
                        _numTransfers += 2;
                        y = _orbits[y];
                        s = _orbits[s];
                    }
                    else
                    {
                        break;
                    }
                }
            });
        }
    }
}