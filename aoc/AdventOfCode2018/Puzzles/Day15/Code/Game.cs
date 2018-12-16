namespace AdventOfCode2018.Puzzles.Day15.Code
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Diagnostics;

    public class Game
    {
        public Square[,] Map { get; set; }
        public Dictionary<Point, Player> Players { get; set; }
        public int Round { get; private set; } = 0;
        public int FullRounds { get; private set; } = 0;

        public string Score => $"{FullRounds} * {Players.Values.Sum(p => p.HitPoints)} = {FullRounds * Players.Values.Sum(p => p.HitPoints)}";

        public Game(IEnumerable<string> inputState) 
        {
            Players = new Dictionary<Point, Player>();
            LoadMap(inputState);
        }

        private void LoadMap(IEnumerable<string> i)
        {
            int width = i.First().Length, height = i.Count(), x = 0, y = 0, playerId = 0;
            Map = new Square[width, height];
            foreach(var row in i)
            {
                x = 0;
                foreach(var cell in row)
                {
                    if (cell == '#') Map[x, y] = new Wall();
                    else
                    {
                        var f = new Floor();
                        var p = new Point(x, y);
                        if (cell == 'E') f.Occupant = new Elf { Id = ++playerId, Coordinate = p };
                        if (cell == 'G') f.Occupant = new Goblin { Id = ++playerId, Coordinate = p };
                        Map[x, y] = f;
                        if(f.Occupant!= null) Players.Add(p, f.Occupant);
                    }
                    x++;
                }
                y++;
            }
        }

        public void Play()
        {
            Round = 0;
            FullRounds = 0;
            PrintMap();
            do
            {
                Round++;
                if (PlayRound()) FullRounds++;
                PrintMap();
            } while (PlayersFromBothSidesRemain());

        }

        private bool PlayersFromBothSidesRemain()
        {
            return Players.Values.OfType<Elf>().Any() && Players.Values.OfType<Goblin>().Any();
        }

        private bool PlayRound()
        {
            bool fullRound = false;
            var orderedPlayers = Players
                .Values
                .OrderBy(o => o.Coordinate.Y)
                .ThenBy(t => t.Coordinate.X)
                .ToList();

            foreach (var player in orderedPlayers)
            {
                if (player.HitPoints <= 0) continue;                        //this player was killed by someone else this round
                if (!PlayersFromBothSidesRemain()) goto leaveRoundEarly;    //there are no more enemies left so this is a partial round
                
                
                //Find a move
                int closestDistance = int.MaxValue;
                Point nextMove = player.Coordinate;
                foreach (var other in orderedPlayers)
                {
                    if(!player.IsEnemyOf(other) || player.IsSelf(other)) continue; //bail out if they're teammates
                    var (dist, nm) = Map.GetDistance(player, other);        //GetDistance & next move
                    if (dist >= closestDistance) continue;                  //If dist is an improvement
                    closestDistance = dist;                                 //record it & next move
                    nextMove = nm;
                }

                //Take move if available
                if (closestDistance > 1 && closestDistance < int.MaxValue)  //If we found a dist, but we're not already adjacent
                    UpdatePlayer(player, nextMove);                         //Move
                
                //Find an Enemy
                var nearByEnemy = GetNearbyEnemies(player)                  //Get all adjacent players
                    .Where(w => w.Item1.IsEnemyOf(player))                  //Where enemies
                    .OrderBy(o => o.Item1.HitPoints)                        //Order by lowest hitpoints
                    .ThenBy(t => t.Item2)                                   //tie break by reading order
                    .FirstOrDefault().Item1;

                //Attack Enemy if available
                if (nearByEnemy == null) continue;                          //If we find someone to attack
                nearByEnemy.ReceiveDamage(player.AttackPower);              //Attack them
                if (nearByEnemy.HitPoints <= 0) RemovePlayer(nearByEnemy);  //Remove them if dead.
                    
            }
            fullRound = true;                                               //Only count full rounds.
leaveRoundEarly:
            return fullRound;
        }

        

        private IEnumerable<(Player,int)> GetNearbyEnemies(Player player)
        {
            var pu = new Point(player.Coordinate.X, player.Coordinate.Y - 1);
            var pl = new Point(player.Coordinate.X - 1, player.Coordinate.Y);
            var pr = new Point(player.Coordinate.X + 1, player.Coordinate.Y);
            var pd = new Point(player.Coordinate.X, player.Coordinate.Y + 1);

            //In reading order from top-bottom/left-right
            if (Players.ContainsKey(pu)) yield return (Players[pu], 1); //above  
            if (Players.ContainsKey(pl)) yield return (Players[pl], 2); //left
            if (Players.ContainsKey(pr)) yield return (Players[pr], 3); //right
            if (Players.ContainsKey(pd)) yield return (Players[pd], 4); //below
        }


        private void RemovePlayer(Player player)
        {
            Players.Remove(player.Coordinate);
            Map[player.Coordinate.X, player.Coordinate.Y].Occupant = null;
        }

        private void UpdatePlayer(Player player, Point value)
        {
            Players.Remove(player.Coordinate);
            Map[player.Coordinate.X, player.Coordinate.Y].Occupant = null;
            player.Move(value);
            Players.Add(player.Coordinate, player);
            Map[player.Coordinate.X, player.Coordinate.Y].Occupant = player;
        }

        public void PrintMap()
        {
            int height = Map.GetLength(1), width = Map.GetLength(0);

            if(Round > 0)
                Console.WriteLine($"------------ Round {Round} ----------------------------");
            else
                Console.WriteLine($"------------ Initial State ----------------------------");

            Console.Write("  ");
            for (int i = 0; i < width; i++) Console.Write(i % 10);
            Console.WriteLine("");

            for(int y = 0; y < Map.GetLength(1); y++)
            {
                Console.Write($"{y % 10} ");
                var appender = "";
                for(int x = 0; x < Map.GetLength(0); x++)
                {
                    var p = new Point(x, y);
                    if (Players.ContainsKey(p))
                    {
                        var player = Players[p];
                        appender += $"{player.Avatar}:{player.Id}({player.HitPoints}), ";
                        Console.Write(player.Avatar);
                    }
                    else 
                        Console.Write(Map[x, y].Type);
                }
                Console.WriteLine(" " + appender);
            }
        }
    }
}