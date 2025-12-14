namespace ToolsFramework.Geometry
{
    public class Rect
    {
        public Rect(Coordinate corner1, Coordinate corner2, bool bothInclusive = false)
        {
            Corner1 = corner1;
            Corner2 = corner2;

            if (corner2.X > corner1.X)
                Width = corner2.X - corner1.X;
            else
                Width = corner1.X - corner2.X;

            if (corner2.Y > corner1.Y)
                Height = corner2.Y - corner1.Y;
            else
                Height = corner1.Y - corner2.Y;

            if (bothInclusive)
            {
                Width++;
                Height++;
            }
        }

        public Coordinate Corner1 { get; }
        public Coordinate Corner2 { get; }

        public long Width { get; }
        public long Height { get; }

        public static Rect FromCoordinates(Coordinate corner1, Coordinate corner2, bool bothInclusive = false)
        {
            return new Rect(corner1, corner2, bothInclusive);
        }

        // Utility: check if a point lies inside
        public bool Contains(Coordinate point) =>
            point.X >= Corner1.X && point.X <= Corner2.X &&
            point.Y >= Corner1.Y && point.Y <= Corner2.Y;

        public long Area => Width * Height;

        public override string ToString() =>
            $"Rect [TopLeft=({Corner1.X},{Corner1.Y}), BottomRight=({Corner2.X},{Corner2.Y}), Width={Width}, Height={Height}]";
    }
}
