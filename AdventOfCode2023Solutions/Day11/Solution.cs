using Common;

namespace AdventOfCode2023Solutions.Day11
{
    public class Solution : IAOCSolution
    {
        public string PuzzleName => "Day 11: Cosmic Expansion";

        public string SolvePart1(string[] datasetLines) => SolvePart1(datasetLines, 2);
        public string SolvePart1(string[] datasetLines, long expanding)
        {
            var universe = new Universe(datasetLines);
            universe.ExpandUniverse(expanding);
            universe.InitializeGalaxyRoutes();
            var sumOfRouteDistances = universe.GalaxyRoutes.Sum(gr => gr.CalculateDistance());
            return sumOfRouteDistances.ToString();
        }

        public string SolvePart2(string[] datasetLines) => SolvePart1(datasetLines, 1000000);
        public string SolvePart2(string[] datasetLines, long expanding)
        {
            var universe = new Universe(datasetLines);
            universe.ExpandUniverse(expanding);
            universe.InitializeGalaxyRoutes();
            var sumOfRouteDistances = universe.GalaxyRoutes.Sum(gr => gr.CalculateDistance());
            return sumOfRouteDistances.ToString();
        }
    }
}
