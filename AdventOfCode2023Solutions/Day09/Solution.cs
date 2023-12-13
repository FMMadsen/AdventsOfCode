using Common;

namespace AdventOfCode2023Solutions.Day09
{
    public class Solution(string[] datasetLines) : IAOCSolution
    {
        public string PuzzleName => "Day 9: Mirage Maintenance";
        public string[] DatasetLines => datasetLines;

        public string SolvePart1()
        {
            var oasisReport = new OASIS(DatasetLines);
            oasisReport.AnalyzeValueHistoryLines();
            return oasisReport.FindSumOfAllNextValues().ToString();
        }

        public string SolvePart2()
        {
            var oasisReport = new OASIS(DatasetLines);
            oasisReport.AnalyzeValueHistoryLines();
            return oasisReport.FindSumOfAllPreviousValues().ToString();
        }
    }
}
