using Common;

namespace AdventOfCode2023Solutions.Day04
{
    public class Solution(string[] datasetLines) : IAOCSolution
    {
        public string PuzzleName => "Day 4: Scratchcards";
        public string[] DatasetLines => datasetLines;

        public long Winnings = 0;

        public string SolvePart1()
        {
            Winnings = 0;

            for (int lineNumber = 0; lineNumber < DatasetLines.Length; lineNumber++)
            {
                string[] card = DatasetLines[lineNumber].Split(':','|');
            }


            return Winnings.ToString();
        }


        public string SolvePart2()
        {
            Winnings = 0;
            return Winnings.ToString();
        }
    }
}
