namespace AdventsOfCode2022.Day1CalorieCounting
{
    /// <summary>
    /// Time consumption: 3 hours in total
    /// </summary>
    internal class Day1Puzzle
    {
        public static int SolvePart1(string[] datasetLines, bool doPrintOut)
        {
            //var inventory = ReadInventoryList(dataset);
            var elvesParty = CreateElvesPartyOverview(datasetLines);
            var sumOfMostCarriedCalories = elvesParty.GetSumOfMostCarriedCalories();

            if (doPrintOut)
                PrintElvesList(elvesParty);

            return sumOfMostCarriedCalories;
        }

        public static int SolvePart2(string[] datasetLines, bool doPrintOut)
        {
            //var inventory = ReadInventoryList(dataset);
            var elvesParty = CreateElvesPartyOverview(datasetLines);
            var totalOfTopTreeSumCaloriesCarried = elvesParty.GetSumOfTop3MostCarriedCalories();

            if(doPrintOut)
                PrintElvesList(elvesParty);

            return totalOfTopTreeSumCaloriesCarried;
        }

        private static ElvesParty CreateElvesPartyOverview(string[] inventoryList)
        {
            var elvesParty = new ElvesParty();
            int elfNo = 1;

            int startIndex = 0, endIndex, noOfInventoryItems;
            string[] elfInventoryItems;

            while(startIndex < inventoryList.Length)
            {
                endIndex = Array.FindIndex(inventoryList, startIndex, item => string.IsNullOrWhiteSpace(item));
                if (endIndex == -1)
                    endIndex = inventoryList.Length;

                noOfInventoryItems = endIndex - startIndex;
                elfInventoryItems = inventoryList[startIndex..endIndex];
                elvesParty.Add(new Elf(elfNo, elfInventoryItems));

                startIndex = endIndex + 1;
            }

            return elvesParty;
        }

        private static void PrintElvesList(ElvesParty elvesParty)
        {
            Console.WriteLine("List of calorysums for elves - Part 2");
            foreach(var elf in elvesParty.Elves)
            {
                Console.WriteLine($"{(elf.ElfNumber).ToString().PadLeft(3, '0')}: {elf.SumCalories:N0}");
            }
        }

        private static IEnumerable<int> ConvertToElvesInventorySums(string[] inventoryList)
        {
            var elvesList = new List<int>();

            int calory = 0;
            int calorySum = 0;
            foreach (string inventoryItem in inventoryList)
            {
                if (int.TryParse(inventoryItem, out calory))
                {
                    calorySum += calory;
                }
                else
                {
                    elvesList.Add(calorySum);
                    calory = 0;
                    calorySum = 0;
                }
            }
            
            return elvesList;
        }

        private static string[] ReadInventoryList(string filecontent)
        {
            string[] lines = filecontent.Split(
                new string[] { Environment.NewLine },
                StringSplitOptions.None
            );
            return lines;
        }

        private static void PrintFullInventoryList(string[] inventoryList)
        {
            int index = 0;
            foreach(var inventoryItem in inventoryList)
            {
                Console.WriteLine($"{index++}: {inventoryItem}");
            }

        }
    }
}
