using Common;
using ToolsFramework;

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
                    if (!ValidateProductNumberV1(p))
                        sumInvalidProductNumbers += p;
                }
            }

            return sumInvalidProductNumbers.ToString();
        }

        public string SolvePart2(string[] datasetLines)
        {
            var input = datasetLines[0];
            var numberRanges = input.Split(",");

            long sumInvalidProductNumbers = 0;
            foreach (var rangeString in numberRanges)
            {
                var range = new Range(rangeString);
                for (long p = range.RangeFrom; p <= range.RangeTo; p++)
                {
                    if (!ValidateProductNumberV2(p))
                        sumInvalidProductNumbers += p;
                }
            }

            return sumInvalidProductNumbers.ToString();
        }

        public static bool ValidateProductNumberV1(long productNumber)
        {
            return ValidateProductNumberV1(productNumber.ToString());
        }

        public static bool ValidateProductNumberV2(long productNumber)
        {
            return ValidateProductNumberV2(productNumber.ToString());
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
        public static bool ValidateProductNumberV1(string productNumber)
        {
            if (StringValidator.IsUnevenLength(productNumber))
                return true;

            var strings = SplitEvenLengthString(productNumber);
            var isValid = !StringValidator.AreStringsEqual(strings);

            return isValid;
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
        public static bool ValidateProductNumberV2(string productNumber)
        {
            var isInvalid = StringValidator.CanSplitIntoEqualParts(productNumber);
            return !isInvalid;
        }

        

        private static string[] SplitEvenLengthString(string txt)
        {
            int halfLength = txt.Length / 2;
            return [
                txt.Substring(0, halfLength),
                txt.Substring(halfLength)
            ];
        }

        private static string[] SplitInPiecesOfSize(string stringToSplit, int size)
        {
            var noOfPieces = stringToSplit.Length / size;
            string[] pieces = new string[noOfPieces];

            for (int i = 0; i < noOfPieces; i++)
            {
                {
                    pieces[i] = stringToSplit.Substring(i * size, 2);
                }
            }
            return pieces;
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