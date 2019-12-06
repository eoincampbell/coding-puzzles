namespace AdventOfCode2019.Puzzles.Day06
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Base;

    public class Impl : BasePuzzle<string, int>
    {
        public Impl() : base("Day 06: Universal Orbit Map", ".\\Puzzles\\Day06\\Input.txt") { }

        public struct Orbit
        {
            public string Center{get;set;}
            public string Planet{get;set;}
        }

        private readonly Dictionary<string, int> _orbitLevels = new Dictionary<string, int>();
        private readonly Dictionary<string, string> _orbits = new Dictionary<string, string>();

        public override async Task<int> RunPart1Async()
        {
            var inputs = Inputs.Select(s =>
            {
                var i = s.Split(')');
                return new Orbit {Center = i[0], Planet = i[1]};
            }).ToList();

            var planets = inputs.Select(p => p.Planet);
            var centerOfUniverse = inputs
                .Select(c => c.Center)
                .First(w => !planets.Contains(w));

            _orbitLevels.Add(centerOfUniverse, 0);
            
            var q = new Queue<Orbit>(inputs);

            while (q.Any())
            {
                var pair = q.Dequeue();
                if (!_orbitLevels.ContainsKey(pair.Center)) q.Enqueue(pair);
                else
                {
                    var orbitalLevel = _orbitLevels[pair.Center];
                    _orbitLevels.Add(pair.Planet, ++orbitalLevel);
                    _orbits.Add(pair.Planet, pair.Center);
                }
            }

            return _orbitLevels.Values.Sum(); //122782
        }

        public override async Task<int> RunPart2Async()
        {
            var youCurrPos = "YOU";
            var sanCurrPos = "SAN";

            var numTransfers = 0;

            while (true)
            {
                if (_orbitLevels[youCurrPos] > _orbitLevels[sanCurrPos])
                {
                    numTransfers++;
                    youCurrPos = _orbits[youCurrPos];
                }
                else if (_orbitLevels[youCurrPos] < _orbitLevels[sanCurrPos])
                {
                    numTransfers++;
                    sanCurrPos = _orbits[sanCurrPos];
                }
                else if (_orbitLevels[youCurrPos] == _orbitLevels[sanCurrPos] &&
                         _orbits[youCurrPos] != _orbits[sanCurrPos])
                {
                    numTransfers += 2;
                    youCurrPos = _orbits[youCurrPos];
                    sanCurrPos = _orbits[sanCurrPos];
                }
                else
                {
                    break;
                }
            }

            return numTransfers; //271
        }
            
    }
}