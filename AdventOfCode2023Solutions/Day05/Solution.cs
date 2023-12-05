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
            var almanac = new Almanac();
            almanac.Load(datasetLines);
            var plantInstructions = almanac.GetPlantInstructionsPart2();
            var lowestLocationNumber = plantInstructions.Min(i => i.PlantLocation);
            return lowestLocationNumber.ToString();
        }
    }
}
