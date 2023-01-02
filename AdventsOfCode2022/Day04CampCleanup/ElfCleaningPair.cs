namespace AdventsOfCode2022.Day04CampCleanup
{
    internal class ElfCleaningPair
    {
        internal int ElfPairId { get; private set; }
        internal Elf Elf1 { get; private set; }
        internal Elf Elf2 { get; private set; }
        internal bool HasPairSomeSectionOverlap { get; private set; }
        internal bool HasPairFullSectionOverlap { get; private set; }

        internal ElfCleaningPair(int elfPairId, Elf elf1, Elf elf2)
        {
            ElfPairId = elfPairId;
            Elf1 = elf1;
            Elf2 = elf2;
            HasPairSomeSectionOverlap = CalculateIfSomeSectionOverlap();
            HasPairFullSectionOverlap = CalculateFullSectionOverlap();
        }

        private bool CalculateIfSomeSectionOverlap()
        {
            var elf1From = Elf1.Assignment.RangeFrom;
            var elf1To = Elf1.Assignment.RangeTo;
            var elf2From = Elf2.Assignment.RangeFrom;
            var elf2To = Elf2.Assignment.RangeTo;

            if (elf1To < elf2From)
                return false;

            if (elf2To < elf1From)
                return false;

            return true;
        }

        private bool CalculateFullSectionOverlap()
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
                    return true;
            }

            if(elf1NoSections < elf2NoSections)
            {
                if (elf1From >= elf2From && elf1To <= elf2To)
                    return true;
            }

            if (elf1NoSections > elf2NoSections)
            {
                if (elf2From >= elf1From && elf2To <= elf1To)
                    return true;
            }

            return false;
        }
    }
}
