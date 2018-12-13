namespace AdventOfCode2018.Puzzles.Day13
{
    using System;
    using Base;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    public class Impl : BasePuzzle
    {
        public const string FILE = ".\\Puzzles\\Day13\\Input.txt";
        //public const string FILE = ".\\Puzzles\\Day13\\InputSimple.txt";
        //public const string FILE = ".\\Puzzles\\Day13\\InputSimple2.txt";

        private string _part1 = "";
        private string _part2 = "";
        public Impl() : base("Day13 ", FILE) { }

        public override async Task<string> RunPart1()
        {
            var (map, carts, x, y) = LoadMap(Inputs);

            while (true)
            {
                foreach (var c in carts.Values.Where(w => !w.Crashed).OrderBy(o => o.Y).ThenBy(t => t.X))
                {
                    if (c.Crashed) continue;

                    var newLoc = c.MoveNext(map);
                    if (newLoc.CartCount == 2)
                    {
                        _part1 = $"{c.X},{c.Y}";
                        goto exit;
                    }
                    newLoc.Cart = c.Facing;
                }
            }
exit:
            return await Task.FromResult($"{_part1}");
        }

        public override async Task<string> RunPart2()
        {
            var (map, carts, x, y) = LoadMap(Inputs);

            while (carts.Values.Count(w => !w.Crashed) > 1)
            {
                //Print(map, x, y);
                foreach (var c in carts.Values.Where(w => !w.Crashed).OrderBy(o => o.Y).ThenBy(t => t.X))
                {
                    if (c.Crashed) continue;
                    var newLoc = c.MoveNext(map);
                    if (newLoc.CartCount == 2)
                    {
                        carts[c.CartId].Crashed = true;
                        carts[newLoc.CartId.Value].Crashed = true;
                        newLoc.CartCount = 0;
                        newLoc.Cart = ' ';
                        newLoc.CartId = null;
                        continue;
                    }
                    
                    newLoc.Cart = c.Facing;
                    newLoc.CartId = c.CartId;
                }
            }

            var lastCart = carts.Values.FirstOrDefault(w => !w.Crashed);
            _part2 = lastCart.X + "," + lastCart.Y;
            return await Task.FromResult($"{_part2}");
        }


        public void Print(Dictionary<(int, int), Location> map, int x, int y)
        {
            for (int j = 0; j <= y; j++)
            {
                for (int i = 0; i <= x; i++)
                {
                    if (map.ContainsKey((i, j)) && map[(i, j)].Cart != ' ')
                            Console.Write(map[(i, j)].Cart);
                    else if(map.ContainsKey((i, j)))
                        Console.Write(map[(i, j)].Track);
                    else
                        Console.Write(' ');
                }
                Console.WriteLine("");
            }
        }


        private (Dictionary<(int, int), Location>, Dictionary<int, Cart>, int, int) LoadMap(IEnumerable<string> inputs)
        {
            var map = new Dictionary<(int, int), Location>();
            var carts = new Dictionary<int, Cart>();
            int x = -1, y = -1, cid = 0;
            foreach(var i in inputs)
            {
                x = 0; y++;
                foreach(var c in i.ToCharArray())
                {
                    if (c == Enums.D || c == Enums.U)
                    {
                        cid++;
                        carts.Add(cid, new Cart { X = x, Y = y, Facing = c, CartId = cid });
                        map.Add((x, y), new Location { Track = Enums.UD, Cart = c, CartId = cid, CartCount = 1 });
                    }
                    else if (c == Enums.L || c == Enums.R)
                    {
                        cid++;
                        carts.Add(cid, new Cart { X = x, Y = y, Facing = c, CartId = cid });
                        map.Add((x, y), new Location { Track = Enums.LR, Cart = c, CartId = cid, CartCount = 1 });
                    }
                    else
                    {
                        map.Add((x, y), new Location { Track = c, Cart = ' ', CartCount = 0 });
                    }
                    x++;
                }
            }

            return (map, carts, x, y);
        }

        public class Location
        {
            public char Track;
            public char Cart;
            public int? CartId;
            public int CartCount;

            public void Leave()
            {
                CartCount--;
                Cart = ' ';
                CartId = null;
            }
        }

        public static class Enums
        {
            public const char U = '^';
            public const char L = '<';
            public const char D = 'v';
            public const char R = '>';

            public const char LR = '-';
            public const char UD = '|';
            public const char FS = '/';
            public const char BS = '\\';
            public const char IN = '+';
        }

        public class Cart
        {
            public int CartId;
            public int X;
            public int Y;
            public char Facing;
            private int _intersectionCount = 0;
            public bool Crashed = false;

            public override string ToString() => $"{Facing} ({X},{Y})";

            public Location MoveNext(Dictionary<(int, int), Location> map)
            {
                map[(X, Y)].Leave();

                switch (Facing)
                {
                    case Enums.U: Y--; break;
                    case Enums.L: X--; break;
                    case Enums.D: Y++; break;
                    case Enums.R: X++; break;
                }

                var newLoc = map[(X, Y)];
                newLoc.CartCount++;
                ChangeDirection(newLoc.Track); 
                return newLoc;
            }

            public void ChangeDirection(char beneath)
            {
                if (beneath == Enums.UD || beneath == Enums.LR) return;

                if (beneath == Enums.IN)
                {
                    if (_intersectionCount % 3 == 0) TurnLeft();
                    else if (_intersectionCount % 3 == 2) TurnRight();

                    _intersectionCount++;
                    return;
                }
                else if(beneath == Enums.BS)
                {
                    if (Facing == Enums.D || Facing == Enums.U) TurnLeft();
                    else if (Facing == Enums.L || Facing == Enums.R) TurnRight();
                }
                else if(beneath == Enums.FS)
                {
                    if (Facing == Enums.D || Facing == Enums.U) TurnRight();
                    else if (Facing == Enums.L || Facing == Enums.R) TurnLeft();
                }
            }

            private void TurnLeft()
            {
                switch (Facing)
                {
                    case Enums.U: Facing = Enums.L; break;
                    case Enums.L: Facing = Enums.D; break;
                    case Enums.D: Facing = Enums.R; break;
                    case Enums.R: Facing = Enums.U; break;
                }
            }

            private void TurnRight()
            {
                switch (Facing)
                {
                    case Enums.U: Facing = Enums.R; break;
                    case Enums.R: Facing = Enums.D; break;
                    case Enums.D: Facing = Enums.L; break;
                    case Enums.L: Facing = Enums.U; break;
                }
            }
        }
      
    }
}