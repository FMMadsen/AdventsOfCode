

namespace AdventOfCode2023Solutions.Day05
{
    public class AlmanacItem
    {
        public UInt32 Destination;
        public UInt32 Source;
        public UInt32 Range;

        public AlmanacItem()
        {

        }

        public AlmanacItem(UInt32[] dataset)
        {
            Destination = dataset[0];
            Source = dataset[1];
            Range = dataset[2];
        }
    }
}
