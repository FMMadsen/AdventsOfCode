

namespace AdventOfCode2023Solutions.Day10
{
    public struct PipeVector2
    {
        public int X, Y;

        public static PipeVector2 operator +(PipeVector2 vector1, PipeVector2 vector2)
        {
            return new PipeVector2() { X = vector1.X + vector2.X, Y = vector1.Y + vector2.Y };
        }
        public static PipeVector2 operator -(PipeVector2 vector1, PipeVector2 vector2)
        {
            return new PipeVector2() { X = vector1.X - vector2.X, Y = vector1.Y - vector2.Y };
        }
        public static PipeVector2 operator -(PipeVector2 vector1)
        {
            return new PipeVector2() { X = -vector1.X, Y = -vector1.Y };
        }
        public static PipeVector2 operator *(PipeVector2 vector1, PipeVector2 vector2)
        {
            return new PipeVector2() { X = vector1.X * vector2.X, Y = vector1.Y * vector2.Y };
        }
        public static PipeVector2 operator /(PipeVector2 vector1, PipeVector2 vector2)
        {
            return new PipeVector2() { X = vector1.X / vector2.X, Y = vector1.Y / vector2.Y };
        }
        public static bool operator ==(PipeVector2 vector1, PipeVector2 vector2)
        {
            return vector1.X == vector2.X && vector1.Y == vector2.Y;
        }
        public static bool operator !=(PipeVector2 vector1, PipeVector2 vector2)
        {
            return vector1.X != vector2.X || vector1.Y != vector2.Y;
        }

        public override bool Equals(object? obj)
        {
            return obj is PipeVector2 vector &&
                   X == vector.X &&
                   Y == vector.Y;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y);
        }


        public PipeVector2 Right90()
        {
            return new PipeVector2 { X = -Y, Y = X };
        }
        public PipeVector2 Left90()
        {
            return new PipeVector2 { X = Y, Y = -X };
        }
        public PipeVector2 Back180()
        {
            return new PipeVector2 { X = -X, Y = -Y };
        }
    }


}
