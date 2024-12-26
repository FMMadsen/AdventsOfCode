namespace AdventOfCode2024Solutions.Day16.GenericMapping
{
    public class GenericMapTile(int x, int y, char source)
    {
        public string Id { get; private set; } = $":{x},{y}:";
        public int Y { get; private set; } = y;
        public int X { get; private set; } = x;
        public char Source { get; protected set; } = source;
        public GenericMapTile? North { get; set; } = null;
        public GenericMapTile? South { get; set; } = null;
        public GenericMapTile? East { get; set; } = null;
        public GenericMapTile? West { get; set; } = null;

        public GenericMapTile? GetTileStraightAhead(GenericDirection GenericDirection)
        {
            switch (GenericDirection)
            {
                case GenericDirection.North: return North;
                case GenericDirection.South: return South;
                case GenericDirection.West: return West;
                case GenericDirection.East: return East;
                default: return null;
            }
        }

        public GenericMapTile? GetTileToTheLeft(GenericDirection GenericDirection)
        {
            switch (GenericDirection)
            {
                case GenericDirection.North: return West;
                case GenericDirection.South: return East;
                case GenericDirection.West: return South;
                case GenericDirection.East: return North;
                default: return null;
            }
        }

        public GenericMapTile? GetTileToTheRight(GenericDirection GenericDirection)
        {
            switch (GenericDirection)
            {
                case GenericDirection.North: return East;
                case GenericDirection.South: return West;
                case GenericDirection.West: return North;
                case GenericDirection.East: return South;
                default: return null;
            }
        }
    }
}