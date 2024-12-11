using Common;

namespace AdventOfCode2024Solutions.Day05
{
    public class Solution : IAOCSolution
    {
        public string PuzzleName => "Day 5: Print Queue";

        public string SolvePart1(string[] datasetLines)
        {
            Manual manual = new Manual(datasetLines);
            manual.CheckUpdatesAgainstRules();
            return manual.SumMiddleNumbers.ToString();
        }

        public string SolvePart2(string[] datasetLines)
        {
            return "To be implemented";
        }
    }
}
