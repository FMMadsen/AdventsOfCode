using Common;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AdventOfCode2024Solutions.Day24
{
    public class Solution : IAOCSolution
    {
        public string PuzzleName => "Day 24: ";

        private Process[] Gates = Array.Empty<Process>();
        private Wire[] Wires = Array.Empty<Wire>();
        private Dictionary<string, bool> InitWires = new();

        private int TaskStatusLine = -1;

        private int DataLinesBreakPoint = -1;

        public string SolvePart1(string[] datasetLines)
        {
            Console.WriteLine("Loading XYWires");
            LoadXYWires(datasetLines);
            Console.WriteLine("Loading Gates");
            LoadGates(datasetLines);

            PrintWires();

            Console.WriteLine("Loading Initial Values");
            LoadInitialValues();

            Task[] tasks = Gates.Select(a=> a.Ready).ToArray();
            Console.WriteLine("Await Tasks");
           
            Task.WaitAll(tasks);

            Console.WriteLine("Convert results to Byte");
            Console.WriteLine('[' + String.Join(',', GetResults().Select(a=> a.ToString()).ToArray() ) + ']');

            return ConvertBoolArrayToInt(GetResults()).ToString();
        }

        private void TaskStatus()
        {
            int cursorTop = Console.GetCursorPosition().Top;
            Console.CursorTop = TaskStatusLine;

            foreach (Task calcTask in Gates.Select(a => a.Ready).ToArray())
            {
                Console.WriteLine("Task " + calcTask.Id.ToString() + " " + calcTask.Status.ToString());
            }

            Console.CursorTop = cursorTop;
        }

        private void PrintWires()
        {
            foreach(Wire currentWire in Wires)
            {
                Console.WriteLine(currentWire.ToString());
            }
        }

        private bool[] GetResults()
        {
            IEnumerable<bool> endWires = Wires.Where(a=> 0 == a.Output.Length).OrderBy(a=> a.Name).Select(a => (bool)a.Input?.Process.GetOutput("Result"));
            
            return endWires.ToArray();
        }

        private static long ConvertBoolArrayToInt(bool[] source)
        {
            long result = 0;
            
            int index = 0;

            foreach (bool b in source)
            {
                if (b)
                { result += (long)Math.Pow(2, index); }
                Console.WriteLine("bit " + index + " " + ((long)Math.Pow(2, index)).ToString() + " -> " + result.ToString());
                index++;
            }

            return result;
        }

        private static bool[] ConvertByteToBoolArray(byte b)
        {
            // prepare the return result
            bool[] result = new bool[8];

            // check each bit in the byte. if 1 set to true, if 0 set to false
            for (int i = 0; i < 8; i++)
                result[i] = (b & (1 << i)) != 0;

            // reverse the array
            Array.Reverse(result);

            return result;
        }

        public void Reset()
        {
            foreach (Process process in Gates)
            {
                process.Reset();
            }
        }

        public void Reload()
        {
            foreach (KeyValuePair<string, bool> init in InitWires)
            {
                Wire theWire = Wires.Where(a => init.Key == a.Name).First();
                Process theGate = theWire.Input?.Process ?? throw new Exception("wire input must not be empty");

                theGate.SetInput("Value", init.Value);
            }
        }

        private void LoadInitialValues()
        {
            foreach (KeyValuePair<string,bool> init in InitWires)
            {
                Wire theWire = Wires.Where(a=> init.Key == a.Name).First();
                Process valueP = new ValueProcess<bool>();

                theWire.Input = new ProcessConnect() { Argument = "Result", Process = valueP };
                valueP.SetOutputConnection("Result", theWire);

                Gates = Gates.Append(valueP).ToArray();

                valueP.SetInput("Value", init.Value);
            }
        }

        private void LoadXYWires(string[] datasetLines)
        {
            IEnumerable<Wire> wires = Wires;

            int i = 0;
            foreach (string line in datasetLines)
            {
                DataLinesBreakPoint = i;

                if (line.Length < 3)
                { break; }

                int colon = line.IndexOf(':');
                string name = line.Substring(0, colon);

                InitWires.Add(name, 0 != Int16.Parse(line.Substring(colon+2)));

                wires = wires.Append(new Wire(name));

                i++;
            }

            Wires = wires.ToArray();


        }

        private void LoadGates(string[] datasetLines)
        {
            
            for (int i = DataLinesBreakPoint+1; i < datasetLines.Length; i++)
            {
                string line = datasetLines[i];
                int space = line.IndexOf(' ');
                int begin = 0;

                string left = line.Substring(begin, space - begin);
                begin = space + 1;
                space = line.IndexOf(' ', begin);

                string gate = line.Substring(begin, space - begin);
                begin = space + 1;
                space = line.IndexOf(' ', space + 1);

                string right = line.Substring(begin, space - begin);
                begin = space + 1;
                space = line.IndexOf(' ', space + 1);

                begin = space + 1;
                string result = line.Substring(begin);


                Wire leftWire = AddGetWire(left);
                Wire rightWire = AddGetWire(right);
                Wire resultWire = AddGetWire(result);
                Gate processGate = AddGate(gate);

                resultWire.Input = new ProcessConnect() { Argument = "Result", Process = processGate };
                leftWire.AttachOutput(new ProcessConnect() { Argument = "Left", Process = processGate });
                rightWire.AttachOutput(new ProcessConnect() { Argument = "Right", Process = processGate });

                processGate.Left.Connection = leftWire;
                processGate.Right.Connection = rightWire;
                processGate.Result.Connection = resultWire;

                if (i+1 == datasetLines.Length)
                {
                    Console.WriteLine("Last DataLine");
                }
            }


        }

        private Wire AddGetWire(string name)
        {
            int wireIndex = Array.FindIndex(Wires, a => a.Name == name);

            if (-1 < wireIndex)
            {
                return Wires[wireIndex];
            }

            return AddWire(name);
        }

        private Wire AddWire(string name)
        {
            Wire newWire = new Wire(name);
            Wires = Wires.Append(newWire).ToArray();
            return newWire;
        }

        private Gate AddGate(string gateMethod)
        {
            Gate newGate;

            switch (gateMethod)
            {
                case "AND":
                    newGate = new AndGate();
                    break;
                case "OR":
                    newGate = new OrGate();
                    break;
                case "XOR":
                    newGate = new XorGate();
                    break;

                default:
                    throw new ArgumentOutOfRangeException("argument gateMethod \""+ gateMethod + "\" need to be a known method.");
                    break;
            }

            Gates = Gates.Append(newGate).ToArray();
            return newGate;
        }

        public string SolvePart2(string[] datasetLines)
        {
            return "To be implemented";
        }
    }
}
