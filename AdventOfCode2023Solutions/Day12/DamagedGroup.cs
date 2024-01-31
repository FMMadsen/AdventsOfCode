using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023Solutions.Day12
{
    internal class DamagedGroup
    {
        private int _Length = 0;
        private List<int> _Indexes = [];
        private int _MinIndex = 0;
        private int _MaxIndex = 0;

        public int Length { get { return _Length; } }
        public List<int> Indexes { get {  return _Indexes; } }
        public int MinIndex { get { return _MinIndex; } set { _MinIndex = value; } }
        public int MaxIndex { get { return _MaxIndex; } set { _MaxIndex = value; } }

        public DamagedGroup(int length)
        {
            _Length = length;
        }
    }
}
