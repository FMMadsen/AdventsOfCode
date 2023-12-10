using Common;

namespace AdventOfCode2023Solutions.Day08
{
    public class Solution(string[] datasetLines) : IAOCSolution
    {
        public string PuzzleName => "Day 8: Haunted Wasteland";
        public string[] DatasetLines => datasetLines;

        public string SolvePart1()
        {
            var map = new Map(DatasetLines);
            var moveCount = map.CountMovesPart1("AAA", "ZZZ");
            return moveCount.ToString();
        }

        public string SolvePart2()
        {
            return "Not running - need fix!";
            var map = new Map(DatasetLines);
            var moveCount = map.CountMovesPart2("A", "Z");
            return moveCount.ToString();
        }
    }
}
