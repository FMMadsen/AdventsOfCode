namespace ToolsFramework.Geometry
{
    public class Rect
    {
        public Coordinate Corner1 { get; set; }
        public Coordinate Corner2 { get; set; }

        public long Width => Corner2.X - Corner1.X;
        public long Height => Corner2.Y - Corner1.Y;

        // Constructor
        public Rect(Coordinate corner1, Coordinate corner2)
        {
            Corner1 = corner1;
            Corner2 = corner2;
        }

        // Static factory method
        public static Rect FromCoordinates(Coordinate corner1, Coordinate corner2)
        {
            return new Rect(corner1, corner2);
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
