using Common;

namespace AdventOfCode2023Solutions.Day16
{
    public class Solution : IAOCSolution
    {
        public string PuzzleName => "Day 16: The Floor Will Be Lava";

        public string SolvePart1(string[] datasetLines)
        {
            var contrap = new Contraption(datasetLines);
            contrap.BeamIntoField(0, 0, Direction.Rightward);
            var sum = contrap.CountEnergizedFields();
            return sum.ToString();
        }

        public string SolvePart2(string[] datasetLines)
        {
            return "To be implemented";
        }
    }
}
