namespace AdventOfCode2024Solutions.Day07
{
    public class CalibrationEquation
    {
        public long TestValue { get; set; }
        public long[] Numbers { get; set; }

        public CalibrationEquation(string stringLine)
        {
            var stringParts = stringLine.Split(":");

            TestValue =
                long.Parse(stringParts[0]);
            Numbers =
                stringParts[1].
                Split(" ").
                Where(i => !string.IsNullOrWhiteSpace(i)).
                Select(i => long.Parse(i)).
                ToArray();
        }

        public long TryCalibrateWithTwoOperators()
        {
            var calibrationResult = calibrateRecursive(1, Numbers[0]);
            return calibrationResult ? TestValue : 0;
        }

        public bool calibrateRecursive(int nextIndex, long currentValue)
        {
            if (nextIndex == Numbers.Length)
                return currentValue == TestValue;

            var result1 = calibrateRecursive(nextIndex + 1, currentValue * Numbers[nextIndex]);
            var result2 = calibrateRecursive(nextIndex + 1, currentValue + Numbers[nextIndex]);

            return result1 || result2;
        }
    }
}
