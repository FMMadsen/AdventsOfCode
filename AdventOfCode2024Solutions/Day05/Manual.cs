namespace AdventOfCode2024Solutions.Day05
{
    public class Manual
    {
        public List<string> Rules { get; private set; }
        public List<string[]> Updates { get; private set; }
        public List<string[]> CorrectUpdates { get; private set; }
        public List<int> CorrectUpdatesMiddleNumbers { get; private set; }
        public int SumMiddleNumbers => CorrectUpdatesMiddleNumbers.Sum();

        public Manual(string[] rulesAndUpdateLines)
        {
            this.Updates = [];
            this.Rules = [];
            CorrectUpdates = [];
            CorrectUpdatesMiddleNumbers = [];

            var isRules = true;
            foreach (var line in rulesAndUpdateLines)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    isRules = false;
                    continue;
                }

                if (isRules)
                    Rules.Add(line.Replace("|", ""));
                else
                    Updates.Add(line.Split(",", StringSplitOptions.RemoveEmptyEntries));
            }
        }

        public void CheckUpdatesAgainstRules()
        {
            foreach (var update in Updates)
            {
                if (CheckUpdate(update))
                    CorrectUpdates.Add(update);
            }
        }

        public bool CheckUpdate(string[] update)
        {
            for (var i = 0; i < update.Length - 1; i++)
            {
                for (var ci = i + 1; ci < update.Length; ci++)
                {
                    if (Rules.Contains(update[ci] + update[i]))
                        return false;
                }
            }

            var middleNumber = GetMiddleNumber(update);
            CorrectUpdatesMiddleNumbers.Add(middleNumber);
            return true;
        }

        private int GetMiddleNumber(string[] update)
        {
            var middleIndex = (update.Length - 1) / 2;
            var middleNumberString = update[middleIndex];
            return int.Parse(middleNumberString);
        }
    }
}
