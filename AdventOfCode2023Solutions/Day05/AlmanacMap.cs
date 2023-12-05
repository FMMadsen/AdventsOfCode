using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023Solutions.Day05
{
    internal class AlmanacMap
    {
        internal int Map(int source)
        {
            return source;
        }

        internal void InitializeMap(IEnumerable<string> mapLines)
        {
            foreach (var mapLine in mapLines)
            {
                var mapLineSplit = mapLine.Trim().Split(' ');
            }
        }

        //internal void AddMap(string mapLine) { }
    }
}
