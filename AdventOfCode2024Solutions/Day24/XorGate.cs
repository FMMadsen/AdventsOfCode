using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024Solutions.Day24
{
    public class XorGate : Gate
    {
        protected override string GateOperator { get; } = "XOR";

        public XorGate() : base()
        { }

        protected override void CalcOut()
        {
            OutputValue.First().Value = (bool)Left.Value != (bool)Right.Value;
        }
    }
}
