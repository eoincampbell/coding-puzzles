/*
 * Day 20: 
 * https://adventofcode.com/2019/day/20
 * Part 1: 
 * Part 2: 
 */
namespace AdventOfCode2019.Puzzles.Day20
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Base;
    using QuickGraph;
    using QuickGraph.Algorithms;
    using Graph = QuickGraph.UndirectedGraph<(int x, int y), QuickGraph.Edge<(int x, int y)>>;
    using RGraph = QuickGraph.UndirectedGraph<(int x, int y, int lvl), QuickGraph.Edge<(int x, int y, int lvl)>>;

    public class Impl : Puzzle<string, int>
    {
        public Impl() : base("Day 20: ", ".\\Puzzles\\Day20\\Input.txt")
        {
            _c = new Dictionary<(int x, int y),  (string portal, bool outward)>();
            _graph = new Graph();
            _rGraph = new RGraph();
        }

        private readonly Dictionary<(int x, int y), (string portal, bool outward)> _c;
        private readonly Graph _graph;
        private readonly RGraph _rGraph;

        public override async Task<int> RunPart1Async() => await Task.Run(() =>
        {
            PopulateCoords();
            PopulateGraph();

            var start = _c.First(w => w.Value.portal == "AA").Key;
            var end = _c.First(w => w.Value.portal == "ZZ").Key;

            var distFunc = _graph.ShortestPathsDijkstra(EdgeWeight, start);
            var s = distFunc(end, out var path);
            var p = path.Count();

            return p;
        });

        

        public override async Task<int> RunPart2Async() => await Task.Run(() =>
        {
            PopulateCoords();
            
            PopulateRGraph();

            var start = _c.First(w => w.Value.portal == "AA").Key;
            var end = _c.First(w => w.Value.portal == "ZZ").Key;

            var distFunc = _graph.ShortestPathsDijkstra(EdgeWeight, start);
            var s = distFunc(end, out var path);
            var p = path.Count();

            return p;
        });

        private static (int x, int y)[] _adjs = { (0, -1), (0, 1), (-1, 0), (1, 0) };

        private void PopulateGraph()
        {
            for (var y = 0; y < Inputs.Count; y++)
            for (var x = 0; x < Inputs[y].Length; x++)
                if (Inputs[y][x] == '.')
                {
                    foreach(var (xx, yy) in _adjs)
                        if(Inputs[y+yy][x+xx] == '.')
                            _graph.AddVerticesAndEdge(new Edge<(int x, int y)>((x, y), (x+xx, y+yy)));

                    if (!_c.ContainsKey((x, y)) || string.IsNullOrEmpty(_c[(x, y)].portal) || _c[(x, y)].portal == "AA" || _c[(x, y)].portal == "ZZ") continue;
                    var o = _c.FirstOrDefault(w => w.Value == _c[(x, y)] && w.Key != (x, y));
                    _graph.AddVerticesAndEdge(new Edge<(int x, int y)>((x,y), o.Key));
                }
        }

        private void PopulateRGraph()
        {
            //for (var y = 0; y < Inputs.Count; y++)
            //for (var x = 0; x < Inputs[y].Length; x++)
            //    if (Inputs[y][x] == '.')
            //    {
            //        foreach(var (xx, yy) in _adjs)
            //            if(Inputs[y+yy][x+xx] == '.')
            //                _rGraph.AddVerticesAndEdge(new Edge<(int x, int y, int lvl)>((x, y, 0), (x+xx, y+yy, 0)));

            //        if (!_c.ContainsKey((x, y)) || string.IsNullOrEmpty(_c[(x, y)].portal)|| _c[(x, y)].portal == "AA" || _c[(x, y)].portal == "ZZ") continue;
            //        var o = _c.FirstOrDefault(w => w.Value == _c[(x, y)] && w.Key != (x, y));
            //        _rGraph.AddVerticesAndEdge(new Edge<(int x, int y, int lvl)>((x,y,0), (o.Key.x, o.Key.y, 0)));
            //    }
        }
        
        private static double EdgeWeight(Edge<(int x, int y)> input) => 1;

        private void PopulateCoords()
        {
            for (var y = 0; y < Inputs.Count; y++)
            for (var x = 0; x < Inputs[y].Length; x++)
                if (Inputs[y][x] == '.')
                    _c.Add((x, y), (CheckForPortals(x, y), false));
        }

        private string CheckForPortals(int x, int y)
        { 
            //north
            if (IsLetter(Inputs[y - 1][x])) return GetPortal(x, y - 2, x, y - 1); 
            //south
            if (IsLetter(Inputs[y + 1][x])) return GetPortal(x, y + 1, x, y + 2);
            //east
            if (IsLetter(Inputs[y][x + 1])) return GetPortal(x +1, y, x + 2, y );
            //west
            if (IsLetter(Inputs[y][x - 1])) return GetPortal(x - 2, y, x - 1, y);
            

            return string.Empty;
        }

        public string GetPortal(int x1, int y1, int x2, int y2) => string.Join("", Inputs[y1][x1], Inputs[y2][x2]);

        private static bool IsLetter(char c) => c >= 'A' && c <= 'Z';
    }
}