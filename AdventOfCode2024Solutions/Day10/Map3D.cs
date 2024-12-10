namespace AdventOfCode2024Solutions.Day10
{
    public class Map3D
    {
        private int[,] mapInts;
        private MapPoint[,] map;
        private List<MapPoint> heads = [];
        private List<MapPoint> tops = [];
        private readonly int xMax;
        private readonly int yMax;

        public int NumberOfHeads => heads.Count;
        public int NumberOfTops => tops.Count;
        public int XMax => xMax;
        public int YMax => yMax;

        public Map3D(string[] inputMapLines)
        {
            heads = new List<MapPoint>();
            tops = new List<MapPoint>();

            var xLength = inputMapLines[0].Length;
            var yLength = inputMapLines.Length;

            mapInts = new int[xLength, yLength];
            map = new MapPoint[xLength, yLength];

            this.xMax = xLength - 1;
            this.yMax = yLength - 1;

            var y = 0;
            foreach (var line in inputMapLines)
            {
                for (int x = 0; x < xLength; x++)
                {
                    var mapPoint = new MapPoint(x, y, line[x]);
                    map[x, y] = mapPoint;
                    mapInts[x, y] = mapPoint.Z;

                    if (mapPoint.Z == 0)
                        heads.Add(mapPoint);
                    if (mapPoint.Z == 9)
                        tops.Add(mapPoint);
                }
                y++;
            }
        }

        public MapPoint? GetPosition(int x, int y)
        {
            if (x < 0 || y < 0 || x > xMax || y > yMax)
                return null;

            return map[x, y];
        }

        public List<Trail> IdentifyTrails()
        {
            List<Trail> trails = [];
            foreach (var head in heads)
            {
                trails.Add(new Trail(head, this));
            }
            return trails;
        }
    }
}
