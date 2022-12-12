using AdventsOfCode2022.Day1CalorieCounting;
using AdventsOfCode2022.Day2RockPaperScissors;
using AdventsOfCode2022.Day3Rucksack;
using AdventsOfCode2022.Day4CampCleanup;
using AdventsOfCode2022.Day5CraneAndSupplyStacks;

namespace AdventsOfCode2022;

class Program
{
    /// <summary>
    /// https://adventofcode.com/2022
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        Console.WriteLine("Advents of Code 2022");

        ExecutePuzzle(1, false, "Elves inventory list", @"Datasets\Day1ElvesInventoryList.txt");
        ExecutePuzzle(2, false, "Rock-Paper-Scissors", @"Datasets\Day2RPSGameLog.txt");
        ExecutePuzzle(3, false, "Misplaced supplies", @"Datasets\Day3RucksacksContent.txt");
        ExecutePuzzle(4, false, "Camp cleanup", @"Datasets\Day4SectionAssignments.txt");
        ExecutePuzzle(5, false, "Crane & supply stacks", @"Datasets\Day5CraneAndSupplyStacks.txt");
    }

    private static void ExecutePuzzle(int day, bool extraPrintout, string puzzleName, string datasetFile)
    {
        ConsoleOutLine();
        Console.WriteLine($"Day {day} challenge: {puzzleName}");
        var datasetLines = MyFileReader.ReadFileIntoLineArrayFromCurrentFolder(datasetFile);

        var part1Answer = string.Empty;
        var part2Answer = string.Empty;

        switch (day)
        {
            case 1:
                part1Answer = Day1Puzzle.SolvePart1(datasetLines, extraPrintout);
                part2Answer = Day1Puzzle.SolvePart2(datasetLines, extraPrintout);
                break;
            case 2:
                part1Answer = Day2Puzzle.SolvePart1(datasetLines, extraPrintout);
                part2Answer = Day2Puzzle.SolvePart2(datasetLines, extraPrintout);
                break;
            case 3:
                part1Answer = Day3Puzzle.SolvePart1(datasetLines, extraPrintout);
                part2Answer = Day3Puzzle.SolvePart2(datasetLines, extraPrintout);
                break;
            case 4:
                part1Answer = Day4Puzzle.SolvePart1(datasetLines, extraPrintout);
                part2Answer = Day4Puzzle.SolvePart2(datasetLines, extraPrintout);
                break;
            case 5:
                part1Answer = Day5Puzzle.SolvePart1(datasetLines, extraPrintout);
                part2Answer = Day5Puzzle.SolvePart2(datasetLines, extraPrintout);
                break;
            //case 6:
            //    part1Answer = Day6Puzzle.SolvePart1(datasetLines, false);
            //    part2Answer = Day6Puzzle.SolvePart2(datasetLines, false);
            //    break;
            //case 7:
            //    part1Answer = Day7Puzzle.SolvePart1(datasetLines, false);
            //    part2Answer = Day7Puzzle.SolvePart2(datasetLines, false);
            //    break;
            //case 8:
            //    part1Answer = Day8Puzzle.SolvePart1(datasetLines, false);
            //    part2Answer = Day8Puzzle.SolvePart2(datasetLines, false);
            //    break;
            //case 9:
            //    part1Answer = Day9Puzzle.SolvePart1(datasetLines, false);
            //    part2Answer = Day9Puzzle.SolvePart2(datasetLines, false);
            //    break;
        }

        Console.WriteLine($"ANSWER ON DAY {day} CHALLENGE - PART 1: {part1Answer}");
        Console.WriteLine($"ANSWER ON DAY {day} CHALLENGE - PART 2: {part2Answer}");
    }

    private static void ConsoleOutLine()
    {
        Console.WriteLine("-------------------------------------------");
    }
}