using ToolsFramework.Map;

namespace AdventOfCode2025Solutions.Day07
{
    public class TachyonManifoldTileFactory : GenericMapTileFactory
    {
        public override GenericMapTile Create(int x, int y, char source)
        {
            return source switch
            {
                '.' => new SpaceTile(x, y, source),
                'S' => new StartTile(x, y, source),
                '^' => new SplitterTile(x, y, source),
                _ => new Location(x, y, source),
            };
        }
    }
}