

namespace AdventOfCode2023Solutions.Day11
{
    public struct IntVector2
    {
        public int X, Y;

        public static IntVector2 operator +(IntVector2 vector1, IntVector2 vector2)
        {
            return new IntVector2() { X = vector1.X + vector2.X, Y = vector1.Y + vector2.Y };
        }
        public static IntVector2 operator -(IntVector2 vector1, IntVector2 vector2)
        {
            return new IntVector2() { X = vector1.X - vector2.X, Y = vector1.Y - vector2.Y };
        }
        public static IntVector2 operator -(IntVector2 vector1)
        {
            return new IntVector2() { X = -vector1.X, Y = -vector1.Y };
        }
        public static IntVector2 operator *(IntVector2 vector1, IntVector2 vector2)
        {
            return new IntVector2() { X = vector1.X * vector2.X, Y = vector1.Y * vector2.Y };
        }
        public static IntVector2 operator /(IntVector2 vector1, IntVector2 vector2)
        {
            return new IntVector2() { X = vector1.X / vector2.X, Y = vector1.Y / vector2.Y };
        }
        public static bool operator ==(IntVector2 vector1, IntVector2 vector2)
        {
            return vector1.X == vector2.X && vector1.Y == vector2.Y;
        }
        public static bool operator !=(IntVector2 vector1, IntVector2 vector2)
        {
            return vector1.X != vector2.X || vector1.Y != vector2.Y;
        }

        public override bool Equals(object? obj)
        {
            return obj is IntVector2 vector &&
                   X == vector.X &&
                   Y == vector.Y;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y);
        }


        public IntVector2 Right90()
        {
            return new IntVector2 { X = -Y, Y = X };
        }
        public IntVector2 Left90()
        {
            return new IntVector2 { X = Y, Y = -X };
        }
        public IntVector2 Back180()
        {
            return new IntVector2 { X = -X, Y = -Y };
        }
    }


}
