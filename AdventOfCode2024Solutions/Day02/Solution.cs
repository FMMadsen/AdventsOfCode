using Common;

namespace AdventOfCode2024Solutions.Day02
{
    public class Solution : IAOCSolution
    {
        public string PuzzleName => "Day 2: ";
        protected Report[] Reports { get; set; } = Array.Empty<Report>();

        public string SolvePart1(string[] datasetLines)
        {
            Reports = datasetLines.Select(a => new Report(a)).Where(a => 0 != a.MaxDirection && !(a.MaxDirection < -3 || 3 < a.MaxDirection)).ToArray();

            return Reports.LongLength.ToString();
        }

        public string SolvePart2(string[] datasetLines)
        {
            return "To be implemented";
        }
    }
}
