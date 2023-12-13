using Common;

namespace AdventOfCode2023Solutions.Day12
{
    public class Solution(string[] datasetLines) : IAOCSolution
    {
        public string PuzzleName => "Day 12: Hot Springs";
        public string[] DatasetLines => datasetLines;

        internal IEnumerable<SpringRow>? SpringRows { get; set; }

        public string SolvePart1()
        {
            SpringRows = DatasetLines.Select(r => new SpringRow(r));
            foreach (var row in SpringRows)
            {
                row.AnalyzeNumberOfPotentialStates();
            }
            var sumOfStates = SpringRows.Sum(r => r.NumberOfPotentialStates);
            return sumOfStates.ToString();
        }

        public string SolvePart2()
        {
            return "To be implemented";
        }
    }
}
