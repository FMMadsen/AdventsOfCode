using Common;
using ToolsFramework;

namespace AdventOfCode2025Solutions.Day01
{
    public class Solution : IAOCSolution
    {
        public string PuzzleName => "Day 1: Secret Entrance";

        public string SolvePart1(string[] datasetLines)
        {
            Dialer dialer = new(0, 99, 50);
            var zeroHitCounter = 0;

            foreach (var datasetLine in datasetLines)
            {
                var movement = new Movement(datasetLine);
                dialer.RotateV2(movement.Clicks, movement.Direction);
                if (dialer.Pointer == 0)
                    zeroHitCounter++;
            }
            return zeroHitCounter.ToString();
        }

        public string SolvePart2(string[] datasetLines)
        {
            Dialer dialer = new(0, 99, 50);
            foreach (var datasetLine in datasetLines)
            {
                var movement = new Movement(datasetLine);
                dialer.RotateV2(movement.Clicks, movement.Direction);
            }
            return dialer.ZeroHits.ToString();
        }
    }

    internal class Movement
    {
        public Movement(string commandString)
        {
            Direction = commandString.StartsWith("L") ? Direction.Left : Direction.Right;
            Clicks = int.Parse(commandString.Substring(1));
        }

        public Direction Direction { get; init; }
        public int Clicks { get; init; }

    }
}
