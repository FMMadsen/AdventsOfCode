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
            var sumHash = Solution.SumRaindeerHashing(initializationSteps);
            return sumHash.ToString();
        }

        public string SolvePart2(string[] datasetLines)
        {
            var boxes = new Box[256];
            for (int i = 0; i < 256; i++)
                boxes[i] = new Box(i);

            var line1 = datasetLines[0];
            var initializationSteps = line1.Split(',');

            foreach (var step in initializationSteps)
            {
                var IsAddingLens = step.Contains('=');

                if (IsAddingLens)
                {
                    var label = step[..^2];
                    var focalLengthChar = step[^1];
                    var focalLength = (int)char.GetNumericValue(focalLengthChar);
                    var boxNumber = RaindeerHashing(label);
                    var lens = new Lens(focalLength, label);
                    boxes[boxNumber].AddLens(lens);
                }
                else
                {
                    var label = step[..^1];
                    var boxNumber = RaindeerHashing(label);
                    boxes[boxNumber].RemoveLens(label);
                }
            }

            return boxes.Sum(x => x.SummarizeLensFocusPower()).ToString();
        }

        public static int SumRaindeerHashing(string[] strings) => strings.Sum(s => Solution.RaindeerHashing(s));

        public static int RaindeerHashing(string text, int currentValue = 0, int index = 0)
        {
            if (index > text.Length - 1)
                return currentValue;

            var character = text[index];
            int ascii = (int)character;
            currentValue += ascii;
            currentValue *= 17;
            currentValue = currentValue % 256;
            return RaindeerHashing(text, currentValue, ++index);
        }
    }
}
