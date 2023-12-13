namespace AdventOfCode2023Solutions.Day09
{
    internal class ValueHistory
    {
        public long[] NumberSequence { get; private set; }
        public IList<long[]> InterpolationLines { get; private set; }

        public ValueHistory(string valueHistoryString)
        {
            NumberSequence = valueHistoryString.Trim().Split(' ').Where(s => !String.IsNullOrEmpty(s)).Select(s => long.Parse(s)).ToArray();
            InterpolationLines = new List<long[]>();
        }

        public void AnalyzeInterpolationLines()
        {
            var newProjectionSequence = NumberSequence;
            InterpolationLines.Add(newProjectionSequence);

            bool haveReachedZeroSequence = false;
            while (!haveReachedZeroSequence)
            {
                newProjectionSequence = FindProjectionsValues(newProjectionSequence);
                InterpolationLines.Add(newProjectionSequence);
                haveReachedZeroSequence = IsAllValuesZeros(newProjectionSequence);
            }
        }

        private long[] FindProjectionsValues(long[] sourceSequence)
        {
            if (sourceSequence.Length < 2)
                return [0];

            List<long> projectionValues = new List<long>();
            for (int i = 1; i < sourceSequence.Length; i++)
            {
                projectionValues.Add(sourceSequence[i] - sourceSequence[i - 1]);
            }
            return projectionValues.ToArray();
        }

        private bool IsAllValuesZeros(long[] sourceSequence)
        {
            return sourceSequence.Count(n => n == 0) == sourceSequence.Length;
        }

        public long FindNextInSequence()
        {
            var interpolationLinesArray = InterpolationLines.ToArray();
            long projectionFactor = 0;
            
            for (int i = interpolationLinesArray.Length-2; i >=0; i--)
                projectionFactor = interpolationLinesArray[i].Last() + projectionFactor;

            long projectedNumber = projectionFactor;
            return projectedNumber;
        }
    }
}
