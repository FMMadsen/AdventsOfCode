using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024Solutions.Day05
{
    public class Page
    {
        private int PageNumberValue = -1;
        public int PageNumber { get { return PageNumberValue; } }
        private int[] DependenciesValue = Array.Empty<int>();
        public int[] Dependencies { get { return DependenciesValue; } set { DependenciesValue = value; } }

        public Page(int pageNumber)
        {
            PageNumberValue = pageNumber;
        }
        public Page(int pageNumber, int[] dependencies)
        {
            PageNumberValue = pageNumber;
            DependenciesValue = dependencies;
        }
    }
}
