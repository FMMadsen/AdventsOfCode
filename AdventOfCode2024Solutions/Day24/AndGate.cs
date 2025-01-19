using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024Solutions.Day24
{
    public class AndGate : Gate
    {
        protected override string GateOperator { get; } = "AND";

        public AndGate() : base()
        { }

        protected override void CalcOut()
        {
            OutputValue.First().Value = (bool)Left.Value && (bool)Right.Value;
            
            Console.WriteLine(String.Concat("Calc ", Left.Value.ToString(), " AND ", Right.Value.ToString(), " = ", OutputValue.First().Value.ToString()));

        }
    }
}
