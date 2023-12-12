using AdventsOfCode2022.Day04CampCleanup;
using Common;

namespace AdventOfCode2022Solutions.Day04
{
    /// <summary>
    /// Time consumption PART 1: 09:00 - 10:15 = 1h 15m
    /// Time consumption PART 2: 10:15 - 10:30 + 16:15 - 16:47 = 1h
    /// Time consumption TOTAL: 2h 15min
    /// </summary>
    public class Solution(string[] datasetLines) : IAOCSolution
    {
        public string PuzzleName => "Day 4: Camp Cleanup";
        public string[] DatasetLines => datasetLines;
        public bool DoPrintOut => false;


        public string SolvePart1()
        {
            var elfCleaningPairs = CreateElfCleaningPairs(datasetLines);
            var countElvPairsWithFullOverlap = elfCleaningPairs.Count(ep => ep.HasPairFullSectionOverlap);

            if (DoPrintOut)
                PrintSolutionPart1Detail(elfCleaningPairs);

            return countElvPairsWithFullOverlap.ToString();
        }

        public string SolvePart2()
        {
            var elfCleaningPairs = CreateElfCleaningPairs(datasetLines);
            var countElvPairsWithSomeOverlap = elfCleaningPairs.Count(ep => ep.HasPairSomeSectionOverlap);

            if (DoPrintOut)
                PrintSolutionPart2Detail(elfCleaningPairs);

            return countElvPairsWithSomeOverlap.ToString();
        }

        private static List<ElfCleaningPair> CreateElfCleaningPairs(string[] datasetLines)
        {
            var elfCleaningPairs = new List<ElfCleaningPair>();
            var elfId = 0;
            var elfPairId = 0;

            foreach (var line in datasetLines)
            {
                var sectionAssignmentPair = line.Split(",");
                if (sectionAssignmentPair.Length != 2)
                    throw new Exception($"Exception: Day4Puzzle: One line in the input is invalid: {line}");
                var sectionAssignment1 = new CampSectionRange(sectionAssignmentPair[0]);
                var sectionAssignment2 = new CampSectionRange(sectionAssignmentPair[1]);
                var elf1 = new Elf(++elfId, sectionAssignment1);
                var elf2 = new Elf(++elfId, sectionAssignment2);
                var elfCleaningPair = new ElfCleaningPair(++elfPairId, elf1, elf2);
                elfCleaningPairs.Add(elfCleaningPair);
            }

            return elfCleaningPairs;
        }

        private static void PrintSolutionPart1Detail(IEnumerable<ElfCleaningPair> elfCleaningPairs)
        {
            Console.WriteLine("Elf cleaning pairs");

            int pairCounter = 0;
            foreach (var elfCleaningPair in elfCleaningPairs)
            {
                Console.WriteLine($"Pair {(++pairCounter).ToString().PadLeft(3, '0')} | Full overlap:{(elfCleaningPair.HasPairFullSectionOverlap ? 1 : 0)} | Some overlap:{(elfCleaningPair.HasPairSomeSectionOverlap ? 1 : 0)}");
                Console.WriteLine($"1st elf assigned to sections: {elfCleaningPair.Elf1.Assignment.RangeFrom}-{elfCleaningPair.Elf1.Assignment.RangeTo} ({elfCleaningPair.Elf1.Assignment.NoOfSectionsAssignedTo} sections)");
                Console.WriteLine($"2nd elf assigned to sections: {elfCleaningPair.Elf2.Assignment.RangeFrom}-{elfCleaningPair.Elf2.Assignment.RangeTo} ({elfCleaningPair.Elf2.Assignment.NoOfSectionsAssignedTo} sections)");
                Console.WriteLine("");
            }
        }

        private static void PrintSolutionPart2Detail(IEnumerable<ElfCleaningPair> elfCleaningPairs)
        {
            Console.WriteLine("Elf cleaning pairs");

            int pairCounter = 0;
            foreach (var elfCleaningPair in elfCleaningPairs)
            {
                Console.WriteLine($"Pair {(++pairCounter).ToString().PadLeft(3, '0')} | Full overlap:{(elfCleaningPair.HasPairFullSectionOverlap ? 1 : 0)} | Some overlap:{(elfCleaningPair.HasPairSomeSectionOverlap ? 1 : 0)}");
            }
        }
    }
}
