using Common;

namespace AdventOfCode2023Solutions.Day01
{
    public class Solution : IAOCSolution
    {
        public string PuzzleName => "Day 1: Trebuchet?!";

        public string SolvePart1(string[] datasetLines)
        {
            int sum = 0;
            foreach (string line in datasetLines)
            {
                sum += GetCalibrationValueFromLine(line);
            }
            return sum.ToString();
        }

        public string SolvePart2(string[] datasetLines)
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
            var first = line.First(c => char.IsNumber(c));
            var last = line.Last(c => char.IsNumber(c));
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

        private readonly string[] numberStrings = ["one", "two", "three", "four", "five", "six", "seven", "eight", "nine"];

        private char GetFirstNumberInLineExtended(string lineLC)
        {
            int firstIndex = int.MaxValue;
            int firstNumber = 0;
            char firstNumberChar = default;

            //Check for text described numbers in the text line
            for (int i = 0; i < 9; i++)
            {
                int index = lineLC.IndexOf(numberStrings[i]);
                if (index != -1)
                {
                    if (index < firstIndex)
                    {
                        firstIndex = index;
                        firstNumber = i + 1;
                    }
                }
            }
            if (firstIndex != int.MaxValue)
                firstNumberChar = char.Parse(firstNumber.ToString());

            //Check real char numbers in the text line
            char numberChar = lineLC.FirstOrDefault(c => char.IsNumber(c));
            if (numberChar != default(char))
            {
                var index = lineLC.IndexOf(numberChar);
                if (index != -1 && index < firstIndex)
                {
                    firstIndex = index;
                    firstNumberChar = numberChar;
                }
            }

            if (firstNumberChar != default(char))
                return firstNumberChar;
            else
                return '0';
        }

        private char GetLastNumberInLineExtended(string lineLC)
        {
            int lastIndex = -1;
            int lastNumber = 0;
            char lastNumberChar = default;

            //Check for text described numbers in the text line
            for (int i = 0; i < 9; i++)
            {
                int index = lineLC.LastIndexOf(numberStrings[i]);
                if (index != -1)
                {
                    if (index > lastIndex)
                    {
                        lastIndex = index;
                        lastNumber = i + 1;
                    }
                }
            }
            if (lastIndex != -1)
                lastNumberChar = char.Parse(lastNumber.ToString());

            //Check real char numbers in the text line
            char numberChar = lineLC.LastOrDefault(c => char.IsNumber(c));
            if (numberChar != default(char))
            {
                var index = lineLC.LastIndexOf(numberChar);
                if (index != -1 && index > lastIndex)
                {
                    lastIndex = index;
                    lastNumberChar = numberChar;
                }
            }

            if (lastNumberChar != default(char))
                return lastNumberChar;
            else
                return '0';
        }
    }
}
