namespace ToolsFramework.Geometry
{
    public class Coordinate
    {
        public long X { get; private set; }
        public long Y { get; private set; }

        public static T FromCoordinateStringXY<T>(long x, long y)
            where T : Coordinate, new()
        {
            return new T()
            {
                X = x,
                Y = y,
            };
        }

        public static T FromCoordinateStringXY<T>(string input)
            where T : Coordinate, new()
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

            return new T()
            {
                X = x,
                Y = y,
            };
        }

        public override string ToString()
        {
            return $"{X},{Y}";
        }
    }
}