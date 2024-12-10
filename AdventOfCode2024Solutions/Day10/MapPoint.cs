namespace AdventOfCode2024Solutions.Day10
{
    public class MapPoint
    {
        public MapPoint(int x, int y, char z)
        {
            if (!int.TryParse(z.ToString(), out int zInt))
                zInt = -1;

            Initialize(x, y, zInt);
        }

        public MapPoint(int x, int y, int z)
        {
            Initialize(x, y, z);
        }

        private void Initialize(int x, int y, int z)
        {
            X = x; Y = y; Z = z;
        }

        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }
    }
}
