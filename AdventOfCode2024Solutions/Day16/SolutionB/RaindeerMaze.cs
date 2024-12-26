using AdventOfCode2024Solutions.Day16.GenericMapping;

namespace AdventOfCode2024Solutions.Day16.SolutionB
{
    public class RaindeerMaze : GenericMap
    {
        public StartTile StartLocation { get; private set; }
        public EndTile EndLocation { get; private set; }
        public List<WallTile> WallTiles { get; private set; }
        public List<PathTile> PathTiles { get; private set; }

        public RaindeerMaze(string[] mapLines, GenericMapTileFactory mapTileFactory) : base(mapLines, mapTileFactory)
        {
            StartLocation = MapTileList.Where(x => x is StartTile).FirstOrDefault() as StartTile ?? throw new Exception("StartLocation not found");
            EndLocation = MapTileList.Where(x => x is EndTile).FirstOrDefault() as EndTile ?? throw new Exception("EndLocation not found");

            WallTiles = MapTileList.Where(x => x is WallTile).Select(x => (WallTile)x).ToList();
            PathTiles = MapTileList.Where(x => x is PathTile).Select(x => (PathTile)x).ToList();

            PathTiles.ForEach(x => x.SetPathTileProperties());

            if (Solution.WriteDebugInfoToConsole_PrintMapInitially)
                Solution.PrintMapToConsole(MapTiles);

            BlockDeadEnds();

            if (Solution.WriteDebugInfoToConsole_PrintMapInitially)
                Solution.PrintMapToConsole(MapTiles);
        }

        private void BlockDeadEnds()
        {
            var deadEndPathTiles = PathTiles.Where(x => x.IsDeadEnd);

            foreach (var deadEndTile in deadEndPathTiles)
            {
                BlockPath(deadEndTile);
            }
        }

        private void BlockPath(PathTile pathTile)
        {
            pathTile.BlockTile();

            PathTile? connectedTileToBlock = null;

            var north = pathTile.North as PathTile;
            if (north != null && ShoudBlockTile(north))
                connectedTileToBlock = north;

            var south = pathTile.South as PathTile;
            if (south != null && ShoudBlockTile(south))
                connectedTileToBlock = south;

            var east = pathTile.East as PathTile;
            if (east != null && ShoudBlockTile(east))
                connectedTileToBlock = east;

            var west = pathTile.West as PathTile;
            if (west != null && ShoudBlockTile(west))
                connectedTileToBlock = west;

            if (connectedTileToBlock != null)
                BlockPath(connectedTileToBlock);
        }

        private bool ShoudBlockTile(PathTile pathTile)
        {
            pathTile.CountExitsAndSetDeadEnd();
            return pathTile.IsDeadEnd && !pathTile.IsBlocked && !pathTile.IsJunction && !pathTile.IsStartLocation && !pathTile.IsEndLocation;
        }
    }
}
