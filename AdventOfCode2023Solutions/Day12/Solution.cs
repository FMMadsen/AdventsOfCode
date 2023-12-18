using Common;

namespace AdventOfCode2023Solutions.Day12
{
    public class Solution(string[] datasetLines) : IAOCSolution
    {
        public string PuzzleName => "Day 12: Hot Springs";
        public string[] DatasetLines => datasetLines;

        public string SolvePart1()
        {
            //List<SpringRow> springRows = DatasetLines.Select(r => new SpringRow(r)).ToList() ?? [];
            //springRows.ForEach(r => r.AnalyzeNumberOfPotentialStates());
            //var sumOfStates = springRows.Sum(r => r.NumberOfPotentialStates);

            var rows = DatasetLines.Select(r => new RowPermutationApproach(r, false)).ToList();
            rows.ForEach(r => r.ExpandAllUnknownsToPotentialSituations());
            var sum = rows.Sum(r => r.NumberOfPossibleSituations);

            //PrintExtraInfo(springRows, solver);
            return sum.ToString();

        }

        public string SolvePart2()
        {
            var rows = DatasetLines.Select(r => new RowPermutationApproach(r, false)).ToList();
            rows.ForEach(r => r.ExpandAllUnknownsToPotentialSituations());
            var sum = rows.Sum(r => r.Part2_CalculateNumberOfPossibleSituations());
            //PrintExtraInfo(rows);
            return sum.ToString() + " Not correct!";
        }

        private static void PrintExtraInfo(List<SpringRow> rows)
        {
            for (int i = 0; i < rows.Count; i++)
                Console.WriteLine($"{i + 1,4}  " + rows[i].ToString());
        }

        private static void PrintExtraInfo(List<RowPermutationApproach> rows)
        {
            for (int i = 0; i < rows.Count; i++)
                Console.WriteLine($"{i + 1,4}  " + rows[i].ToString());

            Console.WriteLine($"Number of rows: {rows.Count():0,0}");
            Console.WriteLine($"Total number of combinations: {rows.Sum(r => r.TotalNumberOfCombinations):0,0}");
            Console.WriteLine($"Number of possible combinations: {rows.Sum(r => r.NumberOfPossibleSituations):0,0}");
        }

        private static void PrintExtraInfo(List<SpringRow> springRows, List<RowPermutationApproach> rows)
        {
            for (int i = 0; i < springRows.Count; i++)
            {
                var isSame = springRows[i].NumberOfPotentialStates == rows[i].NumberOfPossibleSituations;
                var diffString = isSame ? "" : " DIFF!";

                Console.WriteLine($"{i + 1,4} {springRows[i]} {rows[i].NumberOfPossibleSituations} {diffString}");
            }
        }
    }
}
