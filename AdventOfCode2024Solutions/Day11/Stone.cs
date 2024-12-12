namespace AdventOfCode2024Solutions.Day11
{
    public class Stone
    {
        public long Number { get; private set; }
        public long CurrentNumberOfThisStone { get; private set; }
        public Stone[]? DevelopTo { get; private set; }

        public Stone(long number, long numberOfStones)
        {
            CurrentNumberOfThisStone = numberOfStones;
            DevelopTo = null;
            Number = number;
        }

        public void Blink(StoneIndex stoneIndex, List<Stone> newStonesList)
        {
            if (DevelopTo == null)
            {
                var newNumbers = ChangeRules.Change(Number);
                var numberOfNumbers = newNumbers.Length;
                DevelopTo = new Stone[numberOfNumbers];

                for (int i = 0; i < newNumbers.Length; i++)
                {
                    var stoneNumber = newNumbers[i];
                    var stone = stoneIndex.Get(stoneNumber);
                    if (stone == null)
                    {
                        stone = new Stone(stoneNumber, CurrentNumberOfThisStone);
                        stoneIndex.Add(stone);
                    }
                    else
                    {
                        stone.IncreaseNumberOfThisStone(CurrentNumberOfThisStone);
                    }
                    DevelopTo[i] = stone;
                }
            }
            else
            {
                DevelopTo[0].IncreaseNumberOfThisStone(CurrentNumberOfThisStone);
                if (DevelopTo.Length == 2)
                    DevelopTo[1].IncreaseNumberOfThisStone(CurrentNumberOfThisStone);
            }
            CurrentNumberOfThisStone = 0;
            newStonesList.AddRange(DevelopTo);
        }

        public void IncreaseNumberOfThisStone(long number)
        {
            CurrentNumberOfThisStone += number;
        }
    }
}
