using Common;

namespace AdventOfCode2023Solutions.Day05
{
    public class Solution : IAOCSolution
    {
        public string PuzzleName => "Day 5: If You Give A Seed A Fertilizer";

        public string SolvePart1(string[] datasetLines)
        {
            var almanac = new Almanac();
            almanac.Load(datasetLines);
            var plantInstructions = almanac.GetPlantInstructionsPart1();
            var lowestLocationNumber = plantInstructions.Min(i => i.PlantLocation);
            return lowestLocationNumber.ToString();
        }

        public string SolvePart2(string[] datasetLines)
        {
            return "Disabled automatic run. Run takes 45 min";

            //var almanac = new Almanac();
            //almanac.Load(datasetLines);
            //Console.WriteLine($"Found {almanac.CountNumberOfSeedsPart2():#,##0} plant seeds");
            //almanac.ProcessPlantInstructions();
            //var lowestLocationNumber = almanac.GetLowestLocationNumber();
            //return lowestLocationNumber.ToString();
        }

        public string SolvePart2ForUnitTest(string[] datasetLines)
        {
            var almanac = new Almanac();
            almanac.Load(datasetLines);
            Console.WriteLine($"Found {almanac.CountNumberOfSeedsPart2():#,##0} plant seeds");
            almanac.ProcessPlantInstructions();
            var lowestLocationNumber = almanac.GetLowestLocationNumber();
            return lowestLocationNumber.ToString();
        }

    }
}
