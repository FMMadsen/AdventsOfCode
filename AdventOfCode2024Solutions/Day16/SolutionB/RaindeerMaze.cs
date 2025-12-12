using AdventOfCode2024Solutions.Day16.SolutionB.Tiles;
using ToolsFramework.Map;

namespace AdventOfCode2024Solutions.Day16.SolutionB
{
    public class RaindeerMaze : GenericMap
    {
        public StartTile StartLocation { get; private set; }
        public EndTile EndLocation { get; private set; }
        public List<WallTile> WallTiles { get; private set; }
        public List<PathTile> PathTiles { get; private set; }

        public RaindeerMaze(string[] mapLines) : base(mapLines, new RaindeerMazeTileFactory())
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

        private static void BlockPath(PathTile pathTile)
        {
            pathTile.BlockTile();

            PathTile? connectedTileToBlock = null;

            if (pathTile.North is PathTile north && ShoudBlockTile(north))
                connectedTileToBlock = north;

            if (pathTile.South is PathTile south && ShoudBlockTile(south))
                connectedTileToBlock = south;

            if (pathTile.East is PathTile east && ShoudBlockTile(east))
                connectedTileToBlock = east;

            if (pathTile.West is PathTile west && ShoudBlockTile(west))
                connectedTileToBlock = west;

            if (connectedTileToBlock != null)
                BlockPath(connectedTileToBlock);
        }

        private static bool ShoudBlockTile(PathTile pathTile)
        {
            pathTile.CountExitsAndSetDeadEnd();
            return pathTile.IsDeadEnd && !pathTile.IsBlocked && !pathTile.IsJunction && !pathTile.IsStartLocation && !pathTile.IsEndLocation;
        }
    }
}