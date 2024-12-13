namespace AdventOfCode2024Solutions.Day11
{
    public class Pluto
    {
        private Dictionary<long, long> stoneCounter;
        private readonly Dictionary<long, long[]> stoneDevelopmentIndex;

        public long TotalNumberOfStones => stoneCounter.Sum(x => x.Value);

        public Pluto(string input)
        {
            stoneCounter = [];
            stoneDevelopmentIndex = [];

            var initialStoneNumbers = input.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(x => long.Parse(x)).ToList();

            foreach (var stoneNumber in initialStoneNumbers)
                AddStone(stoneCounter, stoneNumber, 1);
        }

        public void Blink(long blinkTimes)
        {
            for (int i = 0; i < blinkTimes; i++)
                Blink();
        }

        public void Blink()
        {
            Dictionary<long, long> newStoneCounter = [];
            foreach (var stoneCount in stoneCounter)
            {
                if (stoneCount.Value == 0)
                    continue;

                if (!stoneDevelopmentIndex.ContainsKey(stoneCount.Key))
                {
                    stoneDevelopmentIndex.Add(stoneCount.Key, ChangeRules.Change(stoneCount.Key));
                }
                var newStones = stoneDevelopmentIndex[stoneCount.Key];

                AddStone(newStoneCounter, newStones[0], stoneCount.Value);
                if (newStones.Length == 2)
                    AddStone(newStoneCounter, newStones[1], stoneCount.Value);
            }
            stoneCounter = newStoneCounter;
        }

        private void AddStone(Dictionary<long, long> stoneCounter, long stoneValue, long stoneCount)
        {
            if (stoneCounter.ContainsKey(stoneValue))
                stoneCounter[stoneValue] += stoneCount;
            else
                stoneCounter.Add(stoneValue, stoneCount);
        }
    }
}
