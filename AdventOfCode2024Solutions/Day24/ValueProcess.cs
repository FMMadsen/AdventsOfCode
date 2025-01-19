using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024Solutions.Day24
{
    public class ValueProcess<T> : Process
    {
        protected override ProcessArgument[] InputValue { get; set; } = [new ProcessArgument("Value", typeof(T).Name)];
        protected override ProcessArgument[] OutputValue { get; set; } = [new ProcessArgument("Result", typeof(T).Name)];

        protected override void CalcOut()
        {
            OutputValue.First().Value = InputValue.First().Value;
        }

        public override string ToString()
        {
            return InputValue.First().Value?.ToString() + " : ";
        }
    }
}
