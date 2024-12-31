using AdventOfCode2024Solutions.Day16.GenericMapping;

namespace AdventOfCode2024Solutions.Day12
{
    public class GardenPlot(int x, int y, char source) : GenericMapTile(x, y, source)
    {
        public int RegionID { get; set; } = -1;
        public GardenPlot? NorthRegionLink { get; set; } = null;
        public GardenPlot? SouthRegionLink { get; set; } = null;
        public GardenPlot? EastRegionLink { get; set; } = null;
        public GardenPlot? WestRegionLink { get; set; } = null;

        public int CountPeremiters()
        {
            int count = 0;
            if (NorthRegionLink == null)
                count++;
            if (SouthRegionLink == null)
                count++;
            if (EastRegionLink == null)
                count++;
            if (WestRegionLink == null)
                count++;
            return count;
        }
    }
}
