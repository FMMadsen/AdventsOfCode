using Common;

namespace AdventOfCode2025Solutions.Day02
{
    public class Solution : IAOCSolution
    {
        public string PuzzleName => "Day 2: Gift Shop";

        public string SolvePart1(string[] datasetLines)
        {
            var input = datasetLines[0];
            var numberRanges = input.Split(",");

            long sumInvalidProductNumbers = 0;
            foreach (var rangeString in numberRanges)
            {
                var range = new Range(rangeString);
                for (long p = range.RangeFrom; p <= range.RangeTo; p++)
                {
                    if (!ValidateProductNumber(p))
                        sumInvalidProductNumbers += p;
                }
            }

            return sumInvalidProductNumbers.ToString();
        }

        public string SolvePart2(string[] datasetLines)
        {
            return "To be implemented";
        }

        public static bool ValidateProductNumber(long productNumber)
        {
            return ValidateProductNumber(productNumber.ToString());
        }

        /// <summary>
        /// invalid IDs is any ID which is made only of some sequence of 
        /// digits repeated twice. 
        /// So:
        /// 55 (5 twice), 
        /// 6464 (64 twice)
        /// 123123 (123 twice)
        /// </summary>
        /// <param name="productNumber">string number</param>
        /// <returns>true if valide, false if invalid</returns>
        public static bool ValidateProductNumber(string productNumber)
        {
            if (IsEvenLength(productNumber))
                return true;

            var strings = SplitEvenLengthString(productNumber);
            var isValid = !IsStringsEqual(strings);

            return isValid;
        }

        private static bool IsEvenLength(string txt)
            => txt.Length % 2 > 0;

        private static (string, string) SplitEvenLengthString(string txt)
        {
            int halfLength = txt.Length / 2;
            string p1 = txt.Substring(0, halfLength);
            string p2 = txt.Substring(halfLength);
            return (p1, p2);
        }

        private static bool IsStringsEqual((string, string) stringTuple)
        {
            return stringTuple.Item1.Equals(stringTuple.Item2);
        }
    }

    internal class Range
    {
        public Range(string rangeSpanString)
        {
            var rangeSplint = rangeSpanString.Split("-");
            var rangeFromString = rangeSplint[0];
            var rangeToString = rangeSplint[1];
            RangeFrom = long.Parse(rangeFromString);
            RangeTo = long.Parse(rangeToString);
        }

        internal long RangeFrom { get; private init; }
        internal long RangeTo { get; private init; }
    }
}