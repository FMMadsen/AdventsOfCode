using Common;

namespace AdventOfCode2023Solutions.Day18
{
    public class Solution : IAOCSolution
    {
        public string PuzzleName => "Day 18: Lavaduct Lagoon";

        public string SolvePart1(string[] datasetLines)
        {
            var digInstructions = datasetLines.Select(l => new DigInstruction(l));
            var map = CreateEmptyMap(digInstructions, out Position startPosition);
            PopupateMapsWithDots(map);
            PopulateMapWithPipes(map, digInstructions, startPosition);

            var diggedInnerArea = AdventOfCode2023Solutions.Day10.Solution.CountEnclosedSpaces(map);
            var countDiggedOutherLine = digInstructions.Sum(i => i.Meters);
            var sumDigged = diggedInnerArea + countDiggedOutherLine;
            return sumDigged.ToString();
        }

        public string SolvePart2(string[] datasetLines)
        {
            //var digInstructions = datasetLines.Select(l => new DigInstruction(l)).ToList();
            //digInstructions.ForEach(i => i.ConvertHexToInstruction());
            //var map = CreateEmptyMap(digInstructions, out Position startPosition);
            //PopupateMapsWithDots(map);
            //PopulateMapWithPipes(map, digInstructions, startPosition);

            //var diggedInnerArea = AdventOfCode2023Solutions.Day10.Solution.CountEnclosedSpaces(map);
            //var countDiggedOutherLine = digInstructions.Sum(i => i.Meters);
            //var sumDigged = diggedInnerArea + countDiggedOutherLine;
            //return sumDigged.ToString();
            return "Cannot implement the usual way, as the map gets out of memory. It is going to be too large!";
        }

        public static char[,] CreateEmptyMap(IEnumerable<DigInstruction> digInstructions, out Position StartPosition)
        {
            long xMin = 0;
            long xMax = 0;
            long yMin = 0;
            long yMax = 0;

            long xCurrent = 0;
            long yCurrent = 0;

            foreach (DigInstruction digInstruction in digInstructions)
            {
                switch (digInstruction.Direction)
                {
                    case Direction.Upward:
                        yCurrent -= digInstruction.Meters;
                        break;
                    case Direction.Downward:
                        yCurrent += digInstruction.Meters;
                        break;
                    case Direction.Leftward:
                        xCurrent -= digInstruction.Meters;
                        break;
                    case Direction.Rightward:
                        xCurrent += digInstruction.Meters;
                        break;
                }
                if (xCurrent < xMin) xMin = xCurrent;
                if (xCurrent > xMax) xMax = xCurrent;
                if (yCurrent < yMin) yMin = yCurrent;
                if (yCurrent > yMax) yMax = yCurrent;
            }

            StartPosition = new Position(yCurrent - yMin, xCurrent - xMin);
            var map = new char[yMax - yMin + 1, xMax - xMin + 1];
            return map;
        }

        private void PopulateMapWithPipes(char[,] map, IEnumerable<DigInstruction> digInstructions, Position startPosition)
        {
            var currentPosition = startPosition.Clone();

            map[currentPosition.Y, currentPosition.X] = 'S';
            var firstDirection = digInstructions.ToList().First().Direction;
            var previousDirection = firstDirection;

            foreach (DigInstruction digInstruction in digInstructions)
            {
                var newDirection = digInstruction.Direction;
                map[currentPosition.Y, currentPosition.X] = GetCornerPipe(previousDirection, newDirection);

                switch (newDirection)
                {
                    case Direction.Upward:
                        Repeat(digInstruction.Meters, () => DigUp(map, currentPosition));
                        break;
                    case Direction.Downward:
                        Repeat(digInstruction.Meters, () => DigDown(map, currentPosition));
                        break;
                    case Direction.Leftward:
                        Repeat(digInstruction.Meters, () => DigLeft(map, currentPosition));
                        break;
                    case Direction.Rightward:
                        Repeat(digInstruction.Meters, () => DigRight(map, currentPosition));
                        break;
                }
                previousDirection = digInstruction.Direction;
            }
            map[currentPosition.Y, currentPosition.X] = GetCornerPipe(previousDirection, firstDirection);
        }

        public static void DigUp(char[,] map, Position currentPosition)
        {
            currentPosition.Y -= 1;
            map[currentPosition.Y, currentPosition.X] = '|';
        }

        public static void DigDown(char[,] map, Position currentPosition)
        {
            currentPosition.Y += 1;
            map[currentPosition.Y, currentPosition.X] = '|';
        }

        public static void DigLeft(char[,] map, Position currentPosition)
        {
            currentPosition.X -= 1;
            map[currentPosition.Y, currentPosition.X] = '-';
        }

        public static void DigRight(char[,] map, Position currentPosition)
        {
            currentPosition.X += 1;
            map[currentPosition.Y, currentPosition.X] = '-';
        }

        public static char GetCornerPipe(Direction previoisDirection, Direction newDirection)
        {
            if (previoisDirection == Direction.Upward && newDirection == Direction.Leftward)
                return '7';

            if (previoisDirection == Direction.Upward && newDirection == Direction.Rightward)
                return 'F';

            if (previoisDirection == Direction.Downward && newDirection == Direction.Leftward)
                return 'J';

            if (previoisDirection == Direction.Downward && newDirection == Direction.Rightward)
                return 'L';

            if (previoisDirection == Direction.Leftward && newDirection == Direction.Upward)
                return 'L';

            if (previoisDirection == Direction.Leftward && newDirection == Direction.Downward)
                return 'F';

            if (previoisDirection == Direction.Rightward && newDirection == Direction.Upward)
                return 'J';

            if (previoisDirection == Direction.Rightward && newDirection == Direction.Downward)
                return '7';

            return 'S';
        }

        public static void Repeat(long count, Action action)
        {
            for (long i = 0; i < count; i++)
                action();
        }

        private static void PopupateMapsWithDots(char[,] map)
        {
            long noOfYs = map.GetLength(0);
            long noOfXs = map.GetLength(1);

            for (long y = 0; y < noOfYs; y++)
            {
                for (long x = 0; x < noOfXs; x++)
                {
                    map[y, x] = '.';
                }
            }
        }

        private void PrintMap(char[,] map)
        {
            long noOfYs = map.GetLength(0);
            long noOfXs = map.GetLength(1);

            Console.WriteLine("Print map:");
            for (long y = 0; y < noOfYs; y++)
            {
                for (long x = 0; x < noOfXs; x++)
                {
                    Console.Write(map[y, x]);
                }
                Console.WriteLine("");
            }
        }
    }
}
