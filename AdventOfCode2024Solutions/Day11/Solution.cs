using Common;

namespace AdventOfCode2024Solutions.Day11
{
    public class Solution : IAOCSolution
    {
        public string PuzzleName => "Day 11: Plutonian Pebbles";

        public string SolvePart1(string[] datasetLines)
        {
            var stones = new Pepples(datasetLines[0]);
            Console.WriteLine("After 0 blink: {0}", stones.Count());

            for (int b = 1; b <= 25; b++)
            {
                stones.Blink();
                Console.WriteLine("After {0} blink: {1}", b, stones.Count());
            }
            //stones.Blink(25);
            return stones.Count().ToString();
        }

        public string SolvePart2(string[] datasetLines)
        {
            var pluto = new Pluto(datasetLines[0]);
            Console.WriteLine("After 0 blink: {0}", pluto.TotalNumberOfStones);
            for (int b = 1; b <= 25; b++)
            {
                pluto.Blink();
                Console.WriteLine("After {0} blink: {1}", b, pluto.TotalNumberOfStones);
            }
            return pluto.TotalNumberOfStones.ToString();
        }
    }
}
