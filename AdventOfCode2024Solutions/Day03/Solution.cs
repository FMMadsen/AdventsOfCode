using Common;
using System.Text.RegularExpressions;

namespace AdventOfCode2024Solutions.Day03
{
    public class Solution : IAOCSolution
    {
        public string PuzzleName => "Day 3: Mull It Over";

        private static Regex regex3Ciffers = new Regex("\\d{1,3}");
        private static Regex regexMultiplierInstruction = new Regex("mul[(][0-9]{1,3}[,][0-9]{1,3}[)]");

        public string SolvePart1(string[] datasetLines)
        {
            var result = 0;
            foreach (var line in datasetLines)
            {
                result += Compute(line);
            }
            return result.ToString();
        }

        public string SolvePart2(string[] datasetLines)
        {
            return "To be implemented";
        }

        private int Compute(string instructions)
        {
            var result = 0;
            var instructionsList = ExtractInstructions(instructions);

            foreach (var instruction in instructionsList)
            {
                var matches = regex3Ciffers.Matches(instruction);

                var numberString1 = matches[0].Value;
                var numberString2 = matches[1].Value;

                var int1 = int.Parse(numberString1);
                var int2 = int.Parse(numberString2);

                if (instruction.StartsWith("mul"))
                {
                    result += (int1 * int2);
                }
            }

            return result;
        }

        private List<string> ExtractInstructions(string instructions)
        {
            var instructionsList = new List<string>();
            var matches = regexMultiplierInstruction.Matches(instructions);

            foreach (Match match in matches)
            {
                instructionsList.Add(match.Value);
            }

            return instructionsList;
        }
    }
}
