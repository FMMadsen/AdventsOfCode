using System;

namespace AdventsOfCode.Day3
{
    internal class ElfGroup
    {
        internal Rucksack? Elf1 { get; private set; }
        internal Rucksack? Elf2 { get; private set; }
        internal Rucksack? Elf3 { get; private set; }
        internal String Badge { get; private set; }
        internal int BadgePriority { get; private set; }

        internal ElfGroup(Rucksack elf1, Rucksack elf2, Rucksack elf3)
        {
            Elf1 = elf1;
            Elf2 = elf2;
            Elf3 = elf3;
            Badge = string.Empty;
            BadgePriority = 0;

            var elf1Supplies = Elf1?.ItitialSuppliesString.ToCharArray() ?? Enumerable.Empty<char>();
            var elf2Supplies = Elf2?.ItitialSuppliesString.ToCharArray() ?? Enumerable.Empty<char>();
            var elf3Supplies = Elf3?.ItitialSuppliesString.ToCharArray() ?? Enumerable.Empty<char>();

            var potentialBadge = elf1Supplies.Intersect(elf2Supplies);
            var badgeChar = potentialBadge.Intersect(elf3Supplies);
            Badge = badgeChar.FirstOrDefault().ToString();
            var badgePriorityArray = Rucksack.GetPriority(badgeChar.ToArray<char>());
            BadgePriority = badgePriorityArray[0];
        }
    }
}
