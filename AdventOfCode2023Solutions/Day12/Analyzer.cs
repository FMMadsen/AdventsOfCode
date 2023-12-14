namespace AdventOfCode2023Solutions.Day12
{
    public static class Analyzer
    {
        private const char BROKEN = '#';
        private const char UNKNOWN = '?';
        private const char GOOD = '.';

        internal static List<SpringGroup>? IdentifyPossibleBrokenSpringGroupsForNextTarget(SpringRow? springRowReference, char[] springs, int[] targetDamagedSpringsGroups, int currentSpringsIndex = 0, int targetDamagedSpringsGroupsIndex = 0, SpringGroup? groupBefore = null)
        {
            List<SpringGroup>? possibleBrokenSpringGroups = null;

            int nextTargetForNumberOfDamagedSprings = targetDamagedSpringsGroups[targetDamagedSpringsGroupsIndex];

            //Skip all good springs
            var goodSprings = CountNumberOfBeginsWithConsequitiveGoodSprings(springs, currentSpringsIndex);
            var startIndex = currentSpringsIndex + goodSprings;

            //Identify UNKNOWNS as they impact multiple states for a given target level
            var unknownSprings = CountNumberOfBeginsWithConsequitiveUnknownSprings(springs, startIndex);

            //Calculate end-index as the last possible index that are used to look for this level of states
            var endIndex = startIndex + unknownSprings + nextTargetForNumberOfDamagedSprings - 1;

            //Search for the possible cases for groups of broken springs
            var potentialBrokenSpringGroups = IdentifyPotentialBrokenSpringsGroupsWithinLimitedArea(springRowReference, springs, startIndex, endIndex, nextTargetForNumberOfDamagedSprings, groupBefore);

            //Reqursive get Spring groups to find continuing groups
            if (potentialBrokenSpringGroups != null)
            {
                var advancedTargetDamagedStringsGroupsIndex = targetDamagedSpringsGroupsIndex + 1;
                foreach (var brokenSpringGroup in potentialBrokenSpringGroups)
                {
                    var success = brokenSpringGroup.IdentifySubsequentBrokenSpringGroups(targetDamagedSpringsGroups, advancedTargetDamagedStringsGroupsIndex);

                    if (success)
                    {
                        possibleBrokenSpringGroups ??= [];
                        possibleBrokenSpringGroups.Add(brokenSpringGroup);
                    }
                }
            }

            if (possibleBrokenSpringGroups == null || possibleBrokenSpringGroups.Count == 0)
            {
                //Is this a dead end? Maybe not, try and identify subsequent groups further down the row
                //Don't advance the springGroupTarget, since we didn't yet find the target
                //this is done by recursively call this same method
                var newStartIndex = startIndex + unknownSprings;
                if(newStartIndex == startIndex)
                    return null;

                if (endIndex >= springs.Length - 1)
                    return null;

                return Analyzer.IdentifyPossibleBrokenSpringGroupsForNextTarget(springRowReference, springs, targetDamagedSpringsGroups, newStartIndex, targetDamagedSpringsGroupsIndex, groupBefore);
            }
            else
            {
                return possibleBrokenSpringGroups;
            }
        }

        /// <summary>
        /// Search for possible groups within the span of starting index to expected target length plus unknown springs
        // ex:  ..???##??.. 3  =>  should search the sub area: [???##?] - so in index 2-7. Not the [..] and [?..]
        // and result should be [?##] and [##?]
        //
        // By the way, the subsequent spring must be potential non-broken
        // Which means: A group always ands with a potential non-broken spring. The end-index is EXCL. the termintating non-broken spring
        /// </summary>
        public static List<SpringGroup>? IdentifyPotentialBrokenSpringsGroupsWithinLimitedArea(SpringRow? springRowReference, char[] springs, int startIndex, int endIndex, int targetLength, SpringGroup? GroupBefore)
        {
            List<SpringGroup>? springGroups = null;

            if (endIndex >= springs.Length)
                endIndex = springs.Length - 1;

            int maxNumberOfGroups = CalculateNumberOfPotentialGroupsWithinRange(startIndex, endIndex, targetLength);

            if (maxNumberOfGroups <= 0)
                return springGroups;

            int[] potentialGroupsIndexStart = new int[maxNumberOfGroups];
            int[] potentialGroupsIndexEnd = new int[maxNumberOfGroups];
            bool[] potentialGroupsPossible = new bool[maxNumberOfGroups];
            for (int g = 0; g < maxNumberOfGroups; g++)
            {
                potentialGroupsIndexStart[g] = startIndex + g;
                potentialGroupsIndexEnd[g] = startIndex + targetLength - 1 + g;
                potentialGroupsPossible[g] = true;
            }

            for (int i = startIndex; i <= endIndex; i++)
            {
                for (int g = 0; g < maxNumberOfGroups; g++)
                {
                    if (!potentialGroupsPossible[g])
                        continue;
                    else if (i < potentialGroupsIndexStart[g])
                        continue;
                    else if (i > potentialGroupsIndexEnd[g])
                    {
                        if (i == potentialGroupsIndexEnd[g] + 1)
                            potentialGroupsPossible[g] = IsSpringPotentialNonBroken(springs, i);
                        continue;
                    }
                    potentialGroupsPossible[g] = IsSpringPotentialBroken(springs, i);
                }
            }

            for (int g = 0; g < maxNumberOfGroups; g++)
            {
                if (potentialGroupsPossible[g])
                {
                    var newSpringSection = new SpringGroup(springRowReference, springs, potentialGroupsIndexStart[g], potentialGroupsIndexEnd[g], GroupBefore);
                    if (springGroups == null)
                        springGroups = [];
                    springGroups.Add(newSpringSection);
                }
            }

            return springGroups;
        }

        public static bool IsSpringPotentialBroken(char[] springs, int springIndex)
        {
            if (springs == null || springs.Length == 0 || springIndex >= springs.Length)
                return false;

            var nextSpring = springs[springIndex];
            return nextSpring == BROKEN || nextSpring == UNKNOWN;
        }

        public static bool IsSpringPotentialNonBroken(char[] springs, int springIndex)
        {
            if (springs == null || springs.Length == 0 || springIndex >= springs.Length)
                return false;

            var nextSpring = springs[springIndex];
            return nextSpring == GOOD || nextSpring == UNKNOWN;
        }

        public static int CountNumberOfBeginsWithConsequitiveGoodSprings(char[] springs, int springsIndex)
        {
            int count = 0;

            for (int i = springsIndex; i < springs.Length; i++)
            {
                if (springs[i] == GOOD)
                    count++;
                else
                    break;
            }

            return count;
        }

        public static int CountNumberOfBeginsWithConsequitiveUnknownSprings(char[] springs, int springsIndex)
        {
            int count = 0;

            for (int i = springsIndex; i < springs.Length; i++)
            {
                if (springs[i] == UNKNOWN)
                    count++;
                else
                    break;
            }

            return count;
        }

        public static int CalculateNumberOfPotentialGroupsWithinRange(int startIndex, int endIndex, int targetLength)
        {
            int maxNumberOfGroups = endIndex - startIndex + 2 - targetLength;
            return maxNumberOfGroups;
        }

        public static bool IsAllRemainingSpringsGood(char[] springs, int startIndex)
        {
            if (startIndex >= springs.Length)
                return false;

            for (int i = startIndex; i < springs.Length; i++)
            {
                if (IsSpringPotentialNonBroken(springs, i))
                    continue;
                else
                    return false;
            }
            return true;
        }
    }
}