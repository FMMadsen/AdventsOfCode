using Common;

namespace AdventOfCode2024Solutions.Day11
{
    public class Solution : IAOCSolution
    {
        public string PuzzleName => "Day 11: Plutonian Pebbles";

        public string SolvePart1(string[] datasetLines)
        {
            var stones = new Stones(datasetLines[0]);
            stones.Blink(25);
            return stones.Count().ToString();
        }

        public string SolvePart2(string[] datasetLines)
        {
            return "To be implemented";
        }
    }
}
