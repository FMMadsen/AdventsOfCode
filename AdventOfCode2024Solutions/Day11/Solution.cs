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
            var stones = new Stones(datasetLines[0]);
            for (int b = 1; b <= 25; b++)
            {
                stones.Blink();
                //Console.WriteLine("After {0} blink: {1}", b, stones.Count());
                //Console.WriteLine(stones.ToString());
            }
            return stones.Count().ToString();
        }
    }
}
