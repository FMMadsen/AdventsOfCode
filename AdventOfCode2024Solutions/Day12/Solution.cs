using Common;

namespace AdventOfCode2024Solutions.Day12
{
    public class Solution : IAOCSolution
    {
        public string PuzzleName => "Day 12: Garden Groups";

        public string SolvePart1(string[] datasetLines)
        {
            var map = new GardenPlotMap(datasetLines, new GardenPlotFactory());
            return map.CalculateTotalPriceOfFences().ToString();
        }

        public string SolvePart2(string[] datasetLines)
        {
            var map = new GardenPlotMap(datasetLines, new GardenPlotFactory());
            return map.CalculateTotalPriceOfFencesWithDiscount().ToString();
        }
    }
}
