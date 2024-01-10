using Common;
using System.Numerics;
using System.Text.RegularExpressions;

namespace AdventOfCode2023Solutions.Day03
{
    public class Solution : IAOCSolution
    {
        public string PuzzleName => "Day 3: Gear Ratios";

        public long PartsNumber = 0;

        public string SolvePart1(string[] datasetLines)
        {
            Dictionary<int,List<int>> symbols = new();

            for (int lineNumber = 0; lineNumber < DatasetLines.Length; lineNumber++)
            {
                foreach (Match symbol in Regex.Matches(DatasetLines[lineNumber], @"[^\d.]"))
                {
                    symbols.TryAdd( lineNumber, new List<int>() );
                    symbols[lineNumber].Add(symbol.Index);
                }
            }

            for (int lineNumber = 0; lineNumber < DatasetLines.Length; lineNumber++)
            {
                foreach (Match numberObject in Regex.Matches(DatasetLines[lineNumber], @"\d+"))
                {
                    bool adjacent = false;

                    for(int i = -1; i < 2; i++)
                    {
                        if (symbols.ContainsKey(lineNumber+i))
                        {
                            foreach(int symbol in symbols[lineNumber + i])
                            {
                                if(numberObject.Index - 1 <= symbol && symbol <= numberObject.Index + numberObject.Length)
                                { adjacent = true; }
                            }
                        }
                    }

                    if (adjacent)
                    {
                        PartsNumber += long.Parse(numberObject.Value);
                    }

                }
            }

            return PartsNumber.ToString();
        }

        public string SolvePart2(string[] datasetLines)
        {
            PartsNumber = 0;

            Dictionary<int, List<Part>> parts = new();

            for (int lineNumber = 0; lineNumber < DatasetLines.Length; lineNumber++)
            {
                foreach (Match numberObject in Regex.Matches(DatasetLines[lineNumber], @"\d+"))
                {
                    parts.TryAdd(lineNumber, new List<Part>());
                    parts[lineNumber].Add(new Part()
                    {
                        Number = Int32.Parse(numberObject.Value),
                        Start = numberObject.Index,
                        End = numberObject.Index + numberObject.Length - 1
                    });
                }
            }

            for (int lineNumber = 0; lineNumber < DatasetLines.Length; lineNumber++)
            {
                foreach (Match symbol in Regex.Matches(DatasetLines[lineNumber], @"[^\d.]"))
                {
                    List<Part> adjacent = new List<Part>();

                    for (int i = -1; i < 2; i++)
                    {
                        if (parts.ContainsKey(lineNumber + i))
                        {
                            
                            for (int p = parts[lineNumber + i].Count - 1; -1 < p; p--)
                            {
                                Part part = parts[lineNumber + i][p];
                                if (part.Start - 2 < symbol.Index && symbol.Index < part.End + 2)
                                { 
                                    adjacent.Add(part);
                                    parts[lineNumber + i].RemoveAt(p);
                                }
                            }
                        }
                    }

                    if (@"*" == symbol.Value && 2 == adjacent.Count)
                    {
                        PartsNumber += adjacent[0].Number * adjacent[1].Number;
                    }
                }
            }

            return PartsNumber.ToString();
        }
    }
}
