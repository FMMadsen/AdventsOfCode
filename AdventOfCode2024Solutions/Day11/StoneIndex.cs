namespace AdventOfCode2024Solutions.Day11
{
    public class StoneIndex
    {
        private Dictionary<long, Stone> index;

        public StoneIndex()
        {
            index = new Dictionary<long, Stone>();
        }

        internal Stone? Get(long number)
        {
            Stone? stone = null;
            index.TryGetValue(number, out stone);
            return stone;
        }

        internal void Add(Stone stone)
        {
            index.Add(stone.Number, stone);
        }
    }
}
