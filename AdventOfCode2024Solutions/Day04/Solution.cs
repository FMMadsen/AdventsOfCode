using Common;

namespace AdventOfCode2024Solutions.Day04
{
    public class Solution : IAOCSolution
    {
        public string PuzzleName => "Day 4: Ceres Search";

        public string SolvePart1(string[] datasetLines)
        {
            var matrix = new Matrix(datasetLines);
            var matches = 0;
            matches += CountXMASMatches(matrix.Rows);
            matches += CountXMASMatches(matrix.Columns);
            matches += CountXMASMatches(matrix.DiagonalsDown);
            matches += CountXMASMatches(matrix.DiagonalsUp);
            return matches.ToString();
        }

        public string SolvePart2(string[] datasetLines)
        {
            return "To be implemented";
        }

        private int CountXMASMatches(string[] datasetLines)
        {
            var matches = 0;
            foreach (var datasetLine in datasetLines)
            {
                matches += CountXMAS(datasetLine);
                matches += CountXMASBackwards(datasetLine);
            }
            return matches;
        }

        private int CountXMAS(string textLine)
        {
            const string TOFIND = "XMAS";
            return CountFindings(textLine, TOFIND, 0, 0);
        }

        private int CountXMASBackwards(string textLine)
        {
            const string TOFIND = "SAMX";
            return CountFindings(textLine, TOFIND, 0, 0);
        }

        private int CountFindings(string textLine, string searchString, int startIndex, int accumulatedFindings)
        {
            var i = textLine.IndexOf(searchString, startIndex);
            if (i >= 0)
            {
                accumulatedFindings++;
                return CountFindings(textLine, searchString, i + searchString.Length, accumulatedFindings);
            }
            return accumulatedFindings;
        }
    }
}
