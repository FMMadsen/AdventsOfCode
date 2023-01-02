namespace AdventsOfCode2022.Day03Rucksack
{
    /// <summary>
    /// Time consumption PART 1: 1t 45m
    /// Time consumption PART 2: 1t 15m
    /// Time consumption TOTAL: 3 timer
    /// </summary>
    internal class Day3Puzzle
    {
        internal static string SolvePart1(string[] datasetLines, bool doPrintOut)
        {
            //if (doPrintOut)
            //    PrintInputData(datasetLines);

            var jungleJourney = new JungleJourneyRucksacks();
            jungleJourney.LoadRucksacks(datasetLines);

            if(doPrintOut)
                PrintJungleJourneyRucksacks(jungleJourney);

            return jungleJourney.FindPrioritySumOfMisplacedItems().ToString();
        }

        internal static string SolvePart2(string[] datasetLines, bool doPrintOut)
        {
            List<ElfGroup> elfGroups = new List<ElfGroup>();

            int elfNumber = 1;
            for(int i = 0; i<datasetLines.Length; i++)
            {
                if (elfNumber == 3)
                {
                    var elf1 = new Rucksack(datasetLines[i - 2]);
                    var elf2 = new Rucksack(datasetLines[i - 1]);
                    var elf3 = new Rucksack(datasetLines[i]);
                    ElfGroup elfGroup = new ElfGroup(elf1, elf2, elf3);
                    elfGroups.Add(elfGroup);
                    elfNumber = 1;
                }
                else
                {
                    elfNumber++;
                }
            }

            if (doPrintOut)
                PrintElfGroups(elfGroups);

            var sumBadgePriority = 0;
            foreach (var group in elfGroups)
                sumBadgePriority += group.BadgePriority;

            return sumBadgePriority.ToString();
        }

        private static void PrintInputData(string[] inputData)
        {
            Console.WriteLine("Raw supply list for jungle journey:");
            foreach (var line in inputData)
            {
                Console.WriteLine(line);
            }
        }

        private static void PrintJungleJourneyRucksacks(JungleJourneyRucksacks jungleJourney)
        {
            Console.WriteLine("Supply list pr. rycksack:");
            var i = 0;
            foreach(var rucksack in jungleJourney.Rucksacks)
            {
                Console.WriteLine($"#{++i}: {rucksack.Compartment1Content} | {rucksack.Compartment2Content}");
                foreach(var priority in rucksack.Compartment1ContentPriority)
                {
                    Console.Write($"{priority} ");
                }
                Console.Write($" | ");
                foreach (var priority in rucksack.Compartment2ContentPriority)
                {
                    Console.Write($"{priority} ");
                }
                Console.WriteLine(" ");

                var misplacedItemTypes = rucksack.FindMisplacedItemTypes();
                var misplacedItemTypesPriorities = rucksack.FindMisplacedItemTypePriorities();

                Console.Write("Misplaced types: ");
                foreach (var type in misplacedItemTypes)
                {
                    Console.Write(type + " ");
                }
                Console.Write("Priorities: ");
                foreach (var priority in misplacedItemTypesPriorities)
                {
                    Console.Write(priority + " ");
                }
                Console.WriteLine(" ");
            }
        }

        private static void PrintElfGroups(List<ElfGroup> elfGroups)
        {
            int groupNumber = 1;

            foreach(var group in elfGroups)
            {
                Console.Write($"Group {groupNumber++}: ");
                Console.Write($"{group.Elf1?.ItitialSuppliesString} | {group.Elf2?.ItitialSuppliesString} | {group.Elf3?.ItitialSuppliesString}");

                Console.WriteLine(" ");
                Console.WriteLine($"Badge: {group.Badge}. Priority: {group.BadgePriority}");
            }
        }
    }
}
