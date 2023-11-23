using Common;

namespace PuzzleRunner
{
    internal class Program
    {
        static void Main(string[] args)
        {
            RunAdventOfCode2022();
        }

        private static void RunAdventOfCode2022()
        {
            var year = 2022;
            Console.WriteLine($"Running Advent of Code year {year}");
            var repo = new DataSetRepository(year);

            RunPuzzle(new AdventOfCode2022Solutions.Day1.Solution(repo.GetDataSet(day: 1)));
            //RunPuzzle(new AdventOfCode2022Solutions.Day2.Solution(repo.GetDataSet(day: 2)));
        }

        private static void RunAdventOfCode2023()
        {
            //Console.WriteLine($"Running Advent of Code year {year}");
            //var repo = new DataSetRepository(year);

            //RunPuzzle(2023);
        }

        //private static void RunPuzzle(int year)
        //{
            



        //}

        private static void RunPuzzle(IAOCSolution puzzleSolution)
        {
            Console.WriteLine($"{puzzleSolution.PuzzleName}");

            var resultPart1 = puzzleSolution.SolvePart1();
            Console.WriteLine($"Solution part 1: {resultPart1}");

            var resultPart2 = puzzleSolution.SolvePart2();
            Console.WriteLine($"Solution part 2: {resultPart2}");
        }
    }
}
