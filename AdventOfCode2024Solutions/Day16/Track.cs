namespace AdventOfCode2024Solutions.Day16
{
    public class Track
    {
        private HashSet<int> trackHistory = [];
        private Dictionary<int, Position> trackHistoryPositions = [];

        public void AddTrackPosition(int x, int y)
        {
            var encodedPosition = EncodeTrackPosition(x, y);
            trackHistory.Add(encodedPosition);
            if (Solution.WriteDebugInfoToConsole_PrintMapEverySolution || Solution.WriteDebugInfoToConsole_PrintMapEveryStep || Solution.WriteDebugInfoToConsole_PrintMapEverySolutionWhenSmaller)
                trackHistoryPositions.Add(encodedPosition, new Position(x, y));
        }

        public void AddTrackPosition(Position position)
        {
            var encodedPosition = EncodeTrackPosition(position.X, position.Y);
            trackHistory.Add(encodedPosition);
            if (Solution.WriteDebugInfoToConsole_PrintMapEverySolution || Solution.WriteDebugInfoToConsole_PrintMapEveryStep || Solution.WriteDebugInfoToConsole_PrintMapEverySolutionWhenSmaller)
                trackHistoryPositions.Add(encodedPosition, position);
        }

        public void RemoveVisitedLocation(int x, int y)
        {
            var encodedPosition = EncodeTrackPosition(x, y);
            trackHistory.Remove(encodedPosition);
            if (Solution.WriteDebugInfoToConsole_PrintMapEverySolution || Solution.WriteDebugInfoToConsole_PrintMapEveryStep || Solution.WriteDebugInfoToConsole_PrintMapEverySolutionWhenSmaller)
                trackHistoryPositions.Remove(encodedPosition);
        }

        public List<Position> GetPositionHistory()
        {
            return trackHistoryPositions.Values.ToList();
        }

        public bool HasVisitedPosition(int x, int y)
        {
            return trackHistory.Contains(EncodeTrackPosition(x, y));
        }

        private int EncodeTrackPosition(int x, int y)
        {
            return x * 100 + y;
        }
    }
}
