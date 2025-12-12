namespace ToolsFramework.Map
{
    public class GenericMapTile(int x, int y, char source)
    {
        public string Id { get; private set; } = $":{x},{y}:";
        public int X => x;
        public int Y => y;
        public char Source { get; set; } = source;
        public GenericMapTile? North { get; set; } = null;
        public GenericMapTile? NorthEast { get; set; } = null;
        public GenericMapTile? East { get; set; } = null;
        public GenericMapTile? SouthEast { get; set; } = null;
        public GenericMapTile? South { get; set; } = null;
        public GenericMapTile? SouthWest { get; set; } = null;
        public GenericMapTile? West { get; set; } = null;
        public GenericMapTile? NorthWest { get; set; } = null;

        public override string ToString() => Source.ToString();
        public string ToStringExtended() => $"{Source}({x},{y})";
        public string ToStringSouroundingsClockwise()
            => $"{North}{NorthEast}{East}{SouthEast}{South}{SouthWest}{West}{NorthWest}";

        public int CountSouroundedBy(char tileType)
        {
            int count = 0;
            count += North?.Source == tileType ? 1 : 0;
            count += NorthEast?.Source == tileType ? 1 : 0;
            count += East?.Source == tileType ? 1 : 0;
            count += SouthEast?.Source == tileType ? 1 : 0;
            count += South?.Source == tileType ? 1 : 0;
            count += SouthWest?.Source == tileType ? 1 : 0;
            count += West?.Source == tileType ? 1 : 0;
            count += NorthWest?.Source == tileType ? 1 : 0;
            return count;
        }

        public GenericMapTile? GetTile(GenericDirection GenericDirection)
        {
            return GenericDirection switch
            {
                GenericDirection.North => North,
                GenericDirection.South => South,
                GenericDirection.West => West,
                GenericDirection.East => East,
                _ => null,
            };
        }

        public GenericMapTile? GetTileLeftOf(GenericDirection GenericDirection)
        {
            return GenericDirection switch
            {
                GenericDirection.North => West,
                GenericDirection.South => East,
                GenericDirection.West => South,
                GenericDirection.East => North,
                _ => null,
            };
        }

        public GenericMapTile? GetTileRightOf(GenericDirection GenericDirection)
        {
            return GenericDirection switch
            {
                GenericDirection.North => East,
                GenericDirection.South => West,
                GenericDirection.West => North,
                GenericDirection.East => South,
                _ => null,
            };
        }
    }
}