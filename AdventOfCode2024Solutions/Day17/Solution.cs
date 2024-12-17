using Common;

namespace AdventOfCode2024Solutions.Day17
{
    public class Solution : IAOCSolution
    {
        public string PuzzleName => "Day 17: ";


        protected int RegA = 0;
        protected int RegB = 0;
        protected int RegC = 0;

        protected Opcode[] Instructions = Array.Empty<Opcode>();
        protected int Pointer = 0;

        protected IEnumerable<int> Output = Enumerable.Empty<int>();

        public string SolvePart1(string[] datasetLines)
        {
            Load(datasetLines);
            bool advance = true;

            while (Pointer < Instructions.Length)
            {
                switch (Instructions[Pointer])
                {
                    case Opcode.cadv:
                        Cadv(Instructions[Pointer+1]);
                        break;
                    case Opcode.cbxl:
                        Cbxl(Instructions[Pointer + 1]);
                        break;
                    case Opcode.cbst:
                        Cbst(Instructions[Pointer + 1]);
                        break;
                    case Opcode.cjnz:
                        advance = Cjnz(Instructions[Pointer + 1]);
                        break;
                    case Opcode.cbxc:
                        Cbxc(Instructions[Pointer + 1]);
                        break;
                    case Opcode.cout:
                        Cout(Instructions[Pointer + 1]);
                        break;
                    case Opcode.cbdv:
                        Cbdv(Instructions[Pointer + 1]);
                        break;
                    case Opcode.ccdv:
                        Ccdv(Instructions[Pointer + 1]);
                        break;
                    default:
                        throw new Exception("Opcode out of bounds, program in critical error.");
                        break;
                }

                if (advance) { Pointer += 2; }
                else { advance = true; }
            }

            Pointer = 0;


            Console.WriteLine(String.Join(',', Output));
            return String.Join(',', Output);
        }

        protected void Load(string[] datasetLines)
        {
            int startNum = datasetLines[0].IndexOf(':') + 2;
            RegA = int.Parse(datasetLines[0].Substring(startNum));
            startNum = datasetLines[1].IndexOf(':') + 2;
            RegB = int.Parse(datasetLines[1].Substring(startNum));
            startNum = datasetLines[2].IndexOf(':') + 2;
            RegC = int.Parse(datasetLines[2].Substring(startNum));
            startNum = datasetLines[4].IndexOf(':') + 2;

            string[] instructions = datasetLines[4].Substring(startNum).Split(',');
            Instructions = new Opcode[instructions.Length];

            for (int i = 0; i < instructions.Length; i++)
            {
                Instructions[i] = Enum.Parse<Opcode>(instructions[i]);
            }
        }

        protected void Cadv(Opcode input)
        {
            RegA = (int)( RegA / Math.Pow(2, GetCombo(input)) );
        }

        protected void Cbxl(Opcode input)
        {
            RegB = RegB | (int)input;
        }
        protected void Cbst(Opcode input)
        {
            RegB = GetCombo(input) % 8;
        }
        protected bool Cjnz(Opcode input)
        {
            if (0 != RegA)
            {
                Pointer = (int)input;
                return false;
            }

            return true;
        }
        protected void Cbxc(Opcode input)
        {
            RegB = RegB | RegC;
        }
        protected void Cout(Opcode input)
        {
            Output = Output.Append(GetCombo(input) % 8);
        }
        protected void Cbdv(Opcode input)
        {
            RegB = (int)(RegA / Math.Pow(2, GetCombo(input)));
        }
        protected void Ccdv(Opcode input)
        {
            RegC = (int)(RegA / Math.Pow(2, GetCombo(input)));
        }

        protected int GetCombo(Opcode input)
        {
            int output;

            switch (input)
            {
                case Opcode.cadv:
                    output = 0;
                    break;
                case Opcode.cbxl:
                    output = 1;
                    break;
                case Opcode.cbst:
                    output = 2;
                    break;
                case Opcode.cjnz:
                    output = 3;
                    break;
                case Opcode.cbxc:
                    output = RegA;
                    break;
                case Opcode.cout:
                    output = RegB;
                    break;
                case Opcode.cbdv:
                    output = RegC;
                    break;
                case Opcode.ccdv:
                    throw new Exception("combo 7 detected, program in critical error.");
                    break;
                default:
                    throw new Exception("combo out of bounds, program in critical error.");
                    break;
            }

            return output;
        }

        public string SolvePart2(string[] datasetLines)
        {
            return "To be implemented";
        }
    }
}
