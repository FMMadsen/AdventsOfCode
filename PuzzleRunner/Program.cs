using Common;

namespace PuzzleRunner
{
    internal class Program
    {
        static void Main(string[] args)
        {
            RunAdventOfCode2022();
            RunAdventOfCode2023();
        }

        private static void RunAdventOfCode2022()
        {
            var year = 2022;
            Console.WriteLine($"Advent of Code year {year}");
            
            var repo = new DataSetRepository(year);
            string[] dataSet;

            dataSet = repo.GetDataSet(day: 1);
            RunPuzzle(new AdventOfCode2022Solutions.Day1.Solution(dataSet));
            
        }

        private static void RunAdventOfCode2023()
        {
            var year = 2023;
            Console.WriteLine($"Advent of Code year {year}");

            var repo = new DataSetRepository(year);
            string[] dataSet;

            dataSet = repo.GetDataSet(day: 1);
            RunPuzzle(new AdventOfCode2023Solutions.Day01.Solution(dataSet));

            dataSet = repo.GetDataSet(day: 2);
            RunPuzzle(new AdventOfCode2023Solutions.Day02.Solution(dataSet));
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
