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
            var calibrationResult = calibrateRecursiveAddMul(1, Numbers[0]);
            return calibrationResult ? TestValue : 0;
        }
        public long TryCalibrateWithThreeOperators()
        {
            var calibrationResult = calibrateRecursiveAddMulCon(1, Numbers[0]);
            return calibrationResult ? TestValue : 0;
        }


        private bool calibrateRecursiveAddMul(int nextIndex, long currentValue)
        {
            if (nextIndex == Numbers.Length)
                return currentValue == TestValue;

            var result1 = calibrateRecursiveAddMul(nextIndex + 1, Multip(currentValue, Numbers[nextIndex]));
            var result2 = calibrateRecursiveAddMul(nextIndex + 1, Additi(currentValue, Numbers[nextIndex]));

            return result1 || result2;
        }

        private bool calibrateRecursiveAddMulCon(int nextIndex, long currentValue)
        {
            if (nextIndex == Numbers.Length)
                return currentValue == TestValue;

            var result1 = calibrateRecursiveAddMulCon(nextIndex + 1, Multip(currentValue, Numbers[nextIndex]));
            var result2 = calibrateRecursiveAddMulCon(nextIndex + 1, Additi(currentValue, Numbers[nextIndex]));
            var result3 = calibrateRecursiveAddMulCon(nextIndex + 1, Concat(currentValue, Numbers[nextIndex]));

            return result1 || result2 || result3;
        }




        private static long Concat(long left, long right)
        {
            return long.Parse(left.ToString() + right.ToString());
        }

        private static long Additi(long left, long right)
        {
            return left + right;
        }

        private static long Multip(long left, long right)
        {
            return left * right;
        }
    }
}
