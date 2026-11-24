using System.Diagnostics;
using Common;
using Common.PuzzleRegistration;

namespace PuzzleRunner
{
    internal class Program
    {
        static int Main(string[] args)
        {
            if (!CliOptions.TryParse(args, out var options, out var error))
            {
                Console.Error.WriteLine(error);
                Console.Error.WriteLine(CliOptions.UsageText);
                return 2;
            }

            var all = PuzzleDiscovery.DiscoverAll().ToList();
            if (all.Count == 0)
            {
                Console.Error.WriteLine("No puzzle solutions discovered.");
                return 1;
            }

            var selected = options.Filter(all).ToList();
            if (selected.Count == 0)
            {
                Console.Error.WriteLine("No puzzles matched the given filters.");
                return 1;
            }

            int currentYear = -1;
            DataSetRepo? repo = null;

            foreach (var puzzle in selected)
            {
                if (puzzle.Year != currentYear)
                {
                    currentYear = puzzle.Year;
                    repo = new DataSetRepo(puzzle.ProjectFolderName);
                    Console.WriteLine();
                    Console.WriteLine($"Advent of Code year {currentYear}");
                    Console.WriteLine("-------------------------------------------");
                }

                RunPuzzle(puzzle, repo!);
            }

            return 0;
        }

        private static void RunPuzzle(PuzzleDescriptor puzzle, DataSetRepo repo)
        {
            Console.WriteLine(puzzle.Solution.PuzzleName);

            string[] data;
            try
            {
                data = repo.ReadDataSetForDay(puzzle.Day);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"  [skip] dataset unavailable: {ex.Message}");
                Console.WriteLine("-------------------------------------------");
                return;
            }

            RunPart("Part 1", () => puzzle.Solution.SolvePart1(data));
            RunPart("Part 2", () => puzzle.Solution.SolvePart2(data));
            Console.WriteLine("-------------------------------------------");
        }

        private static void RunPart(string label, Func<string> solve)
        {
            var sw = Stopwatch.StartNew();
            try
            {
                var result = solve();
                sw.Stop();
                Console.WriteLine($"  {label}: {result}  ({sw.ElapsedMilliseconds:N0} ms)");
            }
            catch (Exception ex)
            {
                sw.Stop();
                Console.WriteLine($"  {label}: FAILED after {sw.ElapsedMilliseconds:N0} ms -- {ex.GetType().Name}: {ex.Message}");
            }
        }
    }
}
