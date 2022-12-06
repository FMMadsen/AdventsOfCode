namespace _2022;

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

        ConsoleOut("Welcome to Day 1 challenge");
        var dataset = MyFileReader.ReadFileFromCurrentFolder(
            @"Datasets\Dai1ElvesInventoryList.txt"
        );
        var answer = Day1CalorieCounting.Day1Puzzle.Solve(dataset);
        ConsoleOut("ANSWER ON DAY 1 CHALLENGE: " + answer);

        ConsoleOutEmptyLine();
        ConsoleOutLine();
        ConsoleOut("    BYE! ");
        ConsoleOutDoubleLine();
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
