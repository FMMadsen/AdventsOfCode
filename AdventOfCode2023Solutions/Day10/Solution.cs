using Common;

namespace AdventOfCode2023Solutions.Day10
{
    public class Solution : IAOCSolution
    {
        public string PuzzleName => "Day 10: Pipe Maze";
        public char[,]? ResultData { get; set; } = null;

        public string SolvePart1(string[] datasetLines)
        {
            var pipeMap = CreateAndPopulatePipeMap(
                datasetLines,
                out PositionAndDirection yourCurrentLocation,
                out PositionAndDirection animalCurrentLocation);

            int moveCounter = 0;
            bool youAndAnimalMeet = false;
            while (!youAndAnimalMeet)
            {
                moveCounter++;
                yourCurrentLocation = Move(yourCurrentLocation, pipeMap);
                animalCurrentLocation = Move(animalCurrentLocation, pipeMap);
                if (yourCurrentLocation.IsSamePositionAs(animalCurrentLocation))
                    break;
            }

            return moveCounter.ToString();
        }

        public string SolvePart2(string[] datasetLines)
        {
            var pipeMap = CreateAndPopulatePipeMap(
                datasetLines,
                out PositionAndDirection yourCurrentLocation,
                out PositionAndDirection animalCurrentLocation);

            var newMap = CreateNewCleanedMapPolulatedWithDots(pipeMap);
            newMap[yourCurrentLocation.Y, yourCurrentLocation.X] = yourCurrentLocation.PipeChar;

            bool youAndAnimalMeet = false;
            while (!youAndAnimalMeet)
            {
                yourCurrentLocation = Move(yourCurrentLocation, pipeMap);
                animalCurrentLocation = Move(animalCurrentLocation, pipeMap);
                newMap[yourCurrentLocation.Y, yourCurrentLocation.X] = yourCurrentLocation.PipeChar;
                newMap[animalCurrentLocation.Y, animalCurrentLocation.X] = animalCurrentLocation.PipeChar;

                if (yourCurrentLocation.IsSamePositionAs(animalCurrentLocation))
                    break;
            }

            var enclosedSpaces = CountEnclosedSpaces(newMap);
            ResultData = newMap;

            return enclosedSpaces.ToString();
        }

        private static char[,] CreateAndPopulatePipeMap(
            string[] datasetLines,
            out PositionAndDirection yourCurrentLocation,
            out PositionAndDirection animalCurrentLocation)
        {
            int noOfYPipes = datasetLines.Length;
            int noOfXPipes = datasetLines[0].Length;
            var pipeMap = new Char[noOfYPipes, noOfXPipes];
            int? startX = null, startY = null;

            for (int y = 0; y < noOfYPipes; y++)
            {
                for (int x = 0; x < noOfXPipes; x++)
                {
                    pipeMap[y, x] = datasetLines[y][x];
                    if (pipeMap[y, x] == 'S')
                    {
                        startX = x;
                        startY = y;
                    }
                }
            }

            if (startX == null || startY == null)
                throw new Exception("start location not found!");

            yourCurrentLocation = new PositionAndDirection(pipeMap, startY.Value, startX.Value, 'S', 1);
            animalCurrentLocation = new PositionAndDirection(pipeMap, startY.Value, startX.Value, 'S', 2);

            return pipeMap;
        }

        private static char[,] CreateNewCleanedMapPolulatedWithDots(char[,] map)
        {
            int noOfYs = map.GetLength(0);
            int noOfXs = map.GetLength(1);

            var newMap = new Char[noOfYs, noOfXs];
            for (int y = 0; y < noOfYs; y++)
            {
                for (int x = 0; x < noOfXs; x++)
                {
                    newMap[y, x] = '.';
                }
            }
            return newMap;
        }

        private static PositionAndDirection Move(PositionAndDirection currentLocation, Char[,] map)
        {
            PositionAndDirection newPositionAndDirection = new(map, currentLocation);
            return newPositionAndDirection;
        }

        private static int CountEnclosedSpaces(char[,] map)
        {
            int noOfYs = map.GetLength(0);
            int noOfXs = map.GetLength(1);

            int enclosedCounter = 0;

            for (int y = 0; y < noOfYs; y++)
            {
                bool isInEnclosed = false;
                bool onPipeFromNorth = false;
                bool onPipeFromSouth = false;

                for (int x = 0; x < noOfXs; x++)
                {
                    var pipeChar = map[y, x];

                    if (pipeChar == '|')
                        isInEnclosed = !isInEnclosed;

                    if (pipeChar == 'L')
                        onPipeFromNorth = true;

                    if (pipeChar == 'F')
                        onPipeFromSouth = true;

                    if (pipeChar == 'J')
                        if (onPipeFromNorth)
                            onPipeFromNorth = false;
                        else
                        {
                            isInEnclosed = !isInEnclosed;
                            onPipeFromSouth = false;
                        }

                    if (pipeChar == '7')
                        if (onPipeFromSouth)
                            onPipeFromSouth = false;
                        else
                        {
                            isInEnclosed = !isInEnclosed;
                            onPipeFromNorth = false;
                        }

                    if (pipeChar == '.')
                    {
                        if (isInEnclosed)
                        {
                            enclosedCounter++;
                            map[y, x] = 'o';
                        }
                    }
                }
            }

            return enclosedCounter;
        }
    }
}
