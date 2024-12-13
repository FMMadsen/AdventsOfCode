using Common;

namespace AdventOfCode2024Solutions.Day11
{
    public class Solution : IAOCSolution
    {
        public string PuzzleName => "Day 11: Plutonian Pebbles";

        public string SolvePart1(string[] datasetLines)
        {
            var stones = new Pepples(datasetLines[0]);
            stones.Blink(25);
            return stones.Count().ToString();
        }

        public string SolvePart2(string[] datasetLines)
        {
            var pluto = new Pluto(datasetLines[0]);
            pluto.Blink(75);
            return pluto.TotalNumberOfStones.ToString();
        }
    }
}
