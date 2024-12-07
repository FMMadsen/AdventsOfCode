using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024Solutions.Day05
{
    public struct DependencyRule
    {
        public int Dependency = -1;
        public int Page = -1;

        public DependencyRule()
        {

        }

        public DependencyRule(int page, int dependency)
        {
            Page = page;
            Dependency = dependency;
        }
    }
}
