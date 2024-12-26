using AdventOfCode2024Solutions.Day16.GenericMapping;

namespace AdventOfCode2024Solutions.Day16.SolutionB
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

            var north = North as PathTile;
            var south = South as PathTile;
            var east = East as PathTile;
            var west = West as PathTile;

            if (north != null && !north.IsBlocked)
            {
                HasNorthPath = true;
                countPathsFromHere++;
            }

            if (south != null && !south.IsBlocked)
            {
                HasSouthPath = true;
                countPathsFromHere++;
            }

            if (east != null && !east.IsBlocked)
            {
                HasEastPath = true;
                countPathsFromHere++;
            }

            if (west != null && !west.IsBlocked)
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

            var north = North as PathTile;
            var south = South as PathTile;
            var east = East as PathTile;
            var west = West as PathTile;

            if (north != null)
                north.HasSouthPath = false;

            if (south != null)
                south.HasNorthPath = false;

            if (east != null)
                east.HasWestPath = false;

            if (west != null)
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
