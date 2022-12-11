namespace AdventsOfCode2022.Day4CampCleanup
{
    /// <summary>
    /// Time consumption PART 1: 09:00 - 10:15: 1h 15m
    /// Time consumption PART 2: 
    /// Time consumption TOTAL: 
    /// </summary>
    internal class Day4Puzzle
    {
        internal static int SolvePart1(string[] datasetLines, bool doPrintOut)
        {
            var elfCleaningPairs = CreateElfCleaningPairs(datasetLines);

            if (doPrintOut)
                PrintSolutionDetail(elfCleaningPairs);

            var countElvesWithOverlap = elfCleaningPairs.Count(cp => cp.ElfWithFullSectionOverlap != 0);

            return countElvesWithOverlap;
        }

        internal static int SolvePart2(string[] datasetLines, bool doPrintOut)
        {
            var elfCleaningPairs = new List<ElfCleaningPair>();

            if (doPrintOut)
                PrintSolutionDetail(elfCleaningPairs);

            return 0;
        }

        private static List<ElfCleaningPair> CreateElfCleaningPairs(string[] datasetLines)
        {
            var elfCleaningPairs = new List<ElfCleaningPair>();

            foreach (var line in datasetLines)
            {
                var sectionAssignmentPair = line.Split(",");
                if (sectionAssignmentPair.Length != 2)
                    throw new Exception($"Exception: Day4Puzzle: One line in the input is invalid: {line}");
                var sectionAssignment1 = new CampSectionRange(sectionAssignmentPair[0]);
                var sectionAssignment2 = new CampSectionRange(sectionAssignmentPair[1]);
                var elf1 = new Elf(sectionAssignment1);
                var elf2 = new Elf(sectionAssignment2);
                var elfCleaningPair = new ElfCleaningPair(elf1, elf2);
                elfCleaningPairs.Add(elfCleaningPair);
            }

            return elfCleaningPairs;
        }

        private static void PrintSolutionDetail(List<ElfCleaningPair> elfCleaningPairs)
        {
            Console.WriteLine("Elf cleaning pairs");

            int pairCounter = 0;
            foreach(var elfCleaningPair in elfCleaningPairs)
            {
                Console.WriteLine($"Pair {++pairCounter}:  (Overlap:{elfCleaningPair.ElfWithFullSectionOverlap})");
                Console.WriteLine($"1st elf assigned to sections: {elfCleaningPair.Elf1.Assignment.RangeFrom}-{elfCleaningPair.Elf1.Assignment.RangeTo} ({elfCleaningPair.Elf1.Assignment.NoOfSectionsAssignedTo} sections)");
                Console.WriteLine($"2nd elf assigned to sections: {elfCleaningPair.Elf2.Assignment.RangeFrom}-{elfCleaningPair.Elf2.Assignment.RangeTo} ({elfCleaningPair.Elf2.Assignment.NoOfSectionsAssignedTo} sections)");
            }
        }
    }
}
