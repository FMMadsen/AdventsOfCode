using Common;

namespace PuzzleRunner
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //RunAdventOfCode2022();
            RunAdventOfCode2023();
        }

        private static void RunAdventOfCode2022()
        {
            var repo = new DataSetRepo(AdventOfCode2022Solutions.Constants.DataSetsSourceFolderName);
            Console.WriteLine($"Advent of Code year 2022");
            Console.WriteLine("-------------------------------------------");
            RunPuzzle(new AdventOfCode2022Solutions.Day01.Solution(repo.ReadDataSet(AdventOfCode2022Solutions.Constants.DataSetFileDay01)));
            RunPuzzle(new AdventOfCode2022Solutions.Day02.Solution(repo.ReadDataSet(AdventOfCode2022Solutions.Constants.DataSetFileDay02)));
            RunPuzzle(new AdventOfCode2022Solutions.Day03.Solution(repo.ReadDataSet(AdventOfCode2022Solutions.Constants.DataSetFileDay03)));
            RunPuzzle(new AdventOfCode2022Solutions.Day04.Solution(repo.ReadDataSet(AdventOfCode2022Solutions.Constants.DataSetFileDay04)));
            RunPuzzle(new AdventOfCode2022Solutions.Day05.Solution(repo.ReadDataSet(AdventOfCode2022Solutions.Constants.DataSetFileDay05)));
            RunPuzzle(new AdventOfCode2022Solutions.Day06.Solution(repo.ReadDataSet(AdventOfCode2022Solutions.Constants.DataSetFileDay06)));
            RunPuzzle(new AdventOfCode2022Solutions.Day07.Solution(repo.ReadDataSet(AdventOfCode2022Solutions.Constants.DataSetFileDay07)));
            RunPuzzle(new AdventOfCode2022Solutions.Day13.Solution(repo.ReadDataSet(AdventOfCode2022Solutions.Constants.DataSetFileDay13)));
        }

        private static void RunAdventOfCode2023()
        {
            var repo = new DataSetRepo(AdventOfCode2023Solutions.Constants.DataSetsSourceFolderName);
            Console.WriteLine($"Advent of Code year 2023");
            Console.WriteLine("-------------------------------------------");
            RunPuzzle(new AdventOfCode2023Solutions.Day01.Solution(repo.ReadDataSet(AdventOfCode2023Solutions.Constants.DataSetFileDay01)));
            RunPuzzle(new AdventOfCode2023Solutions.Day02.Solution(repo.ReadDataSet(AdventOfCode2023Solutions.Constants.DataSetFileDay02)));
            RunPuzzle(new AdventOfCode2023Solutions.Day03.Solution(repo.ReadDataSet(AdventOfCode2023Solutions.Constants.DataSetFileDay03)));
            RunPuzzle(new AdventOfCode2023Solutions.Day04.Solution(repo.ReadDataSet(AdventOfCode2023Solutions.Constants.DataSetFileDay04)));
            RunPuzzle(new AdventOfCode2023Solutions.Day05.Solution(repo.ReadDataSet(AdventOfCode2023Solutions.Constants.DataSetFileDay05)));
            RunPuzzle(new AdventOfCode2023Solutions.Day06.Solution(repo.ReadDataSet(AdventOfCode2023Solutions.Constants.DataSetFileDay06)));
            RunPuzzle(new AdventOfCode2023Solutions.Day07.Solution(repo.ReadDataSet(AdventOfCode2023Solutions.Constants.DataSetFileDay07)));
            RunPuzzle(new AdventOfCode2023Solutions.Day08.Solution(repo.ReadDataSet(AdventOfCode2023Solutions.Constants.DataSetFileDay08)));
            //RunPuzzle(new AdventOfCode2023Solutions.Day09.Solution(repo.ReadDataSet(AdventOfCode2023Solutions.Constants.DataSetFileDay09)));
            //RunPuzzle(new AdventOfCode2023Solutions.Day10.Solution(repo.ReadDataSet(AdventOfCode2023Solutions.Constants.DataSetFileDay10)));
            //RunPuzzle(new AdventOfCode2023Solutions.Day11.Solution(repo.ReadDataSet(AdventOfCode2023Solutions.Constants.DataSetFileDay11)));
            //RunPuzzle(new AdventOfCode2023Solutions.Day12.Solution(repo.ReadDataSet(AdventOfCode2023Solutions.Constants.DataSetFileDay12)));
            //RunPuzzle(new AdventOfCode2023Solutions.Day13.Solution(repo.ReadDataSet(AdventOfCode2023Solutions.Constants.DataSetFileDay13)));
            //RunPuzzle(new AdventOfCode2023Solutions.Day14.Solution(repo.ReadDataSet(AdventOfCode2023Solutions.Constants.DataSetFileDay14)));
            //RunPuzzle(new AdventOfCode2023Solutions.Day15.Solution(repo.ReadDataSet(AdventOfCode2023Solutions.Constants.DataSetFileDay15)));
            //RunPuzzle(new AdventOfCode2023Solutions.Day16.Solution(repo.ReadDataSet(AdventOfCode2023Solutions.Constants.DataSetFileDay16)));
            //RunPuzzle(new AdventOfCode2023Solutions.Day17.Solution(repo.ReadDataSet(AdventOfCode2023Solutions.Constants.DataSetFileDay17)));
            //RunPuzzle(new AdventOfCode2023Solutions.Day18.Solution(repo.ReadDataSet(AdventOfCode2023Solutions.Constants.DataSetFileDay18)));
            //RunPuzzle(new AdventOfCode2023Solutions.Day19.Solution(repo.ReadDataSet(AdventOfCode2023Solutions.Constants.DataSetFileDay19)));
            //RunPuzzle(new AdventOfCode2023Solutions.Day20.Solution(repo.ReadDataSet(AdventOfCode2023Solutions.Constants.DataSetFileDay20)));
            //RunPuzzle(new AdventOfCode2023Solutions.Day21.Solution(repo.ReadDataSet(AdventOfCode2023Solutions.Constants.DataSetFileDay21)));
            //RunPuzzle(new AdventOfCode2023Solutions.Day22.Solution(repo.ReadDataSet(AdventOfCode2023Solutions.Constants.DataSetFileDay22)));
            //RunPuzzle(new AdventOfCode2023Solutions.Day23.Solution(repo.ReadDataSet(AdventOfCode2023Solutions.Constants.DataSetFileDay23)));
            //RunPuzzle(new AdventOfCode2023Solutions.Day24.Solution(repo.ReadDataSet(AdventOfCode2023Solutions.Constants.DataSetFileDay24)));
            //RunPuzzle(new AdventOfCode2023Solutions.Day25.Solution(repo.ReadDataSet(AdventOfCode2023Solutions.Constants.DataSetFileDay25)));
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
