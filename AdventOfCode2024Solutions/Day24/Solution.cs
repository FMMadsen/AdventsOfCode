using Common;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Numerics;
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

        private bool[] Results = [];

        private Wire[] Affected = [];

        public string SolvePart1(string[] datasetLines)
        {
            Console.WriteLine("Loading XYWires");
            LoadXYWires(datasetLines);
            Console.WriteLine("Loading Gates");
            LoadGates(datasetLines);

            //PrintWires();

            Console.WriteLine("Loading Initial Values");
            LoadInitialValues();

            
            Console.WriteLine("Await Tasks");
            Task[] tasks = Gates.Select(a => a.Ready).ToArray();
            Task.WaitAll(tasks);

            Console.WriteLine("Convert results to Long");
            Results = GetResults();
            Console.WriteLine('[' + String.Join(',', Results.Select(a=> a.ToString()).ToArray() ) + ']');

            return ConvertBoolArrayToLong(Results).ToString();
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
            IEnumerable<bool> endWires = Wires.Where(a=> 'z' == a.Name[0]).OrderBy(a=> a.Name).Select(a => (bool)a.Input?.Process.GetOutput("Result"));
            
            return endWires.ToArray();
        }

        private bool[] GetXs()
        {
            IEnumerable<bool> endWires = Wires.Where(a => 'x' == a.Name[0]).OrderBy(a => a.Name).Select(a => (bool)a.Input?.Process.GetOutput("Result"));

            return endWires.ToArray();
        }

        private bool[] GetYs()
        {
            IEnumerable<bool> endWires = Wires.Where(a => 'y' == a.Name[0]).OrderBy(a => a.Name).Select(a => (bool)a.Input?.Process.GetOutput("Result"));

            return endWires.ToArray();
        }

        private static long ConvertBoolArrayToLong(bool[] source)
        {
            long result = 0;
            
            int index = 0;

            foreach (bool b in source)
            {
                if (b)
                { result += (long)Math.Pow(2, index); }
                //Console.WriteLine("bit " + index + " " + ((long)Math.Pow(2, index)).ToString() + " -> " + result.ToString());
                index++;
            }

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

                if (leftWire.Name == resultWire.Name || rightWire.Name == resultWire.Name)
                {
                    throw new Exception("A Process may not output to it's own input. It does through Wire " + resultWire.Name);
                }

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
            Console.WriteLine("Part2");
            Console.WriteLine(ConvertBoolArrayToLong(GetXs()));
            Console.WriteLine(String.Join(',', GetXs()));

            Console.WriteLine(ConvertBoolArrayToLong(GetYs()));
            Console.WriteLine(String.Join(',', GetYs()));

            Console.WriteLine(ConvertBoolArrayToLong(GetXs()) + ConvertBoolArrayToLong(GetYs()));

            bool[] newXY = GetResultCheck();
            Console.WriteLine(String.Join(',', newXY));


            Console.WriteLine(ConvertBoolArrayToLong(newXY));
            Console.WriteLine("0 " + String.Join(',', newXY));
            Console.WriteLine("1 " + String.Join(',', AddBoolArray(GetXs(), GetYs())));

            //int i = 0;
            //foreach (bool v in newXY)
            //{

            //    if (Results[i] != v)
            //    {
            //        Console.WriteLine("E in " + i.ToString());
            //    }

            //    i++;
            //}

            PrintFlow();

            for(int i = 0; i < Affected.Length; i++)
            {
                if ( "mrs" == Affected[i].Name)
                {
                    Affected[i] = Wires.Where(a => a.Name == "jmh").First();
                }
            }

            return String.Join(',', Affected.Select(a=> a.Name).Order().ToArray());
        }

        private bool[] GetResultCheck()
        {
            bool[] newXY = ConvertLongToBoolArray(ConvertBoolArrayToLong(GetXs()) + ConvertBoolArrayToLong(GetYs()));
            

            if (newXY.Length < Results.Length)
            { newXY = ExtendTo(newXY, Results.Length); }
            
            //else if (Results.Length < newXY.Length)
            //{
            //    Results = ExtendTo(Results, newXY.Length);
            //}

            return newXY;
        }

        private bool[] ConvertLongToBoolArray(long aNumber)
        {
            IEnumerable<bool> result = Enumerable.Empty<bool>();

            int maxPosition = -1;
            long poweredMax = 0;
            while (poweredMax <= aNumber)
            {
                maxPosition++;
                poweredMax = (long)Math.Pow(2, maxPosition);
            }
            maxPosition--;

            for (int i = maxPosition; -1 < i; i--)
            {
                long temp = aNumber - (long)Math.Pow(2, i);

                result = result.Append(-1 < temp);

                if (-1 < temp)
                {
                    aNumber = temp;
                }
            }

            return result.Reverse().ToArray();
        }

        private bool[] ExtendTo(bool[] anArray, int length)
        {
            if (anArray.Length < length)
            {
                IEnumerable<bool> temp = anArray;
                int deltaLeft = length - anArray.Length;

                while (0 < deltaLeft)
                {
                    temp = temp.Append(false);

                    deltaLeft--;
                }

                anArray = temp.ToArray();
            }
            else if (length < anArray.Length)
            {
                anArray = anArray.Where((a, i) => i < length).ToArray();
            }

            return anArray;
        }

        private void PrintFlow()
        {

            Wire[] zWires = Wires.Where(a => 'z' == a.Name[0]).OrderBy(a => a.Name).ToArray();

            int i = 0;
            foreach (Wire zWire in zWires)
            {
                PrintWire(zWire);
                Console.WriteLine("");
                Console.WriteLine("");

                i++;
            }

            Console.WriteLine();
            foreach (var aff in Affected)
            {
                Console.WriteLine(aff?.ToString() ?? Affected.Length.ToString());
            }
        }

        private void PrintWire(Wire aWire)
        {

            if (null == aWire.Input) { throw new Exception("z wires need to be connected to a Gate on the input"); }

            if (aWire.Input?.Process is Gate zGate)
            {
                Wire temp;
                Wire previousWire;

                //Console.WriteLine(zGate.Left.Connection?.Input?.Process is XorGate);
                //if (zGate.Left.Connection?.Input?.Process is XorGate)
                //{
                //    Console.WriteLine(((Gate)zGate.Left.Connection?.Input?.Process).Left.Connection?.Name[0]);
                //    Console.WriteLine('x' == ((Gate)zGate.Left.Connection?.Input?.Process).Left.Connection?.Name[0] || 'y' == ((Gate)zGate.Left.Connection?.Input?.Process).Left.Connection?.Name[0]);
                //    Console.WriteLine(aWire.Name.Substring(1, 2) );
                //    Console.WriteLine(((Gate)zGate.Left.Connection?.Input?.Process).Left.Connection?.Name.Substring(1, 2));

                //}

                if (zGate.Left.Connection?.Input?.Process is XorGate 
                    //&& ('x' == ((Gate)zGate.Left.Connection?.Input?.Process).Left.Connection?.Name[0] || 'y' == ((Gate)zGate.Left.Connection?.Input?.Process).Left.Connection?.Name[0]) 
                    && aWire.Name.Substring(1, 2) == ((Gate)zGate.Left.Connection?.Input?.Process).Left.Connection?.Name.Substring(1, 2))
                {
                    temp = zGate.Left.Connection;
                    previousWire = zGate.Right.Connection;
                }
                else
                {
                    temp = zGate.Right.Connection;
                    previousWire = zGate.Left.Connection;
                }

                string resultT = temp.ToString();
                string result = aWire.ToString();
                string extra = previousWire.ToString();

                if (String.Concat('z', Results.Length - 1) != aWire.Name 
                    && "z00" != aWire.Name 
                    && "z01" != aWire.Name)
                {
                    if (zGate is not XorGate)
                    {
                        AddAffected(zGate.Result.Connection);

                        // find XOR gate with input XOR and OR and the XOR left or right begins with xXX or yXX
                        Wire? alsoAffected = default;

                        foreach (Process ps in Gates)
                        {
                            if (ps is XorGate gate)
                            {
                                XorGate inputXOR;
                                OrGate inputOr;

                                if (gate.Left.Connection?.Input?.Process is XorGate xg && gate.Right.Connection?.Input?.Process is OrGate og)
                                {
                                    inputXOR = xg;
                                    inputOr = og;
                                }
                                else if (gate.Right.Connection?.Input?.Process is XorGate xg2 && gate.Left.Connection?.Input?.Process is OrGate og2)
                                {
                                    inputXOR = xg2;
                                    inputOr = og2;
                                }
                                else
                                {
                                    continue;
                                }

                                {
                                    string xName = string.Concat("x", aWire.Name.AsSpan(1, 2));
                                    string yName = string.Concat("y", aWire.Name.AsSpan(1, 2));

                                    if ((inputXOR.Left.Connection?.Name == xName
                                        && inputXOR.Right.Connection?.Name == yName)
                                        ||
                                        (inputXOR.Right.Connection?.Name == xName
                                        && inputXOR.Left.Connection?.Name == yName))
                                    {
                                        alsoAffected = gate.Result.Connection;
                                        break;
                                    }
                                }

                                if (null == alsoAffected)
                                {
                                    if (inputOr.Left.Connection?.Input?.Process is AndGate leftAnd && inputOr.Right.Connection?.Input?.Process is AndGate rightAnd)
                                    {
                                        int number = Int32.Parse(aWire.Name.AsSpan(1, 2)) - 1;
                                        string nameEnd = number < 10 ? '0' + number.ToString() : number.ToString();

                                        string xName = string.Concat("x", nameEnd);
                                        string yName = string.Concat("y", nameEnd);

                                        if ((leftAnd.Left.Connection?.Name == xName
                                        && leftAnd.Right.Connection?.Name == yName)
                                        ||
                                        (leftAnd.Right.Connection?.Name == xName
                                        && leftAnd.Left.Connection?.Name == yName)
                                        ||
                                        (rightAnd.Left.Connection?.Name == xName
                                        && rightAnd.Right.Connection?.Name == yName)
                                        ||
                                        (rightAnd.Right.Connection?.Name == xName
                                        && rightAnd.Left.Connection?.Name == yName))
                                        {
                                            alsoAffected = gate.Result.Connection;
                                        }


                                    }
                                    else
                                    {
                                        continue;
                                    }
                                }

                            }
                        }

                        if (null != alsoAffected)
                        {
                            AddAffected(alsoAffected);
                        }
                        else
                        {
                            //throw new Exception("Not found XorGate");
                        }
                    }
                    else
                    {
                        if (temp.Input?.Process is Gate && temp.Input?.Process is not XorGate)
                        {
                            AddAffected(temp);
                            // find XOR gate with input XOR and OR and the XOR left or right begins with xXX or yXX
                            Wire? alsoAffected = default;

                            foreach (Process ps in Gates)
                            {
                                if (ps is XorGate gate)
                                {
                                    string xName = string.Concat("x", aWire.Name.AsSpan(1, 2));
                                    string yName = string.Concat("y", aWire.Name.AsSpan(1, 2));

                                    if ((gate.Left.Connection?.Name == xName
                                        && gate.Right.Connection?.Name == yName)
                                        ||
                                        (gate.Right.Connection?.Name == xName
                                        && gate.Left.Connection?.Name == yName))
                                    {
                                        alsoAffected = gate.Result.Connection;
                                        break;
                                    }
                                }
                            }

                            if (null != alsoAffected)
                            {
                                AddAffected(alsoAffected);
                            }
                            else
                            {
                                throw new Exception("Not found XorGate");
                            }
                        }
                        else if (previousWire.Input?.Process is Gate && previousWire.Input?.Process is not OrGate)
                        {
                            AddAffected(previousWire);
                            // find XOR gate with input XOR and OR and the XOR left or right begins with xXX or yXX
                            Wire? alsoAffected = default;

                            foreach (Process ps in Gates)
                            {
                                if (ps is OrGate gate)
                                {
                                    
                                    if (gate.Left.Connection?.Input?.Process is AndGate leftAnd && gate.Right.Connection?.Input?.Process is AndGate rightAnd)
                                    {
                                        int number = Int32.Parse(aWire.Name.AsSpan(1, 2)) - 1;
                                        string nameEnd = number < 10 ? '0' + number.ToString() : number.ToString();

                                        string xName = string.Concat("x", nameEnd);
                                        string yName = string.Concat("y", nameEnd);

                                        if ((leftAnd.Left.Connection?.Name == xName
                                        && leftAnd.Right.Connection?.Name == yName)
                                        ||
                                        (leftAnd.Right.Connection?.Name == xName
                                        && leftAnd.Left.Connection?.Name == yName)
                                        ||
                                        (rightAnd.Left.Connection?.Name == xName
                                        && rightAnd.Right.Connection?.Name == yName)
                                        ||
                                        (rightAnd.Right.Connection?.Name == xName
                                        && rightAnd.Left.Connection?.Name == yName))
                                        {
                                            alsoAffected = gate.Result.Connection;
                                            break;
                                        }

                                    }
                                }
                            }

                            if (null != alsoAffected)
                            {
                                AddAffected(alsoAffected);
                            }
                            else
                            {
                                throw new Exception("Not found OrGate");
                            }
                        }
                    }

                }


                Console.Write(result + " (XOR) <- ");
                Console.Write(resultT + " (XOR) <- ");
                Console.Write(extra + " (OR) <- ");

                if (previousWire.Input?.Process is Gate previousGate)
                {
                    string extraT = previousGate.Left.Connection.ToString();
                    string extraTT = previousGate.Right.Connection.ToString();


                    if (previousGate.Left.Connection.Input?.Process is Gate && previousGate.Left.Connection.Input?.Process is not AndGate)
                    {
                        //AddAffected(previousGate.Left.Connection);
                        // solve
                    }

                    if (previousGate.Left.Connection.Input?.Process is Gate && previousGate.Right.Connection.Input?.Process is not AndGate)
                    {
                        //AddAffected(previousGate.Right.Connection);
                        // solve
                    }

                    Console.Write(extraT + " (AND) <- ");
                    Console.Write(extraTT + " (AND) ");
                }
               

            }

        }

        private void AddAffected(Wire affected)
        {
            if ( ! Affected.Any(a=> affected.Name == a.Name) )
            {
                Affected = Affected.Append(affected).ToArray();
            }

        }

        private bool[] AddBoolArrayOld(bool[] left, bool[] right)
        {
            if (left.Length < right.Length) 
            { left = ExtendTo(left, right.Length); }
            else if (right.Length < left.Length) 
            { right = ExtendTo(right, left.Length); }

            var result = new bool[left.Length + 1];
            var extra  = new bool[left.Length + 1];

            result[0] = left[0] != right[0]; // XOR
            extra[0] = left[0] && right[0]; // AND
            bool resultT = false;
            bool extraT;
            bool extraTT;

            for (int i = 1; i < left.Length; i++)
            {
                resultT = left[i] != right[i]; // XOR 
                result[i] = extra[i-1] != resultT; // XOR
                extraT = left[i] && right[i]; // AND
                extraTT = extra[i-1] && resultT; // AND
                extra[i] = extraT || extraTT; // OR
            }

            result[result.Length-1] = extra[result.Length - 2] != resultT; // XOR

            return result;
        }

        private bool[] AddBoolArray(bool[] left, bool[] right)
        {
            if (left.Length < right.Length)
            { left = ExtendTo(left, right.Length); }
            else if (right.Length < left.Length)
            { right = ExtendTo(right, left.Length); }

            var result = new bool[left.Length + 1];
            var extra = new bool[left.Length + 1];

            result[0] = left[0] != right[0]; // XOR
            
            bool resultT = result[0];
            bool extraT;
            bool extraTT;

            for (int i = 1; i < result.Length; i++)
            {
                extraT = left[i-1] && right[i-1]; // AND
                if (1 < i)
                {
                    extraTT = extra[i - 2] && resultT; // AND
                    extra[i - 1] = extraT || extraTT; // OR
                }
                else
                {
                    extra[i - 1] = extraT;
                }

                if (i+1 == result.Length)
                {
                    resultT = false;
                }
                else
                {

                    resultT = left[i] != right[i]; // XOR 
                }
                result[i] = extra[i-1] != resultT; // XOR
                
            }

            return result;
        }
    }
}
