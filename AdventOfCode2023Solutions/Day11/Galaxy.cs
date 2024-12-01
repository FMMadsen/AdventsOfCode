
namespace AdventOfCode2023Solutions.Day11
{
    public class Galaxy
    {
        public int ID { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        public long expX { get; set; }
        public long expY { get; set; }


        public Galaxy(int y, int x, int id)
        {
            ID = id;
            X = x;
            Y = y;
        }

        internal void ExpandLocation(int[] columnsNotUsed, int[] rowsNotUsed, long expandingSize)
        {
            var numberOfColumnsExpandingLeftForGalaxy = columnsNotUsed.Count(g => g < X);
            var numberOfRowsExpandingAboveGalaxy = rowsNotUsed.Count(g => g < Y);

            expX = X + ((expandingSize - 1) * numberOfColumnsExpandingLeftForGalaxy);
            expY = Y + ((expandingSize - 1) * numberOfRowsExpandingAboveGalaxy);
        }
    }
}
