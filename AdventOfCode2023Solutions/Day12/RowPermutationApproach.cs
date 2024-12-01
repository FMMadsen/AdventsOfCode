using System.Text;

namespace AdventOfCode2023Solutions.Day12
{
    public class RowPermutationApproach
    {
        private const char BROKEN = '#';
        private const char UNKNOWN = '?';
        private const char GOOD = '.';

        public int[] TargetBrokenSpringGroups { get; private set; }
        public string SpringRowString { get; private set; }
        public char[] SpringsArray { get; private set; }
        public int NumberOfSprings { get; private set; }
        public int NumberOfBroken { get; private set; }
        public int NumberOfUnknowns { get; private set; }
        public int MaxNumberOfBroken { get; private set; }
        public int NumberOfUnknownToBroken { get; private set; }
        public long TotalNumberOfCombinations { get; private set; }

        public List<string> ExpandedPotentialSituations { get; private set; } = [];
        public List<string> PossibleSituations { get; private set; } = [];
        public long NumberOfPossibleSituations { get; private set; }


        public RowPermutationApproach(string rowConditionString, bool doUnfoldRecords = false)
        {
            var conditionStringSplit = rowConditionString.Split(' ');

            var targetGroupsString = conditionStringSplit[1].Trim();
            if (doUnfoldRecords)
                targetGroupsString = $"{targetGroupsString},{targetGroupsString},{targetGroupsString},{targetGroupsString},{targetGroupsString}";

            TargetBrokenSpringGroups = targetGroupsString.Split(',').Select(s => int.Parse(s)).ToArray();

            SpringRowString = conditionStringSplit[0].Trim();
            if (doUnfoldRecords)
                SpringRowString = $"{SpringRowString}?{SpringRowString}?{SpringRowString}?{SpringRowString}?{SpringRowString}";

            SpringsArray = SpringRowString.ToArray();
            NumberOfSprings = SpringRowString.Length;
            NumberOfBroken = SpringRowString.Count(s => s == BROKEN);
            NumberOfUnknowns = SpringRowString.Count(s => s == UNKNOWN);
            MaxNumberOfBroken = TargetBrokenSpringGroups.Sum();
            NumberOfUnknownToBroken = MaxNumberOfBroken - NumberOfBroken;
            TotalNumberOfCombinations = (long)Math.Pow(2, NumberOfUnknowns);
        }

        public long Part2_CalculateNumberOfPossibleSituations()
        {
            var possibleSolution = (long)Math.Pow(NumberOfPossibleSituations, 5);
            return possibleSolution;
        }

        public void ExpandAllUnknownsToPotentialSituations()
        {
            var permutationMatrix = BuildPermutatinos(NumberOfUnknownToBroken, NumberOfUnknowns).ToArray();
            var noOfCombinations = permutationMatrix.Length;

            StringBuilder sb = new();
            char spring;
            string potentialRowSituation;
            int permutationIndex, unknownFoundCounter;

            for (int c = 0; c < noOfCombinations; c++)
            {
                permutationIndex = 0;
                unknownFoundCounter = 0;

                for (int s = 0; s < NumberOfSprings; s++)
                {
                    spring = SpringsArray[s];

                    if (spring == UNKNOWN)
                    {
                        unknownFoundCounter++;

                        if (permutationMatrix.Length > 0 && permutationMatrix[c][permutationIndex] == unknownFoundCounter)
                        {
                            sb.Append(BROKEN);
                            if (permutationIndex < permutationMatrix[c].Length - 1)
                                permutationIndex++;
                        }
                        else
                            sb.Append(GOOD);
                    }
                    else
                        sb.Append(spring);
                }

                potentialRowSituation = sb.ToString();
                sb.Clear();
                ExpandedPotentialSituations.Add(potentialRowSituation);

                if (VerifyCheckSum(potentialRowSituation))
                {
                    PossibleSituations.Add(potentialRowSituation);
                    NumberOfPossibleSituations++;
                }
            }
        }

        public static IEnumerable<int[]> BuildPermutatinos(int noOfUnknownToBroken, int noOfUnknowns)
        {
            int[] result = new int[noOfUnknownToBroken];
            Stack<int> stack = new();
            if (noOfUnknowns > 0 && noOfUnknownToBroken > 0)
                stack.Push(0);

            while (stack.Count > 0)
            {
                int index = stack.Count - 1;
                int value = stack.Pop();

                while (value < noOfUnknowns)
                {
                    result[index++] = ++value;
                    stack.Push(value);

                    if (index == noOfUnknownToBroken)
                    {
                        yield return result.Clone() as int[] ?? new int[noOfUnknownToBroken];
                        break;
                    }
                }
            }
        }

        public bool VerifyCheckSum(string springs)
        {
            var g = springs.Trim().Split('.', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).Select(s => s.Length);
            return g.SequenceEqual(TargetBrokenSpringGroups);
        }

        public static long Factorial(long number)
        {
            long result = 1;
            for (long i = number; i > 0; i--)
                result *= i;
            return result;
        }

        public override string ToString()
        {
            return $"  {SpringRowString,-30} {string.Join('-', TargetBrokenSpringGroups),-15}";// base.ToString();
        }
    }
}
