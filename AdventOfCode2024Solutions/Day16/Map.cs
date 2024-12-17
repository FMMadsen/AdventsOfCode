namespace AdventOfCode2024Solutions.Day16
{
    public class Map
    {
        public char[,] MapTiles { get; private set; }
        public int NoOfYTiles { get; private set; }
        public int NoOfXTiles { get; private set; }
        public Position StartLocation { get; set; }
        public Position EndLocation { get; set; }

        public Map(string[] datasetLines)
        {
            MapTiles = CreateMap(datasetLines, out Position startLocation, out Position endLocation, out int noOfYTiles, out int noOfXTiles);
            StartLocation = startLocation;
            EndLocation = endLocation;
            NoOfYTiles = noOfYTiles;
            NoOfXTiles = noOfXTiles;
        }

        public long StartRace()
        {
            var rudolf = new Raindeer(this);
            rudolf.StartTraverse(Direction.East);
            return rudolf.LowestScore;
        }

        private static char[,] CreateMap(
            string[] datasetLines,
            out Position startLocation,
            out Position endLocation,
            out int noOfYTiles,
            out int noOfXTiles)
        {
            noOfYTiles = datasetLines.Length;
            noOfXTiles = datasetLines[0].Length;
            var map = new Char[noOfYTiles, noOfXTiles];
            int? startX = null, startY = null, endX = null, endY = null;

            for (int y = 0; y < noOfYTiles; y++)
            {
                for (int x = 0; x < noOfXTiles; x++)
                {
                    map[y, x] = datasetLines[y][x];
                    if (map[y, x] == 'S')
                    {
                        startX = x;
                        startY = y;
                    }
                    if (map[y, x] == 'E')
                    {
                        endX = x;
                        endY = y;
                    }
                }
            }

            if (startX == null || startY == null)
                throw new Exception("start location not found!");
            startLocation = new Position(startX.Value, startY.Value);

            if (endX == null || endY == null)
                throw new Exception("end location not found!");
            endLocation = new Position(endX.Value, endY.Value);

            return map;
        }
    }
}
