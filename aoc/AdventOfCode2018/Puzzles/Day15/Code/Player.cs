using System.Collections.Generic;

namespace AdventOfCode2018.Puzzles.Day15.Code
{
    public abstract class Player
    {
        public int Id { get; set; }
        public int AttackPower => 3;
        public int HitPoints { get; private set; } = 200;
        public abstract char Avatar { get; }
        public Point Coordinate { get; set; }

        
        public void Move(Point coordinate) => Coordinate = coordinate;
        public void ReceiveDamage(int damage) => HitPoints -= damage;
        public bool IsEnemyOf(Player other) => Avatar != other.Avatar;

        public bool IsSelf(Player other) => Id == other.Id;
    }
    public class Elf : Player
    {
        public override char Avatar => 'E';
    }
    public class Goblin : Player
    {
        public override char Avatar => 'G';
    }
}