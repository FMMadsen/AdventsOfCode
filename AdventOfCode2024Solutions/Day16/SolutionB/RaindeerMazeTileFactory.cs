using AdventOfCode2024Solutions.Day16.SolutionB.Tiles;
using ToolsFramework.Map;

namespace AdventOfCode2024Solutions.Day16.SolutionB
{
    public class RaindeerMazeTileFactory : GenericMapTileFactory
    {
        public override GenericMapTile Create(int x, int y, char source)
        {
            return source switch
            {
                'E' => new EndTile(x, y, source),
                'S' => new StartTile(x, y, source),
                '.' => new PathTile(x, y, source),
                '#' => new WallTile(x, y, source),
                _ => new GenericMapTile(x, y, source),
            };
        }
    }
}