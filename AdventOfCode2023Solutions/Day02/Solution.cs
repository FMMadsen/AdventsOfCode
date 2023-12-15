using Common;
using System.Linq.Expressions;
using System.Numerics;
using System.Text.RegularExpressions;

namespace AdventOfCode2023Solutions.Day02
{
    public class Solution(string[] datasetLines) : IAOCSolution
    {
        public string PuzzleName => "Day 2: Cube Conundrum";
        public string[] DatasetLines => datasetLines;

        private string patternGame = @"\s?Game\s?(\d+)\s?";
        private string patternHand = @"\s?(\d+)\s?(\w+),?";
        private Dictionary<string,int> MaxCubesInBag = new() {["red"]=12, ["green"]=13, ["blue"]=14 };
        private int GamesSucceeded = 0;
        private int GamesMinPower = 0;

        public string SolvePart1()
        {
            for(int index = 0; index < DatasetLines.Length; index++)
            {
                if (-1 < DatasetLines[index].IndexOf("Game"))
                {
                    string[] game = DatasetLines[index].Split(':', ';');
                    bool handsWasPossible = true;

                    for (int i = 1; i < game.Length; i++)
                    {
                        if (!IsHandPossible(game[i]))
                        {
                            handsWasPossible = false;
                            break;
                        }
                    }

                    if (handsWasPossible)
                    {
                        GamesSucceeded += Int32.Parse(Regex.Matches(game[0], patternGame, RegexOptions.IgnoreCase)[0].Groups[1].Value);
                    }
                }
            }

            return GamesSucceeded.ToString();
        }

        public bool IsHandPossible(string hand)
        {
            bool handPossible = true;

            foreach (Match cubeOption in Regex.Matches(hand, patternHand, RegexOptions.IgnoreCase))
            {
                
                if(Int32.Parse(cubeOption.Groups[1].Value) > MaxCubesInBag[cubeOption.Groups[2].Value.ToLower()] )
                {
                    handPossible = false;
                    break;
                }
                
            }

            return handPossible;
        }

        public string SolvePart2()
        {
            for (int index = 0; index < DatasetLines.Length; index++)
            {
                if (-1 < DatasetLines[index].IndexOf("Game"))
                {
                    string[] game = DatasetLines[index].Split(':', ';');
                    bool handsWasPossible = true;
                    Dictionary<string, int> minCubesInBag = new() { ["red"] = 0, ["green"] = 0, ["blue"] = 0 };

                    for (int i = 1; i < game.Length; i++)
                    {
                        if (!IsHandPossible2(game[i], ref minCubesInBag))
                        {
                            handsWasPossible = false;
                        }
                    }

                    GamesMinPower += (minCubesInBag["red"] * minCubesInBag["green"] * minCubesInBag["blue"]);

                    if (handsWasPossible)
                    {
                        var m = Regex.Matches(game[0], patternGame, RegexOptions.IgnoreCase);
                        var mt = m[0];
                        GamesSucceeded += Int32.Parse(mt.Groups[1].Value);
                    }
                }
            }

            return GamesMinPower.ToString();
        }

        public bool IsHandPossible2(string hand, ref Dictionary<string, int> minCubesInBag)
        {
            bool handPossible = true;

            foreach (Match cubeOption in Regex.Matches(hand, patternHand, RegexOptions.IgnoreCase))
            {
                int amount = Int32.Parse(cubeOption.Groups[1].Value);
                string cubeType = cubeOption.Groups[2].Value.ToLower();

                if (minCubesInBag[cubeType] < amount) { minCubesInBag[cubeType] = amount; }

                if (amount > MaxCubesInBag[cubeType])
                {
                    handPossible = false;
                }

            }

            return handPossible;
        }
    }
}
