namespace AdventOfCode2023Solutions.Day18
{
    public class Position(long y, long x)
    {
        public long Y { get; set; } = y;
        public long X { get; set; } = x;

        public Position Clone()
        {
            return new Position(Y, X);
        }
    }
}
