namespace AdventsOfCode2022.Day1CalorieCounting
{
    internal class Elf
    {
        public int ElfNumber { get; private set; }
        public int SumCalories { get; private set; }
        public int[] InventoryCaloryArray { get; private set; }

        public Elf(int elfNo, string[] inventoryItems)
        {
            ElfNumber = elfNo;
            InventoryCaloryArray = CreateCaloryArray(inventoryItems);
            SumCalories = InventoryCaloryArray.Sum();
        }

        private static int[] CreateCaloryArray(string[] inventoryItems)
        {
            return Array.ConvertAll(inventoryItems, i => int.Parse(i));
        }
    }
}
