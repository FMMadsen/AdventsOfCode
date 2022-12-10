namespace AdventsOfCode.Day3
{
    internal class JungleJourneyRucksacks
    {
        internal List<Rucksack> Rucksacks { get; private set; }

        internal JungleJourneyRucksacks()
        {
            Rucksacks = new List<Rucksack>();
        }

        internal void LoadRucksacks(string[] datasetLines)
        {
            foreach(var line in datasetLines)
            {
                var rucksack = new Rucksack(line);
                Rucksacks.Add(rucksack);
            }
        }

        internal int FindPrioritySumOfMisplacedItems()
        {
            int prioritySum = 0;
            foreach(var rucksack in Rucksacks)
            {
                var priorities = rucksack.FindMisplacedItemTypePriorities();
                prioritySum += priorities.Sum();
            }

            return prioritySum;
        }
    }
}
