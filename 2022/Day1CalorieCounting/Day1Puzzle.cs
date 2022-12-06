namespace Day1CalorieCounting
{
    class Day1Puzzle
    {
        public static int Solve(string dataset)
        {
            var inventory = ReadInventoryList(dataset);
            var elvesCalorySumList = ConvertToElvesInventorySums(inventory);
            //printElvesList(elvesCalorySumList);
            var higestNumber = IdentifyHigestCaloryContent(elvesCalorySumList);
            return higestNumber;
        }

        private static int IdentifyHigestCaloryContent(int[] elvesList)
        {
            var maxCaloryContent = elvesList.Max();
            return maxCaloryContent;
        }

        private static void printElvesList(int[] elvesList)
        {
            int elvNr = 1;
            foreach (int elfCalorySum in elvesList)
            {
                Console.WriteLine("" + elvNr++ + ": " + elfCalorySum);
            }
        }

        private static int[] ConvertToElvesInventorySums(string[] inventoryList)
        {
            List<int> elvesList = new List<int>();

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

            return elvesList.ToArray();
        }

        private static string[] ReadInventoryList(string filecontent)
        {
            string[] lines = filecontent.Split(
                new string[] { Environment.NewLine },
                StringSplitOptions.None
            );
            return lines;
        }
    }
}
