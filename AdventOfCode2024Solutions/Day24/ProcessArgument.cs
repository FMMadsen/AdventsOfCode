using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024Solutions.Day24
{
    public class ProcessArgument
    {
        public object? Value { get; set; }
        public string Name { get; }
        public string Type { get; }
        public Wire? Connection { get; set; } = null;

        public ProcessArgument(string name, string type)
        {
            Name = name;
            Type = type;
            Value = null;
        }
        public ProcessArgument(string name, string type, object value)
        {
            Name = name;
            Type = type;
            Value = value;
        }
    }
}
