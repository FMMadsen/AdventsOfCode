using Common;

namespace AdventOfCode2023Solutions.Day05
{
    public class Solution : IAOCSolution
    {
        public string PuzzleName => "Day 5: If You Give A Seed A Fertilizer";

        public string SolvePart1(string[] datasetLines)
        {
            return SolvePart1Version3(datasetLines);
        }

        public string SolvePart2(string[] datasetLines)
        {
            return SolvePart2Version3(datasetLines);
        }

        public static string SolvePart1Version1(string[] datasetLines)
        {
            var almanac = new Almanac();
            almanac.Load(datasetLines);
            almanac.GetLowestLocationNumberPart1Version1();
            return almanac.LowestLocationNumber.ToString();
        }

        public static string SolvePart1Version2(string[] datasetLines)
        {
            var almanac = new Almanac();
            almanac.Load(datasetLines);
            almanac.CreateSeedToLocationMap();
            almanac.GetLowestLocationNumberPart1Version2();
            return almanac.LowestLocationNumber.ToString();
        }

        public static string SolvePart1Version3(string[] datasetLines)
        {
            var almanac = new Almanac();
            almanac.Load(datasetLines);
            almanac.CreateSeedToLocationMap();
            almanac.GetLowestLocationNumberPart1Version3();
            return almanac.LowestLocationNumber.ToString();
        }

        public static string SolvePart2Version1Runtime45Min(string[] datasetLines)
        {
            var almanac = new Almanac();
            almanac.Load(datasetLines);
            almanac.Part2CreateSeedRanges();
            Console.WriteLine($"Found {almanac.CountNumberOfSeedsPart2():#,##0} plant seeds");
            almanac.GetLowestLocationNumberPart2Version1();
            var lowestLocationNumber = almanac.LowestLocationNumber;
            return lowestLocationNumber.ToString();
        }

        public static string SolvePart2Version2(string[] datasetLines)
        {
            var almanac = new Almanac();
            almanac.Load(datasetLines);
            almanac.Part2CreateSeedRanges();
            Console.WriteLine($"Found {almanac.CountNumberOfSeedsPart2():#,##0} plant seeds");
            almanac.CreateSeedToLocationMap();
            almanac.GetLowestLocationNumberPart2Version2();
            var lowestLocationNumber = almanac.LowestLocationNumber;
            return lowestLocationNumber.ToString();
        }

        public static string SolvePart2Version3(string[] datasetLines)
        {
            var almanac = new Almanac();
            almanac.Load(datasetLines);
            almanac.Part2CreateSeedRanges();
            //Console.WriteLine($"Found {almanac.CountNumberOfSeedsPart2():#,##0} plant seeds");
            almanac.CreateSeedToLocationMap();
            almanac.GetLowestLocationNumberPart2Version3();
            var lowestLocationNumber = almanac.LowestLocationNumber;
            return lowestLocationNumber.ToString();
        }
    }
}
