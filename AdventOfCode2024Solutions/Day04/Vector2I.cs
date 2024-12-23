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

        public static Vector2I Zero { get; } = new Vector2I(); 
        public static Vector2I Off { get; } = new Vector2I(-1,-1);

        public static Vector2I NN { get; } = new Vector2I(0, -1);
        public static Vector2I NE { get; } = new Vector2I(1, -1);
        public static Vector2I EE { get; } = new Vector2I(1, 0);
        public static Vector2I SE { get; } = new Vector2I(1, 1);
        public static Vector2I SS { get; } = new Vector2I(0, 1);
        public static Vector2I SW { get; } = new Vector2I(-1, 1);
        public static Vector2I WW { get; } = new Vector2I(-1, 0);
        public static Vector2I NW { get; } = new Vector2I(-1, -1);

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

        public Vector2I TurnLeft()
        {
            int y = -X;
            int x = Y;
            return new Vector2I(x, y);
        }

        public Vector2I TurnRight()
        {
            int y = X;
            int x = -Y;
            return new Vector2I(x, y);
        }

        public Vector2I[] Neighbors(bool useDiagonal = false, bool onlyDiagonal = false)
        {
            Vector2I[] neighbors;

            if (useDiagonal)
            {
                if (onlyDiagonal)
                {
                    neighbors = new Vector2I[4];

                    neighbors[0] = this + Vector2I.NE;
                    neighbors[1] = this + Vector2I.SE;
                    neighbors[2] = this + Vector2I.SW;
                    neighbors[3] = this + Vector2I.NW;
                }
                else
                {
                    neighbors = new Vector2I[8];

                    neighbors[0] = this + Vector2I.NN;
                    neighbors[1] = this + Vector2I.NE;
                    neighbors[2] = this + Vector2I.EE;
                    neighbors[3] = this + Vector2I.SE;
                    neighbors[4] = this + Vector2I.SS;
                    neighbors[5] = this + Vector2I.SW;
                    neighbors[6] = this + Vector2I.WW;
                    neighbors[7] = this + Vector2I.NW;
                }
                
            }
            else
            {
                neighbors = new Vector2I[4];

                neighbors[0] = this + Vector2I.NN;
                neighbors[1] = this + Vector2I.EE;
                neighbors[2] = this + Vector2I.SS;
                neighbors[3] = this + Vector2I.WW;
            }

            return neighbors;
        }

        public override bool Equals(object? other)
        {
            return null != other && other is Vector2I p && (p.X, p.Y).Equals((X, Y));
        }

        public override int GetHashCode() => (X, Y).GetHashCode();

        public override string ToString()
        {
            return String.Concat(X.ToString(), ",", Y.ToString());
        }

    }
}
