using ToolsFramework.Map;

namespace AdventOfCode2025Solutions.Day07
{
    internal class TachyonManifold : GenericMap
    {
        public TachyonManifold(string[] mapLines) : base(mapLines, new TachyonManifoldTileFactory())
        {
            Start = MapTileList.Where(x => x is StartTile).FirstOrDefault() as StartTile ?? throw new Exception("StartLocation not found");
        }

        public StartTile Start { get; private set; }
    }
}