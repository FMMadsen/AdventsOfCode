using System.Text.RegularExpressions;

namespace AdventsOfCode2022.Day5CraneAndSupplyStacks
{
    /// <summary>
    /// Time consumption PART 1: 07:20 - 07:30 + 13:00 - 13:30 + 16:00 - 17:42 = 
    /// Time consumption PART 2: 
    /// Time consumption TOTAL: 
    /// </summary>
    internal class Day5Puzzle
    {
        internal static string SolvePart1(string[] datasetLines, bool doPrintOut)
        {
            var stacks = ReadInitialSupplyStacks(datasetLines);
            var ship = new Ship(stacks);
            ship.LoadCraneCommands(datasetLines);

            if (doPrintOut)
                PrintSolutionPart1Detail(ship);

            ship.ExecuteCraneCommands();

            if (doPrintOut)
                PrintSolutionPart1Detail(ship);

            var topCrates = ship.GetTopCrates();

            return topCrates;
        }

        internal static string SolvePart2(string[] datasetLines, bool doPrintOut)
        {

            if (doPrintOut)
                PrintSolutionPart2Detail();

            return string.Empty;
        }

        /// <summary>
        /// Reading this kind of data
        ///    [D]    
        ///[N] [C]    
        ///[Z] [M] [P]
        /// 1   2   3 
        ///                 <<-- expected a whitespace line here
        /// ... (more data here, not relevant)
        /// ... (more data here, not relevant)
        /// ...
        /// ..
        /// .
        /// </summary>
        /// <param name="datasetLines">above lines</param>
        /// <returns>Would like to organize it into an array of stacks of chars. Number of stacks expected constant.</returns>
        private static Stack<Char>[] ReadInitialSupplyStacks(string[] datasetLines)
        {
            var stackCount = CountStacks(datasetLines);
            var startingStackStringsTopDown = ReadStartingStackStrings(datasetLines);
            var stacks = CreateStacksWithCrates(stackCount, startingStackStringsTopDown);
            return stacks;
        }
        
        private static int CountStacks(string[] datasetLines)
        {
            var stackCount = 0;

            foreach (var line in datasetLines)
            {
                if (line.Contains('['))
                    continue;
                else if (string.IsNullOrWhiteSpace(line))
                    throw new Exception("Day5Puzzle.CountStacks: expected to find line with stack counts, but found empty line before");
                else
                {
                    stackCount = GetLastNumberFromNumberLine(line);
                    break;
                }
            }
            
            return stackCount;
        }

        private static int GetLastNumberFromNumberLine(string numberLineString)
        {
            var numberPieces = numberLineString.Split(' ');
            var lastNumber = 0;

            for(int i = 0; i < numberPieces.Length; i++)
            {
                if (int.TryParse(numberPieces[i], out int number))
                {
                    lastNumber = number;
                }
            }

            return lastNumber;
        }

        private static Stack<string> ReadStartingStackStrings(string[] datasetLines)
        {
            var stackHorizontalLayerStringsTopToDown = new Stack<string>();

            foreach (var line in datasetLines)
            {
                if (line.Contains('['))
                {
                    stackHorizontalLayerStringsTopToDown.Push(line);
                    continue;
                }
                else if (string.IsNullOrWhiteSpace(line))
                    break;
                else
                    break;
            }
            return stackHorizontalLayerStringsTopToDown;
        }

        private static Stack<Char>[] CreateStacksWithCrates(int numberOfStacks, Stack<string> startingStackStringsTopDown)
        {
            if (numberOfStacks == 0)
                throw new Exception("Day5Puzzle.CreateStacksWithCrates:Number of stacks may not be 0");

            var stacks = new Stack<Char>[numberOfStacks];

            for(int i = 0; i<numberOfStacks; i++)
            {
                stacks[i] = new Stack<char>();
            }

            while(startingStackStringsTopDown.Any())
            {
                string stackRowLineString = startingStackStringsTopDown.Pop();
                LoadCratesOntoStacks(stacks, stackRowLineString);
            }

            return stacks;
        }

        /// <summary>
        /// Assuming row like these examples - and assuming only 1 character crate identifications
        /// 
        ///    [D]    
        ///[N] [C]    
        ///[Z] [M] [P]
        /// 
        /// </summary>
        /// <param name="stacks"></param>
        /// <param name="stackRowLineString"></param>
        private static void LoadCratesOntoStacks(Stack<Char>[] stacks, string stackRowLineString)
        {
            string pattern = Regex.Escape("[");
            var matches = Regex.Matches(stackRowLineString, pattern);
            var numberOfCrates = matches.Count;

            var startSearchIndex = 0;

            for (int i = 0; i < numberOfCrates; i++)
            {
                var indexOfNextCrateZeroBased = stackRowLineString.IndexOf('[', startSearchIndex) + 1;
                if (indexOfNextCrateZeroBased == -1)
                    break;

                var crate = stackRowLineString.ElementAt(indexOfNextCrateZeroBased);
                var stackNumber = (indexOfNextCrateZeroBased + 3) / 4;
                stacks[stackNumber - 1].Push(crate);
                
                startSearchIndex = indexOfNextCrateZeroBased;
            }
        }


        private static void PrintSolutionPart1Detail(Ship ship)
        {
            Console.WriteLine("Ship stacks:");
            var stacks = ship.Stacks;

            for (int i = 0; i<stacks.Length; i++)
            {
                Console.Write($"Stack {i} contains {stacks[i].Count} crates:");
                
                for(int c = stacks[i].Count-1; c>-1; c--)
                {
                    Console.Write(stacks[i].ElementAt(c));
                }
                Console.WriteLine();
            }

            Console.WriteLine("Ship crane commands:");
            var commands = ship.Crane.CommandList;

            foreach(var command in commands)
            {
                Console.WriteLine($"Move {command.MoveFrom} to {command.MoveTo}");
            }
        }

        private static void PrintSolutionPart2Detail()
        {
        }
    }
}
