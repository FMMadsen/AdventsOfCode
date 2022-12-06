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

        ConsoleOut("Welcome to Day 1 challenge: Elves inventory list");
        var dataset = MyFileReader.ReadFileFromCurrentFolder(
            @"Datasets\Day1ElvesInventoryList.txt"
        );
        var answer = Day1CalorieCounting.Day1Puzzle.Solve(dataset);
        ConsoleOut("ANSWER ON DAY 1 CHALLENGE: " + answer);

        ConsoleOutEmptyLine();
        ConsoleOutLine();
        ConsoleOutEmptyLine();

        ConsoleOut("Welcome to Day 2 challenge: Rock-Paper-Scissors");
        var datasetLines = MyFileReader.ReadFileIntoLineArrayFromCurrentFolder(
            @"Datasets\Day2RPSGameLog.txt"
        );
        var game = Day2RockPaperScissors.Day2Puzzle.Solve(datasetLines);

        Console.WriteLine(" ");
        Console.WriteLine(
            $"Game is completed after {game.GameRoundsCompleted} rounds. Score player 1= {game.Player1CurrentScore}. Score player 2= {game.Player2CurrentScore}."
        );
        Console.WriteLine($"Game winner is {game.Winner}");
        ConsoleOut("ANSWER ON DAY 1 CHALLENGE: " + game.Player2CurrentScore);

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
