using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024Solutions.Day04
{
    public struct Vector2I
    {
        public int X = 0;
        public int Y = 0;

        public Vector2I()
        {

        }

        public Vector2I(int x, int y)
        {
            X = x;
            Y = y;
        }

        public static readonly Vector2I Zero = new Vector2I(); 
        public static readonly Vector2I Off = new Vector2I(-1,-1);

        public static Vector2I NN { get { return new Vector2I(0, -1); } }
        public static Vector2I NE { get { return new Vector2I(1, -1); } }
        public static Vector2I EE { get { return new Vector2I(1, 0); } }
        public static Vector2I SE { get { return new Vector2I(1, 1); } }
        public static Vector2I SS { get { return new Vector2I(0, 1); } }
        public static Vector2I SW { get { return new Vector2I(-1, 1); } }
        public static Vector2I WW { get { return new Vector2I(-1, 0); } }
        public static Vector2I NW { get { return new Vector2I(-1, -1); } }

        public static Vector2I operator +(Vector2I left , Vector2I right)
        {
            return new Vector2I(left.X + right.X, left.Y + right.Y);
        }

        public static Vector2I operator -(Vector2I left, Vector2I right)
        {
            return new Vector2I(left.X - right.X, left.Y - right.Y);
        }

        public static Vector2I operator *(Vector2I left, Vector2I right)
        {
            return new Vector2I(left.X * right.X, left.Y * right.Y);
        }

        public static Vector2I operator *(Vector2I left, int right)
        {
            return new Vector2I(left.X * right, left.Y * right);
        }

        public static Vector2I operator *(int left, Vector2I right)
        {
            return new Vector2I(left * right.X, left * right.Y);
        }

        public static bool operator <(Vector2I left, Vector2I right)
        {
            return left.X * left.X + left.Y * left.Y < right.X * right.X + right.Y * right.Y;
        }

        public static bool operator >(Vector2I left, Vector2I right)
        {
            return left.X * left.X + left.Y * left.Y > right.X * right.X + right.Y * right.Y;
        }

        public static bool operator ==(Vector2I left, Vector2I right)
        {
            return left.X == right.X && left.Y == right.Y;
        }

        public static bool operator !=(Vector2I left, Vector2I right)
        {
            return !(left == right);
        }

        public override bool Equals(object? other)
        {
            return null != other && other is Vector2I p && (p.X, p.Y).Equals((X, Y));
        }

        public override int GetHashCode() => (X, Y).GetHashCode();
        


    }
}
