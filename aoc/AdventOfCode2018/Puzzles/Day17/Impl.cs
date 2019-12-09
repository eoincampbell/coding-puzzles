namespace AdventOfCode2018.Puzzles.Day17
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Base;
    using System.Threading.Tasks;
 
    public enum Direction
    {
        N,
        S,
        E,
        W,
        NE,
        SE,
        NW,
        SW
    }

    public class Point
    {
        public int X;
        public int Y;

        public Point Get(Direction d)
        {
            switch (d)
            {
                case Direction.N:
                    return new Point { X = X, Y = Y - 1 };
                case Direction.S:
                    return new Point { X = X, Y = Y + 1 };
                case Direction.E:
                    return new Point { X = X - 1, Y = Y };
                case Direction.W:
                    return new Point { X = X + 1, Y = Y };
                case Direction.NE:
                    return new Point { X = X - 1, Y = Y - 1 };
                case Direction.SE:
                    return new Point { X = X - 1, Y = Y + 1 };
                case Direction.NW:
                    return new Point { X = X + 1, Y = Y - 1 };
                case Direction.SW:
                    return new Point { X = X - 1, Y = Y + 1 };
                default:
                    throw new Exception("Invalid Direction");
            }
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Point)) return false;

            var p = obj as Point;
            return X == p.X && Y == p.Y;
        }

        public static bool operator ==(Point a, Point b)
        {
            return a.Equals(b);
        }
        public static bool operator !=(Point a, Point b)
        {
            return !(a == b);
        }
    }

    public class Map
    {
        private const char WELL = '+';
        private const char SAND = '.';
        private const char CLAY = '#';
        private const char FLOW = '|';
        private const char WATR = '~';
        private const char OOBN = '@'; //out of bounds

        private char[,] _map;
        private IList<ClayWall> _walls;

        private int _height;
        private int _width; 
        private int _topMostY;
        private int _bottomMostY; 
        private int _leftMostX;
        private int _rightMostX;

        public Map(string [] Inputs)
        {
            _walls = Inputs.Select(i => new ClayWall(i)).ToList();

            _topMostY = 0; //walls.Select(w => w.YRange.Min()).Min();
            _bottomMostY = _walls.Select(w => w.YRange.Max()).Max() + 1;
            _leftMostX = _walls.Select(w => w.XRange.Min()).Min() - 1;
            _rightMostX = _walls.Select(w => w.XRange.Max()).Max() + 1;

            _height = _bottomMostY - _topMostY;
            _width = _rightMostX - _leftMostX;

            _map = new char[_width + 1, _height + 1];

            for (int w = 0; w <= _width; w++)
                for (int h = 0; h <= _height; h++)
                    _map[w, h] = '.';

            foreach (var cw in _walls)
                foreach (var y in cw.YRange)
                    foreach (var x in cw.XRange)
                        _map[x - _leftMostX, y - _topMostY] = CLAY;
        }

        public char GetMapValue(Point p)
        {
            if (p.X < 0 || p.X > _width || p.Y < 0 || p.Y > _height) return OOBN;

            return _map[p.X, p.Y];
        }

        public void SetMapValue(Point p, char val)
        {
            if (p.X < 0 || p.X > _width || p.Y < 0 || p.Y > _height) return;
            _map[p.X, p.Y] = val;
        }

        public void Run()
        {
            var well = new Point { X = 500 - _leftMostX, Y = 0 - _topMostY };
            SetMapValue(well, WELL);
            FillFrom(well, well.Get(Direction.S));
        }

        public void PrintState()
        {
            Console.WriteLine(string.Join("", Enumerable.Range(0, _width+1).Select(s => "$")));
            for (int i = 0; i <= _height; i++)
                for (int j = 0; j <= _width; j++)
                    Console.Write(_map[j, i] + ((j == _width) ? Environment.NewLine : ""));
        }

        public void FillFrom(Point s, Point t)
        {
            //Part1. - Water coming down from above

            //if coming from directly above to below (x = x and ty = sy+1)
            //if cell below again = # Clay or ~ SettledWater
            //turn this cell to water ~
            //else if cell below again = . Sand
            //turn this cell to flow |
            //recursively call the one below


            if (s.Get(Direction.S) == t)
            {
                var southvalue = GetMapValue(t.Get(Direction.S));

                if (southvalue == SAND)
                {
                    SetMapValue(t, FLOW);
                    FillFrom(t, t.Get(Direction.S));
                }
                else if (southvalue == CLAY || southvalue == WATR)
                {
                    SetMapValue(t, WATR);
                    bool clayInBothDirections = CheckForClayInBothDirections(t);

                    if (clayInBothDirections)
                    {
                        var eastTarget = t.Get(Direction.E);
                        while(GetMapValue(eastTarget) != CLAY)
                        {
                            SetMapValue(eastTarget, WATR);
                            eastTarget = eastTarget.Get(Direction.E);
                        }

                        var westTarget = t.Get(Direction.W);
                        while (GetMapValue(westTarget) != CLAY)
                        {
                            SetMapValue(westTarget, WATR);
                            westTarget = westTarget.Get(Direction.W);
                        }
                    }
                    else
                    {
                        //Deal with flow in both directions
                    }
                }
            }
        }

        private bool CheckForClayInBothDirections(Point current)
        {
            bool east = false;
            bool west = false;

            for(int i = current.X; i >= 0; i--)
            {
                if(_map[i, current.Y] == CLAY)
                {
                    east = true;
                    break;
                }
            }

            for (int i = current.X; i <= _width; i++)
            {
                if (_map[i, current.Y] == CLAY)
                {
                    west = true;
                    break;
                }
            }

            return east && west;
        }



    }

    public class ClayWall
    {
        public int[] XRange;
        public int[] YRange;

        public ClayWall(string input)
        {
            string[] parts = input.Split(',');
            string[] part0 = parts[0].Split('=');
            string[] part1 = parts[1].Split('=');

            if (part0[0].Trim() == "x")
            {
                XRange = new int[] { int.Parse(part0[1]) };
            }
            else
            {
                YRange = new int[] { int.Parse(part0[1]) };
            }

            if (part1[0].Trim() == "y")
            {
                var sf = part1[1].Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
                int s = int.Parse(sf[0]);
                int f = int.Parse(sf[1]);
                YRange = Enumerable.Range(s, (f - s) + 1).ToArray();
            }
            else
            {
                var sf = part1[1].Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
                int s = int.Parse(sf[0]);
                int f = int.Parse(sf[1]);
                XRange = Enumerable.Range(s, (f - s) + 1).ToArray();
            }
        }

        public override string ToString()
        {
            return $"x:[{string.Join(",", XRange)}] | y:[{string.Join(",", YRange)}]";
        }
    }

    public class Impl : BasePuzzle
    {
        //public const string FILE = ".\\Puzzles\\Day17\\Input.txt";
        public const string FILE = ".\\Puzzles\\Day17\\InputSimple.txt";

        
        public Impl() : base("Day17 ", FILE) { }

        public override async Task<string> RunPart1()
        {
            Map m = new Map(Inputs);
            m.PrintState();

            m.Run();
            m.PrintState();

            return await Task.FromResult("0");
        }
        public override async Task<string> RunPart2()
        {
            return await Task.FromResult($"");
        }
    
    }

}