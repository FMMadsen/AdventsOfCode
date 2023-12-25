using Common;

namespace AdventOfCode2023Solutions.Day15
{
    public class Solution : IAOCSolution
    {
        public string PuzzleName => "Day 15: Lens Library";

        public string SolvePart1(string[] datasetLines)
        {
            var line1 = datasetLines[0];
            var initializationSteps = line1.Split(',');
            var sumHash = Solution.SumHash(initializationSteps);
            return sumHash.ToString();
        }

        public string SolvePart2(string[] datasetLines)
        {
            return "To be implemented";
        }

        public static int SumHash(string[] strings) => strings.Sum(s => Solution.Hash(s));

        public static int Hash(string text, int currentValue = 0, int index = 0)
        {
            if (index > text.Length-1)
                return currentValue;

            var character = text[index];
            int ascii = (int)character;
            currentValue += ascii;
            currentValue *= 17;
            currentValue = currentValue % 256;
            return Hash(text, currentValue, ++index);
        }
    }
}
