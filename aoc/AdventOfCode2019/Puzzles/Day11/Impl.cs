/*
 * Day 11: 
 * https://adventofcode.com/2019/day/11
 * Part 1: 1564
 * Part 2: 
 */
namespace AdventOfCode2019.Puzzles.Day11
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Base;
    using Base.IntCode;

    public class Impl : Puzzle<string, int>
    {
        private int _dirIdx;
        private int _x;
        private int _y;
        private int _minX;
        private int _minY;
        private int _maxX;
        private int _maxY;
        private IntCodeVm _robotBrain;

        private static readonly (int x, int y) [] _dirs = 
        {
            (0, -1), //N
            (1, 0),  //E
            (0, 1),  //S
            (-1, 0)  //W
        }; 

        private const char W = '#';
        private const char B = ' ';

        public Impl() : base("Day 11: ", ".\\Puzzles\\Day11\\Input.txt") { }

        private ConcurrentDictionary<(int x, int y), int> _paintedSquares;

        public override async Task<int> RunPart1Async()
        {
            return await Task.Run(() =>
            {
                Reset();
                var color = 0;  //part 1 - start on black
                while (color != -1)
                    color = PaintPanel(color);

                return _paintedSquares.Keys.Count;
            });
        }

        public override async Task<int> RunPart2Async()
        {
            return await Task.Run(() =>
            {
                Reset();
                var color = 1; //part 2 - start on white
                while (color != -1)
                    color = PaintPanel(color);
                
                ShowPaint();
                return _paintedSquares.Keys.Count;
            });
        }

        private void Reset()
        {
            _paintedSquares = new ConcurrentDictionary<(int x, int y), int>();
            _x = _y = _dirIdx = _minX = _maxX = _minY = _maxY = 0;
            _robotBrain = new IntCodeVm(Inputs[0]);
        }

        private int ReadColorFromCamera()
            => _paintedSquares.ContainsKey((_x,_y)) ? _paintedSquares[(_x, _y)] : 0;

        private void ApplyPaintToPanel(int color) 
            =>_paintedSquares.AddOrUpdate((_x, _y), color, (x,y) => color);

        private int PaintPanel(int color)
        {
            _robotBrain.SetInput(color);

            var halted = _robotBrain.RunProgramPauseAtOutput();
            if (halted) return -1;
            
            color = (int) _robotBrain.GetOutput();
            ApplyPaintToPanel(color);

            _robotBrain.RunProgramPauseAtOutput();
            var turnCode = (int) _robotBrain.GetOutput();
            Move(turnCode == 0 ? -1 : 1);

            return ReadColorFromCamera();
        }

        private void Move(int turnCode)
        {
            _dirIdx = (_dirIdx + turnCode) switch {
                -1 => 3, //ROT-L
                4 => 0,  //ROT-R
                _ => _dirIdx + turnCode
            };

            _x += _dirs[_dirIdx].x;
            _y += _dirs[_dirIdx].y;
            
            if(_x < _minX) _minX = _x;
            if(_y < _minY) _minY = _y;
            if(_x > _maxX) _maxX = _x;
            if(_y > _maxY) _maxY = _y;
        }

        private void ShowPaint()
        {
            for (var iy = _minY; iy <= _maxY; iy++)
            for (var ix = _minX; ix <= _maxX; ix++)
            {
                if (_paintedSquares.ContainsKey((ix, iy)))
                    Console.Write(_paintedSquares[(ix,iy)] == 1 ? W : B);
                else
                    Console.Write(B);

                if (ix == _maxX) 
                    Console.WriteLine("");
            }
        }
    }
}