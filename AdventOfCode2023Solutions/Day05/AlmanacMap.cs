namespace AdventOfCode2023Solutions.Day05
{
    internal class AlmanacMap
    {
        internal List<MappingRule> MappingRules { get; private set; } = new List<MappingRule>();

        internal long Map(long source)
        {
            var destination = source;
            var rule = MappingRules.FirstOrDefault(r => r.SourceRangeFrom <= source && r.SourceRangeTo >= source);

            if (rule != null)
                destination = source + rule.SourceToTargetModifyer;

            return destination;
        }

        internal void InitializeMap(IEnumerable<string> mapLines)
        {
            foreach (var mapLine in mapLines)
            {
                var mapLineSplit = mapLine.Trim().Split(' ');
                var source = long.Parse(mapLineSplit[1]);
                var target = long.Parse(mapLineSplit[0]);
                var range = long.Parse(mapLineSplit[2]);

                var mappingRule = new MappingRule(source, target, range);
                MappingRules.Add(mappingRule);
            }
        }

        internal void InitializeMap(List<MappingRule> newMap)
        {
            MappingRules = newMap;
        }

        internal void SortMapByTargetRange()
        {
            MappingRules = MappingRules.OrderBy(rule => rule.TargetRangeFrom).ToList();
        }

        internal void ExpandMapToIncludeZeroToMax()
        {
            var firstTargetIntervalStart = MappingRules.Min(m => m.TargetRangeFrom);
            var lastTargetIntervalEnd = MappingRules.Max(m => m.TargetRangeTo);
            if (firstTargetIntervalStart > 0)
            {
                var newStartRuleInterval = new MappingRule(0, 0, firstTargetIntervalStart);
                MappingRules.Insert(0, newStartRuleInterval);
            }
            var newEndRuleInterval = new MappingRule(lastTargetIntervalEnd + 1, long.MaxValue);
            MappingRules.Add(newEndRuleInterval);
        }

        internal void ExpandMapToFillInGapInRuleRanges()
        {
            var rulesSortedBySource = MappingRules.OrderBy(r => r.SourceRangeFrom).ToList() ?? [];
            var rulesSortedByTarget = MappingRules.OrderBy(r => r.TargetRangeFrom).ToList() ?? [];

            if (rulesSortedByTarget.Count != rulesSortedBySource.Count)
                throw new Exception("Exception in ExpandMapToFillInGapInRuleRanges(): Not same number of source and target values");

            var newGapRules = new List<MappingRule>();
            for (int i = 1; i < rulesSortedBySource.Count; i++)
            {
                if (rulesSortedBySource[i].SourceRangeFrom != rulesSortedBySource[i - 1].SourceRangeTo + 1)
                {
                    if (rulesSortedBySource[i - 1].SourceRangeTo != rulesSortedByTarget[i - 1].TargetRangeTo)
                        throw new Exception("Exception in ExpandMapToFillInGapInRuleRanges(): source and target ordered lists are not aligned on gaps Begin!");

                    if (rulesSortedBySource[i].SourceRangeFrom != rulesSortedByTarget[i].TargetRangeFrom)
                        throw new Exception("Exception in ExpandMapToFillInGapInRuleRanges(): source and target ordered lists are not aligned on gaps End!");

                    //Here is a gap in the ranges
                    var newGapRule = new MappingRule(rulesSortedBySource[i - 1].SourceRangeTo + 1, rulesSortedBySource[i].SourceRangeFrom - 1);
                    newGapRules.Add(newGapRule);
                }
            }
            MappingRules.AddRange(newGapRules);
        }

        internal void ExpandMapToAlignRangesOnRightHandSideMap(IList<MappingRule> mapRight)
        {
            for (int i = 0; i < mapRight.Count; i++)
            {
                var rightFrom = mapRight[i].SourceRangeFrom;
                var rightTo = mapRight[i].SourceRangeTo;

                if (MappingRules.FirstOrDefault(m => m.TargetRangeFrom == rightFrom) == null)
                    SplitARangeInAMap(rightFrom); //OK, we don't have a range on left side starting with same number, we need to create it

                if (MappingRules.FirstOrDefault(m => m.TargetRangeTo == rightTo) == null)
                    SplitARangeInAMap(rightTo + 1); //OK, we don't have a range on left side starting with same number, we need to create it
            }
        }

        private void SplitARangeInAMap(long newTargetRangeFrom)
        {
            var ruleToSplit = MappingRules.FirstOrDefault(rule => rule.TargetRangeFrom < newTargetRangeFrom && rule.TargetRangeTo >= newTargetRangeFrom);
            if (ruleToSplit == null)
                throw new Exception($"The input map does not have a range spanning across the number {newTargetRangeFrom}");

            var newRule = new MappingRule(newTargetRangeFrom + ruleToSplit.TargetToSourceModifyer, ruleToSplit.SourceRangeTo, newTargetRangeFrom, ruleToSplit.TargetRangeTo);
            ruleToSplit.TargetRangeTo = newRule.TargetRangeFrom - 1;
            ruleToSplit.SourceRangeTo = newRule.SourceRangeFrom - 1;
            MappingRules.Add(newRule);
        }
    }
}