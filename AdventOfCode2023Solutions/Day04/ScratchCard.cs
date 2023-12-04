namespace AdventOfCode2023Solutions.Day04
{
    internal class ScratchCard
    {
        private readonly int cardstringLength = "Card ".Length;
        internal int CardNumber { get; private set; }
        internal IEnumerable<int> WinningNumbers { get; private set; }
        internal IEnumerable<int> ScratchCardNumbers { get; private set; }
        internal IEnumerable<int> MatchingNumbers { get; private set; }
        internal int NumberOfMatches { get; private set; }

        internal ScratchCard(string inputLine)
        {
            var mainCardLineSections = inputLine.Split(":");

            var numberString = mainCardLineSections[0].Substring(cardstringLength);
            CardNumber = int.Parse(numberString);

            var numberSections = mainCardLineSections[1].Split("|");

            WinningNumbers = GetNumberList(numberSections[0]);
            ScratchCardNumbers = GetNumberList(numberSections[1]);
            MatchingNumbers = IdentifyMatchingNumbers();
            NumberOfMatches = MatchingNumbers.Count();
            CalculatePointsScored();
        }

        private IEnumerable<int> GetNumberList(string numbersString)
        {
            return numbersString.Trim().Split(" ").Where(i => !String.IsNullOrWhiteSpace(i)).Select(i => int.Parse(i));
        }

        private IEnumerable<int> IdentifyMatchingNumbers()
        {
            return ScratchCardNumbers.Where(i => WinningNumbers.Contains(i));
        }

        internal int CalculatePointsScored()
        {
            var points = 0;
            MatchingNumbers.ToList().ForEach(i => IncrementPoints(ref points));
            return points;
        }

        private void IncrementPoints(ref int points)
        {
            if(points == 0)
                points++;
            else
                points *= 2;
        }
    }
}
