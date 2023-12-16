namespace AdventOfCode2023Solutions.Day12
{
    public class AlternativeKombiExpander
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
        public long NumberOfPotentialCombinations { get; private set; }


        public List<string> ExpandedPotentialSituations { get; private set; } = [];

        public AlternativeKombiExpander(string rowConditionString)
        {
            var conditionStringSplit = rowConditionString.Split(' ');
            TargetBrokenSpringGroups = conditionStringSplit[1].Trim().Split(',').Select(s => int.Parse(s)).ToArray();

            SpringRowString = conditionStringSplit[0].Trim();
            SpringsArray = SpringRowString.ToArray();
            NumberOfSprings = SpringRowString.Length;
            NumberOfBroken = SpringRowString.Count(s => s == BROKEN);
            NumberOfUnknowns = SpringRowString.Count(s => s == UNKNOWN);
            MaxNumberOfBroken = TargetBrokenSpringGroups.Sum();
            NumberOfUnknownToBroken = MaxNumberOfBroken - NumberOfBroken;
            TotalNumberOfCombinations = (long)Math.Pow(2, NumberOfUnknowns);
            NumberOfPotentialCombinations = NumberOfUnknowns == 0 ? 1 : TotalNumberOfCombinations - NumberOfUnknowns - NumberOfUnknownToBroken;
            NumberOfPotentialCombinations = Factorial(NumberOfUnknowns) / (Factorial(NumberOfUnknownToBroken) * Factorial(NumberOfUnknowns - NumberOfUnknownToBroken));
        }

        public void ExpandAllUnknownsToPotentialSituations()
        {
            ExpandedPotentialSituations = CreateAllCombinations(SpringRowString);


        }

        

        public static List<string> CreateAllCombinations(string springRowString)
        {
            List<string> possibleCombinations = [];

            var numberOfUnknowns = springRowString.Count(s => s == UNKNOWN);

            var index = 0;
            while (index > -1)
            {
                index = springRowString.IndexOf(UNKNOWN, index);


            }

            return possibleCombinations;
        }

        public static long Factorial(long number)
        {
            long result = 1;
            for (long i = number; i > 0; i--)
                result *= i;
            return result;
        }
    }
}
