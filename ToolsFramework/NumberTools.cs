namespace ToolsFramework
{
    public class NumberTools
    {
        public static int[] StringToIntArray(string stringOfNumbers)
        {
            return stringOfNumbers.Select(x => int.Parse(x.ToString())).ToArray();
        }

        public static long CountSumOfEvery2nd(long[] array)
        {
            return array.Where((element, index) => index % 2 == 0).Sum(x => x);
        }

        public static long CountSumOfEvery2nd(int[] array)
        {
            return array.Where((element, index) => index % 2 == 0).Sum(x => x);
        }

        public static long CountSumOfNumberArray(long[] array)
        {
            return array.Sum();
        }

        public static long CountSumOfNumberArray(int[] array)
        {
            return array.Sum();
        }

        public static int RandomInt(int from, int to)
        {
            return new Random().Next(from, to);
        }

        public static int ModulusConverNumberIntoRange(int number, int from, int to)
        {
            return (number % to) + from;
        }
    }
}
