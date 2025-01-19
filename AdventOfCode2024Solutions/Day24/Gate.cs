using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AdventOfCode2024Solutions.Day24
{
    public abstract class Gate : Process
    {
        protected override ProcessArgument[] InputValue { get; set; } = [new ProcessArgument("Left", typeof(bool).Name), new ProcessArgument("Right", typeof(bool).Name)];
        protected override ProcessArgument[] OutputValue { get; set; } = [new ProcessArgument("Result", typeof(bool).Name)];

        protected abstract string GateOperator { get; }

        public ProcessArgument Left 
        { 
            get { return InputValue[0]; } 
            set  { SetInput("Left", value); } 
        }
        public ProcessArgument Right
        {
            get { return InputValue[1]; }
            set { SetInput("Right", value); }
        }

        public ProcessArgument Result { get { return OutputValue[0]; } }

        public Gate() : base()
        {
            
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.Append(InputValue[0].Connection?.Name ?? "   ");

            sb.Append(' ');
            sb.Append(GateOperator);
            sb.Append(' ');

            sb.Append(InputValue[1].Connection?.Name ?? "   ");

            return sb.ToString();
        }
    }
}
