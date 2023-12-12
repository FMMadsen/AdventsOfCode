namespace AdventOfCode2023Solutions.Day08
{
    public static class Day8MathSupport
    {
        public static double CalculateLowestCommonMultiplier(long[] numbersArray)
        {
            var convertedToDouble = numbersArray.Select(i => (double)i).ToArray();
            return CalculateLowestCommonMultiplier(convertedToDouble);
        }

        /// <summary>
        /// https://en.wikipedia.org/wiki/Least_common_multiple#Computing_the_least_common_multiple
        /// </summary>
        public static double CalculateLowestCommonMultiplier(double[] numbersArray)
        {
            if (numbersArray.Length == 0)
                return 0;

            if (numbersArray.Length == 1)
                return numbersArray[0];

            double gcd = CalculateGreatestCommonDivisor(numbersArray);

            double result = numbersArray[0] / gcd;
            for (int i = 1; i < numbersArray.Length; i++)
                result *= numbersArray[i];

            return result;
        }

        public static double CalculateGreatestCommonDivisor(double[] numbersArray)
        {
            if (numbersArray.Length == 0)
                return 0;

            if (numbersArray.Length == 1)
                return numbersArray[0];

            double result = numbersArray[0];
            for (int i = 1; i < numbersArray.Length; i++)
            {
                result = CalculateGreatestCommonDivisor(result, numbersArray[i]);
            }
            return result;
        }

        /// <summary>
        /// https://en.wikipedia.org/wiki/Greatest_common_divisor
        /// </summary>
        public static double CalculateGreatestCommonDivisor(double x, double y)
        {
            double tmp;
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
