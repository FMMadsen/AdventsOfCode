using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024Solutions.Day24
{
    public abstract class Process
    {
        public readonly Guid Id = Guid.NewGuid();

        protected abstract ProcessArgument[] InputValue { get; set; }
        //public Dictionary<string, object?> Input {  get { return InputValue; } }

        protected abstract ProcessArgument[] OutputValue { get; set; }
        //public Dictionary<string, object?> Output { get { return OutputValue; } }

        protected Task ReadyValue;
        public Task Ready { get { return ReadyValue; } }

        public Process()
        {
            ReadyValue = ReadyTask();
        }

        protected Task ReadyTask()
        {
            return new Task(() =>
            {
                CalcOut();

                foreach (ProcessArgument output in OutputValue)
                {
                    if (null != output.Connection)
                    {
                        foreach (ProcessConnect connection in output.Connection.Output)
                        {
                            connection.Process.SetInput(connection.Argument, output.Value);
                        }
                    }
                }
            });
        }

        public void SetInput(string argumentName, object? value)
        {
            int argIndex = Array.FindIndex(InputValue, a => a.Name == argumentName);

            if (-1 == argIndex) { throw new ArgumentOutOfRangeException("No input by the name " + argumentName); }

            InputValue[argIndex].Value = value;

            IfReady();
        }

        public void SetOutputConnection(string argumentName, Wire connection)
        {
            int argIndex = Array.FindIndex(OutputValue, a => a.Name == argumentName);


            if (-1 == argIndex) { throw new ArgumentOutOfRangeException("No output by the name " + argumentName); }

            OutputValue[argIndex].Connection = connection;
        }

        public virtual object? GetOutput(string argumentName)
        {
            int argIndex = Array.FindIndex(OutputValue, a => a.Name == argumentName);

            if (-1 == argIndex) { throw new ArgumentOutOfRangeException("No output by the name " + argumentName); }

            return OutputValue[argIndex].Value;
        }

        private void IfReady()
        {
            if (!(InputValue.Any(a=> null == a.Value)))
            {
                ReadyValue.Start();
            }
        }

        public void Reset()
        {
            ReadyValue = ReadyTask();

            foreach (var argument in InputValue)
            {
                argument.Value = null;
            }
            foreach (var argument in OutputValue)
            {
                argument.Value = null;
            }
        }

        protected abstract void CalcOut();
    }
}
