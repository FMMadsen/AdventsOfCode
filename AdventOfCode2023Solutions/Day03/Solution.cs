using Common;

namespace AdventOfCode2023Solutions.Day03
{
    public class Solution : IAOCSolution
    {
        public string PuzzleName => "Day 3: Gear Ratios";

        public string SolvePart1(string[] datasetLines)
        {
            var engine = new Engine(datasetLines);
            var partNumberSum = engine.EngineParts.Where(p => p.IsMissingPart).Sum(p => p.PartNumber);
            return partNumberSum.ToString();
        }

        public string SolvePart2(string[] datasetLines)
        {
            var engine = new Engine(datasetLines);
            var gears = engine.EngineSymbols.Where(s => s.IsGeer()).Select(s => new Gear(s));
            var gearRatioSum = gears.Sum(s => s.GearRatio);
            return gearRatioSum.ToString();
        }
    }
}
