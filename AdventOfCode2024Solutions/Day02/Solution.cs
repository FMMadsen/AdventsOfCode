using Common;

namespace AdventOfCode2024Solutions.Day02
{
    public class Solution : IAOCSolution
    {
        public string PuzzleName => "Day 2: ";
        protected Report[] Reports { get; set; } = Array.Empty<Report>();

        public string SolvePart1(string[] datasetLines)
        {
            Reports = datasetLines.Select(a => new Report(a)).Where(a => 0 != a.MaxDirection).ToArray();
            
            return Reports.Length.ToString();
        }

        public string SolvePart2(string[] datasetLines)
        {
            int count = 0;
            Report report;

            foreach (string datasetLine in datasetLines)
            {
                report = new Report(datasetLine, true);

                if (0 != report.MaxDirection)
                {
                    count++;
                }
            }

            return count.ToString();
        }
    }
}
