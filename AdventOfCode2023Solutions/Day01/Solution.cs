using Common;
using System.Text.RegularExpressions;

namespace AdventOfCode2023Solutions.Day01
{
    public class Solution(string[] datasetLines) : IAOCSolution
    {
        private string _PuzzleName = "Day 1: Trebuchet?!";
        public string PuzzleName { get { return _PuzzleName; } }
        public string[] DatasetLines => datasetLines;

        public string SolvePart1()
        {
            int output = 0;

            for (int i = 0; i < DatasetLines.Length; i++)
            {
                string twodigit = "";
                Match digit;
                string pattern = @"\d";

                digit = Regex.Match(DatasetLines[i], pattern, RegexOptions.IgnoreCase);
                twodigit += digit.Value;
                digit = Regex.Match(DatasetLines[i], pattern, RegexOptions.RightToLeft | RegexOptions.IgnoreCase);
                twodigit += digit.Value;

                output += int.Parse(twodigit);
            }

            // ToString for setup
            return output.ToString();
        }

        public string SolvePart2()
        {
            int output = 0;

            for (int i = 0; i < DatasetLines.Length; i++)
            {
                string twodigit = "";
                Match digit;
                string pattern = @"\d|one|two|three|four|five|six|seven|eight|nine";

                digit = Regex.Match(DatasetLines[i], pattern, RegexOptions.IgnoreCase);
                twodigit += StringToIntchar(digit.Value);
                digit = Regex.Match(DatasetLines[i], pattern, RegexOptions.RightToLeft | RegexOptions.IgnoreCase);
                twodigit += StringToIntchar(digit.Value);

                output += int.Parse(twodigit);
            }

            // ToString for setup
            return output.ToString();
        }

        public char StringToIntchar(string input)
        {
            char output;

            switch (input)
            {
                case "one":
                    output = '1';
                    break;
                case "two":
                    output = '2';
                    break;
                case "three":
                    output = '3';
                    break;
                case "four":
                    output = '4';
                    break;
                case "five":
                    output = '5';
                    break;
                case "six":
                    output = '6';
                    break;
                case "seven":
                    output = '7';
                    break;
                case "eight":
                    output = '8';
                    break;
                case "nine":
                    output = '9';
                    break;
                default:
                    output = input[0];
                    break;
            }

            return output;
        }
    }
}
