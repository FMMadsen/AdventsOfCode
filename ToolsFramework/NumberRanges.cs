namespace ToolsFramework
{
    public class NumberRanges
    {
        public NumberRange[] Ranges { get; private init; } = [];

        /// <summary>
        /// Receive strings of ranges and convert into organized ranges
        /// 7-9
        /// 31-41
        /// 10-14
        /// 16-20
        /// 12-18
        /// 1-2
        /// 4-6
        /// 
        /// Following should end with ranges like
        /// 1-2
        /// 4-9
        /// 10-20
        /// 31-41
        /// </summary>
        /// <param name="rangeStrings">ranges of strings</param>
        public static NumberRanges FromInitializationStringArray(string[] rangeStrings)
        {
            if (rangeStrings.Length == 0)
                return new NumberRanges();

            List<NumberRange> orderedRangeList = [];

            foreach (var rangeString in rangeStrings)
            {
                var newRange = NumberRange.FromString(rangeString);
                OrderIntoRanges(orderedRangeList, newRange);
            }

            return new()
            {
                Ranges = orderedRangeList.ToArray()
            };
        }

        public bool IsIncluded(string numberString)
        {
            if (long.TryParse(numberString.Trim(), out long number))
                return IsIncluded(number);
            return false;
        }

        public long CountRangeItems()
        {
            return Ranges.Sum(r => r.CountRangeItems());
        }

        public bool IsIncluded(long number)
        {
            int? potentialIndex = FindLastIndex(Ranges, p => p.From <= number);
            if (potentialIndex.HasValue)
                return Ranges[potentialIndex.Value].IsIncluded(number);
            else
                return false;
        }

        private static void OrderIntoRanges(List<NumberRange> orderedRangeList, NumberRange newRange)
        {
            if (orderedRangeList.Count == 0)
            {
                orderedRangeList.Add(newRange);
                return;
            }

            //identify the index where new range is overlapping should be places in list
            int? firstIndexOverlapOrBorderNullable = FindFirstIndex(orderedRangeList, p => p.From >= newRange.From);
            int firstIndex = firstIndexOverlapOrBorderNullable ?? orderedRangeList.Count;

            //Identify if last number has an overlap or nabo effect - in which case we extend the previous
            bool isLastPlace = firstIndexOverlapOrBorderNullable == null;
            bool extendPrevious = firstIndex > 0 && orderedRangeList[firstIndex - 1].To >= newRange.From - 1;
            bool isBorderNext = !isLastPlace && newRange.To + 1 == orderedRangeList[firstIndex].From;
            bool isOverlapNext = !isLastPlace && newRange.To >= orderedRangeList[firstIndex].From;

            if (!isOverlapNext)
            {

                if (!extendPrevious && !isBorderNext)
                    orderedRangeList.Insert(firstIndex, newRange);

                else if (extendPrevious && !isBorderNext)
                    orderedRangeList[firstIndex - 1].ExtendToRange(newRange);

                else if (extendPrevious && isBorderNext)
                {
                    orderedRangeList[firstIndex - 1].ExtendToRange(orderedRangeList[firstIndex]);
                    orderedRangeList.RemoveAt(firstIndex);
                }

                else if (!extendPrevious && isBorderNext)
                {
                    orderedRangeList[firstIndex].ExtendFromRange(newRange);
                }

                return;
            }

            else if (isOverlapNext)
            {
                int? lastIndexOverlapNullable = FindLastIndex(orderedRangeList, p => newRange.To >= p.From - 1);
                int lastIndexOverlap = lastIndexOverlapNullable ?? throw new Exception("Overlap but no overlap found!");

                if (extendPrevious)
                {
                    orderedRangeList[firstIndex - 1].ExtendToRange(orderedRangeList[lastIndexOverlap]);
                    for (int i = lastIndexOverlap; i >= firstIndex; i--)
                        orderedRangeList.RemoveAt(i);
                }
                else
                {
                    orderedRangeList[lastIndexOverlap].ExtendFromRange(newRange);
                    orderedRangeList[lastIndexOverlap].ExtendToRange(newRange);
                    for (int i = lastIndexOverlap - 1; i == firstIndex; i--)
                        orderedRangeList.RemoveAt(i);
                }

            }
        }
        private static int? FindFirstIndex(List<NumberRange> list, Predicate<NumberRange> predicate)
            => FindFirstIndex(list.ToArray(), predicate);

        private static int? FindLastIndex(List<NumberRange> list, Predicate<NumberRange> predicate)
            => FindLastIndex(list.ToArray(), predicate);

        private static int? FindFirstIndex(NumberRange[] array, Predicate<NumberRange> predicate)
        {
            for (int i = 0; i < array.Length; i++)
            {
                if (predicate(array[i]))
                    return i;
            }
            return null;
        }

        private static int? FindLastIndex(NumberRange[] array, Predicate<NumberRange> predicate)
        {
            int? indexFound = null;
            for (int i = 0; i < array.Length; i++)
            {
                if (predicate(array[i]))
                    indexFound = i;
            }
            return indexFound;
        }
    }

    public class NumberRange
    {
        public long From { get; private set; }
        public long To { get; private set; }

        public bool IsIncluded(long number)
            => number >= From && number <= To;

        public long CountRangeItems() => To - From + 1;

        public void ExtendToRange(NumberRange newRange)
        {
            if (newRange.To > To)
                To = newRange.To;
        }

        public void ExtendFromRange(NumberRange newRange)
        {
            if (newRange.From < From)
                From = newRange.From;
        }

        public static NumberRange FromString(string str)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(str, $"Trying to create number range from string:{str}");
            var split = str.Split('-', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            if (split.Length != 2)
                throw new ArgumentException("Input string must be format '34-66'", nameof(str));

            var range = new NumberRange();

            if (long.TryParse(split[0], out long from))
                range.From = from;
            else
                throw new ArgumentException("first value must be integeer and follow format '34-66'", nameof(str));

            if (long.TryParse(split[1], out long to))
                range.To = to;
            else
                throw new ArgumentException("last value must be integeer and follow format '34-66'", nameof(str));

            return range;
        }

        public override string ToString() => $"{From}-{To}";
    }
}
