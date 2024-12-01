namespace AdventOfCode2023Solutions.Day18
{
    public class DigInstruction
    {
        public Direction Direction { get; private set; }
        public long Meters { get; private set; }
        public string Hex { get; private set; }

        public DigInstruction(string digInstructionLine)
        {
            var lineParts = digInstructionLine.Split(' ');
            Direction = (Direction)lineParts[0][0];
            Meters = long.Parse(lineParts[1]);
            Hex = lineParts[2].Substring(2, lineParts[2].Length - 2);
        }

        public void ConvertHexToInstruction()
        {
            var hex = Hex[0..^1];
            Meters = Convert.ToInt64(hex, 16);

            var directionChar = Hex.Last();
            switch (directionChar)
            {
                case '0':
                    Direction = Direction.Rightward;
                    break;
                case '1':
                    Direction = Direction.Downward;
                    break;
                case '2':
                    Direction = Direction.Leftward;
                    break;
                case '3':
                    Direction = Direction.Upward;
                    break;
            }
        }
    }
}
