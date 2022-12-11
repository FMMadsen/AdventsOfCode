using AdventsOfCode2022.Day1CalorieCounting;

namespace AdventsOfCode2022.Day4CampCleanup
{
    internal class Elf
    {
        internal int ElfId { get; private set; }
        internal CampSectionRange Assignment { get; private set; }
        
        internal Elf(int elfId, CampSectionRange assignment)
        {
            ElfId = elfId;
            Assignment = assignment;
        }
    }
}
