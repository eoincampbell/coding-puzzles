namespace AdventOfCode2018.Puzzles.Day17
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Base;
    using System.Threading.Tasks;
 
    public class Impl : BasePuzzle
    {
        //public const string FILE = ".\\Puzzles\\Day17\\Input.txt";
        public const string FILE = ".\\Puzzles\\Day17\\InputSimple.txt";

        private char[,] _watercolumn;
        private IList<ClayWall> _walls;
        private int _height;
        private int _width;

        public Impl() : base("Day17 ", FILE) { }

        public override async Task<string> RunPart1()
        {
            _walls = Inputs.Select(i => new ClayWall(i)).ToList();

            int topMostY = 0; //walls.Select(w => w.YRange.Min()).Min();
            int bottomMostY = _walls.Select(w => w.YRange.Max()).Max()+1;
            int leftMostX = _walls.Select(w => w.XRange.Min()).Min()-1;
            int rightMostX = _walls.Select(w => w.XRange.Max()).Max()+1;

            _height = bottomMostY - topMostY;
            _width = rightMostX - leftMostX;

            _watercolumn = new char[_height+1, _width+1];

            for (int h = 0; h <= _height; h++)
                for (int w = 0; w <= _width; w++)
                    _watercolumn[h, w] = '.';

            foreach (var cw in _walls)
                foreach (var y in cw.YRange)
                    foreach (var x in cw.XRange)
                        _watercolumn[y - topMostY, x - leftMostX] = '#';

            PrintState();
            int wellX = 500 - leftMostX;
            int wellY = 0 - topMostY;
            FillFrom(wellX, wellY, wellX, wellY + 1);
            PrintState();

            return await Task.FromResult("0") ;


        }

        public void PrintState()
        {
            for (int i = 0; i <= _height; i++)
                for (int j = 0; j <= _width; j++)
                    Console.Write(_watercolumn[i, j] + ((j == _width) ? Environment.NewLine : ""));
        }

        public void FillFrom(int src_x, int src_y, int target_x, int target_y)
        {
            //Part1. - Water coming down from above

            //if coming from directly above to below (x = x and ty = sy+1)
                //if cell below again = # Clay or ~ SettledWater
                    //turn this cell to water ~
                //else if cell below again = . Sand
                    //turn this cell to flow |
                    //recursively call the one below
            
            
            if (src_x == target_x && src_y == target_y - 1)
            {
                if(_watercolumn[target_y + 1, target_x] == '#' || _watercolumn[target_y + 1, target_x] == '~')
                {
                    _watercolumn[target_y, target_x] = '~';

                    //Once water hits clay or other water... it can go sideways
                    //if the space to that side isn't clay and does have clay or water beneath it
                    if (_watercolumn[target_y, target_x + 1] != '#' && _watercolumn[target_y, target_x + 1] == '~')
                    {
                        FillFrom(target_x, target_y, target_x + 1, target_y);
                    }
                    if (_watercolumn[target_y, target_x - 1] != '#' && _watercolumn[target_y, target_x - 1] == '~')
                    {
                        FillFrom(target_x, target_y, target_x - 1, target_y);
                    }

                }
                else if(_watercolumn[target_y + 1, target_x] == '.')
                {
                    _watercolumn[target_y, target_x] = '|';
                    FillFrom(target_x, target_y, target_x, target_y + 1);
                }
            }


        }

        private class ClayWall
        {
            public int[] XRange;
            public int[] YRange;

            public ClayWall(string input)
            {
                string[] parts = input.Split(',');
                string [] part0 = parts[0].Split('=');
                string[] part1 = parts[1].Split('=');

                if(part0[0].Trim() == "x")
                {
                    XRange = new int[] { int.Parse(part0[1]) };
                }
                else
                {
                    YRange = new int[] { int.Parse(part0[1]) };
                }

                if(part1[0].Trim() == "y")
                {
                    var sf = part1[1].Split(new char[] { '.' } , StringSplitOptions.RemoveEmptyEntries);
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

        public override async Task<string> RunPart2()
        {
            return await Task.FromResult($"");
        }
    }
}