using ToolsFramework.Map;

namespace AdventOfCode2024Solutions.Day12
{
    public class GardenPlotMap : GenericMap
    {
        public List<GardenPlotRegion> regions = [];
        public GardenPlot[,] gardenPlots;

        public GardenPlotMap(string[] mapLines) : base(mapLines, new GardenPlotFactory())
        {
            gardenPlots = new GardenPlot[NoOfXTiles, NoOfYTiles];
            for (int x = 0; x < NoOfXTiles; x++)
                for (int y = 0; y < NoOfYTiles; y++)
                    gardenPlots[x, y] = (GardenPlot)MapTiles[x, y];

            for (int x = 0; x < NoOfXTiles; x++)
                for (int y = 0; y < NoOfYTiles; y++)
                    AddPlotToRegion(gardenPlots[x, y]);
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
            RecursiveAddPlotToRegion(plot, region);
        }

        private static void RecursiveAddPlotToRegion(GardenPlot plot, GardenPlotRegion region)
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
                RecursiveAddPlotToRegion(plot.NorthRegionLink, region);

            if (plot.SouthRegionLink != null && plot.SouthRegionLink.RegionID == -1)
                RecursiveAddPlotToRegion(plot.SouthRegionLink, region);

            if (plot.EastRegionLink != null && plot.EastRegionLink.RegionID == -1)
                RecursiveAddPlotToRegion(plot.EastRegionLink, region);

            if (plot.WestRegionLink != null && plot.WestRegionLink.RegionID == -1)
                RecursiveAddPlotToRegion(plot.WestRegionLink, region);
        }
    }
}