using Common;

namespace AdventOfCode2023Solutions.Day12
{
    public class Solution(string[] datasetLines) : IAOCSolution
    {
        public string PuzzleName => "Day 12: Hot Springs";
        public string[] DatasetLines => datasetLines;

        internal IList<SpringRow>? SpringRows { get; set; }

        public string SolvePart1()
        {
            SpringRows = DatasetLines.Select(r => new SpringRow(r)).ToList();
            foreach (var row in SpringRows)
            {
                row.AnalyzeNumberOfPotentialStates();
            }
            var sumOfStates = SpringRows.Sum(r => r.NumberOfPotentialStates);

            //PrintExtraInfo();

            return sumOfStates.ToString();
        }

        
        private void PrintExtraInfo()
        {
            int count = 0;
            foreach (var row in SpringRows)
            {
                Console.WriteLine($"{count,4}  " + row.ToString());
                count++;
            }
        }

        public string SolvePart2()
        {
            return "To be implemented";
        }
    }
}
