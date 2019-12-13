/*
 * Day 13: 
 * https://adventofcode.com/2019/day/13
 * Part 1: 
 * Part 2: 
 */
namespace AdventOfCode2019.Puzzles.Day13
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Numerics;
    using System.Threading;
    using System.Threading.Tasks;
    using Base;
    using Base.IntCode;

    public class Impl : Puzzle<string, int>
    {
        private const char SPCE = ' ';
        private const char BLOK = '▄';
        private const char WALL = '█';
        private const char PADL = '_';
        private const char BALL = 'o';
        private static readonly Dictionary<int, char> TileCodes = new Dictionary<int, char>
        {
            {0, SPCE},
            {1, WALL},
            {2, BLOK},
            {3, PADL},
            {4, BALL}
        };

        private int _maxX;
        private int _maxY;
        private int _blocks;
        private int _score;
        private IntCodeVm _vm;
        private int _ballPositionX;
        private int _ballPositionY;
        private int _ballPrevPositionX;
        private int _ballPrevPositionY;
        private int _paddlePositionX;

        private Dictionary<(int x, int y), char> _tiles = new Dictionary<(int x, int y), char>();


        public Impl() : base("Day 13: ", ".\\Puzzles\\Day13\\Input.txt") { }

        public override async Task<int> RunPart1Async()
             => await Task.Run(() =>
             {
                 ResetVm();

                 _vm.RunProgramUntilHalt();

                 var results = _vm.GetOutputs().ToList();

                 for (var i = 0; i < results.Count; i+=3)
                 {
                     var x = (int)results[i];
                     var y = (int)results[i+1];
                     var t = TileCodes[(int) results[i + 2]];
                     _tiles.Add((x, y), t);

                     if(x > _maxX) _maxX = x;
                     if(y > _maxY) _maxY = y; 
                     if(t == BLOK) _blocks++;
                 }

                 RenderOutput();
                 

                 return _blocks;
             });

        public override async Task<int> RunPart2Async()
        {
            //ResetVm();
            //_vm.SetValue(0, 2);

            //var state = VmState.Running;

            //while (state != VmState.Halted)
            //{
            //    state = _vm.RunProgramUntilInputRequired();

            //    if (state == VmState.Halted)
            //    {
            //        ProcessAnyOutputs();
            //        break;
            //    }

            //    if (state != VmState.PausedAwaitingInput) continue;

            //    ProcessAnyOutputs();
            //    RenderOutput();
            //    ProcessInputCommand();

            //    await Task.Delay(2000);

            //}

            return _blocks; //_score;
        }

        private void ProcessInputCommand()
        {
            //Out of order logic.

            //The picture on the ball on screen is as of the last output.
            //Therefore any input I supply here needs to put the paddle where the ball is going to be.

            //use prev vs. current vars to figure out ball direction U/D/L/R
            //use paddle+/-1 versus ball-x to check where I need to be.
        }

        private void ResetVm()
        {
            _vm = new IntCodeVm(Inputs[0]);
            _tiles = new Dictionary<(int x, int y), char>();
            _maxX = 0;
            _maxY = 0;
            _blocks = 0;
            _ballPositionX = 0;
            _ballPrevPositionX = 0;
            _ballPositionY = 0;
            _ballPrevPositionY = 0;
            _paddlePositionX = 0;
            _score = 0;
        }

        private void ProcessAnyOutputs()
        {
            if (!_vm.HasOutputs) return;

            var results = _vm.GetOutputs().ToList();

            for (var i = 0; i < results.Count; i += 3)
            {
                if (results[i] == -1 && results[i + 1] == 0)
                {
                    _score = (int)results[i + 2];
                }
                else
                {
                    var x = (int) results[i];
                    var y = (int) results[i + 1];
                    var t = TileCodes[(int) results[i + 2]];

                    if (x > _maxX) _maxX = x;
                    if (y > _maxY) _maxY = y;

                    if (t == BLOK) _blocks++;
                    if (t == PADL) _paddlePositionX = x;

                    if (t == BALL)
                    {
                        _ballPrevPositionX = _ballPositionX;
                        _ballPrevPositionY = _ballPositionY;
                        _ballPositionX = x;
                        _ballPositionY = y;
                    }

                    if (_tiles.ContainsKey((x, y)))
                    {
                        _tiles[(x,y)] = t;
                    }
                    else
                    {
                        _tiles.Add((x, y), t);
                    }

                }

                
            }
        }

        private void RenderOutput()
        {
            if(Console.IsOutputRedirected) return;

            Console.SetCursorPosition(0, 0);
            for (var yy = 0; yy <= _maxY; yy++)
            for (var xx = 0; xx <= _maxX; xx++)
            {
                Console.Write(_tiles.ContainsKey((xx, yy)) ? _tiles[(xx,yy)] : ' ');
                if (xx == _maxX) Console.WriteLine("");
            }
        }
    }
}