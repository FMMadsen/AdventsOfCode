namespace AdventOfCode2023Solutions.Day09
{
    internal class OASIS
    {
        public ValueHistory[] Values { get; private set; }

        public OASIS(string[] valueHistories)
        {
            Values = valueHistories.Select(v => new ValueHistory(v)).ToArray();
        }

        public void AnalyzeValueHistoryLines()
        {
            foreach (var value in Values)
            {
                value.AnalyzeInterpolationLines();
            }
        }

        public long FindSumOfAllNextValues()
        {
            return Values.Sum(v => v.FindNextInSequence());
        }

        public long FindSumOfAllPreviousValues()
        {
            return Values.Sum(v => v.FindPreviousInSequence());
        }
    }
}
