namespace ToolsFramework.Geometry
{
    public class Coordinate(long x, long y)
    {
        public long X { get; set; } = x;
        public long Y { get; set; } = y;

        // Static factory method
        public static Coordinate FromCoordinateStringXY(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                throw new ArgumentException("Input cannot be null or empty.", nameof(input));

            var parts = input.Split(',');
            if (parts.Length != 2)
                throw new FormatException("Input must be in the format 'number,number'.");

            if (!long.TryParse(parts[0].Trim(), out long x))
                throw new FormatException("X coordinate is not a valid long.");

            if (!long.TryParse(parts[1].Trim(), out long y))
                throw new FormatException("Y coordinate is not a valid long.");

            return new Coordinate(x, y);
        }
    }
}
