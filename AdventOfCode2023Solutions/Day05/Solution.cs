using Common;

namespace AdventOfCode2023Solutions.Day05
{
    public class Solution(string[] datasetLines) : IAOCSolution
    {
        public string PuzzleName => "Day 5: If You Give A Seed A Fertilizer";
        public string[] DatasetLines => datasetLines;

        public string SolvePart1()
        {
            var almanac = new Almanac();
            almanac.Load(datasetLines);
            var plantInstructions = almanac.GetPlantInstructionsPart1();
            var lowestLocationNumber = plantInstructions.Min(i => i.PlantLocation);
            return lowestLocationNumber.ToString();
        }

        public string SolvePart2()
        {
            return "Disabled: Do not run, it will take 45 min";

            //var almanac = new Almanac();
            //almanac.Load(datasetLines);
            //Console.WriteLine($"Found {almanac.CountNumberOfSeedsPart2():#,##0} plant seeds");
            //almanac.ProcessPlantInstructions();
            //var lowestLocationNumber = almanac.GetLowestLocationNumber();
            //return lowestLocationNumber.ToString();
        }

        public string SolvePart2ForUnitTest()
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
