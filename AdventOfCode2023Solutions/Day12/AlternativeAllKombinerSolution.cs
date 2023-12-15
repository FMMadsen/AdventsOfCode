using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023Solutions.Day12
{
    internal class AlternativeAllKombinerSolution
    {
        private List<AlternativeKombiExpander> rows;

        public AlternativeAllKombinerSolution(string[] rowConditionStrings)
        {
            rows = rowConditionStrings.Select(r => new AlternativeKombiExpander(r)).ToList();
        }

        public void ExpandAllUnknownsToPotentialSituations()
        {
            rows.ForEach(r => r.ExpandAllUnknownsToPotentialSituations());
        }
    }
}
