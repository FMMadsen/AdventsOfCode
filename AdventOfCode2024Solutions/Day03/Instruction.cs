using System.Text.RegularExpressions;

namespace AdventOfCode2024Solutions.Day03
{
    internal class Instruction
    {
        private static Regex regex3Ciffers = new Regex("\\d{1,3}");

        internal int Index { get; set; }
        internal InstructionType Type { get; set; }
        internal int InstructionValue1 { get; set; }
        internal int InstructionValue2 { get; set; }
        internal int ProductValue { get; set; }

        internal static Instruction CreateMul(Match match)
        {
            var instructionString = match.Value;
            var matches = regex3Ciffers.Matches(instructionString);

            var numberString1 = matches[0].Value;
            var numberString2 = matches[1].Value;

            var int1 = int.Parse(numberString1);
            var int2 = int.Parse(numberString2);
            var product = int1 * int2;

            var instruction = new Instruction(InstructionType.MUL)
            {
                Index = match.Index,
                InstructionValue1 = int1,
                InstructionValue2 = int2,
                ProductValue = product,
            };
            return instruction;
        }

        internal static Instruction CreateDo(Match match)
        {
            var instruction = new Instruction(InstructionType.DO)
            {
                Index = match.Index
            };
            return instruction;
        }

        internal static Instruction CreateDont(Match match)
        {
            var instruction = new Instruction(InstructionType.DONT)
            {
                Index = match.Index
            };
            return instruction;
        }

        private Instruction(InstructionType type)
        {
            Type = type;
        }
    }

    internal enum InstructionType
    {
        DO,
        DONT,
        MUL,
        OTHER,
    }
}
