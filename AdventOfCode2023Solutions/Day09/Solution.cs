using Common;

namespace AdventOfCode2023Solutions.Day09
{
    public class Solution : IAOCSolution
    {
        public string PuzzleName => "Day 9: Mirage Maintenance";

        public string SolvePart1(string[] datasetLines)
        {
            var oasisReport = new OASIS(datasetLines);
            oasisReport.AnalyzeValueHistoryLines();
            return oasisReport.FindSumOfAllNextValues().ToString();
        }

        public string SolvePart2(string[] datasetLines)
        {
            var oasisReport = new OASIS(datasetLines);
            oasisReport.AnalyzeValueHistoryLines();
            return oasisReport.FindSumOfAllPreviousValues().ToString();
        }
    }
}
