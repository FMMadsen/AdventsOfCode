using AdventOfCode2024Solutions.Day04;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024Solutions.Day06
{
    public struct Transform
    {
        public Vector2I Location { get; set; }
        public Vector2I Direction { get; set; }

        public Transform() 
        {
            Location = new Vector2I();
            Direction = new Vector2I();
        }

        public Transform(Vector2I location, Vector2I direction)
        {
            Location = location;
            Direction = direction;
        }

        public static bool operator ==(Transform left, Transform right)
        {
            return left.Location == right.Location && left.Direction == right.Direction;
        }

        public static bool operator !=(Transform left, Transform right)
        {
            return !(left == right);
        }

        public override bool Equals(object? other)
        {
            return null != other && other is Transform p && (p.Location.X, p.Location.Y,p.Direction.X,p.Direction.Y).Equals((Location.X, Location.Y, Direction.X, Direction.Y));
        }

        public override int GetHashCode() => (Location.X, Location.Y, Direction.X, Direction.Y).GetHashCode();

        public override string ToString()
        {
            return String.Concat(Location.X.ToString(), ",", Location.Y.ToString(),">", Direction.X.ToString(), ",", Direction.Y.ToString());
        }
    }
}
