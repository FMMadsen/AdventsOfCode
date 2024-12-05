using Common;
using System.Reflection;
using System.Text.RegularExpressions;
using static System.Net.Mime.MediaTypeNames;

namespace AdventOfCode2024Solutions.Day03
{
    public class Solution : IAOCSolution
    {
        public string PuzzleName => "Day 3: ";
        protected static readonly string MulPattern = @"mul\((\d+),(\d+)\)";
        protected static readonly string DoPattern = @"(?<method>mul)\((?<mulleft>\d+),(?<mulright>\d+)\)|(?<method>don)'t\(\)";
        protected static readonly string DontPattern = @"(?<method>do)\(\)";
        protected static readonly Regex MulFinder = new Regex(MulPattern);
        protected static readonly Regex DoFinder = new Regex(DoPattern);
        protected static readonly Regex DontFinder = new Regex(DontPattern);

        public string SolvePart1(string[] datasetLines)
        {
            int total = 0;

            foreach (string datasetLine in datasetLines)
            {
                MatchCollection mulMatches = MatchMul(datasetLine);

                foreach (Match match in mulMatches)
                {
                    total += Mul(match);
                }
            }

            return total.ToString();
        }

        public string SolvePart2(string[] datasetLines)
        {
            int total = 0;
            var searchMode = DoMode;

            foreach (string datasetLine in datasetLines)
            {
                int lineIndex = 0;

                while (lineIndex < datasetLine.Length)
                {
                    Match match = (Match?)searchMode.DynamicInvoke(datasetLine, lineIndex) ?? Match.Empty;

                    if (!match.Success) { lineIndex = datasetLine.Length; }
                    else
                    {
                        match.Groups.TryGetValue("method", out Group? method);

                        if (null != method)
                        {
                            switch (method.Value)
                            {
                                case "mul":
                                    match.Groups.TryGetValue("mulleft", out Group? mulleft);
                                    match.Groups.TryGetValue("mulright", out Group? mulright);
                                    if (null != mulleft && null != mulright)
                                    {
                                        total += Int32.Parse(mulleft.Value) * Int32.Parse(mulright.Value);
                                    }

                                    break;

                                case "do":
                                    searchMode = DoMode;
                                    break;

                                case "don":
                                    searchMode = DontMode;
                                    break;
                            }
                        }

                        lineIndex = match.Index + match.Length;
                    }
                    
                }

            }

            return total.ToString();
        }

        protected static int Mul(Match match)
        {
            GroupCollection groups = match.Groups;
            return Int32.Parse(groups[1].Value) * Int32.Parse(groups[2].Value);
        }

        protected static MatchCollection MatchMul(string text)
        {
            return MulFinder.Matches(text);
        }

        protected static Match DoMode(string datasetLine, int lineIndex)
        {
            return DoFinder.Match(datasetLine, lineIndex);
        }

        protected static Match DontMode(string datasetLine, int lineIndex)
        {
            return DontFinder.Match(datasetLine, lineIndex);
        }
    }
}
