using AdventsOfCode2022.Day01CalorieCounting;
using AdventsOfCode2022.Day02RockPaperScissors;
using AdventsOfCode2022.Day03Rucksack;
using AdventsOfCode2022.Day04CampCleanup;
using AdventsOfCode2022.Day05CraneAndSupplyStacks;
using AdventsOfCode2022.Day06TuningTrouble;
using AdventsOfCode2022.Day13DistressSignal;

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

        ExecutePuzzle(1, false, false, "Elves inventory list", @"Datasets\Day01ElvesInventoryList.txt");
        ExecutePuzzle(2, false, false, "Rock-Paper-Scissors", @"Datasets\Day02RPSGameLog.txt");
        ExecutePuzzle(3, false, false, "Misplaced supplies", @"Datasets\Day03RucksacksContent.txt");
        ExecutePuzzle(4, false, false, "Camp cleanup", @"Datasets\Day04SectionAssignments.txt");
        ExecutePuzzle(5, false, false, "Crane & supply stacks", @"Datasets\Day05CraneAndSupplyStacks.txt");
        ExecutePuzzle(6, true, true, "Tuning trouble", @"Datasets\Day06TuningTrouble.txt");
        ExecutePuzzle(13, false, false, "Crane & supply stacks", @"Datasets\Day13DistressSignal_test.txt");
    }

    private static void ExecutePuzzle(int day, bool extraPrintoutPart1, bool extraPrintoutPart2, string puzzleName, string datasetFile)
    {
        ConsoleOutLine();
        Console.WriteLine($"Day {day} challenge: {puzzleName}");
        var datasetLines = MyFileReader.ReadFileIntoLineArrayFromCurrentFolder(datasetFile);

        var part1Answer = string.Empty;
        var part2Answer = string.Empty;

        switch (day)
        {
            case 1:
                part1Answer = Day1Puzzle.SolvePart1(datasetLines, extraPrintoutPart1);
                part2Answer = Day1Puzzle.SolvePart2(datasetLines, extraPrintoutPart2);
                break;
            case 2:
                part1Answer = Day2Puzzle.SolvePart1(datasetLines, extraPrintoutPart1);
                part2Answer = Day2Puzzle.SolvePart2(datasetLines, extraPrintoutPart2);
                break;
            case 3:
                part1Answer = Day3Puzzle.SolvePart1(datasetLines, extraPrintoutPart1);
                part2Answer = Day3Puzzle.SolvePart2(datasetLines, extraPrintoutPart2);
                break;
            case 4:
                part1Answer = Day4Puzzle.SolvePart1(datasetLines, extraPrintoutPart1);
                part2Answer = Day4Puzzle.SolvePart2(datasetLines, extraPrintoutPart2);
                break;
            case 5:
                part1Answer = Day5Puzzle.SolvePart1(datasetLines, extraPrintoutPart1);
                part2Answer = Day5Puzzle.SolvePart2(datasetLines, extraPrintoutPart2);
                break;
            case 6:
                part1Answer = Day6Puzzle.SolvePart1(datasetLines, extraPrintoutPart1);
                part2Answer = Day6Puzzle.SolvePart2(datasetLines, extraPrintoutPart2);
                break;
            case 7:
                throw new NotImplementedException("Day 7 is not implemented");
            case 13:
                part1Answer = Day13Puzzle.SolvePart1(datasetLines, extraPrintoutPart1);
                part2Answer = Day13Puzzle.SolvePart2(datasetLines, extraPrintoutPart2);
                break;
        }

        Console.WriteLine($"ANSWER ON DAY {day} CHALLENGE - PART 1: {part1Answer}");
        Console.WriteLine($"ANSWER ON DAY {day} CHALLENGE - PART 2: {part2Answer}");
    }

    private static void ConsoleOutLine()
    {
        Console.WriteLine("-------------------------------------------");
    }
}