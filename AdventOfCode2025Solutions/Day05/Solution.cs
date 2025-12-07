using Common;
using ToolsFramework;

namespace AdventOfCode2025Solutions.Day05
{
    public class Solution : IAOCSolution
    {
        public string PuzzleName => "Day 5: Cafeteria";

        public string SolvePart1(string[] datasetLines)
        {
            var splitLines = SplitInput(datasetLines);

            var qualityRanges = splitLines.Item1;
            var ingredienses = splitLines.Item2;

            var qualityFilter = NumberRanges.FromInitializationStringArray(qualityRanges);

            //Console.WriteLine($"qualityRanges: {qualityRanges.Length}");
            //ConsolePrinter.Print(qualityRanges);
            //Console.WriteLine($"qualityRanges organized: {qualityFilter.Ranges.Length}");
            //ConsolePrinter.Print(qualityFilter.Ranges);
            //Console.WriteLine($"ingredienses: {ingredienses.Length}");
            //ConsolePrinter.Print(ingredienses);

            var count = ingredienses.Count(qualityFilter.IsIncluded);
            return count.ToString();
        }

        public string SolvePart2(string[] datasetLines)
        {
            var splitLines = SplitInput(datasetLines);
            var qualityRanges = splitLines.Item1;
            var qualityFilter = NumberRanges.FromInitializationStringArray(qualityRanges);
            var countPositiveNumbers = qualityFilter.CountRangeItems();

            return countPositiveNumbers.ToString();
        }

        private static (string[], string[]) SplitInput(string[] datasetLines)
        {
            bool isQualityRanges = true;

            List<string> qualityRanges = [];
            List<string> ingredienses = [];

            foreach (var line in datasetLines)
            {
                if (string.IsNullOrWhiteSpace(line))
                    isQualityRanges = false;
                else if (isQualityRanges)
                    qualityRanges.Add(line);
                else
                    ingredienses.Add(line);
            }

            return (qualityRanges.ToArray(), ingredienses.ToArray());
        }
    }
}