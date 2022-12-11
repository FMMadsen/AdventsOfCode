using AdventsOfCode2022.Day1CalorieCounting;

namespace AdventsOfCode2022.Day1CalorieCounting
{
    internal class ElvesParty
    {
        public IList<Elf> Elves { get; private set; }
        public int NoOfElvesInPack => Elves.Count;
        public Elf? Top1Carrying { get; private set; }
        public Elf? Top2Carrying { get; private set; }
        public Elf? Top3Carrying { get; private set; }

        public ElvesParty()
        {
            Elves = new List<Elf>();
            Top1Carrying = null;
            Top2Carrying = null;
            Top3Carrying = null;
    }

        public void Add(Elf elf)
        {
            Elves.Add(elf);
            AdjustLeaderboard(elf);
        }

        private void AdjustLeaderboard(Elf newElf)
        {
            if (Top1Carrying == null || Top1Carrying.SumCalories < newElf.SumCalories)
            {
                AddElfOn1stPlace(newElf);
                return;
            }
            if (Top2Carrying == null || Top2Carrying.SumCalories < newElf.SumCalories)
            {
                AddElfOn2ndPlace(newElf);
                return;
            }
            if (Top3Carrying == null || Top3Carrying.SumCalories < newElf.SumCalories)
            {
                AddElfOn3rdPlace(newElf);
                return;
            }
        }

        private void AddElfOn1stPlace(Elf newElf)
        {
            Top3Carrying = Top2Carrying;
            Top2Carrying = Top1Carrying;
            Top1Carrying = newElf;
        }

        private void AddElfOn2ndPlace(Elf newElf)
        {
            Top3Carrying = Top2Carrying;
            Top2Carrying = newElf;
        }

        private void AddElfOn3rdPlace(Elf newElf)
        {
            Top3Carrying = newElf;
        }

        public int GetSumOfMostCarriedCalories()
        {
            return Top1Carrying?.SumCalories ?? 0;
        }

        public int GetSumOfTop3MostCarriedCalories()
        {
            return (Top1Carrying?.SumCalories ?? 0) + (Top2Carrying?.SumCalories ?? 0) + (Top3Carrying?.SumCalories ?? 0);
        }
    }
}
