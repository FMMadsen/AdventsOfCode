namespace AdventOfCode2025Solutions.Day09
{
    internal class TileRow
    {
        public TileRow(RedTile leftMostRedTile, RedTile rightMostRedTile)
        {
            if (leftMostRedTile.Y != rightMostRedTile.Y)
                throw new ArgumentException("Both input must have same Y value to be in same row");

            LeftRedTile = leftMostRedTile;
            RightRedTile = rightMostRedTile;
            Row = RightRedTile.Y;

            LeftMost = LeftRedTile.X;
            RightMost = RightRedTile.X;
        }

        public long Row { get; init; }
        public long LeftMost { get; set; }
        public long RightMost { get; set; }

        public RedTile LeftRedTile { get; init; }
        public RedTile RightRedTile { get; init; }
    }
}