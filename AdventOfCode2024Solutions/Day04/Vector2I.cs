using System;
using System.Collections.Generic;
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
    }
}
