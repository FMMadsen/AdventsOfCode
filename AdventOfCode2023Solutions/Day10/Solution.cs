using Common;

namespace AdventOfCode2023Solutions.Day10
{
    public class Solution : IAOCSolution
    {
        public string PuzzleName => "Day 10: Pipe Maze";

        public string SolvePart1(string[] datasetLines)
        {
            var noOfXPipes = datasetLines.Length;
            var noOfYPipes = datasetLines[0].Length;
            var pipeMap = new Char[noOfXPipes, noOfYPipes];
            PositionAndDirection? yourCurrentLocation = null;
            PositionAndDirection? animalCurrentLocation = null;

            for (int x = 0; x < noOfXPipes; x++)
            {
                for (int y = 0; y < noOfYPipes; y++)
                {
                    pipeMap[x, y] = datasetLines[x][y];
                    if (pipeMap[x, y] == 'S')
                    {
                        yourCurrentLocation = DetermineStartPositionAndDirection(x, y, pipeMap, 1);
                        animalCurrentLocation = DetermineStartPositionAndDirection(x, y, pipeMap, 2);
                    }
                }
            }

            if(yourCurrentLocation == null || animalCurrentLocation == null) 
            {
                throw new Exception("start location not found!");
            }

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

            //var pipeSystem = new PipeSystem(datasetLines);
            //var moves = pipeSystem.MoveToYouMeetAnimal();
            //return moves.ToString();
        }

        private PositionAndDirection Move(PositionAndDirection currentLocation, Char[,] map)
        {
            PositionAndDirection newPositionAndDirection = new();

            //if(currentLocation == null)
            //    return

            char currentPipe = map[currentLocation.X, currentLocation.Y];

            char currentDirection = currentLocation.Direction;

            switch(currentPipe)
            {
                case '|':
                    if (currentLocation.IsDirectionNorth) newPositionAndDirection.SetDirectionNorth();
                    if (currentLocation.IsDirectionSouth) newPositionAndDirection.SetDirectionSouth();
                    break;
                case '-':
                    if (currentLocation.IsDirectionEast) newPositionAndDirection.SetDirectionEast();
                    if (currentLocation.IsDirectionWest) newPositionAndDirection.SetDirectionWest();
                    break;
                case 'L':
                    if (currentLocation.IsDirectionSouth) newPositionAndDirection.SetDirectionEast();
                    if (currentLocation.IsDirectionWest) newPositionAndDirection.SetDirectionNorth();
                    break;
                case 'J':
                    if (currentLocation.IsDirectionNorth) newPositionAndDirection.SetDirectionNorth();
                    if (currentLocation.IsDirectionSouth) newPositionAndDirection.SetDirectionSouth();
                    break;
                case '7':
                    if (currentLocation.IsDirectionNorth) newPositionAndDirection.SetDirectionNorth();
                    if (currentLocation.IsDirectionSouth) newPositionAndDirection.SetDirectionSouth();
                    break;
                case 'F':
                    if (currentLocation.IsDirectionNorth) newPositionAndDirection.SetDirectionNorth();
                    if (currentLocation.IsDirectionSouth) newPositionAndDirection.SetDirectionSouth();
                    break;
                case 'S':
                    //IsStartLocation = true;
                    //CanGoNorth = true;
                    //CanGoSouth = true;
                    //CanGoEast = true;
                    //CanGoWest = true;
                    break;
                default:
                    break;
            }


            throw new NotImplementedException();
        }

        private PositionAndDirection DetermineStartPositionAndDirection(int x, int y, Char[,] map, int directionNumber)
        {
            var posDir = new PositionAndDirection();

            return posDir;
        }

        public string SolvePart2(string[] DatasetLines)
        {
            return "To be implemented";
        }

        //public PositionAndDirection FindStart(string[] datasetLines)
        //{
        //    for (int x = 0; x < noOfXPipes; x++)
        //        for (int y = 0; y < noOfYPipes; y++)
        //        {
        //            PipeMap[x, y] = new Pipe(pipeMap[x][y], x, y);
        //            if (PipeMap[x, y].IsStartLocation)
        //                StartLocation = PipeMap[x, y];
        //        }
        //}

    }
}
