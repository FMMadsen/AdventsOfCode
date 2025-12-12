using ToolsFramework.Map;

namespace AdventOfCode2024Solutions.Day16.SolutionB.Tiles
{
    public class PathTile(int x, int y, char source) : GenericMapTile(x, y, source)
    {
        public bool IsDeadEnd { get; set; } = false;
        public bool IsJunction { get; set; } = false;
        public bool HasNorthPath { get; private set; } = false;
        public bool HasSouthPath { get; private set; } = false;
        public bool HasEastPath { get; private set; } = false;
        public bool HasWestPath { get; private set; } = false;
        public bool IsBlocked { get; private set; } = false;
        public bool IsStartLocation { get; private set; } = false;
        public bool IsEndLocation { get; private set; } = false;

        private string? tileCacheKeyNorth = null;
        private string? tileCacheKeySouth = null;
        private string? tileCacheKeyEast = null;
        private string? tileCacheKeyWest = null;

        public void SetPathTileProperties()
        {
            IsStartLocation = Source == 'S';
            IsEndLocation = Source == 'E';

            CountExitsAndSetDeadEnd();
        }

        public void CountExitsAndSetDeadEnd()
        {
            var countPathsFromHere = 0;

            if (North is PathTile north && !north.IsBlocked)
            {
                HasNorthPath = true;
                countPathsFromHere++;
            }

            if (South is PathTile south && !south.IsBlocked)
            {
                HasSouthPath = true;
                countPathsFromHere++;
            }

            if (East is PathTile east && !east.IsBlocked)
            {
                HasEastPath = true;
                countPathsFromHere++;
            }

            if (West is PathTile west && !west.IsBlocked)
            {
                HasWestPath = true;
                countPathsFromHere++;
            }

            IsDeadEnd = countPathsFromHere == 1 && !(IsStartLocation || IsEndLocation);
            IsJunction = countPathsFromHere > 2 && !(IsStartLocation || IsEndLocation);
        }

        public void BlockTile()
        {
            IsBlocked = true;

            if (North is PathTile north)
                north.HasSouthPath = false;

            if (South is PathTile south)
                south.HasNorthPath = false;

            if (East is PathTile east)
                east.HasWestPath = false;

            if (West is PathTile west)
                west.HasEastPath = false;

            HasNorthPath = false;
            HasSouthPath = false;
            HasEastPath = false;
            HasWestPath = false;

            Source = 'O';
        }

        public string GetTileAndDirectionCacheKey(GenericDirection direction)
        {
            switch (direction)
            {
                case GenericDirection.North:
                    if (tileCacheKeyNorth == null)
                        tileCacheKeyNorth = $"{X},{Y}N";
                    return tileCacheKeyNorth;
                case GenericDirection.South:
                    if (tileCacheKeySouth == null)
                        tileCacheKeySouth = $"{X},{Y}S";
                    return tileCacheKeySouth;

                case GenericDirection.East:
                    if (tileCacheKeyEast == null)
                        tileCacheKeyEast = $"{X},{Y}E";
                    return tileCacheKeyEast;

                case GenericDirection.West:
                    if (tileCacheKeyWest == null)
                        tileCacheKeyWest = $"{X},{Y}W";
                    return tileCacheKeyWest;

                default: return string.Empty;
            }
        }
    }
}