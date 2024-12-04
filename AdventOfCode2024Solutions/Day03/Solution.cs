using Common;
using System.Text.RegularExpressions;

namespace AdventOfCode2024Solutions.Day03
{
    public class Solution : IAOCSolution
    {
        public string PuzzleName => "Day 3: Mull It Over";

        private static readonly Regex regexDont = new Regex("don't[(][)]");
        private static readonly Regex regexDo = new Regex("do[(][)]");
        private static readonly Regex regexMultiplierInstruction = new Regex("mul[(][0-9]{1,3}[,][0-9]{1,3}[)]");

        public string SolvePart1(string[] datasetLines)
        {
            var result = 0;
            foreach (var instructionsLine in datasetLines)
            {
                var mulInstructions = ExtractInstructions(instructionsLine);
                var sum = mulInstructions.Sum<Instruction>(x => x.ProductValue);
                result += sum;
            }
            return result.ToString();
        }

        public string SolvePart2(string[] datasetLines)
        {
            var result = 0;
            var on = true;
            foreach (var instructionsLine in datasetLines)
            {
                var instructions = ExtractInstructionsExtended(instructionsLine);
                foreach (var instruction in instructions)
                {
                    if (on && instruction.Type == InstructionType.MUL)
                        result += instruction.ProductValue;

                    if (instruction.Type == InstructionType.DO)
                        on = true;

                    if (instruction.Type == InstructionType.DONT)
                        on = false;

                    if (instruction.Type == InstructionType.OTHER)
                        throw new Exception("Invalid instruction type");
                }
            }
            return result.ToString();
        }

        private List<Instruction> ExtractInstructions(string instructionsLine)
        {
            var matches = regexMultiplierInstruction.Matches(instructionsLine);
            var mulInstructions = matches.Select(m => Instruction.CreateMul(m));
            return mulInstructions?.ToList() ?? new List<Instruction>();
        }

        private List<Instruction> ExtractInstructionsExtended(string instructionsLine)
        {
            var matches = regexMultiplierInstruction.Matches(instructionsLine);
            var mulInstructions = matches.Select(m => Instruction.CreateMul(m));

            matches = regexDo.Matches(instructionsLine);
            var doInstructions = matches.Select(m => Instruction.CreateDo(m));

            matches = regexDont.Matches(instructionsLine);
            var dontInstructions = matches.Select(m => Instruction.CreateDont(m));

            var instructionsList = mulInstructions.Concat(doInstructions).Concat(dontInstructions);
            var instructionsListOrderedByIndex = instructionsList.OrderBy(i => i.Index);

            return instructionsListOrderedByIndex?.ToList() ?? new List<Instruction>();
        }
    }
}
