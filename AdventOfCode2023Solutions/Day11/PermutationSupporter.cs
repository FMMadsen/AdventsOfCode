namespace AdventOfCode2023Solutions.Day11
{
    public static class PermutationSupporter
    {
        public static IEnumerable<int[]> BuildPermutatinos(int noOfCombinationITems, int noOfNumbers)
        {
            int[] result = new int[noOfCombinationITems];
            Stack<int> stack = new();
            if (noOfNumbers > 0 && noOfCombinationITems > 0)
                stack.Push(0);

            while (stack.Count > 0)
            {
                int index = stack.Count - 1;
                int value = stack.Pop();

                while (value < noOfNumbers)
                {
                    result[index++] = ++value;
                    stack.Push(value);

                    if (index == noOfCombinationITems)
                    {
                        yield return result.Clone() as int[] ?? new int[noOfCombinationITems];
                        break;
                    }
                }
            }
        }
    }
}
