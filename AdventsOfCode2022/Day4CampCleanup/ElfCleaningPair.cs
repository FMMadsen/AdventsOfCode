namespace AdventsOfCode2022.Day4CampCleanup
{
    internal class ElfCleaningPair
    {
        internal Elf Elf1 { get; private set; }
        internal Elf Elf2 { get; private set; }
        internal int ElfWithFullSectionOverlap { get; private set; }

        internal ElfCleaningPair(Elf elf1, Elf elf2)
        {
            Elf1 = elf1;
            Elf2 = elf2;
            ElfWithFullSectionOverlap = HasFullSectionOverlap();
        }

        private int HasFullSectionOverlap()
        {
            var elf1NoSections = Elf1.Assignment.NoOfSectionsAssignedTo;
            var elf1From = Elf1.Assignment.RangeFrom;
            var elf1To = Elf1.Assignment.RangeTo;
            var elf2NoSections = Elf2.Assignment.NoOfSectionsAssignedTo;
            var elf2From = Elf2.Assignment.RangeFrom;
            var elf2To = Elf2.Assignment.RangeTo;

            if(elf1NoSections == elf2NoSections)
            {
                if (elf1From == elf2From)
                    return 12;
            }

            if(elf1NoSections < elf2NoSections)
            {
                if(elf1From >= elf2From && elf1To <= elf2To)
                    return 1;
            }

            if (elf1NoSections > elf2NoSections)
            {
                if (elf2From >= elf1From && elf2To <= elf1To)
                    return 2;
            }

            return 0;
        }
    }
}
