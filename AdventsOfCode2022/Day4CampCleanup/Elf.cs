namespace AdventsOfCode2022.Day4CampCleanup
{
    internal class Elf
    {
        internal CampSectionRange Assignment { get; private set; }
        
        internal Elf(CampSectionRange assignment)
        {
            Assignment = assignment;
        }
    }
}
