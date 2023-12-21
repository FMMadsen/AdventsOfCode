using Common;

namespace AdventOfCode2023Solutions.Day10
{
    public class Solution : IAOCSolution
    {
        public string PuzzleName => "Day 10: Pipe Maze";

        public string SolvePart1(string[] datasetLines)
        {
            var noOfYPipes = datasetLines.Length;
            var noOfXPipes = datasetLines[0].Length;
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

            var yourCurrentLocation = new PositionAndDirection(pipeMap, startY.Value, startX.Value, 'S', 1);
            var animalCurrentLocation = new PositionAndDirection(pipeMap, startY.Value, startX.Value, 'S', 2);

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

        private static PositionAndDirection Move(PositionAndDirection currentLocation, Char[,] map)
        {
            PositionAndDirection newPositionAndDirection = new(map, currentLocation);
            return newPositionAndDirection;
        }

        public string SolvePart2(string[] DatasetLines)
        {
            return "To be implemented";
        }
    }
}
