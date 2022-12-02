using System.Reflection;

namespace Calorie_Counting;
class Program
{
    static void Main(string[] args)
    {
        var starFruitsMin = 50;
        var deadline = new DateTime(2022, 12, 25);

        ConsoleOutEmptyLine();
        ConsoleOutDoubleLine();
        ConsoleOut("       Advents of Code 2022");
        ConsoleOut("https://adventofcode.com/2022");
        ConsoleOutLine();
        ConsoleOutEmptyLine();
        ConsoleOut("StarFruitsMin: " + starFruitsMin);
        ConsoleOut("Deadline: " + deadline);
        ConsoleOutEmptyLine();
        ConsoleOut("Welcome to Day 1 challenge");
        
        var elfInventoryListName = "ElfInventoryList_2.txt";
        var inventory = ReadInventoryList(elfInventoryListName);   
        var elvesCalorySumList = ConvertToElvesInventorySums(inventory);
        printElvesList(elvesCalorySumList);
        var higestNumber = IdentifyHigestCaloryContent(elvesCalorySumList);
        ConsoleOut("ANSWER ON DAY 1 CHALLENGE: " + higestNumber);

        ConsoleOutEmptyLine();
        ConsoleOutLine();
        ConsoleOut("    BYE! ");
        ConsoleOutDoubleLine();
    }

    private static int IdentifyHigestCaloryContent(int[] elvesList)
    {
        var maxCaloryContent = elvesList.Max();
        return maxCaloryContent;
    }

    private static void printElvesList(int[] elvesList)
    {
        int elvNr = 1;
        foreach(int elfCalorySum in elvesList)
        {
            ConsoleOut("" + elvNr++ + ": " + elfCalorySum);
        }
    }

    private static int[] ConvertToElvesInventorySums(string[] inventoryList)
    {
        List<int> elvesList = new List<int>();

        int calory = 0;
        int calorySum = 0;
        foreach(string inventoryItem in inventoryList)
        {
            if(int.TryParse(inventoryItem, out calory))
            {
                calorySum += calory;
            }
            else
            {
                elvesList.Add(calorySum);
                calory = 0;
                calorySum = 0;
            }
        }

        return elvesList.ToArray();
    }

    private static string[] ReadInventoryList(string listFileName)
    {
        var filecontent = ReadFileFromCurrentFolder(listFileName);
        string[] lines = filecontent.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
        return lines;
    }

    private static string ReadFileFromCurrentFolder(string filename)
    {
        var contentRootPath = GetContentRootPath();
        var fileFullPath = contentRootPath + filename;

        var fileContent = ReadFile(fileFullPath);
        return fileContent;
    }

    private static string GetContentRootPath()
    {
        var appRootDirectory = AppContext.BaseDirectory;

        if(appRootDirectory.Contains("bin"))
        {
            appRootDirectory = appRootDirectory.Substring(0,appRootDirectory.IndexOf("bin"));
        }

        return appRootDirectory;
    }

        private static string ReadFile(string fileFullPath)
    {
        if(System.IO.File.Exists(fileFullPath))
        {
            var filecontent = System.IO.File.ReadAllText(fileFullPath);
            return filecontent;
        }
        else
        {
            throw new Exception("File not found: " + fileFullPath);
        }
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
