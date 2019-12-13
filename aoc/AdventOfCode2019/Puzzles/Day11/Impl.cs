/*
 * Day 11: Space Police
 * https://adventofcode.com/2019/day/11
 * Part 1: 1564
 * Part 2: 249 (258 because of GetOrAdd) - Prints RFEBCFEB
 */
namespace AdventOfCode2019.Puzzles.Day11
{
    using System;
    using System.Collections.Concurrent;
    using System.Threading.Tasks;
    using Base;
    using Base.IntCode;

    public class Impl : Puzzle<string, int>
    {
        private const char White = '#';
        private const char Black = ' ';
        private int _dirIdx;
        private int _x;
        private int _y;
        private int _minX;
        private int _minY;
        private int _maxX;
        private int _maxY;
        private int _color;
        private VmState _halted;
        private IntCodeVm _robotBrain;
        private ConcurrentDictionary<(int x, int y), int> _pnls;
        private static readonly (int x, int y) [] Dirs = 
        {
            (0, -1), //N
            (1, 0),  //E
            (0, 1),  //S
            (-1, 0)  //W
        };

        public Impl() : base("Day 11: Space Police", ".\\Puzzles\\Day11\\Input.txt") { }

        public override async Task<int> RunPart1Async() => await Task.Run(() 
            => {
                Reset(0); //part 1 - start on black
                while (_halted != VmState.Halted) PaintPanel();

                return _pnls.Keys.Count;
            });

        public override async Task<int> RunPart2Async() => await Task.Run(() 
            => {
                Reset(1); //part 2 - start on white
                while (_halted != VmState.Halted) PaintPanel();
                
                ShowPaint();
                return _pnls.Keys.Count;
            });

        private void Reset(int color)
        {
            _pnls = new ConcurrentDictionary<(int x, int y), int>();
            _robotBrain = new IntCodeVm(Inputs[0]);
            _x = _y = _dirIdx = _minX = _maxX = _minY = _maxY = 0;
            _halted = VmState.Running;
            _color = color;
        }

        private int ReadColorFromCamera()
            => _pnls.ContainsKey((_x,_y)) ? _pnls[(_x, _y)] : 0;

        private void ApplyPaintToPanel(int color) 
            =>_pnls.AddOrUpdate((_x, _y), color, (x,y) => color);

        private void PaintPanel()
        {
            _robotBrain.SetInput(_color);                       //Input
            _halted = _robotBrain.RunProgramPauseAtOutput();    //Panel Coloring
            if (_halted == VmState.Halted) return;                                //Bail if Halted
            _color = (int) _robotBrain.GetOutput();
            ApplyPaintToPanel(_color);
            _robotBrain.RunProgramPauseAtOutput();              //Turning & Movement
            Move(_robotBrain.GetOutput() == 0 ? -1 : 1);        //Convert Left 0 into a -1 for indexing
            _color= ReadColorFromCamera();                      //Camera for next Iteration
        }

        private void Move(int turnCode)
        {
            _dirIdx = (_dirIdx + turnCode + 4) % 4;
            _x += Dirs[_dirIdx].x;
            _y += Dirs[_dirIdx].y;
            if(_x < _minX) _minX = _x;
            if(_y < _minY) _minY = _y;
            if(_x > _maxX) _maxX = _x;
            if(_y > _maxY) _maxY = _y;
        }

        private void ShowPaint()
        { 
            for (var iy = _minY; iy <= _maxY; iy++)
                for (var ix = _minX; ix <= _maxX; ix++)
                    Console.Write($"{(_pnls.GetOrAdd((ix, iy), _ => 0) == 1 ? White : Black)}{(ix == _maxX ? "\n" : "")}");
        }
    }
}