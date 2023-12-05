namespace AdventOfCode2023Solutions.Day05
{
    internal class AlmanacMap
    {
        private IList<MappingRule> mappingRules = new List<MappingRule>();

        internal long Map(long source)
        {
            var destination = source;
            var rule = mappingRules.FirstOrDefault(r => r.RangeFrom <= source && r.RangeTo >= source);

            if (rule != null)
                destination = source + rule.Modifyer;

            return destination;
        }

        internal void InitializeMap(IEnumerable<string> mapLines)
        {
            foreach (var mapLine in mapLines)
            {
                var mapLineSplit = mapLine.Trim().Split(' ');
                var from = long.Parse(mapLineSplit[1]);
                var to = long.Parse(mapLineSplit[0]);
                var range = long.Parse(mapLineSplit[2]);
                var mappingRule = new MappingRule(from, to, range);
                mappingRules.Add(mappingRule);
            }
        }
    }
}