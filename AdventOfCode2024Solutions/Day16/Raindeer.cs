namespace AdventOfCode2024Solutions.Day16
{
    public class Raindeer(Map map)
    {
        private long lowestScore = long.MaxValue;
        public long LowestScore => lowestScore;

        public void StartTraverse(Direction startDirection)
        {
            var track = new Track();
            track.AddTrackPosition(map.StartLocation);
            ArrivedAtLocation(map.StartLocation.X, map.StartLocation.Y, startDirection, 0, track);
        }

        private bool ArrivedAtLocation(int x, int y, Direction movingDirection, long moveScore, Track trackRecord)
        {
            if (Solution.WriteDebugInfoToConsole_PrintMapEveryStep)
                Solution.PrintMapToConsole(map.MapTiles, moveScore, new Position(x, y), trackRecord.GetPositionHistory());

            //if(moveScore > lowestScore)
            //    return true;

            bool isDeadEnd = false;
            int moveN = 0, moveS = 0, moveE = 0, moveW = 0;

            if (map.MapTiles[y, x] == 'E')
            {
                Solution.solutionsCount++;
                if (moveScore < lowestScore)
                {
                    lowestScore = moveScore;
                    if (Solution.WriteDebugInfoToConsole_SolutionsScoreWhenSmaller)
                    {
                        Solution.PrintNewSolutionScore(lowestScore);
                        Solution.PrintSolutionsCount();
                    }
                    if (Solution.WriteDebugInfoToConsole_PrintMapEverySolutionWhenSmaller)
                        Solution.PrintMapToConsole(map.MapTiles, moveScore, new Position(x, y), trackRecord.GetPositionHistory());
                }

                if (Solution.WriteDebugInfoToConsole_PrintMapEverySolution)
                    Solution.PrintMapToConsole(map.MapTiles, moveScore, new Position(x, y), trackRecord.GetPositionHistory());

                if (Solution.WriteDebugInfoToConsole_SolutionsCount)
                    Solution.PrintSolutionsCount();

                if (Solution.WriteDebugInfoToConsole_SolutionsScore)
                    Solution.PrintNewSolutionScore(moveScore);
            }
            else
            {
                switch (movingDirection)
                {
                    case Direction.North:
                        moveN = TryMove(Direction.North, x, y, movingDirection, trackRecord, moveScore);
                        moveE = TryMove(Direction.East, x, y, movingDirection, trackRecord, moveScore);
                        moveW = TryMove(Direction.West, x, y, movingDirection, trackRecord, moveScore);
                        isDeadEnd = moveN > 1 && moveE > 1 && moveW > 1;
                        break;
                    case Direction.South:
                        moveS = TryMove(Direction.South, x, y, movingDirection, trackRecord, moveScore);
                        moveE = TryMove(Direction.East, x, y, movingDirection, trackRecord, moveScore);
                        moveW = TryMove(Direction.West, x, y, movingDirection, trackRecord, moveScore);
                        isDeadEnd = moveS > 1 && moveE > 1 && moveW > 1;
                        break;
                    case Direction.East:
                        moveN = TryMove(Direction.North, x, y, movingDirection, trackRecord, moveScore);
                        moveS = TryMove(Direction.South, x, y, movingDirection, trackRecord, moveScore);
                        moveE = TryMove(Direction.East, x, y, movingDirection, trackRecord, moveScore);
                        isDeadEnd = moveN > 1 && moveS > 1 && moveE > 1;
                        break;
                    case Direction.West:
                        moveN = TryMove(Direction.North, x, y, movingDirection, trackRecord, moveScore);
                        moveS = TryMove(Direction.South, x, y, movingDirection, trackRecord, moveScore);
                        moveW = TryMove(Direction.West, x, y, movingDirection, trackRecord, moveScore);
                        isDeadEnd = moveN > 1 && moveS > 1 && moveW > 1;
                        break;
                }
            }

            trackRecord.RemoveVisitedLocation(x, y);
            return isDeadEnd;
        }

        /// <summary>
        /// Try move to a new location
        /// </summary>
        /// <returns>
        ///     0: succeeded. 
        ///     1: hasVisitedPositionAlready. 
        ///     2: isWall
        ///     3: isDeadEndRoad
        /// </returns>
        private int TryMove(Direction newDirection, int currentX, int currentY, Direction currentDirection, Track trackReccord, long moveScore)
        {
            int newX = 0;
            int newY = 0;
            switch (newDirection)
            {
                case Direction.North:
                    newX = currentX;
                    newY = currentY - 1;
                    break;
                case Direction.South:
                    newX = currentX;
                    newY = currentY + 1;
                    break;
                case Direction.East:
                    newX = currentX - 1;
                    newY = currentY;
                    break;
                case Direction.West:
                    newX = currentX + 1;
                    newY = currentY;
                    break;
            }

            bool isWall = map.MapTiles[newY, newX] == '#' || map.MapTiles[newY, newX] == 'O';
            bool hasVisitedPositionAlready = trackReccord.HasVisitedPosition(newX, newY);
            bool canMoveTo = !isWall && !hasVisitedPositionAlready;
            bool isDeadEnd = false;

            if (canMoveTo)
            {
                var isTurning = currentDirection != newDirection;

                if (isTurning)
                    moveScore += 1000;

                moveScore++;
                trackReccord.AddTrackPosition(newX, newY);
                isDeadEnd = ArrivedAtLocation(newX, newY, newDirection, moveScore, trackReccord);

                if (isDeadEnd)
                    map.MapTiles[newY, newX] = 'O';

                trackReccord.RemoveVisitedLocation(newX, newY);
            }
            return isDeadEnd ? 3 : isWall ? 2 : hasVisitedPositionAlready ? 1 : 0;
        }
    }
}
