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
            List<int> testResults = [];

            var contrap = new Contraption(datasetLines);

            //Test beam from top:
            for (int col = 0; col < contrap.noOfCols; col++)
            {
                contrap.Reset();
                contrap.BeamIntoField(0, col, Direction.Downward);
                testResults.Add(contrap.CountEnergizedFields());
            }

            //Test beam from left:
            for (int row = 0; row < contrap.noOfRows; row++)
            {
                contrap.Reset();
                contrap.BeamIntoField(row, 0, Direction.Rightward);
                testResults.Add(contrap.CountEnergizedFields());
            }

            //Test beam from bottom:
            for (int col = 0; col < contrap.noOfCols; col++)
            {
                var startRow = contrap.noOfRows - 1;
                contrap.Reset();
                contrap.BeamIntoField(startRow, col, Direction.Upward);
                testResults.Add(contrap.CountEnergizedFields());
            }

            //Test beam from right:
            for (int row = 0; row < contrap.noOfRows; row++)
            {
                var startCol = contrap.noOfCols - 1;
                contrap.Reset();
                contrap.BeamIntoField(row, startCol, Direction.Leftward);
                testResults.Add(contrap.CountEnergizedFields());
            }

            return testResults.Max().ToString();
        }
    }
}
