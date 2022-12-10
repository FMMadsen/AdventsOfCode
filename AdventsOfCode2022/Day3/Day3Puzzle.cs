namespace AdventsOfCode.Day3
{
    /// <summary>
    /// Time consumption: 22:30 - 22:45
    /// Time consumption: 23:15 - 23:30
    /// </summary>
    internal class Day3Puzzle
    {
        internal static int SolvePart1(string[] datasetLines, bool doPrintOut)
        {
            //if (doPrintOut)
            //    PrintInputData(datasetLines);

            var jungleJourney = new JungleJourneyRucksacks();
            jungleJourney.LoadRucksacks(datasetLines);

            if(doPrintOut)
                PrintJungleJourneyRucksacks(jungleJourney);

            return jungleJourney.FindPrioritySumOfMisplacedItems(); ;
        }

        internal static int SolvePart2(string[] datasetLines, bool doPrintOut)
        {
            return 0;
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
    }
}
