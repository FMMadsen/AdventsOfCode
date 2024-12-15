using Common;

namespace AdventOfCode2024Solutions.Day05
{
    public class Solution : IAOCSolution
    {
        public string PuzzleName => "Day 5: Print Queue";

        public string SolvePart1(string[] datasetLines)
        {
            var manual = new Manual(datasetLines);
            return manual.GetSumMiddleNumbersOfCorrectOrdered().ToString();
        }

        public string SolvePart2(string[] datasetLines)
        {
            var manual = new Manual(datasetLines);
            manual.FixIncorrectUpdates();
            return manual.GetSumMiddleNumbersOfIncorrectOrderedFixed().ToString();
        }
    }
}
