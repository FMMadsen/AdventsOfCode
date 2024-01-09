

namespace AdventOfCode2023Solutions.Day10
{
    [Flags]
    public enum PipeDirection
    {
        NonPipe = 0,
        Horizontal = 1,
        Vertical = 2,
        NE = 4,
        NW = 8,
        SE = 16,
        SW = 32,
        North = Vertical | NE | NW,
        South = Vertical | SE | SW,
        East = Horizontal | NE | SE,
        West = Horizontal | NW | SW
    }


}
