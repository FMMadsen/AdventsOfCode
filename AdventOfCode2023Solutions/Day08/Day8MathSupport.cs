namespace AdventOfCode2023Solutions.Day08
{
    public static class Day8MathSupport
    {
        /// <summary>
        /// https://en.wikipedia.org/wiki/Least_common_multiple#Computing_the_least_common_multiple
        /// </summary>
        public static long CalculateLowestCommonMultiplier(long[] numbersArray)
        {
            if (numbersArray.Length == 0)
                return 0;

            if (numbersArray.Length == 1)
                return numbersArray[0];

            long lcm = numbersArray[0];
            long numberA, numberB, gcd;

            for (int i = 1; i < numbersArray.Length; i++)
            {
                numberA = lcm;
                numberB = numbersArray[i];
                gcd = CalculateGreatestCommonDivisor(numberA, numberB);
                lcm = (numberA / gcd) * numberB;
            }

            return lcm;
        }

        public static long CalculateGreatestCommonDivisor(long[] numbersArray)
        {
            if (numbersArray.Length == 0)
                return 0;

            if (numbersArray.Length == 1)
                return numbersArray[0];

            long result = numbersArray[0];
            for (int i = 1; i < numbersArray.Length; i++)
            {
                result = CalculateGreatestCommonDivisor(result, numbersArray[i]);
            }
            return result;
        }

        /// <summary>
        /// https://en.wikipedia.org/wiki/Greatest_common_divisor
        /// </summary>
        public static long CalculateGreatestCommonDivisor(long x, long y)
        {
            long tmp;
            while (y != 0)
            {
                tmp = y;
                y = x % y;
                x = tmp;
            }
            return x;
        }
    }
}
