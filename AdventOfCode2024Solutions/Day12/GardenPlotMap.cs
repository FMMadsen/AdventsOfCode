using AdventOfCode2024Solutions.Day16.GenericMapping;

namespace AdventOfCode2024Solutions.Day12
{
    public class GardenPlotMap : GenericMap
    {
        public List<GardenPlotRegion> regions = [];
        public GardenPlot[,] gardenPlots;

        public GardenPlotMap(string[] mapLines, GenericMapTileFactory mapTileFactory) : base(mapLines, mapTileFactory)
        {
            gardenPlots = new GardenPlot[NoOfYTiles, NoOfXTiles];
            for (int y = 0; y < NoOfYTiles; y++)
                for (int x = 0; x < NoOfXTiles; x++)
                    gardenPlots[y, x] = (GardenPlot)MapTiles[y, x];

            for (int y = 0; y < NoOfYTiles; y++)
                for (int x = 0; x < NoOfXTiles; x++)
                    AddPlotToRegion(gardenPlots[y, x]);
        }

        public int CalculateTotalPriceOfFences()
        {
            return regions.Sum(r => r.CalculatePriceOfFence());
        }

        public int CalculateTotalPriceOfFencesWithDiscount()
        {
            return regions.Sum(r => r.CalculatePriceOfFenceWithDiscount());
        }

        private void AddPlotToRegion(GardenPlot plot)
        {
            if (plot.RegionID != -1)
                return;

            var region = new GardenPlotRegion();
            regions.Add(region);
            recursiveAddPlotToRegion(plot, region);
        }

        private void recursiveAddPlotToRegion(GardenPlot plot, GardenPlotRegion region)
        {
            region.Add(plot);
            plot.RegionID = region.Id;

            if (plot.North != null && plot.Source == plot.North.Source)
                plot.NorthRegionLink = (GardenPlot)plot.North;

            if (plot.South != null && plot.Source == plot.South.Source)
                plot.SouthRegionLink = (GardenPlot)plot.South;

            if (plot.East != null && plot.Source == plot.East.Source)
                plot.EastRegionLink = (GardenPlot)plot.East;

            if (plot.West != null && plot.Source == plot.West.Source)
                plot.WestRegionLink = (GardenPlot)plot.West;

            if (plot.NorthRegionLink != null && plot.NorthRegionLink.RegionID == -1)
                recursiveAddPlotToRegion(plot.NorthRegionLink, region);

            if (plot.SouthRegionLink != null && plot.SouthRegionLink.RegionID == -1)
                recursiveAddPlotToRegion(plot.SouthRegionLink, region);

            if (plot.EastRegionLink != null && plot.EastRegionLink.RegionID == -1)
                recursiveAddPlotToRegion(plot.EastRegionLink, region);

            if (plot.WestRegionLink != null && plot.WestRegionLink.RegionID == -1)
                recursiveAddPlotToRegion(plot.WestRegionLink, region);
        }
    }
}
