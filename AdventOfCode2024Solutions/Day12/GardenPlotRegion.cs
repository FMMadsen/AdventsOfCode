namespace AdventOfCode2024Solutions.Day12
{
    public class GardenPlotRegion
    {
        private static int _nextRegionID = 0;
        private List<GardenPlot> gardenPlots = [];

        public int Id { get; } = _nextRegionID++;

        public void Add(GardenPlot plot)
        {
            gardenPlots.Add(plot);
        }

        public int CalculatePriceOfFence()
        {
            return gardenPlots.Count * gardenPlots.Sum(p => p.CountPeremiters());
        }

        public int CalculatePriceOfFenceWithDiscount()
        {
            var countFences = 0;
            
            var esternFences = gardenPlots.Where(p => p.EastRegionLink == null);
            countFences += CountVerticalFenceLines(esternFences);

            var westernFences = gardenPlots.Where(p => p.WestRegionLink == null);
            countFences += CountVerticalFenceLines(westernFences);

            var northernFences = gardenPlots.Where(p => p.NorthRegionLink == null);
            countFences += CountHorizontalFenceLines(northernFences);

            var southernFences = gardenPlots.Where(p => p.SouthRegionLink == null);
            countFences += CountHorizontalFenceLines(southernFences);

            return gardenPlots.Count * countFences;
        }

        private int CountVerticalFenceLines(IEnumerable<GardenPlot>? gardenPlotsWithVerticalFence)
        {
            if(gardenPlotsWithVerticalFence == null)
                return 0;

            var columnGroups = gardenPlotsWithVerticalFence.GroupBy(p => p.X);
            var count = 0;
            foreach(var columnGroup in columnGroups)
            {
                if(columnGroup == null)
                    continue;

                if (columnGroup.Count() == 0)
                    continue;

                var plotsInSameColumn = columnGroup.ToList();
                count++;

                if(plotsInSameColumn.Count > 1)
                {
                    var sortedPlotsInSameColumn = plotsInSameColumn.OrderBy(p => p.Y).ToList();
                    for (int i = 1; i < sortedPlotsInSameColumn.Count; i++)
                    {
                        if(sortedPlotsInSameColumn[i-1].Y + 1 != sortedPlotsInSameColumn[i].Y)
                            count++;
                    }
                }
            }
            return count;
        }

        private int CountHorizontalFenceLines(IEnumerable<GardenPlot>? gardenPlotsWithHorizontalFence)
        {
            if (gardenPlotsWithHorizontalFence == null)
                return 0;

            var rowGroups = gardenPlotsWithHorizontalFence.GroupBy(p => p.Y);
            var count = 0;
            foreach (var rowGroup in rowGroups)
            {
                if (rowGroup == null)
                    continue;

                if (rowGroup.Count() == 0)
                    continue;

                var plotsInSameRow = rowGroup.ToList();
                count++;

                if (plotsInSameRow.Count > 1)
                {
                    var sortedPlotsInSameRow = plotsInSameRow.OrderBy(p => p.X).ToList();
                    for (int i = 1; i < sortedPlotsInSameRow.Count; i++)
                    {
                        if (sortedPlotsInSameRow[i - 1].X + 1 != sortedPlotsInSameRow[i].X)
                            count++;
                    }
                }
            }
            return count;
        }
    }
}
