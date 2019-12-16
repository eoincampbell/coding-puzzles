/*
 * Day 13: Care Package
 * https://adventofcode.com/2019/day/13
 * Part 1: 341
 * Part 2: 17138
 */
namespace AdventOfCode2019.Puzzles.Day13
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Base;
    using Base.IntCode;

    public class Impl : Puzzle<string, int>
    {
        private const string SPCE = "  ";
        private const string BLOK = "▄▄";
        private const string WALL = "██";
        private const string PADL = "▬▬";
        private const string BALL = "()";
        private static readonly Dictionary<int, string> TileCodes = new Dictionary<int, string>
        {
            {0, SPCE}, {1, WALL}, {2, BLOK}, {3, PADL}, {4, BALL}
        };

        private (int x, int y) _max;
        private int _blocks;
        private int _score;
        private IntCodeVm? _vm;
        private VmState _state;
        private (int x, int y) _ballPos;
        private (int x, int y) _ballPrevPos;
        private int _paddlePositionX;
        private bool _playAreaRendered;
        private readonly bool _render;
        private Dictionary<(int x, int y), string> _tiles = new Dictionary<(int x, int y), string>();

        public Impl() : this(false) { }
        public Impl(bool render) : base("Day 13: Care Package", ".\\Puzzles\\Day13\\Input.txt") => _render = render;

        public override async Task<int> RunPart1Async() => await Task.Run(() =>
        {
            ResetVm();
            _vm?.RunProgramUntilHalt();
            ProcessAnyOutputs();
            RenderOutput();
            SetFinalCursorPosition(2);
            return _blocks;
        });

        public override async Task<int> RunPart2Async()
        {
            ResetVm();
            _vm?.SetValue(0, 2);
            while (_state != VmState.Halted)
            {
                _state = _vm?.RunProgramUntilInputRequired() ?? VmState.Halted;
                if (_state == VmState.Halted)
                {
                    ProcessAnyOutputs();
                    break;
                }

                if (_state != VmState.PausedAwaitingInput) continue;
                //Output is called twice, once to show you the current state of the ball
                //Then wait for input
                //Then again to show you the updated position of your paddle.
                //So only go into the InputCommandCode if the update included a ball-position updated
                var ballMoved = ProcessAnyOutputs();
                RenderOutput();
                if (ballMoved) ProcessInputCommand();
                await Wait();
            }
            RenderWinner();
            SetFinalCursorPosition(3);
            return _score;
        }

        private void SetFinalCursorPosition(int i)
        {
            if (!_render) return;
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(0, _max.y + i);
        }

        private void ProcessInputCommand()
        {
            //The picture on the ball on screen is as of the last output.
            //Therefore any input I supply here needs to put the paddle where the ball is going to be.
            //use ballPositionY to figure out if the ball is immediately above the cursor and about to bounce away... don't do anything until you know the bounce direction.
            //otherwise
            //      use prev vs. current x position of ball to figure out ball direction U/D/L/R
            //      take into account a wall bounce to modify the direction
            //use the direction to predict a next position and therefore modify the paddle +/-1;

            var dir = (_ballPos.y == _max.y - 2)
                ? 0
                : (_ballPrevPos.x == 0 || _ballPos.x > _ballPrevPos.x) 
                    ? 1 
                    : -1;
            
            if (_ballPos.x + 1 == _max.x || _ballPos.x - 1 == 0) dir *= -1; //at the wall

            var next = _ballPos.x + dir;
            if (_paddlePositionX < next)
            {
                _vm?.SetInput(1);
                _paddlePositionX++;
            }
            else if(_paddlePositionX > next)
            {
                _vm?.SetInput(-1);
                _paddlePositionX--;
            }
            else
            {
                _vm?.SetInput(0);
            }
        }

        private void ResetVm()
        {
            _vm = new IntCodeVm(Inputs[0]);
            _state = _vm.State;
            _tiles = new Dictionary<(int x, int y), string>();
            _max = (0, 0);
            _blocks = 0;
            _ballPos = (0,0);
            _ballPrevPos = (0,0);
            _paddlePositionX = 0;
            _score = 0;
            _playAreaRendered= false;
        }

        private bool ProcessAnyOutputs()
        {
            var ballMoved = false;
            if (_vm == null || !_vm.HasOutputs) return false;

            var results = _vm.GetOutputs().ToList();

            _tiles.Clear();
            for (var i = 0; i < results.Count; i += 3)
            {
                if (results[i] == -1 && results[i + 1] == 0)
                {
                    _score = (int)results[i + 2];
                    RenderScore();
                }
                else
                {
                    var x = (int) results[i];
                    var y = (int) results[i + 1];
                    var t = TileCodes[(int) results[i + 2]];

                    if (x > _max.x) _max.x = x;
                    if (y > _max.y) _max.y = y;

                    if (t == BLOK) _blocks++;
                    if (t == PADL) _paddlePositionX = x;
                    if (t == BALL)
                    {
                        ballMoved = true;
                        _ballPrevPos = _ballPos;
                        _ballPos.x = x;
                        _ballPos.y = y;
                    }

                    if (_tiles.ContainsKey((x, y)))
                        _tiles[(x,y)] = t;
                    else
                        _tiles.Add((x, y), t);
                }
            }
            return ballMoved;
        }

        private async Task Wait() => await Task.Delay(_render ? 0 : 0);


        private void RenderScore()
        {
            if (!_render) return;
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(0, _max.y + 1);
            Console.WriteLine($"Score: {_score}");
        }

        private void RenderPlayArea()
        {
            if (!_render || _playAreaRendered) return;
            Console.SetCursorPosition(0, 0);
            for (var yy = 0; yy <= _max.y; yy++)
            for (var xx = 0; xx <= _max.x; xx++)
            {
                Console.Write(SPCE);
                if (xx == _max.x) Console.WriteLine("");
            }

            _playAreaRendered = true;
        }

        private void RenderOutput()
        {
            if(!_render) return;
            RenderPlayArea();
            Console.SetCursorPosition(0, 0);
            foreach (var key in _tiles.Keys)
            {
                var t = _tiles[key];
                
                if(t == BLOK)
                {
                    Console.ForegroundColor = (((key.x * 10) + key.y) % 3) switch
                    {
                        1 => ConsoleColor.Red,
                        2 => ConsoleColor.Green,
                        _ => ConsoleColor.Blue
                    };
                }
                else {
                    Console.ForegroundColor = ConsoleColor.White;
                }
                Console.SetCursorPosition(key.x*2, key.y);
                Console.Write(t);
            }
        }

        private void RenderWinner()
        {
            if (!_render) return;
            var winner = new []
            {
                "                                                                                    ",
                "                                                                                    ",
                "                                                                                    ",
                "                                                                                    ",
                "                                                                                    ",
                "                                                                                    ",
                "                                                                                    ",
                "      ██      ██      ██   ██   ███   ██   ███   ██   ███████   ███████    ██       ",
                "       ██    ████    ██    ██   ████  ██   ████  ██   ██        ██    ██   ██       ",
                "        ██  ██  ██  ██     ██   ██ ██ ██   ██ ██ ██   █████     ██████     ██       ",
                "         ████    ████      ██   ██  ████   ██  ████   ██        ██   ██             ",
                "          ██      ██       ██   ██   ███   ██   ███   ███████   ██    ██   ██       ",
                "                                                                                    ",
                "                                                                                    ",
                "                                                                                    ",
                "                                                                                    ",
                "                                                                                    ",
                "                                                                                    ",
                "                                                                                    ",
            };

            var i = 1;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(2, i);

            foreach(var s in winner)
            {
                Console.Write(s);
                Console.SetCursorPosition(2, ++i);
            }
        }


    }
}