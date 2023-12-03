using Common;

namespace AdventOfCode2023Solutions.Day1
{
    public class Solution(string[] datasetLines) : IAOCSolution
    {
        public string[] DatasetLines => datasetLines;

        //public int Day => 1;
        public string PuzzleName => "Day 1: Trebuchet?!";

        public string SolvePart1()
        {
            int sum = 0;
            foreach (string line in datasetLines)
            {
                sum += GetCalibrationValueFromLine(line);
            }
            return sum.ToString();
        }

        public string SolvePart2()
        {
            int sum = 0;
            foreach (string line in datasetLines)
            {
                sum += GetCalibrationValueFromLineExtended(line.ToLower());
            }
            return sum.ToString();
        }

        private int GetCalibrationValueFromLine(string line)
        {
            var first = line.First(c => Char.IsNumber(c));
            var last = line.Last(c => Char.IsNumber(c));
            var number = int.Parse($"{first}{last}");
            return number;
        }

        private int GetCalibrationValueFromLineExtended(string lineLC)
        {
            var first = GetFirstNumberInLineExtended(lineLC);
            var last = GetLastNumberInLineExtended(lineLC);
            var number = int.Parse($"{first}{last}");
            return number;
        }

        private int GetFirstNumberInLineExtended(string lineLC)
        {
            //int index = lineLC.IndexOf("one");

            var first = lineLC.First(c => Char.IsNumber(c));
            return first;
        }

        private int GetLastNumberInLineExtended(string lineLC)
        {
            var last = lineLC.Last(c => Char.IsNumber(c));
            return last;
        }
    }
}
