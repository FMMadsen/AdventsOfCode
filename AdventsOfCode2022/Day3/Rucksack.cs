using System.Text;

namespace AdventsOfCode.Day3
{
    internal class Rucksack
    {
        internal string ItitialSuppliesString { get; private set; }
        internal string Compartment1Content { get; private set; }
        internal string Compartment2Content { get; private set; }
        internal int[] Compartment1ContentPriority { get; private set; }
        internal int[] Compartment2ContentPriority { get; private set; }
        internal int NumberOfItemsPerCompartment { get; private set; }

        internal Rucksack(string suppliesString)
        {
            ItitialSuppliesString = suppliesString;

            NumberOfItemsPerCompartment = suppliesString.Length / 2;

            Compartment1Content = ItitialSuppliesString.Substring(0, NumberOfItemsPerCompartment);
            Compartment2Content = ItitialSuppliesString.Substring(NumberOfItemsPerCompartment, NumberOfItemsPerCompartment);

            Compartment1ContentPriority = GetPriority(Compartment1Content);
            Compartment2ContentPriority = GetPriority(Compartment2Content);
        }

        internal char[] FindMisplacedItemTypes()
        {
            var comartment1Items = Compartment1Content.ToCharArray();
            var comartment2Items = Compartment2Content.ToCharArray();
            var misplacedItems = comartment1Items.Intersect(comartment2Items);
            return misplacedItems.ToArray<char>();
        }

        internal IEnumerable<int> FindMisplacedItemTypePriorities()
        {
            var itemArray = FindMisplacedItemTypes();
            var itemPriorities = GetPriority(itemArray);
            return itemPriorities;
        }

        private int[] GetPriority(string itemList)
        {
            return GetPriority(itemList.ToCharArray());
        }

        /// <summary>
        /// Lowercase item types a through z have priorities 1 through 26.
        /// Uppercase item types A through Z have priorities 27 through 52.
        /// </summary>
        /// <param name="item">item character</param>
        /// <returns>priorit integer</returns>
        private int[] GetPriority(char[] itemArray)
        {
            byte[] asciiArray = Encoding.ASCII.GetBytes(itemArray);
            var priorityList = Array.ConvertAll(asciiArray, (byte i) => { return (int)(i > 96 ? i - 96 : i - 38); });
            return priorityList;
        }

    }
}