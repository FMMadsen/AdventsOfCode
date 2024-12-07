using Common;

namespace AdventOfCode2024Solutions.Day07
{
    public class Solution : IAOCSolution
    {
        public string PuzzleName => "Day 7: Bridge Repair";

        public string SolvePart1(string[] datasetLines)
        {
            return 
                datasetLines
                .Select(x => new CalibrationEquation(x))
                .Select(x=> x.TryCalibrateWithTwoOperators())
                .Sum(x => x)
                .ToString();
        }

        public string SolvePart2(string[] datasetLines)
        {
            return
                datasetLines
                .Select(x => new CalibrationEquation(x))
                .Select(x => x.TryCalibrateWithThreeOperators())
                .Sum(x => x)
                .ToString();
        }
    }
}
