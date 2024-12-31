using AdventOfCode2024Solutions.Day16.GenericMapping;

namespace AdventOfCode2024Solutions.Day12
{
    public class GardenPlotFactory : GenericMapTileFactory
    {
        public override GenericMapTile Create(int x, int y, char source)
        {
            return new GardenPlot(x, y, source);
        }
    }
}
