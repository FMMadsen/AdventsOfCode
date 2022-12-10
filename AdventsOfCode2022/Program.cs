using AdventsOfCode2022.Day1CalorieCounting;
using AdventsOfCode2022.Day2RockPaperScissors;

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
        var dataset = MyFileReader.ReadFileFromCurrentFolder(@"Datasets\Day1ElvesInventoryList.txt");
        var day1Answer = Day1Puzzle.SolvePart1(dataset, false);
        ConsoleOut("ANSWER ON DAY 1 CHALLENGE: " + day1Answer);
        var day2Answer = Day1Puzzle.SolvePart2(dataset, false);
        ConsoleOut("ANSWER ON DAY 2 CHALLENGE: " + day2Answer);

        ConsoleOutEmptyLine();
        ConsoleOutLine();
        ConsoleOutEmptyLine();

        /************** DAY 2 ***************/
        ConsoleOut("Welcome to Day 2 challenge: Rock-Paper-Scissors");
        var datasetLines = MyFileReader.ReadFileIntoLineArrayFromCurrentFolder(@"Datasets\Day2RPSGameLog.txt");
        var day2Part1Answer = Day2Puzzle.SolvePart1(datasetLines, false);
        ConsoleOut($"ANSWER ON DAY 1 CHALLENGE - PART 1: {day2Part1Answer}");
        var day2Part2Answer = Day2Puzzle.SolvePart2(datasetLines, false);
        ConsoleOut($"ANSWER ON DAY 1 CHALLENGE - PART 2: {day2Part2Answer}");

        /************** DAY 3 ***************/

        /************** DAY 4 ***************/

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
