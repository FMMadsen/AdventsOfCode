using AdventOfCode2024Solutions.Day16.GenericMapping;

namespace AdventOfCode2024Solutions.Day16.SolutionB
{
    public class RaindeerMazeTileFactory : GenericMapTileFactory
    {
        public override GenericMapTile Create(int x, int y, char source)
        {
            switch (source)
            {
                case 'E':
                    return new EndTile(x, y, source);
                case 'S':
                    return new StartTile(x, y, source);
                case '.':
                    return new PathTile(x, y, source);
                case '#':
                    return new WallTile(x, y, source);
                default:
                    return new GenericMapTile(x, y, source);
            }
        }
    }
}
