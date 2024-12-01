namespace AdventOfCode2023Solutions.Day12
{
    public class SpringGroup
    {
        SpringRow? SpringRowReference { get; set; }

        public string groupSpringsString { get; set; } = string.Empty;
        public char[] groupSprings { get; set; } = [];

        private char[] completeRowSprings;
        private int groupStartIndex;
        private int groupEndIndex;

        public SpringGroup? GroupBefore { get; set; } = null;
        public List<SpringGroup>? GroupsAfter { get; set; } = null;

        public bool IsSuccessGroupSequence { get; set; } = false;

        public SpringGroup(SpringRow? springRowReference, char[] springs, int startIndex, int endIndex, SpringGroup? groupBefore)
        {
            SpringRowReference = springRowReference;
            GroupBefore = groupBefore;
            groupStartIndex = startIndex;
            groupEndIndex = endIndex;
            completeRowSprings = springs;
            groupSprings = completeRowSprings.Take(new Range(startIndex, endIndex + 1)).ToArray();
            groupSpringsString = new string(groupSprings);
        }

        public bool IdentifySubsequentBrokenSpringGroups(int[] targetDamagedSpringsGroups, int targetDamagedSpringsGroupsIndex)
        {
            if (targetDamagedSpringsGroupsIndex == targetDamagedSpringsGroups.Length)
            {
                //All targeted groups are identified, we are at end of line. Need to verify that there are no more broken springs after this point
                var verifyFollowingSpringsAreNotBroken = Analyzer.IsAllRemainingSpringsGood(completeRowSprings, groupEndIndex + 1);
                if (verifyFollowingSpringsAreNotBroken)
                {
                    IsSuccessGroupSequence = true;
                    SpringRowReference?.AddActualBrokenSpringGroupSequenceEnding(this);
                }
                else
                {
                    IsSuccessGroupSequence = false;
                }
                return IsSuccessGroupSequence;
            }

            //If not at the end yet - keep analyzing the spring rows (be sure to +2 on endIndex to move the start index forward for next search, incl. one for the space between the groups.
            GroupsAfter = Analyzer.IdentifyPossibleBrokenSpringGroupsForNextTarget(SpringRowReference, completeRowSprings, targetDamagedSpringsGroups, groupEndIndex + 2, targetDamagedSpringsGroupsIndex, this);
            return GroupsAfter != null;
        }
    }
}
