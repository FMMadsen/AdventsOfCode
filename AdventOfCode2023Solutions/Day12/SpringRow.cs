namespace AdventOfCode2023Solutions.Day12
{
    public class SpringRow
    {
        internal string ConditionRecordString { get; private set; }
        internal char[] Springs { get; private set; }
        internal int NumberOfSprins { get; private set; }
        internal int[] TargetDamagedSpringsGroups { get; private set; }
        internal int NumberOfTargetDamagedSpringsGroups { get; private set; }

        public List<SpringGroup>? PotentialBrokenSpringsGroupsSequenceBegin { get; private set; } = null;
        public long NumberOfPotentialStates { get; private set; } = 0;
        public List<SpringGroup>? ActualBrokenSpringsGroupSequenceEndings { get; private set; } = null;

        public SpringRow(string conditionRecord)
        {
            var conditionStringSplit = conditionRecord.Split(' ');
            ConditionRecordString = conditionStringSplit[0].Trim() + '.';
            Springs = ConditionRecordString.ToArray();
            NumberOfSprins = Springs.Length;
            TargetDamagedSpringsGroups = conditionStringSplit[1].Trim().Split(',').Select(s => int.Parse(s)).ToArray();
            NumberOfTargetDamagedSpringsGroups = TargetDamagedSpringsGroups.Length;
        }

        public void AnalyzeNumberOfPotentialStates()
        {
            PotentialBrokenSpringsGroupsSequenceBegin = Analyzer.IdentifyPossibleBrokenSpringGroupsForNextTarget(this, Springs, TargetDamagedSpringsGroups);
        }

        public void AddActualBrokenSpringGroupSequenceEnding(SpringGroup springGroup)
        {
            if (ActualBrokenSpringsGroupSequenceEndings == null)
                ActualBrokenSpringsGroupSequenceEndings = [];

            ActualBrokenSpringsGroupSequenceEndings.Add(springGroup);
            NumberOfPotentialStates++;
        }

        public override string ToString()
        {
            return $"  {ConditionRecordString,-30} {string.Join('-', TargetDamagedSpringsGroups),-15}  =  {NumberOfPotentialStates}";// base.ToString();
        }
    }
}
