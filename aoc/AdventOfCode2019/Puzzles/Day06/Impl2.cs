namespace AdventOfCode2019.Puzzles.Day06
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Base;
    using QuickGraph;
    using QuickGraph.Algorithms;
    using Graph = QuickGraph.UndirectedGraph<string, QuickGraph.Edge<string>>;

    public class Impl2 : BasePuzzle<string, int>
    {
        public Impl2() : base("Day 06: Universal Orbit Map", ".\\Puzzles\\Day06\\Input.txt") { }

        private static double EdgeWeight(Edge<string> input) => 1;
        
        private Graph _graph;

        private void LoadGraph()
        {
            _graph = new Graph();
            _graph.AddVerticesAndEdgeRange(
                Inputs.Select(s => s.Split(')')).Select(o => new Edge<string>(o[0], o[1]))
            );
        }

        public override async Task<int> RunPart1Async()
        {
            LoadGraph();
            var distFunc = _graph.ShortestPathsDijkstra(EdgeWeight, "COM");
            return _graph.Vertices.Where(w => w != "COM")
                .Select(s => {
                    distFunc(s, out var path);
                    return path.Count();
                }).Sum();
        }
        
        public override async Task<int> RunPart2Async()
        {
            LoadGraph();
            var distFunc = _graph.ShortestPathsDijkstra(EdgeWeight, "YOU");
            var s = distFunc("SAN", out var path);
            return path.Count() - 2;
        }
            
    }
}