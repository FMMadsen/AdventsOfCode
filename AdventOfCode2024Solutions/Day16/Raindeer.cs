namespace AdventOfCode2024Solutions.Day16
{
    public class Raindeer
    {
        private List<long> solutionScores = [];
        public static bool WriteDebugConsoleInfo = false;

        public long LowestScore => solutionScores.Min();

        public void StartTraverse(Direction startDirection, Map map)
        {
            var track = new Track();
            track.AddTrackPosition(map.StartLocation);
            ArrivedAtLocation(map, map.StartLocation.X, map.StartLocation.Y, startDirection, 0, track);
        }

        private bool ArrivedAtLocation(Map map, int x, int y, Direction movingDirection, long moveScore, Track trackRecord)
        {
            if (WriteDebugConsoleInfo)
                Solution.PrintCharGrid(map.MapTiles, moveScore, new Position(x, y), trackRecord.GetPositionHistory());

            if (map.MapTiles[y, x] == 'E')
            {
                solutionScores.Add(moveScore);
                trackRecord.RemoveVisitedLocation(x, y);
                return true;
            }

            var succeededNorth = TryMoveNorth(map, x, y, movingDirection, trackRecord, moveScore);
            var succeededSouth = TryMoveSouth(map, x, y, movingDirection, trackRecord, moveScore);
            var succeededEast = TryMoveEast(map, x, y, movingDirection, trackRecord, moveScore);
            var succeededWest = TryMoveWest(map, x, y, movingDirection, trackRecord, moveScore);

            if (!succeededNorth && !succeededSouth && !succeededEast && !succeededWest)
                trackRecord.RemoveVisitedLocation(x, y);

            return succeededNorth || succeededSouth || succeededEast || succeededWest;
        }

        private bool TryMoveNorth(Map map, int x, int y, Direction currentDirection, Track track, long moveScore)
        {
            int newX = x;
            int newY = y - 1;
            return TryMoveTo(map, newX, newY, currentDirection, Direction.North, track, moveScore);
        }

        private bool TryMoveSouth(Map map, int x, int y, Direction currentDirection, Track track, long moveScore)
        {
            int newX = x;
            int newY = y + 1;
            return TryMoveTo(map, newX, newY, currentDirection, Direction.South, track, moveScore);
        }

        private bool TryMoveEast(Map map, int x, int y, Direction currentDirection, Track track, long moveScore)
        {
            int newX = x + 1;
            int newY = y;
            return TryMoveTo(map, newX, newY, currentDirection, Direction.East, track, moveScore);
        }

        private bool TryMoveWest(Map map, int x, int y, Direction currentDirection, Track track, long moveScore)
        {
            int newX = x - 1;
            int newY = y;
            return TryMoveTo(map, newX, newY, currentDirection, Direction.West, track, moveScore);
        }

        private bool TryMoveTo(Map map, int newX, int newY, Direction currentDirection, Direction newDirection, Track track, long moveScore)
        {
            bool canMoveTo = CanMoveTo(map, newX, newY, track);

            if (canMoveTo)
            {
                var isTurning = currentDirection != newDirection;

                if (isTurning)
                    moveScore += 1000;

                moveScore++;
                track.AddTrackPosition(newX, newY);
                var wasSuccessful = ArrivedAtLocation(map, newX, newY, newDirection, moveScore, track);

                //if(!wasSuccessful)
                //    track.RemoveVisitedLocation(newX, newY);

                return wasSuccessful;
            }
            return false;
        }

        private bool CanMoveTo(Map map, int newX, int newY, Track trackReccord)
        {
            bool isOffGrid = newX < 0 || newY < 0 || newX >= map.NoOfXTiles || newY >= map.NoOfYTiles;
            if (isOffGrid)
                return false;

            bool isWall = map.MapTiles[newY, newX] == '#';
            if (isWall)
                return false;

            bool hasVisitedPositionAlready = trackReccord.HasVisitedPosition(newX, newY);
            if (hasVisitedPositionAlready)
                return false;

            return true;
        }
    }
}
