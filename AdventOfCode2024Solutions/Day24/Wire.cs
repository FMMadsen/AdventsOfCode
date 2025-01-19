using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024Solutions.Day24
{
    public class Wire
    {
        public string Name { get; }

        private ProcessConnect? InputProcessValue;
        private ProcessConnect[] OutputProcessValue = [];

        public ProcessConnect? Input { 
            get { return InputProcessValue; } 
            set { InputProcessValue = value; }
        }
        public ProcessConnect[] Output { get { return OutputProcessValue; } }

        public Wire(string name)
        {
            InputProcessValue = null;
            Name = name;
        }

        public Wire(string name, ProcessConnect inputProcess)
        {
            InputProcessValue = inputProcess;
            Name = name;
        }

        public void AttachOutput(ProcessConnect outputToProcess)
        {
            OutputProcessValue = OutputProcessValue.Append(outputToProcess).ToArray();
        }

        public void RemoveOutput(ProcessConnect outputToProcess)
        {
            OutputProcessValue = OutputProcessValue.Where(a=> !(a.Process.Id == outputToProcess.Process.Id && a.Argument == outputToProcess.Argument) ).ToArray();
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.Append( InputProcessValue?.Process.ToString() ?? "No Process" );

            sb.Append(" -> ");
            sb.Append(Name);

            return sb.ToString();
        }
    }
}
