using Common;

namespace AdventOfCode2023Solutions.Day03
{
    public class Solution(string[] datasetLines) : IAOCSolution
    {
        public string PuzzleName => "Day 3: Gear Ratios";

        public string[] DatasetLines => datasetLines;

        public string SolvePart1()
        {
            var engine = new Engine(DatasetLines);
            var partNumberSum = engine.EngineParts.Where(p => p.IsMissingPart).Sum(p => p.PartNumber);
            return partNumberSum.ToString();
        }

        public string SolvePart2()
        {
            var engine = new Engine(DatasetLines);
            var gears = engine.EngineSymbols.Where(s => s.IsGeer()).Select(s => new Gear(s));
            var gearRatioSum = gears.Sum(s => s.GearRatio);
            return gearRatioSum.ToString();
        }
    }
}
