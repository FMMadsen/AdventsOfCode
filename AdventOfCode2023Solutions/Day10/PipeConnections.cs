

namespace AdventOfCode2023Solutions.Day10
{
    public static class PipeConnections
    {
        public static PipeVector2 North = new PipeVector2() { X = 0, Y = -1 };
        public static PipeVector2 East = new PipeVector2() { X = 1, Y = 0 };
        public static PipeVector2 South = new PipeVector2() { X = 0, Y = 1 };
        public static PipeVector2 West = new PipeVector2() { X = -1, Y = 0 };

        public static Dictionary<char, PipeDirection> PipeDirectionFromChar = new Dictionary<char, PipeDirection>()
        {
            { '-', PipeDirection.Horizontal },
            { '|', PipeDirection.Vertical },
            { 'L', PipeDirection.NE },
            { 'J', PipeDirection.NW },
            {'F', PipeDirection.SE },
            {'7', PipeDirection.SW }
        };

        public static Dictionary<string, PipeDirection> PipeDirectionFromString = new Dictionary<string, PipeDirection>()
        {
            { "EW", PipeDirection.Horizontal },
            { "NS", PipeDirection.Vertical },
            { "NE", PipeDirection.NE },
            { "NW", PipeDirection.NW },
            { "SE", PipeDirection.SE },
            { "SW", PipeDirection.SW }
        };

        public static Dictionary<PipeVector2, Pipe?> ConnectionsFromDirection(PipeDirection direction)
        {
            Dictionary<PipeVector2, Pipe?> connections = new Dictionary<PipeVector2, Pipe?>();

            if (PipeDirection.North.HasFlag(direction))
            {
                connections.Add(new PipeVector2() { X = 0, Y = -1 }, null);
            }
            if (PipeDirection.South.HasFlag(direction))
            {
                connections.Add(new PipeVector2() { X = 0, Y = 1 }, null);
            }
            if (PipeDirection.East.HasFlag(direction))
            {
                connections.Add(new PipeVector2() { X = 1, Y = 0 }, null);
            }
            if (PipeDirection.West.HasFlag(direction))
            {
                connections.Add(new PipeVector2() { X = -1, Y = 0 }, null);
            }

            return connections;
        }
    }


}
