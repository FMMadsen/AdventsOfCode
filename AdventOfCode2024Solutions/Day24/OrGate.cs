using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024Solutions.Day24
{
    public class OrGate : Gate
    {
        protected override string GateOperator { get; } = "OR";

        public OrGate() : base()
        { }

        protected override void CalcOut()
        {
            OutputValue.First().Value = (bool)Left.Value || (bool)Right.Value;

            Console.WriteLine(String.Concat("Calc ", Left.Value.ToString(), " OR ", Right.Value.ToString(), " = ", OutputValue.First().Value.ToString()));
        }
    }
}
