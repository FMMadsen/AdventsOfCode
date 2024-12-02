using Common;

namespace AdventOfCode2024Solutions.Day02
{
    public class Solution : IAOCSolution
    {
        public string PuzzleName => "Day 2: Red-Nosed Reports";

        public string SolvePart1(string[] datasetLines)
        {
            var numberSafeReports = 0;

            foreach (var reportString in datasetLines)
            {
                int[] report = reportString.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x)).ToArray();

                if (CheckReport(report))
                    numberSafeReports++;
            }

            return numberSafeReports.ToString();
        }

        public string SolvePart2(string[] datasetLines)
        {
            var numberSafeReports = 0;

            foreach (var reportString in datasetLines)
            {
                var report = reportString.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x)).ToArray();

                if (CheckReport(report))
                {
                    numberSafeReports++;
                }
                else
                {
                    var reportLength = report.Length;
                    for (int i = 0; i < reportLength; i++)
                    {
                        var newReport = NewArrayWithoutInxed(report, i);
                        if (CheckReport(newReport))
                        {
                            numberSafeReports++;
                            break;
                        }
                    }
                }
            }

            return numberSafeReports.ToString();
        }

        private bool CheckReport(int[] report)
        {
            const int MINCHANGE = 1;
            const int MAXCHANGE = 3;

            var isSafe = true;
            var expectAssending = report[0] < report[1];

            for (int i = 1; i < report.Length; i++)
            {
                var change = report[i] - report[i - 1];
                var absChange = Math.Abs(change);
                var isAccending = change > 0;
                var isLevelDiffSafe = absChange >= MINCHANGE && absChange <= MAXCHANGE;
                var isSafeProjection = expectAssending == isAccending;

                if (!(isLevelDiffSafe && isSafeProjection))
                    isSafe = false;
            }

            return isSafe;
        }

        private int[] NewArrayWithoutInxed(int[] array, int index)
        {
            var newArray = new int[array.Length - 1];
            var newArrayIndex = 0;

            for (int i = 0; i < array.Length; i++)
            {
                if (i == index)
                    continue;

                newArray[newArrayIndex] = array[i];
                newArrayIndex++;
            }

            return newArray;
        }
    }

    public enum ProgressionType
    {
        Accending,
        Descending,
    }
}
