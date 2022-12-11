using AdventsOfCode2022.Day1CalorieCounting;
using AdventsOfCode2022.Day2RockPaperScissors;
using AdventsOfCode2022.Day3Rucksack;
using AdventsOfCode2022.Day4CampCleanup;

namespace AdventsOfCode2022;

class Program
{
    static void Main(string[] args)
    {
        ConsoleOutEmptyLine();
        ConsoleOutLine();
        ConsoleOut("Advents of Code 2022");
        ConsoleOut("https://adventofcode.com/2022");

        ConsoleOutLine();
        ConsoleOutEmptyLine();

        /************** DAY 1 ***************/
        ConsoleOut("Welcome to Day 1 challenge: Elves inventory list");
        var datasetLines = MyFileReader.ReadFileIntoLineArrayFromCurrentFolder(@"Datasets\Day1ElvesInventoryList.txt");
        //var dataset = MyFileReader.ReadFileFromCurrentFolder(@"Datasets\Day1ElvesInventoryList.txt");
        var day1Part1Answer = Day1Puzzle.SolvePart1(datasetLines, false);
        ConsoleOut($"ANSWER ON DAY 1 CHALLENGE - PART 1: {day1Part1Answer}");
        var day1Part2Answer = Day1Puzzle.SolvePart2(datasetLines, false);
        ConsoleOut($"ANSWER ON DAY 1 CHALLENGE - PART 2: {day1Part2Answer}");

        ConsoleOutEmptyLine();
        ConsoleOutLine();
        ConsoleOutEmptyLine();

        /************** DAY 2 ***************/
        ConsoleOut("Welcome to Day 2 challenge: Rock-Paper-Scissors");
        datasetLines = MyFileReader.ReadFileIntoLineArrayFromCurrentFolder(@"Datasets\Day2RPSGameLog.txt");
        var day2Part1Answer = Day2Puzzle.SolvePart1(datasetLines, false);
        ConsoleOut($"ANSWER ON DAY 2 CHALLENGE - PART 1: {day2Part1Answer}");
        var day2Part2Answer = Day2Puzzle.SolvePart2(datasetLines, false);
        ConsoleOut($"ANSWER ON DAY 2 CHALLENGE - PART 2: {day2Part2Answer}");

        ConsoleOutEmptyLine();
        ConsoleOutLine();
        ConsoleOutEmptyLine();

        /************** DAY 3 ***************/
        ConsoleOut("Welcome to Day 3 challenge: Misplaced supplies");
        datasetLines = MyFileReader.ReadFileIntoLineArrayFromCurrentFolder(@"Datasets\Day3RucksacksContent.txt");
        var day3Part1Answer = Day3Puzzle.SolvePart1(datasetLines, false);
        ConsoleOut($"ANSWER ON DAY 3 CHALLENGE - PART 1: {day3Part1Answer}");
        var day3Part2Answer = Day3Puzzle.SolvePart2(datasetLines, false);
        ConsoleOut($"ANSWER ON DAY 3 CHALLENGE - PART 2: {day3Part2Answer}");

        ConsoleOutEmptyLine();
        ConsoleOutLine();
        ConsoleOutEmptyLine();

        /************** DAY 4 ***************/
        ConsoleOut("Welcome to Day 4 challenge: Camp cleanup");
        datasetLines = MyFileReader.ReadFileIntoLineArrayFromCurrentFolder(@"Datasets\Day4SectionAssignments.txt");
        var day4Part1Answer = Day4Puzzle.SolvePart1(datasetLines, false);
        ConsoleOut($"ANSWER ON DAY 4 CHALLENGE - PART 1: {day4Part1Answer}");
        var day4Part2Answer = Day4Puzzle.SolvePart2(datasetLines, false);
        ConsoleOut($"ANSWER ON DAY 4 CHALLENGE - PART 2: {day4Part2Answer}");

        /************** DAY 5 ***************/

        /************** DAY 6 ***************/

        /************** DAY 7 ***************/

        /************** DAY 8 ***************/

        /************** DAY 9 ***************/


        ConsoleOutEmptyLine();
        ConsoleOutLine();
        ConsoleOut("    BYE! ");
        ConsoleOutLine();
    }

    private static void ConsoleOut(string text)
    {
        Console.WriteLine(text);
    }

    private static void ConsoleOutEmptyLine()
    {
        Console.WriteLine(" ");
    }

    private static void ConsoleOutLine()
    {
        Console.WriteLine("-----------------------------------------------------------");
    }

    private static void ConsoleOutDoubleLine()
    {
        Console.WriteLine("-----------------------------------------------------------");
        Console.WriteLine("-----------------------------------------------------------");
    }
}
