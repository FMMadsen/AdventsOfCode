using Common;

namespace PuzzleRunner
{
    internal class Program
    {
        static void Main(string[] args)
        {
            RunPuzzle(2022);
            RunPuzzle(2023);

        }

        private static void RunPuzzle(int year)
        {
            Console.WriteLine($"Running Advent of Code year {year}");
            var repo = new DataSetRepository(year);



            RunPuzzle(new AdventOfCode2022Solutions.Day1.Solution(repo.GetDataSet(day: 1)));
        }

        private static void RunPuzzle(IAOCSolution puzzleSolution)
        {
            var resultPart1 = puzzleSolution.SolvePart1();
            Console.WriteLine($"Solution {puzzleSolution.PuzzleName} : {resultPart1}");

            var resultPart2 = puzzleSolution.SolvePart2();
            Console.WriteLine($"Solution {puzzleSolution.PuzzleName} : {resultPart2}");
        }
    }
}
