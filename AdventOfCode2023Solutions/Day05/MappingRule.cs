namespace AdventOfCode2023Solutions.Day05
{
    internal class MappingRule
    {
        internal MappingRule(long source, long destination, long range)
        {
            RangeFrom = source;
            RangeTo = source + range - 1;
            Modifyer = destination - source;
        }

        internal long RangeFrom { get; set; }

        internal long RangeTo { get; set; }

        internal long Modifyer { get; set; }
    }
}
