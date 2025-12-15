using ToolsFramework.Geometry;

namespace AdventOfCode2025Solutions.Day09
{
    public class RedTile : Coordinate
    {
        public bool IsLeft { get; private set; } = false;
        public bool IsRight => !IsLeft;
        public void SetLeft() => IsLeft = true;
        public void SetRight() => IsLeft = false;
    }
}