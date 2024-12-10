namespace AdventOfCode2024Solutions.Day10
{
    public class Trail(MapPoint head, Map3D map)
    {
        private readonly MapPoint head = head;
        private int score = 0;
        private int rating = 0;
        private Map3D map = map;

        public MapPoint TrailBegin => head;
        public int Score => score;
        public int Rating => rating;

        private List<MapPoint> topsReached = [];

        public int NumberOfWaysToTops => topsReached.Count;

        public void InitializeTrails()
        {
            ReachedNewPosition(head);
            rating = topsReached.Count;
            var uniqueTopsReached = topsReached.Distinct().ToList();
            score = uniqueTopsReached.Count();
        }

        private void ReachedNewPosition(MapPoint currentPosition)
        {
            if (currentPosition.Z == 9)
            {
                topsReached.Add(currentPosition);
                return;
            }

            TryWalkDirection(currentPosition, Direction.North);
            TryWalkDirection(currentPosition, Direction.South);
            TryWalkDirection(currentPosition, Direction.East);
            TryWalkDirection(currentPosition, Direction.West);
        }

        private void TryWalkDirection(MapPoint currentPosition, Direction direction)
        {
            if (currentPosition == null)
                return;

            MapPoint? potentialNewPosition = null;

            switch (direction)
            {
                case Direction.North:
                    potentialNewPosition = map.GetPosition(currentPosition.X, currentPosition.Y - 1);
                    break;
                case Direction.South:
                    potentialNewPosition = map.GetPosition(currentPosition.X, currentPosition.Y + 1);
                    break;
                case Direction.East:
                    potentialNewPosition = map.GetPosition(currentPosition.X + 1, currentPosition.Y);
                    break;
                case Direction.West:
                    potentialNewPosition = map.GetPosition(currentPosition.X - 1, currentPosition.Y);
                    break;
            }

            if (potentialNewPosition == null)
                return;

            if (potentialNewPosition.Z == currentPosition.Z + 1)
                ReachedNewPosition(potentialNewPosition);
        }
    }
}
