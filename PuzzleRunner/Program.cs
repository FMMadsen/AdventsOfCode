using Common;

namespace PuzzleRunner
{
    internal class Program
    {
        private const string DATASET_SOURCE_FOLDER_NAME = "AdventsOfCode";

        static void Main(string[] args)
        {
            RunAdventOfCode2022();
        }

        private static void RunAdventOfCode2022()
        {
            var year = 2022;
            Console.WriteLine($"Advent of Code year {year}");
            var repo = new DataSetRepository(DATASET_SOURCE_FOLDER_NAME, year);
            string[] dataSet;

            dataSet = repo.GetDataSet(day: 1);
            RunPuzzle(new AdventOfCode2022Solutions.Day1.Solution(dataSet));
            
            //RunPuzzle(new AdventOfCode2022Solutions.Day2.Solution(repo.GetDataSet(day: 2)));
        }

        private static void RunAdventOfCode2023()
        {
            //Console.WriteLine($"Running Advent of Code year {year}");
            //var repo = new DataSetRepository(year);

            //RunPuzzle(2023);
        }

        private static void RunPuzzle(IAOCSolution puzzleSolution)
        {
            Console.WriteLine($"{puzzleSolution.PuzzleName}");
            System.Diagnostics.Stopwatch stopWatch = new();
            
            stopWatch.Start();
            var resultPart1 = puzzleSolution.SolvePart1();
            Console.WriteLine($"Answer to part 1: {resultPart1}");

            var resultPart2 = puzzleSolution.SolvePart2();
            Console.WriteLine($"Answer to part 2: {resultPart2}");
            stopWatch.Stop();

            Console.WriteLine($"Time elapsed: {stopWatch.ElapsedMilliseconds} ms");

            Console.WriteLine("-------------------------------------------");
        }
    }
}
