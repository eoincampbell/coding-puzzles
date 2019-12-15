/*
 * Day 15: Oxygen System
 * https://adventofcode.com/2019/day/15
 * Part 1: 304
 * Part 2: 310
 */
namespace AdventOfCode2019.Puzzles.Day15
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using AdventOfCode2019.Base.IntCode;
    using Base;
    using TrackerDict = System.Collections.Generic.Dictionary<(int x, int y), (Tile tile, int dist, bool filledWithAir, int distFromOxy)>;
    public enum Dir { NORTH = 1, SOUTH = 2, WEST = 3, EAST = 4 }
    public enum Tile { START = 'S', OXYGEN = 'X', FLOOR = ' ', FOG = '.', WALL = '#', BOT = 'O', BLOCK = '█' };
    public class Impl : Puzzle<string, int>
    {
        private static (int X, int Y) _orig = (0, 0);
        private (int X, int Y) _min = (-30, -25);
        private (int X, int Y) _max = (30, 25);
        private (int X, int Y) _cur = _orig;
        private (int X, int Y) _prev = _orig;
        private (int X, int Y) _upd = _orig;
        private Dir _currDirection = Dir.NORTH;
        private Dir _prevDirection = Dir.NORTH;
        private int _dist = 0;
        private int _maxDistFromOxy = 0;
        private readonly bool _render;
        private IntCodeVm? _vm;
        private TrackerDict? _map;

        public Impl() : this(false) { }
        public Impl(bool render): base("Day 15: ", ".\\Puzzles\\Day15\\Input.txt") => _render = render;

        public override async Task<int> RunPart1Async() => await Task.Run(() => 
        {
            RunTheMaze();
            var kv = _map.Where(w => w.Value.tile == Tile.OXYGEN).First();
            return kv.Value.dist;
        });

        public override async Task<int> RunPart2Async() => await Task.Run(() =>
        {
            if(_vm == null) RunTheMaze();
            var oxy = _map.Where(w => w.Value.tile == Tile.OXYGEN).First();
            if (_render) Console.ForegroundColor = ConsoleColor.DarkRed;
            FillCellWithAir(oxy.Key.x, oxy.Key.y, 0);
            if (!_render) return _maxDistFromOxy;
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(0, (_max.Y - _min.Y) + 3);
            return _maxDistFromOxy;
        });

        private void RunTheMaze()
        {
            _vm = new IntCodeVm(Inputs[0]);
            _map = new TrackerDict { { (_cur.X, _cur.Y), (Tile.START, _dist, false, -1) } };
            SetupMaze();
            while (_vm.State != VmState.Halted)
            {
                var st = _vm.RunProgramUntilInputRequired();
                if (st == VmState.PausedAwaitingInput) _vm.SetInput((int)_currDirection);
                st = _vm.RunProgramUntilOutputAvailable();
                if (st == VmState.Halted || st == VmState.PausedHasOutput)
                {
                    ProcessOutput();
                    PrintMaze();
                    if (st == VmState.Halted || (_cur == (0, 0) && _prev != (0, 0))) break;
                }
            }
        }

        private void FillCellWithAir(int x, int y, int dist)
        {
            if (_map is null || !_map.ContainsKey((x,y))) return;
            var r = _map[(x, y)]; 
            r.filledWithAir = true;
            r.distFromOxy = dist;
            _map[(x, y)] = r;
            if (dist > _maxDistFromOxy) _maxDistFromOxy = dist;
            PrintAirWay(x,y);
            foreach (var adj in new (int xx, int yy)[] { (x - 1, y), (x + 1, y), (x, y - 1), (x, y + 1) })
                if(_map.TryGetValue(adj, out var a) && (a.tile == Tile.FLOOR || a.tile == Tile.START) && !a.filledWithAir)
                     FillCellWithAir(adj.xx, adj.yy, dist + 1);
        }

        private void PrintAirWay(int x, int y)
        {
            if (!_render) return;
            Console.SetCursorPosition(x + (_min.X * -1), y + (_min.Y * -1));
            Console.Write((char)Tile.BLOCK);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Thread.Sleep(5);
        }

        private void ProcessOutput()
        {
            if (_vm is null || !_vm.HasOutputs) return;
            var result = (int)_vm.GetOutput();
            _upd = GetNewPosition();            //get you intended target position
            if (result == 1 || result == 2)     //if the bot moved to the target
            {
                _prev = _cur;                   //keep a copy of where they were and then move there
                _cur = _upd;
                _dist = (!(_map is null) && _map.ContainsKey(_cur))
                    ?_map[_cur].dist            //Grab the known distance for this co-ord if revisiting
                    : _dist + 1;                //Or inc. the distance
            }
            UpdateMap(_upd, result switch { 1 => Tile.FLOOR, 2 => Tile.OXYGEN, _ => Tile.WALL }); //Update the Map
            _prevDirection = _currDirection;    //Track the old direction (just for UI Output)
            _currDirection = result switch {             
                0 => Turn(left: false),         //if we couldn't move forward, turn right, 
                _ => Turn(left: true)           //otherwise, turn left to hug the left wall
            };
        }
        
        private void UpdateMap((int x, int y) pos, Tile tile)
        {
            if (_map is null) return;
            if (pos.x < _min.X) _min.X = pos.x;
            if (pos.y < _min.Y) _min.Y = pos.y;
            if (pos.x > _max.X) _max.X = pos.x;
            if (pos.y > _max.Y) _max.Y = pos.y;
            if (_map.ContainsKey(pos)) _map[pos] = (tile, _dist, false, 0);
            else _map.Add(pos, (tile, _dist, false, 0));
        }
        
        private void SetupMaze()
        {
            if (!_render) return;
            Console.ForegroundColor = ConsoleColor.Gray;
            for (int y = _min.Y; y <= _max.Y; y++)
                for (int x = _min.X; x <= _max.X; x++)
                    Console.Write((char)Tile.FOG + (x == _max.X ? Environment.NewLine : string.Empty));
        }

        private void PrintMaze()
        {
            if (_map is null || !_render) return;
            PrintPosition(_prev);
            PrintPosition(_upd);
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = _cur == (0, 0) ? ConsoleColor.Green : ConsoleColor.Black;
            Console.SetCursorPosition(_cur.X + (_min.X * -1), _cur.Y + (_min.Y * -1));
            Console.Write((char)Tile.BOT);
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(0, (_max.Y - _min.Y) + 1);
            Console.WriteLine($"Was at: ({_prev.X},{_prev.Y} | Now at: ({_cur.X},{_cur.Y}) | Turned from: {_prevDirection} => {_currDirection}                   ");
            Thread.Sleep(5);
        }

        private void PrintPosition((int x, int y) p)
        {
            Console.SetCursorPosition(p.x + (_min.X * -1), p.y + (_min.Y * -1));
            if (_map is null || !_map.ContainsKey((p.x, p.y))) return;
            var t = _map[(p.x, p.y)].tile;
            Console.ForegroundColor = t switch
            {
                Tile.WALL => ConsoleColor.Cyan,
                Tile.START => ConsoleColor.Green,
                Tile.OXYGEN => ConsoleColor.Red,
                _ => ConsoleColor.White
            };

            Console.BackgroundColor = t switch { Tile.WALL => ConsoleColor.DarkBlue, _ => ConsoleColor.Black };
            Console.Write(t == Tile.WALL ? (char)Tile.WALL : (char)Tile.BLOCK);
        }

        private Dir Turn(bool left) => _currDirection switch
        {
            Dir.NORTH => left ? Dir.WEST : Dir.EAST,
            Dir.WEST => left ? Dir.SOUTH : Dir.NORTH,
            Dir.SOUTH => left ? Dir.EAST : Dir.WEST,
            Dir.EAST => left ? Dir.NORTH : Dir.SOUTH,
            _ => throw new InvalidOperationException("Invalid Direction")
        };

        private (int x, int y) GetNewPosition() => _currDirection switch
        {
            Dir.NORTH => (_cur.X, _cur.Y - 1),
            Dir.SOUTH => (_cur.X, _cur.Y + 1),
            Dir.EAST => (_cur.X + 1, _cur.Y),
            Dir.WEST => (_cur.X - 1, _cur.Y),
            _ => throw new InvalidOperationException("Invalid Direction")
        };
    }
}