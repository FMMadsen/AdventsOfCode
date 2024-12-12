namespace AdventOfCode2024Solutions.Day11
{
    public class Pluto
    {
        private List<Stone> currentDistinctStones;
        private long[] currentDistinctNumbers;

        public long TotalNumberOfStones => currentDistinctStones.Sum(x => x.CurrentNumberOfThisStone);
        public StoneIndex StoneIndex { get; private set; }

        public Pluto(string input)
        {
            var initialStoneNumbers = input.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(x => long.Parse(x)).ToList();
            StoneIndex = new();

            var initialStones = new List<Stone>();
            foreach (var stoneNumber in initialStoneNumbers)
            {
                var stone = StoneIndex.Get(stoneNumber);
                if (stone == null)
                {
                    stone = new Stone(stoneNumber, 1);
                    StoneIndex.Add(stone);
                }
                else
                {
                    stone.IncreaseNumberOfThisStone(1);
                }
                initialStones.Add(stone);
            }

            currentDistinctStones = initialStones.Distinct().ToList();
            currentDistinctNumbers = currentDistinctStones.Select(x => x.Number).ToArray();
        }

        public void Blink(int iterations)
        {
            for (int i = 0; i < iterations; i++)
            {
                Blink();
            }
        }

        public void Blink()
        {
            List<Stone> newCurrentStones = new List<Stone>();

            foreach (var stone in currentDistinctStones)
            {
                stone.Blink(StoneIndex, newCurrentStones);
            }

            currentDistinctStones = newCurrentStones.Distinct().ToList();
            currentDistinctNumbers = currentDistinctStones.Select(x => x.Number).ToArray();
        }
    }
}
