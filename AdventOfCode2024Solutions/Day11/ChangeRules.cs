namespace AdventOfCode2024Solutions.Day11
{
    public static class ChangeRules
    {
        public static long[] Change(long currentStone)
        {
            return DoStoneChangeRule1(currentStone);
        }

        private static long[] DoStoneChangeRule1(long currentStone)
        {
            if (currentStone == 0)
            {
                return [1];
            }
            return DoStoneChangeRule2(currentStone);
        }

        private static long[] DoStoneChangeRule2(long currentStone)
        {
            var numberString = currentStone.ToString();
            var numberStringLength = numberString.Length;
            if (numberStringLength % 2 == 0)
            {
                var newLength = numberStringLength / 2;

                var num1 = numberString.Substring(0, newLength);
                var num2 = numberString.Substring(newLength);
                var long1 = long.Parse(num1);
                var long2 = long.Parse(num2);

                return [long1, long2];
            }
            return DoStoneChangeRule3(currentStone);
        }

        private static long[] DoStoneChangeRule3(long currentStone)
        {
            return [currentStone * 2024];
        }
    }
}
