namespace AdventOfCode2023Solutions.Day05
{
    internal class MappingRule
    {
        internal MappingRule(long sourceTargetFrom, long SourceTargetTo)
        {
            SourceRangeFrom = sourceTargetFrom;
            SourceRangeTo = SourceTargetTo;
            TargetRangeFrom = sourceTargetFrom;
            TargetRangeTo = SourceTargetTo;
            SourceToTargetModifyer = 0;
            TargetToSourceModifyer = 0;
        }

        internal MappingRule(long sourceFrom, long targetFrom, long range)
        {
            SourceRangeFrom = sourceFrom;
            SourceRangeTo = sourceFrom + range - 1;
            SourceToTargetModifyer = targetFrom - sourceFrom;

            TargetRangeFrom = targetFrom;
            TargetRangeTo = targetFrom + range - 1;
            TargetToSourceModifyer = sourceFrom - targetFrom;
        }

        internal MappingRule(long sourceFrom, long sourceTo, long targetFrom, long targetTo)
        {
            SourceRangeFrom = sourceFrom;
            SourceRangeTo = sourceTo;
            TargetRangeFrom = targetFrom;
            TargetRangeTo = targetTo;

            SourceToTargetModifyer = TargetRangeFrom - SourceRangeFrom;
            TargetToSourceModifyer = SourceRangeFrom - TargetRangeFrom;
        }

        internal long SourceRangeFrom { get; set; }

        internal long SourceRangeTo { get; set; }

        internal long SourceToTargetModifyer { get; set; }

        internal long TargetRangeFrom { get; set; }

        internal long TargetRangeTo { get; set; }

        internal long TargetToSourceModifyer { get; set; }

    }
}
