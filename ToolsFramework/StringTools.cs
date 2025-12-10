namespace ToolsFramework
{
    public static class StringTools
    {
        /// <summary>
        /// ABCD => AB CD
        /// ABCDE => AB CDE
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string[] SplitStringInMiddle(string input)
        {
            if (input.Length == 0)
                return [];

            if (input.Length == 1)
                return [input];

            int halfLength = input.Length / 2;
            return [
                input.Substring(0, halfLength),
                input.Substring(halfLength)
            ];
        }

        public static string[] SplitIntoPartsOfSize(string input, int size)
        {
            if (input.Length == 0)
                return [];

            if (size >= input.Length)
                return [input];

            var noOfRequestedSizePieces = input.Length / size;
            var hasRemainder = (input.Length % size) > 0;
            var arraySize = noOfRequestedSizePieces + (hasRemainder ? 1 : 0);

            string[] pieces = new string[arraySize];

            for (int i = 0; i < noOfRequestedSizePieces; i++)
            {
                pieces[i] = input.Substring(i * size, size);
            }

            if (hasRemainder)
                pieces[pieces.Length - 1] = input.Substring((pieces.Length-1) * size);

            return pieces;
        }

        public static string[] SplitIntoNParts(string input, int n)
        {
            if (string.IsNullOrEmpty(input))
                return [];

            if (n <= 1)
                return [input];

            int length = input.Length;

            // If n is greater than length, split into individual characters
            if (n >= length)
            {
                var chars = new string[length];
                for (int i = 0; i < length; i++)
                    chars[i] = input[i].ToString();
                return chars;
            }

            int partSize = length / n;
            int remainder = length % n;

            var result = new string[n];
            int index = 0;

            for (int i = 0; i < n; i++)
            {
                int currentSize = partSize;

                // Add remainder to the last part
                if (i == n - 1)
                    currentSize += remainder;

                result[i] = input.Substring(index, currentSize);
                index += currentSize;
            }

            return result;
        }
    }
}
